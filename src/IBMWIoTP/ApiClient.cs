/*
 *  Copyright (c) 2017 IBM Corporation and other Contributors.
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
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using RestSharp;
using RestSharp.Authenticators;

using log4net;

namespace IBMWIoTP
{
	/// <summary>
	/// ApiClient class to perform all rest Api operations available in Watson IoT Platform 
	/// </summary>
	public class ApiClient
	{
		ILog log = log4net.LogManager.GetLogger(typeof(ApiClient));
		public static string BaseURL = "https://{0}.internetofthings.ibmcloud.com/api/v0002";
		
		private const string OrgInfo = "/";
		
		private const string BulkGet = "/bulk/devices";
		private const string BulkAdd = "/bulk/devices/add";
		private const string BulkRemove = "/bulk/devices/remove";
		
		private const string DeviceTypes = "/device/types";
		private const string DeviceTypesIndigvual = "/device/types/{0}";
		
		private const string Device = "/device/types/{0}/devices";
		private const string DeviceIndigvual = "/device/types/{0}/devices/{1}";
		private const string DeviceGatewayList = "/device/types/{0}/devices/{1}/devices";
		private const string DeviceLocation = "/device/types/{0}/devices/{1}/location";
		private const string DeviceMgmtInfo = "/device/types/{0}/devices/{1}/mgmt";
		
		private const string DeviceLogs = "/device/types/{0}/devices/{1}/diag/logs";
		private const string DeviceLog = "/device/types/{0}/devices/{1}/diag/logs/{2}";
		private const string DeviceErrorCode = "/device/types/{0}/devices/{1}/diag/errorCodes";
	
		private const string Problem = "/logs/connection";
		
		private const string DeviceMgmt = "/mgmt/requests";
		private const string DeviceMgmtInd = "/mgmt/requests/{0}";
		private const string DeviceMgmtStatus = "/mgmt/requests/{0}/deviceStatus";
		private const string DeviceMgmtStatusInd = "/mgmt/requests/{0}/deviceStatus/{1}/{2}";
		
		//private static string DeviceMgmtCustom = "/mgmt/custom/bundle";
		//private static string DeviceMgmtCustomInd = "/mgmt/custom/bundle/{0}";
		
		private const string Usage = "/usage/data-traffic";
		
		private const string ServiceStatus = "/service-status";
		
		private const string LastEventCache = "/device/types/{0}/devices/{1}/events";
		private const string LastEventCacheInd = "/device/types/{0}/devices/{1}/events/{2}";
		
		private const string Weather = "/devices/types/{0}/devices/{1}/exts/twc/ops/geocode";
		
		
		//IM api's
		
		//schema
		private const string SchemaDraftBase = "/draft/schemas";
		private const string SchemaDraftId = "/draft/schemas/{0}";
		private const string SchemaDraftContent = "/draft/schemas/{0}/content";
		
		private const string Schema = "/schemas";
		private const string SchemaId = "/schemas/{0}";
		private const string SchemaContent = "/schemas/{0}/content";
		
		//physicalinterfaces
		private const string PhysicalInterfacesDraft = "/draft/physicalinterfaces";
		private const string PhysicalInterfacesDraftId = "/draft/physicalinterfaces/{0}";
		private const string PhysicalInterfacesDraftEvent = "/draft/physicalinterfaces/{0}/events";
		private const string PhysicalInterfacesDraftEventId = "/draft/physicalinterfaces/{0}/events/{1}";
		
		private const string PhysicalInterfaces = "/physicalinterfaces";
		private const string PhysicalInterfacesId = "/physicalinterfaces/{0}";
		private const string PhysicalInterfacesEvents = "/physicalinterfaces/{0}/events";
		
		//logicalinterfaces
		private const string LogicalInterfacesDraft = "/draft/logicalinterfaces";
		private const string LogicalInterfacesDraftId = "/draft/logicalinterfaces/{0}";
		
		private const string LogicalInterfaces = "/logicalinterfaces";
		private const string LogicalInterfacesId = "/logicalinterfaces/{0}";
		
		//event types
		private const string EventTypeDraft = "/draft/event/types";
		private const string EventTypeDraftId = "/draft/event/types/{0}";
		
		private const string EventType = "/event/types";
		private const string EventTypeId = "/event/types/{0}";
		
		//Device state
		private const string DeviceState = "/device/types/{0}/devices/{1}/state/{2}";
		
		//Device Type IM
		private const string DeviceTypeLI = "/device/types/{0}/logicalinterfaces";
		private const string DeviceTypeLIId = "/device/types/{0}/logicalinterfaces/{1}";
		
		private const string DeviceTypeMapping = "/device/types/{0}/mappings";
		private const string DeviceTypeMappingId = "/device/types/{0}/mappings/{1}";
		
		private const string DeviceTypePI = "/device/types/{0}/physicalinterface";
		
		
		private const string DeviceTypeDraft = "/draft/device/types";
		private const string DeviceTypeDraftId = "/draft/device/types/{0}";
		private const string DeviceTypeDraftLI = "/draft/device/types/{0}/logicalinterfaces";
		private const string DeviceTypeDraftLIId = "/draft/device/types/{0}/logicalinterfaces/{1}";
		
		private const string DeviceTypeDraftMapping = "/draft/device/types/{0}/mappings";
		private const string DeviceTypeDraftMappingId = "/draft/device/types/{0}/mappings/{1}";
		
		private const string DeviceTypeDraftPI = "/draft/device/types/{0}/physicalinterface";
		
		
		private string _apiKey , _authToken,_orgId;
		protected IRestClient _client;
		
		public ApiClient(string apiKey , string authToken)
		{
			if(apiKey.Split('-').Length >0)
			{
				_apiKey = apiKey;
				_authToken = authToken;
				_orgId = apiKey.Split('-')[1];
				this.Timeout = 10000;
				_client = new RestClient(string.Format(BaseURL,_orgId)) as IRestClient;
				_client.Authenticator = new HttpBasicAuthenticator(_apiKey, _authToken);
			}
			else{
				throw new Exception("Invalid api key");
			}
		}
		protected dynamic RestHandler(Method methord,string urlSuffix ,object param , bool parseDynamic , RestRequest customRequest = null)
		{
			RestRequest request;
			bool isQuerry = false;
			if(customRequest != null){
				request = customRequest;
			}
			else
			{
				if(methord == Method.GET && param  is string)
				{
					urlSuffix +=param; 
					isQuerry = true;
				}
				request = new RestRequest(urlSuffix,methord);
			}
			request.Timeout = Timeout;
			if(param != null & !isQuerry){
				request.RequestFormat = DataFormat.Json;
				request.AddBody(param);
			}
			
			log.Info("Request " +methord.ToString()+"  "+_client.BuildUri(request));
			IRestResponse response = _client.Execute(request);
			int numericStatusCode = (int)response.StatusCode;
			log.Info("response " +response.ResponseUri +" with code " +response.StatusCode +"["+ numericStatusCode+"]");
			if(!(numericStatusCode >= 200 && numericStatusCode < 300)){
				throw new System.Net.WebException(response.StatusCode.ToString()+":" +response.Content as string);
			}
			if(parseDynamic){
				return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(response.Content as string);
			}
			return response.Content as string;
		
		}
		
		public int Timeout { get ; set ; }
		
		/// <summary>
		/// Get details about an organization.
		/// </summary>
		/// <returns>Dynamic Object of response </returns>
		public dynamic GetOrganizationDetail(){
			return  RestHandler(Method.GET,OrgInfo,null,true);
		}
		
		
		/// <summary>
		/// To get all devices registered 
		/// </summary>
		/// <returns>Dynamic Object of response </returns>
		public dynamic GetAllDevices(){
			return RestHandler(Method.GET,BulkGet,null,true);
		}
		
		/// <summary>
		/// Register multiple new devices
		/// </summary>
		/// <param name="info">Array of RegisterDevicesInfo object</param>
		/// <returns>Dynamic Object of response </returns>
		public dynamic RegisterMultipleDevices(RegisterDevicesInfo[] info){
			return RestHandler(Method.POST,BulkAdd,info,true);
		}
		
		/// <summary>
		/// Delete multiple devices
		/// </summary>
		/// <param name="info">Array of DeviceListElement object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteMultipleDevices(DeviceListElement[] info){
			return RestHandler(Method.POST,BulkRemove,info,true);
		}
		
		
		/// <summary>
		/// To get all device type registered
		/// </summary>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetAllDeviceTypes(){
			return RestHandler(Method.GET,DeviceTypes,null,true);
		}
		
		/// <summary>
		/// Creates a device type for a normal device or a gateway
		/// </summary>
		/// <param name="info">DeviceTypeInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic RegisterDeviceType(DeviceTypeInfo info){
			return RestHandler(Method.POST,DeviceTypes,info,true);
		}
		
		/// <summary>
		/// Gets device type details.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceType(string type){
			return RestHandler(Method.GET,string.Format(DeviceTypesIndigvual,type),null,true);
		}
		
		/// <summary>
		/// Deletes a device type.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteDeviceType(string type){
			return RestHandler(Method.DELETE,string.Format(DeviceTypesIndigvual,type),null,false);
		}
		
		/// <summary>
		/// Updates a device type
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="info">DeviceTypeInfoUpdate object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UpdateDeviceType(string type,DeviceTypeInfoUpdate info){
			return RestHandler(Method.PUT,string.Format(DeviceTypesIndigvual,type),info,true);
		}
		
		
		/// <summary>
		/// To get all device registered of given device type 
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic ListDevices(string type){
			return RestHandler(Method.GET,string.Format(Device,type),null,true);
		}
		
		/// <summary>
		/// To add a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="info">RegisterSingleDevicesInfo object </param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic RegisterDevice(string type, RegisterSingleDevicesInfo info){
			return RestHandler(Method.POST,string.Format(Device,type),info,true);
		}
		
		/// <summary>
		/// To remove a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UnregisterDevice(string type, string deviceId){
			return RestHandler(Method.DELETE,string.Format(DeviceIndigvual,type,deviceId),null,false);
		}
		
		/// <summary>
		/// Gets device details.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceInfo(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceIndigvual,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Updates a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="info">UpdateDevicesInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UpdateDeviceInfo(string type,string deviceId, UpdateDevicesInfo info){
			return RestHandler(Method.PUT,string.Format(DeviceIndigvual,type,deviceId),info,true);
		}
		
		/// <summary>
		/// Gets information on devices that are connected through the specified gateway (typeId, deviceId) to Watson IoT Platform.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetGatewayConnectedDevice(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceGatewayList,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Gets location information for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceLocationInfo(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceLocation,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Updates the location information for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="info">LocationInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UpdateDeviceLocationInfo(string type, string deviceId,LocationInfo info){
			return RestHandler(Method.PUT,string.Format(DeviceLocation,type,deviceId),info,false);
		}
		
		/// <summary>
		/// Gets device management information for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementInfo(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceMgmtInfo,type,deviceId),null,true);
		}
		
		
		/// <summary>
		/// Gets diagnostic logs for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetAllDiagnosticLogs(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceLogs,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Clears the diagnostic log for the device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic ClearAllDiagnosticLogs(string type, string deviceId){
			return RestHandler(Method.DELETE,string.Format(DeviceLogs,type,deviceId),null,false);
		}
		
		/// <summary>
		/// Adds an entry in the log of diagnostic information for the device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="info">LogInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic AddDeviceDiagLogs(string type, string deviceId,LogInfo info){
			return RestHandler(Method.POST,string.Format(DeviceLogs,type,deviceId),info,true);
		}
		/// <summary>
		/// Gets diagnostic log for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="logId">String value of log id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDiagnosticLog(string type, string deviceId,string logId){
			return RestHandler(Method.GET,string.Format(DeviceLog,type,deviceId,logId),null,true);
		}
		
		/// <summary>
		/// Delete this diagnostic log for the device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="logId">String value of log id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteDiagnosticLog(string type, string deviceId,string logId){
			return RestHandler(Method.DELETE,string.Format(DeviceLog,type,deviceId,logId),null,false);
		}
		
		/// <summary>
		/// Gets diagnostic error codes for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceErrorCodes(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceErrorCode,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Clears the list of error codes for the device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic ClearDeviceErrorCodes(string type, string deviceId){
			return RestHandler(Method.DELETE,string.Format(DeviceErrorCode,type,deviceId),null,false);
		}
		
		/// <summary>
		/// Adds an error code to the list of error codes for the device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="err">ErrorCodeInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic AddErrorCode(string type, string deviceId,ErrorCodeInfo err){
			return RestHandler(Method.POST,string.Format(DeviceErrorCode,type,deviceId),err,false);
		}
		
		/// <summary>
		/// List connection log events for a device to aid in diagnosing connectivity problems.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceConnectionLogs(string type, string deviceId){
			return RestHandler(Method.GET,Problem,"?typeId="+type+"&deviceId="+deviceId,true);
		}
		
		/// <summary>
		/// Gets a list of device management requests, which can be in progress or recently completed
		/// </summary>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetAllDeviceManagementRequests(){
			return RestHandler(Method.GET,DeviceMgmt,null,true);
		}
		
		/// <summary>
		/// Initiates a device management request, such as reboot.
		/// </summary>
		/// <param name="action">String value of action "device/reboot","device/factoryReset","firmware/download" and "firmware/update"</param>
		/// <param name="parameters">Array of DeviceMgmtparameter object</param>
		/// <param name="devices">Array of DeviceListElement object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic InitiateDeviceManagementRequest(string action,DeviceMgmtparameter [] parameters,DeviceListElement [] devices){
			return RestHandler(Method.POST,DeviceMgmt,new { action = action,parameters =parameters ,devices=devices},true);
		}
		
		/// <summary>
		/// Clears the status of a device management request
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteDeviceManagementRequest(string requestId){
			return RestHandler(Method.DELETE,string.Format(DeviceMgmtInd,requestId),null,false);
		}
		
		/// <summary>
		/// Gets details of a device management request.
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementRequest(string requestId){
			return RestHandler(Method.GET,string.Format(DeviceMgmtInd,requestId),null,true);
		}
		
		/// <summary>
		/// Gets a list of device management request device statuses for a particular request.
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementRequestStatus(string requestId){
			return RestHandler(Method.GET,string.Format(DeviceMgmtStatus,requestId),null,true);
		}
		
		/// <summary>
		/// Get an individual device management request device status
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementRequestStatus(string requestId,string type,string deviceId){
			return RestHandler(Method.GET,string.Format(DeviceMgmtStatusInd,requestId,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Retrieve the amount of data used
		/// </summary>
		/// <param name="start">String value of start date of format YYYY ,YYYY-MM,YYYY-MM-DD </param>
		/// <param name="end">String value of end date of format YYYY ,YYYY-MM,YYYY-MM-DD </param>
		/// <param name="detail">bool value states whether a daily breakdown will be included in the result set</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDataUsage(string start, string end,bool detail){
			return RestHandler(Method.GET,Usage,"?start="+start+"&end="+end+"&detail="+detail,true);
		}
		
		/// <summary>
		/// Retrieve the organization-specific status of each of the services offered by Watson IoT Platform.
		/// </summary>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetServiceStatus(){
			return RestHandler(Method.GET,ServiceStatus,null,true);
		}
		
		/// <summary>
		/// Get last event for a specific event id for a specific device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetLastEvents(string type, string deviceId){
			return RestHandler(Method.GET,string.Format(LastEventCache,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Get all last events for a specific device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="eventType">String value of event name</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetLastEventsByEventType(string type, string deviceId,string eventType){
			return RestHandler(Method.GET,string.Format(LastEventCacheInd,type,deviceId,eventType),null,true);
		}
		
		/// <summary>
		/// Retrieve current meteorological observations for the location associated with your device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceLocationWeather (string type, string deviceId){
			return RestHandler(Method.GET,string.Format(Weather,type,deviceId),null,true);
		}
		
		//TODO :All Extention functionalities
        
		
		//IM Api's Section
		//schemas
		/// <summary>
		/// Query active schema definitions,Schemas are used to define the structure of Events, Device State and Thing State in the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas]
		/// </summary>
		/// <returns>SchemaCollection object</returns>
		public SchemaCollection GetAllActiveSchemas(){
			string result = RestHandler(Method.GET,Schema,null,false) as string ;
		    return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaCollection>(result);
		}
		/// <summary>
		/// Retrieves the metadata for the active schema definition with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas_schemaId]
		/// </summary>
		/// <param name="id">String value of Schema id</param>
		/// <returns>SchemaInfo object</returns>
		public SchemaInfo GetActiveSchemaMetadata(string id){
			string result = RestHandler(Method.GET,string.Format(SchemaId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaInfo>(result);
		}
		/// <summary>
		/// Retrieves the content of the active schema definition file with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas_schemaId_content]
		/// </summary>
		/// <param name="id">String value of Schema id</param>
		/// <returns>dynamic object</returns>
		public dynamic GetActiveSchemaContent(string id){
			return RestHandler(Method.GET,string.Format(SchemaContent,id),null,true);
		}
		/// <summary>
		/// Query draft schema definitions
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas]
		/// </summary>
		/// <returns>SchemaCollection object</returns>
		public SchemaCollection GetAllDraftSchemas(){
			string result = RestHandler(Method.GET,SchemaDraftBase,null,false) as string ;
		    return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaCollection>(result);
		}
		/// <summary>
		/// Create a draft schema definition
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/post_draft_schemas]
		/// </summary>
		/// <param name="draft">SchemaDraft object filled with valid properties</param>
		/// <returns>SchemaInfo object</returns>
		public SchemaInfo AddDraftSchema(SchemaDraft draft){
			if(!draft.Validate())
			{
				throw new Exception("Invalid Schema");
			}
			RestRequest request = new RestRequest(SchemaDraftBase, Method.POST);
			request.AddParameter("name", draft.name);
			if(!String.IsNullOrEmpty(draft.description)){
				request.AddParameter("description", draft.description);
			}
			if(!String.IsNullOrEmpty(draft.schemaType)){
				request.AddParameter("schemaType", draft.schemaType);
			}
			request.AddFile("schemaFile",draft.schemaFile,"application/json");
		    request.AlwaysMultipartFormData = true;
			string result = RestHandler(Method.POST,"",null,false,request) as string ;
		    return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaInfo>(result);
		}
		/// <summary>
		/// Deletes the draft schema definition with the specified id from the organization in the Watson IoT Platform. Deleting the schema definition deletes both the metadata and the actual schema definition file from the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/delete_draft_schemas_schemaId]
		/// </summary>
		/// <param name="id">String value of Schema id</param>
		public void DeleteDraftSchema(string id){
			 RestHandler(Method.DELETE,string.Format(SchemaDraftId,id),null,false);
		}
		/// <summary>
		/// Retrieves the metadata for the draft schema definition with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas_schemaId]
		/// </summary>
		/// <param name="id">String value of Schema id</param>
		/// <returns>SchemaInfo object</returns>
		public SchemaInfo GetDraftSchemaMetadata(string id){
			string result = RestHandler(Method.GET,string.Format(SchemaDraftId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaInfo>(result);
		}
		/// <summary>
		/// Updates the metadata for the draft schema definition with the specified id. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/put_draft_schemas_schemaId]
		/// </summary>
		/// <param name="updated">updated SchemaInfo object</param>
		/// <returns>SchemaInfo object</returns>
		public SchemaInfo UpdateDraftSchemaMetadata(SchemaInfo updated){
			string result = RestHandler(Method.PUT,string.Format(SchemaDraftId,updated.id),updated,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaInfo>(result);
		}
		/// <summary>
		/// Retrieves the content of the draft schema definition file with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas_schemaId_content]
		/// </summary>
		/// <param name="id">String value of Schema id</param>
		/// <returns>dynamic object</returns>
		public dynamic GetDraftSchemaContent(string id){
			return RestHandler(Method.GET,string.Format(SchemaDraftContent,id),null,true);
		}
		/// <summary>
		/// Updates the content of a draft schema definition file with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/put_draft_schemas_schemaId_content]
		/// </summary>
		/// <param name="schemaId">String value of Schema id</param>
		/// <param name="schemaFilePath">String value of schema file path</param>
		/// <returns>SchemaInfo object</returns>
		public SchemaInfo UpdateDraftSchemaContent(string schemaId,string schemaFilePath){
			RestRequest request = new RestRequest(string.Format(SchemaDraftContent,schemaId), Method.PUT);
			request.AddFile("schemaFile",schemaFilePath,"application/json");
		    request.AlwaysMultipartFormData = true;
			string result = RestHandler(Method.POST,"",null,false,request) as string ;
		    return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaInfo>(result);
		}
		
		//physical interface
		/// <summary>
		/// Query active phyiscal interfaces
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces]
		/// </summary>
		/// <returns>PhysicalInterfacesCollection object</returns>
		public PhysicalInterfacesCollection GetAllActivePhysicalInterfaces(){
			string result = RestHandler(Method.GET,PhysicalInterfaces,null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesCollection>(result);
		}
		/// <summary>
		/// Retrieve the active physical interface with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces_physicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo GetActivePhysicalInterfaces(string id){
			string result = RestHandler(Method.GET,string.Format(PhysicalInterfacesId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
		/// <summary>
		/// Retrieve the list of event mappings for the active physical interface. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces_physicalInterfaceId_events]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		/// <returns>Array of EventTypeBind object</returns>
		public EventTypeBind[] GetActivePhysicalInterfacesEvents(string id){
			string result = RestHandler(Method.GET,string.Format(PhysicalInterfacesEvents,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeBind[]>(result);
		}
		/// <summary>
		/// Query draft phyiscal interfaces
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces]
		/// </summary>
		/// <returns>PhysicalInterfacesCollection object</returns>
		public PhysicalInterfacesCollection GetAllDraftPhysicalInterfaces(){
			string result = RestHandler(Method.GET,PhysicalInterfacesDraft,null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesCollection>(result);
		}
		/// <summary>
		/// Creates a new draft physical interface for the organization in the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/post_draft_physicalinterfaces]
		/// </summary>
		/// <param name="draft">PhysicalInterfaceDraft object</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo AddDraftPhysicalInterfaces(PhysicalInterfaceDraft draft){
			string result = RestHandler(Method.POST,PhysicalInterfacesDraft,draft,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
		/// <summary>
		/// Retrieve the draft physical interface with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces_physicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo GetDraftPhysicalInterfaces(string id){
			string result = RestHandler(Method.GET,string.Format(PhysicalInterfacesDraftId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
		/// <summary>
		/// Deletes the draft physical interface with the specified id from the organization in the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/delete_draft_physicalinterfaces_physicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		public void DeleteDraftPhysicalInterfaces(string id){
			 RestHandler(Method.DELETE,string.Format(PhysicalInterfacesDraftId,id),null,false);
		}
		/// <summary>
		/// Updates the draft physical interface with the specified id. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/put_draft_physicalinterfaces_physicalInterfaceId]
		/// </summary>
		/// <param name="draft">updated PhysicalInterfacesInfo object</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo UpdateDraftPhysicalInterfaces(PhysicalInterfacesInfo draft){
			string result = RestHandler(Method.PUT,string.Format(PhysicalInterfacesDraftId,draft.id),draft,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
		/// <summary>
		/// Maps an event id to a specific event type for the draft specified physical interface.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/post_draft_physicalinterfaces_physicalInterfaceId_events]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		/// <param name="evnt">EventTypeBind object for the event </param>
		/// <returns>EventTypeBind object</returns>
		public EventTypeBind MapDraftPhysicalInterfacesEvent(string id,EventTypeBind evnt){
			string result = RestHandler(Method.POST,string.Format(PhysicalInterfacesDraftEvent,id),evnt,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeBind>(result);
		}
		/// <summary>
		/// Retrieve the list of event mappings for the draft physical interface.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces_physicalInterfaceId_events]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		/// <returns>Array of EventTypeBind object</returns>
		public EventTypeBind[] GetAllDraftPhysicalInterfacesMappedEvents(string id){
			string result = RestHandler(Method.GET,string.Format(PhysicalInterfacesDraftEvent,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeBind[]>(result);
		}
		/// <summary>
		/// Removes the event mapping with the specified id from the draft physical interface.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/delete_draft_physicalinterfaces_physicalInterfaceId_events_eventId]
		/// </summary>
		/// <param name="id">String value of physicalInterface Id</param>
		/// <param name="eventId">String value of event Id in EventTypeBind object</param>
		public void DeleteDraftPhysicalInterfacesMappedEvents(string id,string eventId){
			 RestHandler(Method.DELETE,string.Format(PhysicalInterfacesDraftEventId,id,eventId),null,false);
		}
		
		//LogicalInterface
		/// <summary>
		/// Query active logical interfaces
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_logicalinterfaces]
		/// </summary>
		/// <returns>LogicalInterfaceCollection object</returns>
		public LogicalInterfaceCollection GetAllActiveLogicalInterfaces(){
			string result = RestHandler(Method.GET,LogicalInterfaces,null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceCollection>(result);
		}
		/// <summary>
		/// Retrieve the active logical interface with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of LogicalInterfaces Id</param>
		/// <returns>LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo GetActiveLogicalInterfaces(string id){
			string result = RestHandler(Method.GET,string.Format(LogicalInterfacesId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(result);
		}
		/// <summary>
		/// Performs the specified operation against the logical interface
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/patch_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of LogicalInterfaces Id</param>
		/// <param name="operate">OperationInfo object with specified operation</param>
		/// <returns>OperationResponse object</returns>
	    public  OperationResponse OperateLogicalInterfaces(string id,OperationInfo operate){
			string result = RestHandler(Method.PATCH,string.Format(LogicalInterfacesId,id),operate,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<OperationResponse>(result);
		}
		/// <summary>
		/// Query draft logical interfaces
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_draft_logicalinterfaces]
		/// </summary>
		/// <returns>LogicalInterfaceCollection object</returns>
		public LogicalInterfaceCollection GetAllDraftLogicalInterfaces(){
			string result = RestHandler(Method.GET,LogicalInterfacesDraft,null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceCollection>(result);
		}
		/// <summary>
		/// Creates a new draft logical interface for the organization in the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/post_draft_logicalinterfaces]
		/// </summary>
		/// <param name="draft">LogicalInterfaceDraft object</param>
		/// <returns>LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo AddDraftLogicalInterfaces(LogicalInterfaceDraft draft){
			string result = RestHandler(Method.POST,LogicalInterfacesDraft,draft,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(result);
		}
		/// <summary>
		/// Deletes the draft logical interface with the specified id from the organization in the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/delete_draft_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of LogicalInterfaces Id</param>
		public void DeleteDraftLogicalInterfaces(string id){
			 RestHandler(Method.DELETE,string.Format(LogicalInterfacesDraftId,id),null,false);
		}
		/// <summary>
		/// Retrieve the draft logical interface with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_draft_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of LogicalInterfaces Id</param>
		/// <returns>LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo GetDraftLogicalInterfaces(string id){
			string result = RestHandler(Method.GET,string.Format(LogicalInterfacesDraftId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(result);
		}
		/// <summary>
		/// Performs the specified operation against the draft logical interface. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/patch_draft_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of LogicalInterfaces Id</param>
		/// <param name="operate">OperationInfo with specified operataion</param>
		/// <returns>OperationDraftResponse object</returns>
		public OperationDraftResponse OperateDraftLogicalInterfaces(string id,OperationInfo operate){
			string result = RestHandler(Method.PATCH,string.Format(LogicalInterfacesDraftId,id),operate,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<OperationDraftResponse>(result);
		}
		/// <summary>
		/// Updates the draft logical interface with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/put_draft_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="update">updated LogicalInterfaceInfo object</param>
		/// <returns>LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo UpdateDraftLogicalInterfaces(LogicalInterfaceInfo update){
			string result = RestHandler(Method.PUT,string.Format(LogicalInterfacesDraftId,update.id),update,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(result);
		}

		//Event Type
		/// <summary>
		/// Query active event types
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_event_types]
		/// </summary>
		/// <returns>EventTypeCollection object</returns>
		public EventTypeCollection GetAllActiveEventType(){
			string result = RestHandler(Method.GET,EventType,null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeCollection>(result);
		}
		/// <summary>
		/// Retrieve the active event type with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_event_types_eventTypeId]
		/// </summary>
		/// <param name="id">String value of EventType Id</param>
		/// <returns>EventTypeInfo object</returns>
		public EventTypeInfo GetActiveEventType(string id){
			string result = RestHandler(Method.GET,string.Format(EventTypeId,id),null,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeInfo>(result);
		}
		/// <summary>
		/// Query draft event types
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_draft_event_types]
		/// </summary>
		/// <returns>EventTypeCollection object</returns>
		public EventTypeCollection GetAllDraftEventType(){
			string result = RestHandler(Method.GET,EventTypeDraft,null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeCollection>(result);
		}
		/// <summary>
		/// Creates a new draft event type for the organization in the Watson IoT Platform. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/post_draft_event_types]
		/// </summary>
		/// <param name="draft">EventTypeDraft object</param>
		/// <returns>EventTypeInfo object</returns>
		public EventTypeInfo AddDraftEventType(EventTypeDraft draft){
			string result = RestHandler(Method.POST,EventTypeDraft,draft,false) as string ;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeInfo>(result);
		}
		/// <summary>
		/// Deletes the draft event type with the specified id from the organization in the Watson IoT Platform.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/delete_draft_event_types_eventTypeId]
		/// </summary>
		/// <param name="id">String value of EventType draft's Id</param>
		public void DeleteDraftEventType(string id){
			 RestHandler(Method.DELETE,string.Format(EventTypeDraftId,id),null,false);
		}
		/// <summary>
		/// Retrieve the draft event type with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_draft_event_types_eventTypeId]
		/// </summary>
		/// <param name="id">String value of EventType draft's Id</param>
		/// <returns>EventTypeInfo object</returns>
		public EventTypeInfo GetDraftEventType(string id){
			string result = RestHandler(Method.GET,string.Format(EventTypeDraftId,id),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeInfo>(result);
		}
		/// <summary>
		/// Updates the draft event type with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/put_draft_event_types_eventTypeId]
		/// </summary>
		/// <param name="info">updated EventTypeInfo object</param>
		/// <returns>EventTypeInfo object</returns>
		public EventTypeInfo UpdateDraftEventType(EventTypeInfo info){
			string result = RestHandler(Method.PUT,string.Format(EventTypeDraftId,info.id),info,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeInfo>(result);
		}

		//device state for IM
		/// <summary>
		/// Retrieve the current state of the device with the specified id.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Devices/get_device_types_typeId_devices_deviceId_state_logicalInterfaceId]
		/// </summary>
		/// <param name="typeId">String value of device type Id</param>
		/// <param name="deviceId">String value of device Id</param>
		/// <param name="logicalInterfaceId">String value of logicalInterfaceId</param>
		/// <returns>dynamic object</returns>
		public dynamic GetCurrentDeviceState(string typeId,string deviceId, string logicalInterfaceId){
			return RestHandler(Method.GET,string.Format(DeviceState,typeId,deviceId,logicalInterfaceId),null,true);
		}
		
		//device type
		/// <summary>
		/// Performs the specified operation against the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/patch_device_types_typeId]
		/// </summary>
		/// <param name="id">String value of device type Id </param>
		/// <param name="operate">OperationInfo object with operation specified</param>
		/// <returns>OperationResponse object</returns>
		public OperationResponse OperateDeviceType(string id,OperationInfo operate){
			string result = RestHandler(Method.PATCH,string.Format(DeviceTypesIndigvual,id),operate,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<OperationResponse>(result);
		}
		/// <summary>
		/// Retrieve the list of active logical interfaces that have been associated with the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_logicalinterfaces]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <returns>Array of LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo[] GetAllActiveDeviceTypeLI(string id){
			string result = RestHandler(Method.GET,string.Format(DeviceTypeLI,id),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo[]>(result);
		}
		/// <summary>
		/// Retrieve the list of active property mappings for the specified device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_mappings]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <returns>Array of MappingInfo object</returns>
		public MappingInfo[] GetAllActiveDeviceTypeMappings(string id){
			string result =  RestHandler(Method.GET,string.Format(DeviceTypeMapping,id),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MappingInfo[]>(result);
		}
		/// <summary>
		/// Retrieves the active property mappings for a specific logical interface for the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_mappings_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="logicalInterfaceId">String value of logicalInterface Id</param>
		/// <returns>MappingInfo object</returns>
		public MappingInfo GetActiveDeviceTypeMappingLI(string id,string logicalInterfaceId){
			string result =  RestHandler(Method.GET,string.Format(DeviceTypeMappingId,id,logicalInterfaceId),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MappingInfo>(result);
		}
		/// <summary>
		/// Retrieve the active physical interface that has been associated with the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_physicalinterface]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo GetActiveDeviceTypePI(string id){
			string result =  RestHandler(Method.GET,string.Format(DeviceTypePI,id),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
		/// <summary>
		/// Retrieves the list of device types that are associated with the logical interface and/or physical interface with the ids specified using the corresponding query parameters.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types]
		/// </summary>
		/// <param name="physicalInterfaceId">String value of physicalInterfaceId </param>
		/// <param name="logicalInterfaceId">String value of logicalInterfaceId</param>
		/// <returns>dynamic object</returns>
		public dynamic GetAllDraftDeviceType(string physicalInterfaceId ,string logicalInterfaceId){
			string query= "?";
			if( !String.IsNullOrEmpty(physicalInterfaceId)){
				query+="physicalInterfaceId="+physicalInterfaceId;
			}
			if( !String.IsNullOrEmpty(logicalInterfaceId)){
				query+="logicalInterfaceId="+logicalInterfaceId;
			}
			if(query == "?"){
				log.Error("Invalid input params");
				throw new Exception("Invalid input params");
			}
			return RestHandler(Method.GET,DeviceTypeDraft,query,true);
		}
		/// <summary>
		/// Performs the specified operation against the draft device type. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/patch_draft_device_types_typeId]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="operate">OperationInfo object with operation specified</param>
		/// <returns>OperationDraftResponse object</returns>
		public OperationDraftResponse OperateDraftDeviceType(string id,OperationInfo operate){
			string result = RestHandler(Method.PATCH,string.Format(DeviceTypeDraftId,id),operate,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<OperationDraftResponse>(result);
		}
		/// <summary>
		/// Retrieve the list of draft logical interfaces that have been associated with the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_logicalinterfaces]
		/// </summary>
		/// <param name="typeId">String value of device type Id</param>
		/// <returns>Array of LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo[]  GetAllDraftDeviceTypeLI(string typeId){
			string result = RestHandler(Method.GET,string.Format(DeviceTypeDraftLI,typeId),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo[]>(result);
		}
		/// <summary>
		/// Associates a draft logical interface with the specified device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_logicalinterfaces]
		/// </summary>
		/// <param name="typeId">String value of device type Id</param>
		/// <param name="info">LogicalInterfaceInfo object</param>
		/// <returns>LogicalInterfaceInfo object</returns>
		public LogicalInterfaceInfo AddDraftDeviceTypeLI(string typeId,LogicalInterfaceInfo info){
			string result = RestHandler(Method.POST,string.Format(DeviceTypeDraftLI,typeId),info,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(result);
		}
		/// <summary>
		/// Disassociates the draft logical interface with the specified id from the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_logicalinterfaces_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="logicalInterfaceId">String value of device logicalInterface Id</param>
		public void DeleteDraftDeviceTypeLI(string id,string logicalInterfaceId){
			RestHandler(Method.DELETE,string.Format(DeviceTypeDraftLIId,id,logicalInterfaceId),null,false);
		}
		/// <summary>
		/// Retrieve the list of draft property mappings for the specified device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_mappings]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <returns>Array of MappingInfo object</returns>
		public MappingInfo[] GetAllDraftDeviceTypeMapping(string id){
			string result =  RestHandler(Method.GET,string.Format(DeviceTypeDraftMapping,id),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MappingInfo[]>(result);
		}
		/// <summary>
		/// Creates the draft property mappings for an logical interface for the device type. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_mappings]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="draft">MappingDraft object with all values</param>
		/// <returns>MappingInfo object</returns>
		public MappingInfo AddDraftDeviceTypeMapping(string id,MappingDraft draft){
			string result = RestHandler(Method.POST,string.Format(DeviceTypeDraftMapping,id),draft,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MappingInfo>(result);
		}
		/// <summary>
		/// Deletes the draft property mappings for a specific logical interface for the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_mappings_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="logicalInterfaceId">String value of logicalInterface Id</param>
		public void DeleteDraftDeviceTypeMapping(string id,string logicalInterfaceId){
			 RestHandler(Method.DELETE,string.Format(DeviceTypeDraftMappingId,id,logicalInterfaceId),null,false);
		}
		/// <summary>
		/// Retrieves the draft property mappings for a specific logical interface for the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_mappings_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="logicalInterfaceId">String value of logicalInterface Id</param>
		/// <returns>MappingInfo object</returns>
		public MappingInfo GetDraftDeviceTypeMapping(string id,string logicalInterfaceId){
			string result =  RestHandler(Method.GET,string.Format(DeviceTypeDraftMappingId,id,logicalInterfaceId),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MappingInfo>(result);
			
		}
		/// <summary>
		/// Updates the draft property mappings for a specific logical interface for the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/put_draft_device_types_typeId_mappings_logicalInterfaceId]
		/// </summary>
		/// <param name="id">String value of device type Id</param>
		/// <param name="info">Updated LogicalInterfaceInfo object</param>
		/// <returns>LogicalInterfaceInfo object</returns>
		public MappingInfo UpdatedDraftDeviceTypeMapping(string id,MappingDraft info){
			string result = RestHandler(Method.PUT,string.Format(DeviceTypeDraftMappingId,id,info.logicalInterfaceId),info,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<MappingInfo>(result);
		} 
		
		/// <summary>
		/// Associates a draft physical interface with the specified device type. 
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_physicalinterface]
		/// </summary>
		/// <param name="deviceTypeId">String value of device type Id</param>
		/// <param name="info">PhysicalInterfacesInfo object</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo AddDraftDeviceTypePI(string deviceTypeId,PhysicalInterfacesInfo info){
			string result = RestHandler(Method.POST,string.Format(DeviceTypeDraftPI,deviceTypeId),info,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
		/// <summary>
		/// Disassociates the draft physical interface from the device type.
		/// For more info [https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_physicalinterface]
		/// </summary>
		/// <param name="deviceTypeId">String value of device type Id</param>
		public void DeleteDraftDeviceTypePI(string deviceTypeId){
			 RestHandler(Method.DELETE,string.Format(DeviceTypeDraftPI,deviceTypeId),null,false);
		}
		/// <summary>
		/// Retrieve the draft physical interface that has been associated with the device type.
		/// </summary>
		/// <param name="deviceTypeId">String value of device type Id</param>
		/// <returns>PhysicalInterfacesInfo object</returns>
		public PhysicalInterfacesInfo GetDraftDeviceTypePI(string deviceTypeId){
			string result = RestHandler( Method.GET,string.Format(DeviceTypeDraftPI,deviceTypeId),null,false) as string;
			return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(result);
		}
	}
}
