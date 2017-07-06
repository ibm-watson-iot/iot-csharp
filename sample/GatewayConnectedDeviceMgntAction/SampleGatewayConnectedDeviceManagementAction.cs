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
using System.Threading;
using System.Collections.Generic;
using IBMWIoTP;

namespace GatewayConnectedDeviceMgntAction
{
	class SampleGatewayConnectedDeviceManagementAction
	{
		public static void Main(string[] args)
		{
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
	
			bool isSync = false;
			List<ConectedDevice> devices = new List<ConectedDevice>();
			
			Console.WriteLine("Gateway connectd device management sample");
			Console.WriteLine("Check Out Following Device manage action on connected device ");
        	Console.WriteLine("1.Reboot");
        	Console.WriteLine("2.Reset");
        	Console.WriteLine("3.Firmware download and update");
        	Console.WriteLine("4.Any key to exit");
        	Console.Write("Please enter your choice :");
        	int val = int.Parse(Console.ReadLine());
	        	
			DeviceInfo simpleDeviceInfo = new DeviceInfo();
		    simpleDeviceInfo.description = "My device";
		    simpleDeviceInfo.deviceClass = "My device class";
		    simpleDeviceInfo.manufacturer = "My device manufacturer";
		    simpleDeviceInfo.fwVersion = "Device Firmware Version";
		    simpleDeviceInfo.hwVersion = "Device HW Version";
		    simpleDeviceInfo.model = "My device model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My device location";
		    
		    GatewayManagement	gwMgmtClient = new GatewayManagement("GatewayCreds.txt",isSync);
			gwMgmtClient.deviceInfo = simpleDeviceInfo;
			gwMgmtClient.mgmtCallback += ( string reqestId, string responseCode) => {
			Console.WriteLine("req Id:" + reqestId +"	responseCode:"+ responseCode);
		};
			gwMgmtClient.actionCallback +=  (string reqestId,string action) => {
				Console.WriteLine("req Id:" + reqestId +"	Action:"+ action +" called");
				if(action == "reboot"){
					gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_ACCEPTED,"");
	
					Thread.Sleep(2000);
					gwMgmtClient.disconnect();
					
					Console.WriteLine("disconnected");
					Thread.Sleep(5000);
					
					Console.WriteLine("Re connected");	
					gwMgmtClient.connect();
					
					gwMgmtClient.managedGateway(4000,true,true);
				}
				if(action == "reset"){
					gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_FUNCTION_NOT_SUPPORTED,"");
				}
		
			};
			gwMgmtClient.fwCallback += (string action , DeviceFirmware fw) => {
				if(action == "download"){
					gwMgmtClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADING);
					Console.WriteLine("Start downloading new Firmware from "+fw.uri);
					Thread.Sleep(2000);
					Console.WriteLine("completed Download");
					gwMgmtClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);
				
				}
				if(action == "update"){
					gwMgmtClient.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
					Console.WriteLine("Start Updating new Firmware ");
					Thread.Sleep(2000);
					Console.WriteLine("Updated new Firmware ");
					gwMgmtClient.setUpdateState(DeviceManagement.UPDATESTATE_SUCCESS);
				}
			};
			gwMgmtClient.connect();
			Console.WriteLine("Manage");
			gwMgmtClient.managedGateway(4000,true,true);
			//Connected device
			ConectedDevice device1 = new ConectedDevice("testgwdev","1234",gwMgmtClient);
			device1.isActionEnabled=true;
			device1.isFramewareEnabled=true;
			device1.canReboot=true;
			device1.canReset=false;
			devices.Add(device1);
			gwMgmtClient.managedDevice(device1.deviceType,device1.deviceId,4000,device1.isActionEnabled,device1.isFramewareEnabled);
			gwMgmtClient.connectedDeviceActionCallback+= ( string devicetype, string deviceid, string reqestId,string action) => {
				Console.WriteLine(devicetype + ": "+deviceid+":"+reqestId+":"+action);
				var itm =  devices.Find( x => x.deviceId == deviceid );
				if(itm is ConectedDevice){
					if(action == GatewayManagement.ACTION_REBOOT){
						if(itm.canReboot){
							gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_ACCEPTED,"",itm.deviceType,itm.deviceId);
							Thread.Sleep(2000);
							gwMgmtClient.managedDevice(itm.deviceType,itm.deviceId,4000,itm.isActionEnabled,itm.isFramewareEnabled);
						}else{
							gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_FUNCTION_NOT_SUPPORTED,"",itm.deviceType,itm.deviceId);
						}
	
					}
					if(action == GatewayManagement.ACTION_RESET){
						if(itm.canReset){
							gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_ACCEPTED,"",itm.deviceType,itm.deviceId);
						}else{
							gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_FUNCTION_NOT_SUPPORTED,"",itm.deviceType,itm.deviceId);
						}
					}
				}
	
	
			};
			
			
			
			gwMgmtClient.connectedDevicefwActionCallback+= (string devicetype, string deviceid, string action, DeviceActionReq req) =>{ 
				ConectedDevice itm =  devices.Find( x => x.deviceId == deviceid );
				switch (action) {
					case GatewayManagement.FIRMWARE_ACTION_INFO :
						itm.infoHandler(req);
						break;
					case GatewayManagement.FIRMWARE_ACTION_DOWNLOAD :
						itm.downloadHandler(req);
						break;
					case GatewayManagement.FIRMWARE_ACTION_UPDATE :
						itm.updatedHandler(req);
						break;
					default:
						
						break;
				}
			};
		
			IBMWIoTP.ApplicationClient appClient = new ApplicationClient("AppCreds.txt");
			IBMWIoTP.ApiClient client = appClient.GetAPIClient();
			client.Timeout = 5000;
			IBMWIoTP.DeviceMgmtparameter [] param = new IBMWIoTP.DeviceMgmtparameter[1];
			IBMWIoTP.DeviceMgmtparameter p = new IBMWIoTP.DeviceMgmtparameter();
			p.name="uri";
			p.value = "https://raw.githubusercontent.com/ibm-watson-iot/iot-python/master/CHANGES.txt";
			param[0] = p;
			IBMWIoTP.DeviceListElement [] deviceList = new IBMWIoTP.DeviceListElement[1];
			IBMWIoTP.DeviceListElement ele = new IBMWIoTP.DeviceListElement();
			ele.typeId =  "testgw";
			ele.deviceId= "1212";
			deviceList[0] = ele;
	
	        	switch (val) {
	        		case 1 : client.InitiateDeviceManagementRequest("device/reboot",param,deviceList);
	        			break;
	        		case 2 : client.InitiateDeviceManagementRequest("device/factoryReset",param,deviceList);
	        			break;
	        		case 3 : client.InitiateDeviceManagementRequest("firmware/download",param,deviceList);
						 Thread.Sleep(15000);
						 client.InitiateDeviceManagementRequest("firmware/update",new IBMWIoTP.DeviceMgmtparameter[0],deviceList);
	        			break;
	        		default:
	        			gwMgmtClient.disconnect();
	        			break;
	        	}
	
		}
	}
}
