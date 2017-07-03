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

using log4net;

namespace IBMWIoTP
{
	/// <summary>
	/// ApiClient class to perform all rest Api operations available in Watson IoT Platform 
	/// </summary>
	public class ApiClient
	{
		ILog log = log4net.LogManager.GetLogger(typeof(ApiClient));
		private static string BaseURL = "https://{0}.internetofthings.ibmcloud.com/api/v0002";
		
		private static string OrgInfo = "/";
		
		private static string BulkGet = "/bulk/devices";
		private static string BulkAdd = "/bulk/devices/add";
		private static string BulkRemove = "/bulk/devices/remove";
		
		private static string DeviceTypes = "/device/types";
		private static string DeviceTypesIndigvual = "/device/types/{0}";
		
		private static string Device = "/device/types/{0}/devices";
		private static string DeviceIndigvual = "/device/types/{0}/devices/{1}";
		private static string DeviceGatewayList = "/device/types/{0}/devices/{1}/devices";
		private static string DeviceLocation = "/device/types/{0}/devices/{1}/location";
		private static string DeviceMgmtInfo = "/device/types/{0}/devices/{1}/mgmt";
		
		private static string DeviceLogs = "/device/types/{0}/devices/{1}/diag/logs";
		private static string DeviceLog = "/device/types/{0}/devices/{1}/diag/logs/{2}";
		private static string DeviceErrorCode = "/device/types/{0}/devices/{1}/diag/errorCodes";
	
		private static string Problem = "/logs/connection";
		
		private static string DeviceMgmt = "/mgmt/requests";
		private static string DeviceMgmtInd = "/mgmt/requests/{0}";
		private static string DeviceMgmtStatus = "/mgmt/requests/{0}/deviceStatus";
		private static string DeviceMgmtStatusInd = "/mgmt/requests/{0}/deviceStatus/{1}/{2}";
		
		//private static string DeviceMgmtCustom = "/mgmt/custom/bundle";
		//private static string DeviceMgmtCustomInd = "/mgmt/custom/bundle/{0}";
		
		private static string Usage = "/usage/data-traffic";
		
		private static string ServiceStatus = "/service-status";
		
		private static string LastEventCache = "/device/types/{0}/devices/{1}/events";
		private static string LastEventCacheInd = "/device/types/{0}/devices/{1}/events/{2}";
		
		private static string Weather = "/devices/types/{0}/devices/{1}/exts/twc/ops/geocode";
		
		
		private string _apiKey , _authToken,_orgId;
		
		public ApiClient(string apiKey , string authToken)
		{
			if(apiKey.Split('-').Length >0)
			{
				_apiKey = apiKey;
				_authToken = authToken;
				_orgId = apiKey.Split('-')[1];
				this.Timeout = 6000;
			}
			else{
				throw new Exception("Invalid api key");
			}
		}
		protected dynamic RestHandler(string methord,string urlSuffix ,object param , bool isJsonResponse){	//,bool validResponseCode){
		
			string url =string.Format(BaseURL,_orgId);
			url = url+urlSuffix;
			bool isQuerry = false;
			if(methord == "GET" && param  is string)
			{
				url +=param; 
				isQuerry = true;
			}
			System.Net.WebRequest request = WebRequest.Create(url);
			request.Method = methord; 
			byte[] credentialBuffer = new UTF8Encoding().GetBytes(_apiKey + ":" +_authToken);
			request.Headers["Authorization"] ="Basic " + Convert.ToBase64String(credentialBuffer);
			request.PreAuthenticate = true;
			request.Timeout = Timeout;
			log.Info("Request " +methord+"  "+url);
			
			if(param != null & !isQuerry){
				request.ContentType = "application/json";
				
				using (var streamWriter = new StreamWriter(request.GetRequestStream()))
				{
				    string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(param);
					log.Info("Request Json " +json);
				
				    streamWriter.Write(json);
				    streamWriter.Flush();
				    streamWriter.Close();
				}
			}
			// Get the response.
			HttpWebResponse  response =(HttpWebResponse) request.GetResponse();
			int responseCode =(int) response.StatusCode;
			log.Info("response " +response.ResponseUri +"  " +responseCode);
			
			using (var responseStream = response.GetResponseStream())
			{
				if (responseStream != null)
				using (var reader = new StreamReader(responseStream))
				{	
					var JsonResponse = reader.ReadToEnd();
					if(isJsonResponse){
						return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(JsonResponse);
					}
				}
			}
			
			return new {};
		}
		
		public int Timeout { get ; set ; }
		
		/// <summary>
		/// Get details about an organization.
		/// </summary>
		/// <returns>Dynamic Object of response </returns>
		public dynamic GetOrganizationDetail(){
			return  RestHandler("GET",OrgInfo,null,true);
		}
		
		
		/// <summary>
		/// To get all devices registered 
		/// </summary>
		/// <returns>Dynamic Object of response </returns>
		public dynamic GetAllDevices(){
			return RestHandler("GET",BulkGet,null,true);
		}
		
		/// <summary>
		/// Register multiple new devices
		/// </summary>
		/// <param name="info">Array of RegisterDevicesInfo object</param>
		/// <returns>Dynamic Object of response </returns>
		public dynamic RegisterMultipleDevices(RegisterDevicesInfo[] info){
			return RestHandler("POST",BulkAdd,info,true);
		}
		
		/// <summary>
		/// Delete multiple devices
		/// </summary>
		/// <param name="info">Array of DeviceListElement object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteMultipleDevices(DeviceListElement[] info){
			return RestHandler("POST",BulkRemove,info,true);
		}
		
		
		/// <summary>
		/// To get all device type registered
		/// </summary>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetAllDeviceTypes(){
			return RestHandler("GET",DeviceTypes,null,true);
		}
		
		/// <summary>
		/// Creates a device type for a normal device or a gateway
		/// </summary>
		/// <param name="info">DeviceTypeInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic RegisterDeviceType(DeviceTypeInfo info){
			return RestHandler("POST",DeviceTypes,info,true);
		}
		
		/// <summary>
		/// Gets device type details.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceType(string type){
			return RestHandler("GET",string.Format(DeviceTypesIndigvual,type),null,true);
		}
		
		/// <summary>
		/// Deletes a device type.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteDeviceType(string type){
			return RestHandler("DELETE",string.Format(DeviceTypesIndigvual,type),null,false);
		}
		
		/// <summary>
		/// Updates a device type
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="info">DeviceTypeInfoUpdate object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UpdateDeviceType(string type,DeviceTypeInfoUpdate info){
			return RestHandler("PUT",string.Format(DeviceTypesIndigvual,type),info,true);
		}
		
		
		/// <summary>
		/// To get all device registered of given device type 
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic ListDevices(string type){
			return RestHandler("GET",string.Format(Device,type),null,true);
		}
		
		/// <summary>
		/// To add a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="info">RegisterSingleDevicesInfo object </param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic RegisterDevice(string type, RegisterSingleDevicesInfo info){
			return RestHandler("POST",string.Format(Device,type),info,true);
		}
		
		/// <summary>
		/// To remove a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UnregisterDevice(string type, string deviceId){
			return RestHandler("DELETE",string.Format(DeviceIndigvual,type,deviceId),null,false);
		}
		
		/// <summary>
		/// Gets device details.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceInfo(string type, string deviceId){
			return RestHandler("GET",string.Format(DeviceIndigvual,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Updates a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="info">UpdateDevicesInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UpdateDeviceInfo(string type,string deviceId, UpdateDevicesInfo info){
			return RestHandler("PUT",string.Format(DeviceIndigvual,type,deviceId),info,true);
		}
		
		/// <summary>
		/// Gets information on devices that are connected through the specified gateway (typeId, deviceId) to Watson IoT Platform.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetGatewayConnectedDevice(string type, string deviceId){
			return RestHandler("GET",string.Format(DeviceGatewayList,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Gets location information for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceLocationInfo(string type, string deviceId){
			return RestHandler("GET",string.Format(DeviceLocation,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Updates the location information for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="info">LocationInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic UpdateDeviceLocationInfo(string type, string deviceId,LocationInfo info){
			return RestHandler("PUT",string.Format(DeviceLocation,type,deviceId),info,false);
		}
		
		/// <summary>
		/// Gets device management information for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementInfo(string type, string deviceId){
			return RestHandler("GET",string.Format(DeviceMgmtInfo,type,deviceId),null,true);
		}
		
		
		/// <summary>
		/// Gets diagnostic logs for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetAllDiagnosticLogs(string type, string deviceId){
			return RestHandler("GET",string.Format(DeviceLogs,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Clears the diagnostic log for the device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic ClearAllDiagnosticLogs(string type, string deviceId){
			return RestHandler("DELETE",string.Format(DeviceLogs,type,deviceId),null,false);
		}
		
		/// <summary>
		/// Adds an entry in the log of diagnostic information for the device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="info">LogInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic AddDeviceDiagLogs(string type, string deviceId,LogInfo info){
			return RestHandler("POST",string.Format(DeviceLogs,type,deviceId),info,true);
		}
		/// <summary>
		/// Gets diagnostic log for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="logId">String value of log id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDiagnosticLog(string type, string deviceId,string logId){
			return RestHandler("GET",string.Format(DeviceLog,type,deviceId,logId),null,true);
		}
		
		/// <summary>
		/// Delete this diagnostic log for the device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="logId">String value of log id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteDiagnosticLog(string type, string deviceId,string logId){
			return RestHandler("DELETE",string.Format(DeviceLog,type,deviceId,logId),null,false);
		}
		
		/// <summary>
		/// Gets diagnostic error codes for a device.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceErrorCodes(string type, string deviceId){
			return RestHandler("GET",string.Format(DeviceErrorCode,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Clears the list of error codes for the device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic ClearDeviceErrorCodes(string type, string deviceId){
			return RestHandler("DELETE",string.Format(DeviceErrorCode,type,deviceId),null,false);
		}
		
		/// <summary>
		/// Adds an error code to the list of error codes for the device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="err">ErrorCodeInfo object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic AddErrorCode(string type, string deviceId,ErrorCodeInfo err){
			return RestHandler("POST",string.Format(DeviceErrorCode,type,deviceId),err,false);
		}
		
		/// <summary>
		/// List connection log events for a device to aid in diagnosing connectivity problems.
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceConnectionLogs(string type, string deviceId){
			return RestHandler("GET",Problem,"?typeId="+type+"&deviceId="+deviceId,true);
		}
		
		/// <summary>
		/// Gets a list of device management requests, which can be in progress or recently completed
		/// </summary>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetAllDeviceManagementRequests(){
			return RestHandler("GET",DeviceMgmt,null,true);
		}
		
		/// <summary>
		/// Initiates a device management request, such as reboot.
		/// </summary>
		/// <param name="action">String value of action "device/reboot","device/factoryReset","firmware/download" and "firmware/update"</param>
		/// <param name="parameters">Array of DeviceMgmtparameter object</param>
		/// <param name="devices">Array of DeviceListElement object</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic InitiateDeviceManagementRequest(string action,DeviceMgmtparameter [] parameters,DeviceListElement [] devices){
			return RestHandler("POST",DeviceMgmt,new { action = action,parameters =parameters ,devices=devices},true);
		}
		
		/// <summary>
		/// Clears the status of a device management request
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic DeleteDeviceManagementRequest(string requestId){
			return RestHandler("DELETE",string.Format(DeviceMgmtInd,requestId),null,false);
		}
		
		/// <summary>
		/// Gets details of a device management request.
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementRequest(string requestId){
			return RestHandler("GET",string.Format(DeviceMgmtInd,requestId),null,true);
		}
		
		/// <summary>
		/// Gets a list of device management request device statuses for a particular request.
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementRequestStatus(string requestId){
			return RestHandler("GET",string.Format(DeviceMgmtStatus,requestId),null,true);
		}
		
		/// <summary>
		/// Get an individual device management request device status
		/// </summary>
		/// <param name="requestId">String value of device management request Id</param>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceManagementRequestStatus(string requestId,string type,string deviceId){
			return RestHandler("GET",string.Format(DeviceMgmtStatusInd,requestId,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Retrieve the amount of data used
		/// </summary>
		/// <param name="start">String value of start date of format YYYY ,YYYY-MM,YYYY-MM-DD </param>
		/// <param name="end">String value of end date of format YYYY ,YYYY-MM,YYYY-MM-DD </param>
		/// <param name="detail">bool value states whether a daily breakdown will be included in the result set</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDataUsage(string start, string end,bool detail){
			return RestHandler("GET",Usage,"?start="+start+"&end="+end+"&detail="+detail,true);
		}
		
		/// <summary>
		/// Retrieve the organization-specific status of each of the services offered by Watson IoT Platform.
		/// </summary>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetServiceStatus(){
			return RestHandler("GET",ServiceStatus,null,true);
		}
		
		/// <summary>
		/// Get last event for a specific event id for a specific device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetLastEvents(string type, string deviceId){
			return RestHandler("GET",string.Format(LastEventCache,type,deviceId),null,true);
		}
		
		/// <summary>
		/// Get all last events for a specific device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <param name="eventType">String value of event name</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetLastEventsByEventType(string type, string deviceId,string eventType){
			return RestHandler("GET",string.Format(LastEventCacheInd,type,deviceId,eventType),null,true);
		}
		
		/// <summary>
		/// Retrieve current meteorological observations for the location associated with your device
		/// </summary>
		/// <param name="type">String value of device type id</param>
		/// <param name="deviceId">String value of device id</param>
		/// <returns>Dynamic Object of response</returns>
		public dynamic GetDeviceLocationWeather (string type, string deviceId){
			return RestHandler("GET",string.Format(Weather,type,deviceId),null,true);
		}
		
		//TODO :All Extention functionalities

	}
}
