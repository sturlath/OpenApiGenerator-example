----------------
 Prerequisites:
----------------

1.
Check if tf.exe executes correctly:

Navigate to 
C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer (your path might be something else)
and run the following in cmd

run tf.exe


2.
Open up PowerShell in admin mode and type in 'Set-ExecutionPolicy RemoteSigned' and press 'y'

3. 
Download the newest OpenApi-Generator and put it in the Generation folder
Either manually here https://github.com/OpenAPITools/openapi-generator#13---download-jar (note you can setup a automatic-up-to-date script)
Or by run the following in PowerShell
Invoke-WebRequest -OutFile openapi-generator-cli.jar https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/4.3.0/openapi-generator-cli-4.3.0.jar


----------------

    Work-flow:
----------------

1. Change the API (in Program.cs) to your liking
2. Update the api.yaml file automatically (by Right click and Debug -> Start new instance on this project).
3. If there isn't any api.yaml file then step 2 will create it.
4. Create the dto's and controllers by running generateApiOutput.ps1 (right click and select execute) 

What just happened was that your changes in Program.cs were taken and the api.yaml file was updated with the specks.
Then the PowerShell script took that and used the template file (controller.mustache) to create the dto's and controllers


----------------

   Debugging:
----------------

Sometimes something doesn't work so here we have few tips to help

1. Download a PowerShell tool so you can debug the generateApiOutPut.ps1. I use this https://marketplace.visualstudio.com/items?itemName=AdamRDriscoll.PowerShellToolsforVisualStudio2017-18561
2. When generating the files they are moved to a temp location outside of the solution first
    C:\Users\[you]\AppData\Local\Temp\oas-temp
    There you find oasoutput.txt with generation errors.
3. 