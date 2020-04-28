# WARNING: the next line with param descriptions should be the first executable line
param([switch]$noco, [switch]$h)


$scriptpath = Split-Path $MyInvocation.MyCommand.Path
Set-Location $scriptpath
$global:error_occured_in_oas_codegen = $false;
$tempdir = $env:temp + "\oas-temp\"
$tfoutput = $tempdir + "tfoutput.txt"
$oasoutput = $tempdir + "oasoutput.txt"


function synchronize($destFolder, $sourceFolder, $fileNamePattern)
{
	# Sync destination folder with oas generated folder

	# $destFolder: 'final destination of the generated files'
	# $sourceFolder: 'the location of the generated files'
	# $fileNamePattern: filter on which files should be processed

	$files = Get-ChildItem -Recurse -Path ($sourceFolder + "\*" + $fileNamePattern)

	# Remove not generated files from tfs
	$filesInDestinationFolder = Get-ChildItem -Path ($destFolder + "\*" + $fileNamePattern)

	foreach ($destinationFile in $filesInDestinationFolder)
	{
		$found = $FALSE
		$destinationFileName = $destinationFile.Name
		foreach ($file in $files)
		{
			if ($destinationFileName -eq $file.Name)
			{
				$found = $TRUE;
			}
		}
		if (!$found)
		{
			Write-Host "- delete from sourcecontrol file '$destinationFileName'"
			& $tf delete $destinationFile > $tfoutput 2>&1
		}
	}
	
	# Move files from sourceFolder to destinationFolder
	foreach ($file in $files) 
	{
		$destinationFile = ($destFolder + "\"+ $file.Name)
		$fileName = $file.Name

		if (Test-Path $destinationFile)
		{	
			if(Compare-Object -ReferenceObject $(Get-Content $file.FullName) -DifferenceObject $(Get-Content $destinationFile))
			{
				#checkout changed file
				Write-Host "- checkout changed file '$fileName'"
				& $tf checkout $destinationFile > $tfoutput 2>&1
				Remove-Item -Force $destinationFile
				Move-Item $file.FullName -Destination $destFolder
			}
			Else
			{
				#do nothing, file is not changed
			}
		}
		else
		{
			# add new file to source control
			Move-Item $file.FullName -Destination $destFolder
			Write-Host "- add new file '$fileName' to source control"
			#& $tf add $destinationFile > $tfoutput 2>&1
		}
	}
}

function generate($destFolder, $tempOutputFolder, $tempOutputSubFolder, $filePattern, $renameOldPattern, $renameNewPattern, $renamePrefix, $oasLanguage, $oasTemplateDir, $oasOutputProperties)
{
	# $destFolder: 'final destination of the generated files'
	# $tempOutputFolder: 'temp directory where oas puts the generatwed code'
	# $tempOutputSubFolder : (optional) which subfolder in the generated oas files should be looked at, this is determined by oas
	# $filePattern: filter on which files should be processed and copied to the final destination
	# $renameOldPattern: this part of the generated filename will be renamed
	# $renameNewPattern: the generated filename will is renamed with this 
	# $renamePrefix = (optional) the filename will be appended with a prefix
	# $oasLanguage: defines which language OAS will produce as output
	# $oasTemplateDir: location of the used templates by OAS
	# $oasOutputProperties: defines the output of OAS

	Write-Host "-----------------------------------------------------------------------------------------------------------------------"
	Write-Host "--- Start generate ($oasLanguage) files in '$destFolder' ---"
	Write-Host "-----------------------------------------------------------------------------------------------------------------------"

	$tf = ''
	$tf_community = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\TF.exe'
	$tf_enterprise = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\TF.exe'
	$tf_professional = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\TF.exe'

	if (Test-Path $tf_enterprise) {
		$tf = $tf_enterprise
	} elseif (Test-Path $tf_professional) {
		$tf = $tf_professional
	} elseif (Test-Path $tf_community) {
		$tf = $tf_community
	}

	#------------------------------------------------------------------------------------------------------------
	# determine location of generated files to process
	$tempOutputFolderToProcess = $tempOutputFolder
	if ($tempOutputSubFolder -ne $null)
	{
		$tempOutputFolderToProcess = $tempOutputFolderToProcess + "\" + $tempOutputSubFolder
	}

	#------------------------------------------------------------------------------------------------------------
	# Clean tempoutput
	If(!(Test-Path $tempdir))
	{
		New-Item -ItemType Directory -Force -Path $tempdir > $null
	} 

	If(!(Test-Path $tempOutputFolder))
	{
		New-Item -ItemType Directory -Force -Path $tempOutputFolder > $null
	} 
	Else 
	{		
		Get-ChildItem ($tempOutputFolder + "\*") -Recurse | Where { ! $_.PSIsContainer } | Remove-Item -Force
	}

	#------------------------------------------------------------------------------------------------------------
	# Restore initial state of files in the source-control

	# Un-checkout only files with the pattern, leave other files unattached
    $filteredDest = "$destFolder\*$renameNewPattern"
	#& { $tf undo $filteredDest > $tfoutput 2>&1 } &

	# remove newly added files from file system
	# leave sub-folders intact, because p.e. the Services folder has an Helper directory
	$filesInDestination = Get-ChildItem -Path ($destFolder + "\*" + $renameNewPattern) | ?{ -not $_.PsIsContainer }
	foreach ($file in $filesInDestination) 
	{
		#Write-Host "- Remove '$file', because not in source control"
		Remove-Item -Force $file
	}
	
	# Get only files with the pattern, so initial state is restored
	#& $tf get /overwrite $filteredDest > $tfoutput 2>&1

	#------------------------------------------------------------------------------------------------------------  
	# Generate files 	
	java -DdebugModels -jar openapi-generator-cli.jar generate -i api.yaml -g $oasLanguage -o $tempOutputFolder -t $oasTemplateDir $oasOutputProperties > $oasoutput 2>&1 
	# If you want specific version java -DdebugModels -jar openapi-generator-cli-4.0.0-20181226.105224-120.jar generate -i api.yaml -g $oasLanguage -o $tempOutputFolder -t $oasTemplateDir $oasOutputProperties > $oasoutput 2>&1 

	#------------------------------------------------------------------------------------------------------------  
	# Test if an error is occurred during the code generation and show errors to the user.
	$oas_outputfile = Get-Content $oasoutput
	$containsWord = $oas_outputfile | %{$_ -match "\berror\b"}
	if ($containsWord -contains $true) {
		$global:error_occured_in_oas_codegen = $true
		Write-Host "ERROR: generation of oas files not OK. Is file 'api.yaml' OK?"
		foreach ($line in $oas_outputfile)
		{
			Write-Host $line
		}
	} 

	if (!$global:error_occured_in_oas_codegen)
	{
		#------------------------------------------------------------------------------------------------------------
		# Rename generated files
		$files = Get-ChildItem -Recurse -Path ($tempOutputFolderToProcess + "\" + $filePattern)
	
		foreach ($file in $files)
		{
			$newFileName=$file.Name.Replace($renameOldPattern, $renameNewPattern)
			if ($renamePrefix -ne $null)
			{
				$newFileName = $renamePrefix + $newFileName
			}

			Rename-Item $file.FullName $newFileName
		}

		#------------------------------------------------------------------------------------------------------------
		# Synchronize the destination folder with the generated files, including TFS actions such as add, checkout, delete
		synchronize $destFolder $tempOutputFolderToProcess $renameNewPattern
	}

	#------------------------------------------------------------------------------------------------------------
	# Clean tempoutput
	If(Test-Path $tempOutputFolder)
	{
		Get-ChildItem ($tempOutputFolder + "\*") -Recurse | Where { ! $_.PSIsContainer } | Remove-Item -Force
	}

	Write-Host "-----------------------------------------------------------------------------------------------------------------------"
	Write-Host "End generate ($oasLanguage) files in:"
	Write-Host "'$destFolder'"
	Write-Host "-----------------------------------------------------------------------------------------------------------------------"
	Write-Host ""
}

If ($h -eq $True)
{
	#------------------------------------------------------------------------------------------------------------
	# help
	Write-Host "options:";
	Write-Host " -noco: no checkout";
}
Else
{
	if (!$global:error_occured_in_oas_codegen)
	{
		# Generate the Controller 
		$destFolder = '..\..\src\Web\Api'
		$tempOutputFolder = $tempdir + 'temp_output_controllers'
		$tempOutputSubFolder = $null
		$filePattern = '*.cs'
		$renameOldPattern = 'Api.cs'
		$renameNewPattern = 'Controller.g.cs'
		$renamePrefix = $null
		$oasLanguage = 'aspnetcore'
		$oasTemplateDir = '..\templates\aspnetcore\controller'
		$oasOutputProperties = '-Dapis'
		generate $destFolder $tempOutputFolder $tempOutputSubFolder $filePattern $renameOldPattern $renameNewPattern $renamePrefix $oasLanguage $oasTemplateDir $oasOutputProperties


		# Generate the dto's that go with the controllers
		$destFolder = '..\..\src\Common\Models'
		$tempOutputFolder = $tempdir + 'temp_output_models'
		$tempOutputSubFolder = $null
		$filePattern = '*.cs'
		$renameOldPattern = 'cs'
		$renameNewPattern = 'g.cs'
		$renamePrefix = $null
		$oasLanguage = 'aspnetcore'
		$oasTemplateDir = '..\templates\aspnetcore\controller'
		$oasOutputProperties = '-Dmodels'
		generate $destFolder $tempOutputFolder $tempOutputSubFolder $filePattern $renameOldPattern $renameNewPattern $renamePrefix $oasLanguage $oasTemplateDir $oasOutputProperties
	}
		
}