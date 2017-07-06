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
	public class ConectedDevice
	{
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

}