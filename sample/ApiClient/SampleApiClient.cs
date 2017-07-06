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
using IBMWIoTP;
using System.Web.Script.Serialization;

namespace ApiClient
{
	class SampleApiClient
	{
		public static void Main(string[] args)
		{
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
			
			string apiKey ="";
			string authToken ="";
			string managedDeviceType = "demotest";
			string managedDeviceId ="1234";
			if(string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(authToken)){
				Console.Write("please Enter API Key : ");
				apiKey = Console.ReadLine();
				Console.WriteLine();
				Console.Write("Please Enter Auth Token : ");
				authToken = Console.ReadLine();
			}
			IBMWIoTP.ApiClient client = new IBMWIoTP.ApiClient(apiKey,authToken);
			client.Timeout = 5000;
			try {
			
				//Organization
				
				Console.WriteLine("get org info");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetOrganizationDetail()));
				
				//Bulk Operations 
				
				Console.WriteLine("get all device list");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetAllDevices()));
				
				IBMWIoTP.RegisterDevicesInfo [] bulk = new IBMWIoTP.RegisterDevicesInfo[1];
				var info = new IBMWIoTP.RegisterDevicesInfo();
				info.deviceId="123qwe";
				info.typeId = managedDeviceType;
				info.authToken ="1qaz2wsx3edc4rfv";
				info.deviceInfo = new IBMWIoTP.DeviceInfo();
				info.location = new IBMWIoTP.LocationInfo();
				info.metadata = new {};
				bulk[0] = info;
				
				Console.WriteLine("get Bulk reg ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.RegisterMultipleDevices(bulk)));
				
				IBMWIoTP.DeviceListElement [] removeBulk = new IBMWIoTP.DeviceListElement[1];
				var del = new IBMWIoTP.DeviceListElement();
				del.deviceId ="123qwe";
				del.typeId=managedDeviceType;
				
				removeBulk[0]=del;
				Console.WriteLine("get Bulk remove ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.DeleteMultipleDevices(removeBulk)));
				
				//Device Types 
				
				Console.WriteLine("GetAllDeviceTypes ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetAllDeviceTypes()));

				DeviceTypeInfo devty = new DeviceTypeInfo();
				devty.classId="Gateway";
				devty.deviceInfo = new DeviceInfo();
				devty.id = "gatewaypi";
				devty.metadata= new {};
				Console.WriteLine("RegisterDeviceType");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.RegisterDeviceType(devty)));
				
				Console.WriteLine("UpdateDeviceType ");
				var u = new IBMWIoTP.DeviceTypeInfoUpdate();
				u.description="test";
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.UpdateDeviceType("gatewaypi",u)));
				
				Console.WriteLine("GetDeviceType ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceType("gatewaypi")));
	
				//Devices
				
				Console.WriteLine("ListDevices");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.ListDevices(managedDeviceType)));
				
				string newDeviceId= DateTime.Now.ToString("yyyyMMddHHmmssffff");
				var newdevice = new RegisterSingleDevicesInfo();
				newdevice.deviceId = newDeviceId;
				newdevice.authToken = "testtest";
				newdevice.deviceInfo = new IBMWIoTP.DeviceInfo();
				newdevice.location = new IBMWIoTP.LocationInfo();
				newdevice.metadata = new {};				
				Console.WriteLine("RegisterDevice");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.RegisterDevice("gatewaypi",newdevice)));
	
				var update  = new IBMWIoTP.UpdateDevicesInfo();
				update.deviceInfo =new IBMWIoTP.DeviceInfo();
				Console.WriteLine("UpdateDeviceInfo");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.UpdateDeviceInfo("gatewaypi",newDeviceId,update)));

				Console.WriteLine("GetDeviceInfo");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceInfo("gatewaypi",newDeviceId)));

				Console.WriteLine("GetGatewayConnectedDevice");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetGatewayConnectedDevice("gatewaypi",newDeviceId)));
				
				Console.WriteLine("GetDeviceLocationInfo");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceLocationInfo("gatewaypi",newDeviceId)));
				
				Console.WriteLine("UpdateDeviceLocationInfo");
				var loc = new IBMWIoTP.LocationInfo();
				loc.accuracy=1;
				loc.measuredDateTime = DateTime.Now.ToString("o");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.UpdateDeviceLocationInfo("gatewaypi",newDeviceId,loc)));
				
				Console.WriteLine("GetDeviceManagementInfo");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceManagementInfo(managedDeviceType,managedDeviceId)));
				
				Console.WriteLine("UnregisterDevice");
				client.UnregisterDevice("gatewaypi",newDeviceId);
				
								
				Console.WriteLine("DeleteDeviceType ");
				client.DeleteDeviceType("gatewaypi");
				
				
				Console.WriteLine("AddDeviceDiagLogs");
				var log =new IBMWIoTP.LogInfo();
				log.message="test";
				log.severity =1;
				
				client.AddDeviceDiagLogs(managedDeviceType,managedDeviceId,log);
				
				Console.WriteLine("GetAllDiagnosticLogs");
				var logResult= client.GetAllDiagnosticLogs(managedDeviceType,managedDeviceId);
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(logResult));
				

				
				Console.WriteLine("GetDiagnosticLog");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDiagnosticLog(managedDeviceType,managedDeviceId,logResult[0]["id"])));
				
				Console.WriteLine("DeleteDiagnosticLog");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.DeleteDiagnosticLog(managedDeviceType,managedDeviceId,logResult[0]["id"])));
				
				Console.WriteLine("ClearAllDiagnosticLogs");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.ClearAllDiagnosticLogs(managedDeviceType,managedDeviceId)));
				
				
				
				Console.WriteLine("AddErrorCode");
				var err =  new IBMWIoTP.ErrorCodeInfo();
				err.errorCode = 0;
				err.timestamp =  "";
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.AddErrorCode(managedDeviceType,managedDeviceId,err)));
				
				Console.WriteLine("GetDeviceErrorCodes");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceErrorCodes(managedDeviceType,managedDeviceId)));
				
				Console.WriteLine("ClearDeviceErrorCodes");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.ClearDeviceErrorCodes(managedDeviceType,managedDeviceId)));
				
				Console.WriteLine("Problem ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceConnectionLogs(managedDeviceType,managedDeviceId)));
				
				Console.WriteLine("data usage ");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDataUsage("2016","2016",false)));
				
				
				Console.WriteLine("Service Status");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetServiceStatus()));	
				
				Console.WriteLine("lastEvents");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetLastEvents(managedDeviceType,managedDeviceId)));
				
				Console.WriteLine("lastEvents of type");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetLastEventsByEventType(managedDeviceType,managedDeviceId,"test")));
	
			
				Console.WriteLine("GetAllDeviceManagementRequests");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetAllDeviceManagementRequests()));
	
				Console.WriteLine("InitiateDeviceManagementRequest");
				IBMWIoTP.DeviceMgmtparameter [] param = new IBMWIoTP.DeviceMgmtparameter[1];
				IBMWIoTP.DeviceMgmtparameter p = new IBMWIoTP.DeviceMgmtparameter();
				p.name="rebootAfter";
				p.value = "100";
				param[0] = p;
				IBMWIoTP.DeviceListElement [] deviceList = new IBMWIoTP.DeviceListElement[1];
				IBMWIoTP.DeviceListElement ele = new IBMWIoTP.DeviceListElement();
				ele.typeId = managedDeviceType;
				ele.deviceId= managedDeviceId;
				deviceList[0] = ele;
				var response =client.InitiateDeviceManagementRequest("device/reboot",param,deviceList);//new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(jsonResponse);
				Console.WriteLine(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response));
				string id = response["reqId"];
				
				Console.WriteLine("GetDeviceManagementRequest");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceManagementRequest(id)));
	
				Console.WriteLine("GetDeviceManagementRequestStatus");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceManagementRequestStatus(id)));
	
				Console.WriteLine("GetDeviceManagementRequestStatus");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceManagementRequestStatus(id,ele.typeId,ele.deviceId)));
	
				Console.WriteLine("DeleteDeviceManagementRequest");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.DeleteDeviceManagementRequest(id)));
	
				Console.WriteLine("weather");
				Console.WriteLine( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(client.GetDeviceLocationWeather(managedDeviceType,managedDeviceId)));
	
				
			
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			Console.Write("Press any key to exit . . . ");
			Console.ReadKey();
		}
		
	}
}