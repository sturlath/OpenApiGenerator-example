using Microsoft.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace TheGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string outputStringYaml = myOpenApiDocument.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Yaml);

			//Saves over the api.yaml file that is then used in post ild to create the dto's and Controllers
			System.IO.File.WriteAllText(@"..\..\..\Generation\api.yaml", outputStringYaml);
		}

		public static OpenApiComponents AdvancedComponentsWithReference = new OpenApiComponents
		{
			Schemas = new Dictionary<string, OpenApiSchema>
			{
				["offerDTO"] = new OpenApiSchema
				{
					Description = "The offer from the company.",
					Type = "object",
					Required = new HashSet<string>
					{
						"title",
						"amountOffered"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["id"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["requestsRequestId"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["title"] = new OpenApiSchema
						{
							Type = "string"
						},
						["user"] = new OpenApiSchema
						{
							Type = "user",
							Reference = new OpenApiReference
							{
								Id = "userDTO",
								Type = ReferenceType.Schema
							}
						},
						//Decimal doesn't work. I have an open issue on it https://github.com/Microsoft/OpenAPI.NET/issues/373
						["amountOffered"] = new OpenApiSchema
						{
							Type = "number",
							Format = "decimal"
						},
						["currency"] = new OpenApiSchema
						{
							Type = "string"
						},
						["comment"] = new OpenApiSchema
						{
							Type = "string"
						},
						["veacleDetails"] = new OpenApiSchema
						{
							Type = "veacleDetails",
							Reference = new OpenApiReference
							{
								Id = "veacleDetailsDTO",
								Type = ReferenceType.Schema
							}
						},
						["expireDateOfOffer"] = new OpenApiSchema()
						{
							Nullable = true,
							Type = "string",
							Format = "date"
						}
					},
					Reference = new OpenApiReference
					{
						Id = "offerDTO",
						Type = ReferenceType.Schema
					}
				},
				["srequestDTO"] = new OpenApiSchema
				{
					Description = "What the customer of a s uses to get then get an offer.",
					Type = "object",
					Required = new HashSet<string>
					{
						"isRoundTrip"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["id"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["pickup"] = new OpenApiSchema
						{
							Type = "pickup",
							Reference = new OpenApiReference
							{
								Id = "destinationDTO",
								Type = ReferenceType.Schema
							}
						},
						["dropOff"] = new OpenApiSchema
						{
							Type = "dropOff",
							Reference = new OpenApiReference
							{
								Id = "destinationDTO",
								Type = ReferenceType.Schema
							}
						},
						["isRoundTrip"] = new OpenApiSchema
						{
							Type = "boolean"
						},
						["isPickup"] = new OpenApiSchema
						{
							Type = "boolean"
						},
						["journeyType"] = new OpenApiSchema
						{
							Type = "object",
							Items = JourneyTypeSchemaWithReference,
							Reference = new OpenApiReference
							{
								Id = "journeyType",
								Type = ReferenceType.Schema
							}
						},
						["veacleType"] = new OpenApiSchema
						{
							Type = "object",
							Items = VeacleTypeSchemaWithReference,
							Reference = new OpenApiReference
							{
								Id = "veacleType",
								Type = ReferenceType.Schema
							}
						},
						["expireDateOfsRequest"] = new OpenApiSchema()
						{
							Type = "string",
							Format = "date"
						},
						["luggageType"] = new OpenApiSchema
						{
							Type = "object",
							Items = LuggageTypeSchemaWithReference,
							Reference = new OpenApiReference
							{
								Id = "luggageType",
								Type = ReferenceType.Schema
							}
						},
						["numberOfPassengers"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["mainContact"] = new OpenApiSchema
						{
							Type = "mainContact",
							Reference = new OpenApiReference
							{
								Id = "userDTO",
								Type = ReferenceType.Schema
							}
						},
						["substituteContact"] = new OpenApiSchema
						{
							Type = "substituteContact",
							Reference = new OpenApiReference
							{
								Id = "userDTO",
								Type = ReferenceType.Schema
							}
						},
						["comment"] = new OpenApiSchema()
						{
							Type = "string"
						},
						["hasAcceptedAOffer"] = new OpenApiSchema
						{
							Type = "boolean",
							Nullable = true
						},
						["acceptedOfferId"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
					},
					Reference = new OpenApiReference
					{
						Id = "srequestDTO",
						Type = ReferenceType.Schema
					}
				},
				["userDTO"] = new OpenApiSchema
				{
					Type = "object",
					Required = new HashSet<string>
					{
						"id",
						"userType",
						"firstName"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["id"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						//Enum see type below
						["userType"] = new OpenApiSchema
						{
							Type = "object",
							Items = UserTypesSchemaWithReference,
							Reference = new OpenApiReference
							{
								Id = "userType",
								Type = ReferenceType.Schema
							}
						},
						["firstName"] = new OpenApiSchema
						{
							Type = "string"
						},
						["lastName"] = new OpenApiSchema
						{
							Type = "string"
						},
						["email"] = new OpenApiSchema
						{
							Type = "string"
						},
						["mobile"] = new OpenApiSchema
						{
							Type = "string"
						},
						["workPhone"] = new OpenApiSchema
						{
							Type = "string"
						},
						["url"] = new OpenApiSchema
						{
							Type = "string",
							Description = "Url to the s station"
						}

					},
					Reference = new OpenApiReference
					{
						Id = "userDTO",
						Type = ReferenceType.Schema
					}
				},
				["veacleDetailsDTO"] = new OpenApiSchema
				{
					Type = "object",
					Required = new HashSet<string>
					{
						"id",
						"userType",
						"firstName"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["id"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						//Enum see type below
						["veacleType"] = new OpenApiSchema
						{
							Type = "object",
							Items = VeacleTypeSchemaWithReference,
							Reference = new OpenApiReference
							{
								Id = "veacleType",
								Type = ReferenceType.Schema
							}
						},
						["image1"] = new OpenApiSchema
						{
							Type = "string",
							Format = "byte"
						},
						// Maybe these should be only image urls and you need to do another call
						// to get all images? Makes it lighter/faster!
						["image2"] = new OpenApiSchema
						{
							Type = "string",
							Format = "byte"
						},
						["image3"] = new OpenApiSchema
						{
							Type = "string",
							Format = "byte"
						},
						["imagesUrl"] = new OpenApiSchema
						{
							Type = "string"
						}
					},
					Reference = new OpenApiReference
					{
						Id = "veacleDetailsDTO",
						Type = ReferenceType.Schema
					}
				},
				["destinationDTO"] = new OpenApiSchema
				{
					Description = "The pickup and drop off information.",
					Type = "object",
					Required = new HashSet<string>
					{
						"id",
						"isPickup",
						"date",
						"address"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["id"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["isPickup"] = new OpenApiSchema
						{
							Type = "boolean"
						},
						["address"] = new OpenApiSchema
						{
							Type = "string"
						},
						["date"] = new OpenApiSchema
						{
							Type = "string",
							Format = "date"
						},
						["gpsCoordinate"] = new OpenApiSchema
						{
							Type = "gpsCoordinate",
							Reference = new OpenApiReference
							{
								Id = "gpsCoordinateDTO",
								Type = ReferenceType.Schema
							}
						},
					},
					Reference = new OpenApiReference
					{
						Id = "destinationDTO",
						Type = ReferenceType.Schema
					}
				},
				["gpsCoordinateDTO"] = new OpenApiSchema
				{
					Type = "object",
					Required = new HashSet<string>
					{
						"id",
						"latitude",
						"longitude"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["id"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["latitude"] = new OpenApiSchema
						{
							Type = "number",
							Format = "double"
						},
						["longitude"] = new OpenApiSchema
						{
							Type = "number",
							Format = "double"
						}
					},
					Reference = new OpenApiReference
					{
						Id = "gpsCoordinateDTO",
						Type = ReferenceType.Schema
					}
				},
				["userType"] = new OpenApiSchema
				{
					Description = "Type of user",
					Type = "string",
					Enum = new List<IOpenApiAny>
							{
								new OpenApiString("NotSet"),
								new OpenApiString("Person"),
								new OpenApiString("Company"),
							}
				},
				["veacleType"] = new OpenApiSchema
				{
					Description = "Type of veacle",
					Type = "string",
					Enum = new List<IOpenApiAny>
							{
								new OpenApiString("NotSet"),
								new OpenApiString("s"),
								new OpenApiString("Limousine"),
							}
				},
				["journeyType"] = new OpenApiSchema
				{
					Description = "Type of journey",
					Type = "string",
					Enum = new List<IOpenApiAny>
							{
								new OpenApiString("NotSet"),
								new OpenApiString("AirPortTransfer"),
								new OpenApiString("siness"),
								new OpenApiString("Government"),
								new OpenApiString("StagHenParty"),
								new OpenApiString("Wedding"),
								new OpenApiString("ElementarySchool"),
								new OpenApiString("HighSchool"),
								new OpenApiString("University")
							}
				},
				["luggageType"] = new OpenApiSchema
				{
					Description = "Type of luggage",
					Type = "string",
					Enum = new List<IOpenApiAny>
							{
								new OpenApiString("NotSet"),
								new OpenApiString("UnderStorage"),
								new OpenApiString("CarrieOn")
							}
				},
				["errorModel"] = new OpenApiSchema
				{
					Type = "object",
					Required = new HashSet<string>
					{
						"code",
						"message"
					},
					Properties = new Dictionary<string, OpenApiSchema>
					{
						["code"] = new OpenApiSchema
						{
							Type = "integer",
							Format = "int32"
						},
						["message"] = new OpenApiSchema
						{
							Type = "string"
						}
					},
					Reference = new OpenApiReference
					{
						Id = "errorModel",
						Type = ReferenceType.Schema
					}
				},
			}
		};

		/// <summary>
		/// Definition of the error model schema. Using this shorthand indexing makes the main api doc more readable!
		/// </summary>
		public static OpenApiSchema OfferSchemaWithReference = AdvancedComponentsWithReference.Schemas["offerDTO"];
		public static OpenApiSchema QuoteSchemaWithReference = AdvancedComponentsWithReference.Schemas["srequestDTO"];
		public static OpenApiSchema UserTypesSchemaWithReference = AdvancedComponentsWithReference.Schemas["userType"];
		public static OpenApiSchema VeacleTypeSchemaWithReference = AdvancedComponentsWithReference.Schemas["veacleType"];
		public static OpenApiSchema JourneyTypeSchemaWithReference = AdvancedComponentsWithReference.Schemas["journeyType"];
		public static OpenApiSchema LuggageTypeSchemaWithReference = AdvancedComponentsWithReference.Schemas["luggageType"];
		public static OpenApiSchema ErrorModelSchemaWithReference = AdvancedComponentsWithReference.Schemas["errorModel"];

		// This is the definition of the api. The "single source of truth"
		// Change this and run the project to create the api.yaml file 
		// that is then used to create the controllers/dto's
		public static OpenApiDocument myOpenApiDocument = new OpenApiDocument
		{
			Info = new OpenApiInfo
			{
				Version = "1.0.0",
				Title = "My OpenApi",
				Description = "This API OpenApi is auto-created by using OpenApi.net and yaml file.",
				Contact = new OpenApiContact()
				{
					Name = "Support center",
					Email = "support@support.is",
					Url = new Uri("https://support.support.is")
				},
				License = new OpenApiLicense
				{
					//I have no idea https://choosealicense.com/licenses/gpl-3.0/
					Name = "GPL-3.0",
					Url = new Uri("https://opensource.org/licenses/GPL-3.0")
				},

			},
			Servers = new List<OpenApiServer>   {
					  		new OpenApiServer
							  {
								  Url = "https://support.is/api",
								  Description =""
							  }
					  	},
			Paths = new OpenApiPaths
			{
				// Offers Controller actions
				["/offers"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Get] = new OpenApiOperation
						{
							//Used in the name of the controller action and service method
							OperationId = "getAllOffers",
							Summary = "GetAll",
							Description = "Get all Offers",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "offers"
					  				}
					  		},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/offers/{id}"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Get] = new OpenApiOperation
						{
							OperationId = "getByIdOffers",
							Summary = "GetById",
							Description = "Get an Offer by id",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "offers"
					  				}
					  		},
							Parameters = new List<OpenApiParameter>
					  		{
								//Parameters sent to the api (can be in Path/Cookies/Query/Header
					  			new OpenApiParameter()
								{
									Name = "id",
					  				In = ParameterLocation.Path,
					  				Description = "The id of Offer that is asked for",
					  				Required = true,
					  				AllowEmptyValue = false,
					  				Schema = new OpenApiSchema()
									{
										Type = "integer",
										Format = "int32"
									}
					  			}
					  		},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/offers/add"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Post] = new OpenApiOperation
						{
							OperationId = "addOffer",
							Summary = "Add",
							Description = "Add an Offer",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "offers"
					  				}
					  		},
							RequestBody = new OpenApiRequestBody
							{
								Description = "Offer to add",
								Required = true,
								Content = new Dictionary<string, OpenApiMediaType>
								{
									["application/json"] = new OpenApiMediaType
									{
										Schema = OfferSchemaWithReference
									}
								}
							},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/offers/update"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Put] = new OpenApiOperation
						{
							OperationId = "updateOffer",
							Summary = "Update",
							Description = "Update an Offer",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "offers"
					  				}
					  		},
							RequestBody = new OpenApiRequestBody
							{
								Description = "Offer to update",
								Required = true,
								Content = new Dictionary<string, OpenApiMediaType>
								{
									["application/json"] = new OpenApiMediaType
									{
										Schema = OfferSchemaWithReference
									}
								}
							},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/offers/delete/{id}"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Delete] = new OpenApiOperation
						{
							OperationId = "deleteOffer",
							Summary = "Delete",
							Description = "Deletes a Offer with certain id",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "offers"
					  				}
					  		},
							Parameters = new List<OpenApiParameter>
					  		{
								//Parameters sent to the api (can be in Path/Cookies/Query/Header
					  			new OpenApiParameter()
								{
									Name = "id",
					  				In = ParameterLocation.Path,
					  				Description = "The id of Offer that is going to be deleted",
					  				Required = true,
					  				AllowEmptyValue = false,
					  				Schema = new OpenApiSchema()
									{
										Type = "integer",
										Format = "int32"
									}
					  			}
					  		},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = OfferSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				}

				// srequest Controller actions
				,
				["/srequest"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Get] = new OpenApiOperation
						{
							OperationId = "getAllsRequests",
							//Used in the name of the controller action and service method
							Summary = "GetAll",
							Description = "Get all sRequests",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "srequest"
					  				}
					  		},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/quotes/{id}"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Get] = new OpenApiOperation
						{
							OperationId = "getByIdQuotes",
							Summary = "GetById",
							Description = "Get an Quote by id",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "quotes"
					  				}
					  		},
							Parameters = new List<OpenApiParameter>
					  		{
								//Parameters sent to the api (can be in Path/Cookies/Query/Header
					  			new OpenApiParameter()
								{
									Name = "id",
					  				In = ParameterLocation.Path,
					  				Description = "The id of Quote that is asked for",
					  				Required = true,
					  				AllowEmptyValue = false,
					  				Schema = new OpenApiSchema()
									{
										Type = "integer",
										Format = "int32"
									}
					  			}
					  		},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/quotes/add"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Post] = new OpenApiOperation
						{
							OperationId = "addQuote",
							Summary = "Add",
							Description = "Add an Quote",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "quotes"
					  				}
					  		},
							RequestBody = new OpenApiRequestBody
							{
								Description = "Quote to add",
								Required = true,
								Content = new Dictionary<string, OpenApiMediaType>
								{
									["application/json"] = new OpenApiMediaType
									{
										Schema = QuoteSchemaWithReference
									}
								}
							},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/quotes/update"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Put] = new OpenApiOperation
						{
							OperationId = "updateQuote",
							Summary = "Update",
							Description = "Update an Quote",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "quotes"
					  				}
					  		},
							RequestBody = new OpenApiRequestBody
							{
								Description = "Quote to update",
								Required = true,
								Content = new Dictionary<string, OpenApiMediaType>
								{
									["application/json"] = new OpenApiMediaType
									{
										Schema = QuoteSchemaWithReference
									}
								}
							},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				},
				["/quotes/delete/{id}"] = new OpenApiPathItem
				{
					Operations = new Dictionary<OperationType, OpenApiOperation>
					{
						[OperationType.Delete] = new OpenApiOperation
						{
							OperationId = "deleteQuote",
							Summary = "Delete",
							Description = "Deletes a Quote with certain id",
							Tags = new List<OpenApiTag>()
					  		{
					  				new OpenApiTag()
									{
										Name = "quotes"
					  				}
					  		},
							Parameters = new List<OpenApiParameter>
					  		{
								//Parameters sent to the api (can be in Path/Cookies/Query/Header
					  			new OpenApiParameter()
								{
									Name = "id",
					  				In = ParameterLocation.Path,
					  				Description = "The id of Quote that is going to be deleted",
					  				Required = true,
					  				AllowEmptyValue = false,
					  				Schema = new OpenApiSchema()
									{
										Type = "integer",
										Format = "int32"
									}
					  			}
					  		},
							Responses = new OpenApiResponses
							{
								//All the responses from the api based on the http code
								["200"] = new OpenApiResponse
								{
									Description = "OK",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["application/json"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										},
										["application/xml"] = new OpenApiMediaType
										{
											Schema = new OpenApiSchema
											{
												Type = "array",
												Items = QuoteSchemaWithReference
											}
										}
									}
								},
								["401"] = new OpenApiResponse
								{
									Description = "Unauthorized"
								},
								["404"] = new OpenApiResponse
								{
									Description = "Not found"
								},
								["500"] = new OpenApiResponse
								{
									Description = "unexpected server error",
									Content = new Dictionary<string, OpenApiMediaType>
									{
										["text/html"] = new OpenApiMediaType
										{
											Schema = ErrorModelSchemaWithReference
										}
									}
								}
							}
						}
					}
				}
			},
			Components = AdvancedComponentsWithReference
		};
	}
}
