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
	public class ConectedDevice{
		private IBMWIoTP.DeviceFirmware firmware = new DeviceFirmware();
		public string deviceType {get;set;}
		public string deviceId {get;set;}
		public bool canReboot {get;set;}
		public bool canReset {get;set;}
		public bool isActionEnabled {get;set;}
		public bool isFramewareEnabled {get;set;}
		private GatewayManagement parentGateway;
		
		public ConectedDevice(string deviceType,string deviceId,GatewayManagement gateway){
			this.deviceType = deviceType;
			this.deviceId = deviceId;
			this.parentGateway = gateway;
		}
		public void infoHandler(DeviceActionReq req){
			var fields = req.d.fields;
			for (int i = 0; i < fields.Length; i++) {
				if(fields[i].field == "mgmt.firmware" ){
					var update =  fields[i].value as IBMWIoTP.DeviceFirmware;
					this.firmware.uri =update.uri;
					this.firmware.verifier = update.verifier;
					this.firmware.name = update.name;
					this.firmware.state = update.state;
					this.firmware.updatedDateTime = update.updatedDateTime;
					this.firmware.updateStatus = update.updateStatus;
					this.firmware.version = update.version;
					break;
				}
			}
			if(this.firmware != null)
			{
				parentGateway.sendResponse(req.reqId,204,"",this.deviceType,this.deviceId);
			}
		}
		
		public void downloadHandler (DeviceActionReq req){
			int rc = DeviceManagement.RESPONSECODE_ACCEPTED;
			string msg ="";
			if(this.firmware.state != DeviceManagement.UPDATESTATE_IDLE){
				rc = DeviceManagement.RESPONSECODE_BAD_REQUEST;
				msg = "Cannot download as the device is not in idle state";
				parentGateway.sendResponse(req.reqId,rc,msg,this.deviceType,this.deviceId);
				return ;
			}
			parentGateway.sendResponse(req.reqId,rc,msg,this.deviceType,this.deviceId);
			this.setState(DeviceManagement.UPDATESTATE_DOWNLOADING);
			Console.WriteLine("Start downloading new Firmware form "+firmware.uri);
			Thread.Sleep(2000);
			//call your device to take all the action to download 
			Console.WriteLine("completed Download");
			this.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);
		
		}
		public void setState(int state){
			if(this.firmware ==null)
				return;
			object[] field = new object[1];
			field[0]= new {
					        	field="mgmt.firmware",
					        	value= new {
					        		state=state
					        	}
			           };
			var notify = new {
				d = new {
					fields=field
					}
				};
			this.firmware.state = state;
			string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(notify);
			parentGateway.notify(this.deviceType,this.deviceId,json);
		}
		public void updatedHandler (DeviceActionReq req){
			int rc = DeviceManagement.RESPONSECODE_ACCEPTED;
			string msg ="";

			if(this.firmware.state  != DeviceManagement.UPDATESTATE_DOWNLOADED){
				rc = DeviceManagement.RESPONSECODE_BAD_REQUEST;
				msg = "Firmware is still not successfully downloaded.";		
				parentGateway.sendResponse(req.reqId,rc,msg,this.deviceType,this.deviceId);
				return ;
			}
			parentGateway.sendResponse(req.reqId,rc,msg,this.deviceType,this.deviceId);
			this.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
			Console.WriteLine("Start Updateting new Firmware ");
			Thread.Sleep(2000);
			Console.WriteLine("Updated new Firmware ");
			this.setUpdateState(DeviceManagement.UPDATESTATE_SUCCESS);
		
		}
		public void setUpdateState(int updateState){
			if(this.firmware ==null)
				return;
			object[] field = new object[1];
			field[0]= new {
					        	field="mgmt.firmware",
					        	value= new {
					        		state=DeviceManagement.UPDATESTATE_IDLE,
					        		updateStatus = updateState
					        	}
			           };
			var notify = new {
				d = new {
					fields=field
					}
				};
			
			this.firmware.state = DeviceManagement.UPDATESTATE_IDLE;
			this.firmware.updateStatus = updateState;
			string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(notify);
			parentGateway.notify(this.deviceType,this.deviceId,json);
		}
		
	}
	class Program
	{
		public static void Main(string[] args)
		{

			bool isSync = false;
			List<ConectedDevice> devices = new List<ConectedDevice>();
			
			Console.WriteLine("Gateway connectd device management sample");
			Console.WriteLine("Check Out Following Device manage action on connected device ");
        	Console.WriteLine("1.Reboot");
        	Console.WriteLine("2.Reset");
        	Console.WriteLine("3.Firmware download and update");
        	Console.WriteLine("4.Any key to exit");
        	Console.Write("Please enter your choise :");
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
					Console.WriteLine("Start downloading new Firmware form "+fw.uri);
					Thread.Sleep(2000);
					Console.WriteLine("completed Download");
					gwMgmtClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);
				
				}
				if(action == "update"){
					gwMgmtClient.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
					Console.WriteLine("Start Updateting new Firmware ");
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