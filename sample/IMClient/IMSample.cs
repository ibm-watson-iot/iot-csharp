/*
 *  Copyright (c) $(YEAR) IBM Corporation and other Contributors.
 *
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.eclipse.org/legal/epl-v10.html 
 *
 * Contributors:
 *   Hari hara prasad Viswanathan  - Initial Contribution
 */
using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using IBMWIoTP;

namespace IMClient
{
	class IMSample
	{
		private static string orgId = "";
        private static string appId = "";
        private static string apiKey = "";
        private static string authToken = "";

		private static string deviceType= "";
		private static string deviceID= "";
		private static string deviceEvent= "";
		
		private static IBMWIoTP.ApplicationClient client;
		
		private static IBMWIoTP.ApiClient cli;
		
		
		public static EventTypeInfo EventType(IBMWIoTP.ApiClient cli,SchemaInfo sc){
			Console.WriteLine("Event Type sample request");
			Console.WriteLine("Add Draft EventType");
			EventTypeDraft draft = new EventTypeDraft();
			draft.name="Temprature";
			draft.description="eventType for temperature";
			draft.schemaId = sc.id;
			EventTypeInfo info = cli.AddDraftEventType(draft);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(info));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get All Draft EventType");
			EventTypeCollection collection =  cli.GetAllDraftEventType();
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(collection));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Delete Draft");
			//creating another one to delete
			EventTypeInfo del = cli.AddDraftEventType(draft);
			cli.DeleteDraftEventType(del.id);
			Console.WriteLine("Get event type draft of id" +info.id);
			EventTypeInfo reccivedInfo = cli.GetDraftEventType(info.id);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reccivedInfo));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Update draft");
			reccivedInfo.description = "updated info";
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.UpdateDraftEventType(reccivedInfo)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all active EventType");
			EventTypeCollection coll = cli.GetAllActiveEventType();
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(coll));
			Console.WriteLine("===============================================================================");
			if(coll.results.Count > 0 ){
				Console.WriteLine("Get all active EventType with id");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActiveEventType(coll.results[0].id)));
				Console.WriteLine("===============================================================================");
			}
			
			return reccivedInfo;
		}
		public static PhysicalInterfacesInfo PhysicalInterfaces(IBMWIoTP.ApiClient cli,EventTypeInfo evnt){
			Console.WriteLine("Physical Interfaces sample request");
			Console.WriteLine("Add Draft PhysicalInterfaces");
			PhysicalInterfaceDraft draft = new PhysicalInterfaceDraft();
			draft.name="TempPI";
			draft.description="new PhysicalInterfaces for temp";
			PhysicalInterfacesInfo newPI =  cli.AddDraftPhysicalInterfaces(draft);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(newPI));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all Draft PhysicalInterfaces");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftPhysicalInterfaces()));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Update Draft PhysicalInterfaces");
			newPI.description="changed to new discription";
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.UpdateDraftPhysicalInterfaces(newPI)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get Draft PhysicalInterfaces");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetDraftPhysicalInterfaces(newPI.id)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Map PhysicalInterfaces EventType");
			EventTypeBind bind = new EventTypeBind();
			bind.eventTypeId = evnt.id;
			bind.eventId="temperature";
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.MapDraftPhysicalInterfacesEvent(newPI.id,bind)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all Mapping PhysicalInterfaces EventType");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftPhysicalInterfacesMappedEvents(newPI.id)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Delete Mapping PhysicalInterfaces EventType");
			cli.DeleteDraftPhysicalInterfacesMappedEvents(newPI.id,bind.eventId);
          	Console.WriteLine("===============================================================================");
			Console.WriteLine("Get All active PhysicalInterfaces");
			PhysicalInterfacesCollection coll = cli.GetAllActivePhysicalInterfaces();
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(coll));
			Console.WriteLine("===============================================================================");
			if(coll.results.Count >0){
				Console.WriteLine("Get active PhysicalInterfaces");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActivePhysicalInterfaces(coll.results[0].id)));
				Console.WriteLine("===============================================================================");
				Console.WriteLine("Get active PhysicalInterfaces Events ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActivePhysicalInterfacesEvents(coll.results[0].id)));
				Console.WriteLine("===============================================================================");
			}
			return newPI;
		}
		public static LogicalInterfaceInfo LogicalInterface(IBMWIoTP.ApiClient cli,SchemaInfo sc){
			Console.WriteLine("Logical Interface sample request");
			Console.WriteLine("Add Draft LogicalInterface");
			LogicalInterfaceDraft draft =  new LogicalInterfaceDraft();
			draft.name="lidraft";
			draft.schemaId = sc.id;
			draft.description="some thing";
			var li = cli.AddDraftLogicalInterfaces(draft);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(li));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all Draft Logical Interfaces");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftLogicalInterfaces()));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Update Draft Logical Interfaces");
			li.description="changed to new discription";
			li = cli.UpdateDraftLogicalInterfaces(li);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(li));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get Draft Logical Interfaces");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetDraftLogicalInterfaces(li.id)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Perform action on Draft Logical Interfaces");
			OperationInfo work = new OperationInfo(OperationInfo.Validate);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.OperateDraftLogicalInterfaces(li.id,work)));
			
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Delete Draft Logical Interfaces");
			var dummyli = cli.AddDraftLogicalInterfaces(draft);
			cli.DeleteDraftLogicalInterfaces(dummyli.id);
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all Draft Logical Interfaces");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftLogicalInterfaces()));
			Console.WriteLine("===============================================================================");
			
			Console.WriteLine("Get All active Logical Interfaces");
			LogicalInterfaceCollection coll = cli.GetAllActiveLogicalInterfaces();
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(coll));
			Console.WriteLine("===============================================================================");
			if(coll.results.Count > 0 ){
				Console.WriteLine("Get active Logical Interfaces");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActiveLogicalInterfaces(coll.results[0].id)));
				Console.WriteLine("===============================================================================");
				Console.WriteLine("Oparate on Logical Interfaces  ");
				work = new OperationInfo(OperationInfo.Deactivate);
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.OperateLogicalInterfaces(coll.results[0].id,work)));
				Console.WriteLine("===============================================================================");
			}
			return li;
		}
		public static void Devices(IBMWIoTP.ApiClient cli,LogicalInterfaceInfo li){
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get Device state ");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetCurrentDeviceState(deviceType,deviceID,li.id)));
			Console.WriteLine("===============================================================================");
		}
		public static void DeviceType(IBMWIoTP.ApiClient cli,LogicalInterfaceInfo li,PhysicalInterfacesInfo pi){
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all device type");
			var deviceTypes =  cli.GetAllDeviceTypes();
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(deviceTypes));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("operate on draft device type");
			OperationInfo work = new OperationInfo(OperationInfo.Validate);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.OperateDraftDeviceType(deviceType,work)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all draft Logical Interface associated device type");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftDeviceTypeLI(deviceType)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Assocate Logical Interface with draft device type");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.AddDraftDeviceTypeLI(deviceType,li)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all draft Device type mapping");
			MappingInfo[] info =  cli.GetAllDraftDeviceTypeMapping(deviceType);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(info));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Add draft Device type mapping");
			MappingDraft liMappingDraft = new MappingDraft();
			liMappingDraft.notificationStrategy = "on-every-event";
			liMappingDraft.propertyMappings = new { 
				test = new {
					temperature = "$event.temp"
				}
			};
			liMappingDraft.logicalInterfaceId = li.id;
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.AddDraftDeviceTypeMapping(deviceType,liMappingDraft)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get  draft Device type mapping with LI id");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetDraftDeviceTypeMapping(deviceType,li.id)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Update  draft Device type mapping with LI id");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.UpdatedDraftDeviceTypeMapping(deviceType,liMappingDraft)));

			Console.WriteLine("===============================================================================");
			Console.WriteLine("Add draft Device type To PI ");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.AddDraftDeviceTypePI(deviceType,pi)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get draft Device type To PI ");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetDraftDeviceTypePI(deviceType)));


			Console.WriteLine("===============================================================================");
			Console.WriteLine("Delete draft Device type mapping");
			cli.DeleteDraftDeviceTypeMapping(deviceType,li.id);
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Delete draft Device type To PI ");
			cli.DeleteDraftDeviceTypePI(deviceType);
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Operate on Device type");
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all active Device type Logical interface");
			LogicalInterfaceInfo[] coll = cli.GetAllActiveDeviceTypeLI(deviceType);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(coll));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get all active Device type mapping");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllActiveDeviceTypeMappings(deviceType)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get active Device type Physical interface");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActiveDeviceTypePI(deviceType)));
			Console.WriteLine("===============================================================================");
			if(coll.Length > 0){
				Console.WriteLine("Get all on Device type mapping");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActiveDeviceTypeMappingLI(deviceType,coll[0].id)));
				Console.WriteLine("===============================================================================");
				Console.WriteLine("Get Device Type associated with logical interface id");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftDeviceType("",coll[0].id)));
				Console.WriteLine("===============================================================================");
			}
			OperationInfo action =  new OperationInfo(OperationInfo.Deactivate);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.OperateDeviceType(deviceType,action)));
			
		}
		public static SchemaInfo Schema(IBMWIoTP.ApiClient cli){
			Console.WriteLine("Get all draft schima");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetAllDraftSchemas()));
			Console.WriteLine("===============================================================================");
			SchemaDraft sd = new SchemaDraft();
			sd.name ="test";
			sd.schemaFile="tempSchemaPi.json";
			Console.WriteLine("Add schima draft");
			SchemaInfo si = cli.AddDraftSchema(sd);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(si));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Updated draft schema content");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.UpdateDraftSchemaContent(si.id,"tempSchema.json")));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Delete draft schema");
			cli.DeleteDraftSchema( cli.AddDraftSchema(sd).id);
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Updated draft schema metadata");
			si.description="new temp change";
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.UpdateDraftSchemaMetadata(si)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get draft schima metadata");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetDraftSchemaMetadata(si.id)));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Get draft schima Content");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetDraftSchemaContent(si.id)));
			Console.WriteLine("===============================================================================");
			
			Console.WriteLine("Get All active schimas");
			SchemaCollection coll = cli.GetAllActiveSchemas();
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(coll));
			Console.WriteLine("===============================================================================");
			if(coll.results.Length >0 ){
				Console.WriteLine("Get active schimas");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActiveSchemaMetadata(coll.results[0].id)));
				Console.WriteLine("===============================================================================");
				Console.WriteLine("Get active schima content ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetActiveSchemaContent(coll.results[0].id)));
				Console.WriteLine("===============================================================================");
			}
			
			return si;
		}
		public static void sampleSequence(IBMWIoTP.ApiClient cli){

			SchemaDraft sd = new SchemaDraft();
			sd.name ="tempEventSchema";
			sd.schemaFile="tempSchemaPi.json";
			Console.WriteLine("Create a draft event schema resource for your event type");
			SchemaInfo sc = cli.AddDraftSchema(sd);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(sc));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Create a draft event type that references the event schema");
			EventTypeDraft draft = new EventTypeDraft();
			draft.name="test";
			draft.description="eventType for temperature";
			draft.schemaId = sc.id;
			EventTypeInfo info = cli.AddDraftEventType(draft);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(info));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Create a draft physical interface");
			PhysicalInterfaceDraft draftPI = new PhysicalInterfaceDraft();
			draftPI.name="TempPI";
			draftPI.description="new PhysicalInterfaces for temp";
			PhysicalInterfacesInfo newPI =  cli.AddDraftPhysicalInterfaces(draftPI);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(newPI));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Add the event type to the draft physical interface");
			EventTypeBind bind = new EventTypeBind();
			bind.eventId=deviceEvent;
			bind.eventTypeId =info.id;
			EventTypeBind bindResponse = cli.MapDraftPhysicalInterfacesEvent(newPI.id,bind);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(bindResponse));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Update the draft device type to connect the draft physical interface");
			PhysicalInterfacesInfo updatedPI = cli.AddDraftDeviceTypePI(deviceType,newPI);
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Create a draft logical interface schema resource");
			SchemaDraft sdLI = new SchemaDraft();
			sdLI.name ="temperatureEventSchema";
			sdLI.schemaFile="tempSchemaLi.json";
			SchemaInfo schemaForLI = cli.AddDraftSchema(sdLI);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(schemaForLI));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Create a draft logical interface that references a draft logical interface schema");
			LogicalInterfaceDraft draftLI =  new LogicalInterfaceDraft();
			draftLI.name="environment sensor interface";
			draftLI.schemaId = schemaForLI.id;
			LogicalInterfaceInfo newLI = cli.AddDraftLogicalInterfaces(draftLI);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(newLI));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Add the draft logical interface to a device type");
			LogicalInterfaceInfo mappedLI = cli.AddDraftDeviceTypeLI(deviceType,newLI);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(mappedLI));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Define mappings to map properties in the inbound event to properties in the logical interface");
			MappingDraft liMappingDraft = new MappingDraft();
			liMappingDraft.notificationStrategy = "on-every-event";
			liMappingDraft.propertyMappings = new { 
				test = new {
					temperature = "$event.temp"
				}
			};
			liMappingDraft.logicalInterfaceId = mappedLI.id;
			MappingInfo mappedLiInfo = cli.AddDraftDeviceTypeMapping(deviceType,liMappingDraft);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(mappedLiInfo));
			Console.WriteLine("===============================================================================");
			Console.WriteLine("Validate and activate the configuration");
			OperationInfo activate = new OperationInfo(OperationInfo.Activate);
			var responce = cli.OperateDraftDeviceType(deviceType,activate);
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(responce));
			Console.WriteLine("===============================================================================");
			Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cli.GetCurrentDeviceState(deviceType,"1qaz2wsx", mappedLI.id)));

		}
		public static void Init(){
			Console.WriteLine("Please enter applicaiton details");
        	Console.Write("Enter your org id :");
			orgId = Console.ReadLine();
			
			Console.Write("Enter your app id :");
			appId = Console.ReadLine();
			
			Console.Write("Enter your api Key :");
			apiKey = Console.ReadLine();
			
			Console.Write("Enter your auth token :");
			authToken = Console.ReadLine();
			
			Console.WriteLine("Please enter device details to which you want to implement Infomation Mangement");
			
            Console.Write("Enter your device type :");
			deviceType = Console.ReadLine();
			
			Console.Write("Enter your device id :");
			deviceID = Console.ReadLine();

			Console.Write("Enter your device event :");
			deviceEvent = Console.ReadLine();
		
			client =  new ApplicationClient(orgId, appId, apiKey, authToken);
        	client.connect();
        	cli = client.GetAPIClient();
			cli.Timeout=30000;
			//MQTT Topic subscription for IM State
			client.subscribeToIMState(deviceType,deviceID);
			client.IMStateCallback+=processIM;
		}
		public static void Main(string[] args)
		{
			try{
			
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
        	Console.WriteLine("===============================================================================");

			Console.WriteLine("Check Out Following Samples For Information Management");
        	Console.WriteLine("1.Basic scenario");
        	Console.WriteLine("2.All API sample");
        	Console.WriteLine("3.Any key to exit");
        	Console.Write("Please enter your choice :");
        	int val = int.Parse(Console.ReadLine());
    		
        	Console.WriteLine("===============================================================================");
        	switch(val){
    			case 1: Init();
        				Console.WriteLine ("Starting Basic scenario");
    					sampleSequence(cli);
        				break;
        			
        		case 2 : Init();
        				Console.WriteLine ("Starting All API sample");
        				SchemaInfo sc = Schema(cli);
						EventTypeInfo info = EventType(cli,sc);
						var pi = PhysicalInterfaces(cli,info);
						SchemaDraft sd = new SchemaDraft();
						sd.name ="test";
						sd.schemaFile="tempSchemaLi.json";
						SchemaInfo si = cli.AddDraftSchema(sd);
						var li = LogicalInterface(cli,si);
						DeviceType(cli,li,pi);
						Devices(cli,li);
        				break;
			default : break; 
        	}
        	Console.Write("Press any key to continue . . . ");
			Console.ReadKey();
			}
			catch(Exception e){
				Console.WriteLine("Err :" +e.Message);
			}


		}
		public static void processEvent(string deviceType,string deviceId ,string eventName, string format, string data)
        {
             Console.WriteLine("Sample Application Client : Device Type"+deviceType+" Device ID:"+deviceId+"Sample Event : " + eventName + " format : " + format + " data : " + data);
        }
		public static void processIM(string deviceType, string deviceId, string logicalInterfaceId , string data) 
		{
				Console.WriteLine("IM Status of Device " +deviceType +"/"+deviceId + "For Logical interface of ID " + logicalInterfaceId +"data"+data);
		}

	}
}
