/*
 *  Copyright (c) 2016-2017 IBM Corporation and other Contributors.
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
using System.Text;
using System.Dynamic;
using System.Threading;
using System.Collections.Generic;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using log4net;

namespace IBMWIoTP
{
	/// <summary>
    ///    A device class that connects the device as managed device to IBM Watson IoT Platform Connect and enables the device to perform one or more Device Management operations. Also the DeviceManagement instance can be used to do normal device operations like publishing device events and listening for commands from application.
    /// </summary>
	public class DeviceManagement : DeviceClient
	{
		// Publish MQTT topics
		const string MANAGE_TOPIC = "iotdevice-1/mgmt/manage";
		const string UNMANAGE_TOPIC = "iotdevice-1/mgmt/unmanage";
		const string UPDATE_LOCATION_TOPIC = "iotdevice-1/device/update/location";
		const string ADD_ERROR_CODE_TOPIC = "iotdevice-1/add/diag/errorCodes";
		const string CLEAR_ERROR_CODES_TOPIC = "iotdevice-1/clear/diag/errorCodes";
		const string NOTIFY_TOPIC = "iotdevice-1/notify";
		const string RESPONSE_TOPIC = "iotdevice-1/response";
		const string ADD_LOG_TOPIC = "iotdevice-1/add/diag/log";
		const string CLEAR_LOG_TOPIC = "iotdevice-1/clear/diag/log";
		
		// Subscribe MQTT topics
		const string DM_RESPONSE_TOPIC = "iotdm-1/response";
		const string DM_OBSERVE_TOPIC = "iotdm-1/observe";
		const string DM_REBOOT_TOPIC = "iotdm-1/mgmt/initiate/device/reboot";
		const string DM_FACTORY_RESET ="iotdm-1/mgmt/initiate/device/factory_reset";
		const string DM_UPDATE_TOPIC = "iotdm-1/device/update";
		const string DM_CANCEL_OBSERVE_TOPIC = "iotdm-1/cancel";
		const string DM_FIRMWARE_DOWNLOAD_TOPIC = "iotdm-1/mgmt/initiate/firmware/download";
		const string DM_FIRMWARE_UPDATE_TOPIC = "iotdm-1/mgmt/initiate/firmware/update";
		
		//ResponseCode 
		public const int RESPONSECODE_FUNCTION_NOT_SUPPORTED = 501;
		public const int RESPONSECODE_ACCEPTED = 202;
		public const int RESPONSECODE_INTERNAL_ERROR = 500;
		public const int RESPONSECODE_BAD_REQUEST = 400;
		
		public const int UPDATESTATE_IDLE = 0;
		public const int UPDATESTATE_DOWNLOADING = 1;
		public const int UPDATESTATE_DOWNLOADED = 2;
		public const int UPDATESTATE_SUCCESS = 0;
		public const int UPDATESTATE_IN_PROGRESS = 1;
		public const int UPDATESTATE_OUT_OF_MEMORY = 2;
		public const int UPDATESTATE_CONNECTION_LOST = 3;
		public const int UPDATESTATE_VERIFICATION_FAILED = 4;
		public const int UPDATESTATE_UNSUPPORTED_IMAGE = 5;
		public const int UPDATESTATE_INVALID_URI = 6;
		
		public const string ACTION_RESET = "reset";
		public const string ACTION_REBOOT = "reboot";

		
		public const string FIRMWARE_ACTION_INFO = "info";
		public const string FIRMWARE_ACTION_DOWNLOAD = "download";
		public const string FIRMWARE_ACTION_UPDATE = "update";
		
				
		public DeviceInfo deviceInfo = new DeviceInfo();
		public LocationInfo locationInfo = new LocationInfo();
		List<DMRequest> collection = new List<DMRequest>();
		ManualResetEvent oSignalEvent = new ManualResetEvent(false);
		bool isSync = false;
		ILog log = log4net.LogManager.GetLogger(typeof(DeviceManagement));
		DeviceFirmware fw = null;
		/// <summary>
		///  Constructor of device management, helps to connect as a managed device to Watson IoT 
 		/// </summary>
		/// <param name="orgId">
		///  object of String which denotes your organization Id</param>
		/// <param name="deviceType">object of String which denotes your device type</param>
		/// <param name="deviceID">object of String which denotes device Id</param>
		/// <param name="authmethod">object of String which denotes your authentication method</param>
		/// <param name="authtoken">object of String which denotes your authentication token</param>
		public DeviceManagement(string orgId, string deviceType, string deviceID, string authmethod, string authtoken):
			base(orgId,deviceType,deviceID,authmethod,authtoken)
		{
			
		}
		/// <summary>
		///  Constructor of device management, helps to connect as a managed device to Watson IoT 
		/// </summary>
		///  object of String which denotes your organization Id</param>
		/// <param name="orgId">
		///  object of String which denotes your organization Id</param>
		/// <param name="deviceType">object of String which denotes your device type</param>
		/// <param name="deviceID">object of String which denotes device Id</param>
		/// <param name="authmethod">object of String which denotes your authentication method</param>
		/// <param name="authtoken">object of String which denotes your authentication token</param>
		/// <param name="isSync"> Boolean value to represent the device management request and response in synchronize mode or Async mode</param>
		public DeviceManagement(string orgId, string deviceType, string deviceID, string authmethod, string authtoken,bool isSync):
			base(orgId,deviceType,deviceID,authmethod,authtoken)
		{
			this.isSync = isSync;
		}
		/// <summary>
		///  Constructor of device management, helps to connect as a managed device to Watson IoT  
		/// </summary>
		/// <param name="filePath">object of String which denotes file path that contains device credentials in specified format</param>
		public DeviceManagement(string filePath):
			base(filePath)
		{
		}
		/// <summary>
		///  Constructor of device management, helps to connect as a managed device to Watson IoT  
		/// </summary>
		/// <param name="filePath">object of String which denotes file path that contains device credentials in specified format</param>
		/// <param name="isSync">Boolean value to represent the device management request and response in synchronize mode or Async mode</param>
		public DeviceManagement(string filePath,bool isSync):
			base(filePath)
		{
			this.isSync = isSync;
		}
		/// <summary>
		/// To connect the device to the Watson IoT Platform
		/// </summary>
		public override void connect()
		{
			base.connect();
			subscribeTOManagedTopics();
		}
		
		
		private void subscribeTOManagedTopics(){
			if(mqttClient.IsConnected)
			{
				
				string[] topics = { "iotdm-1/#"};
				byte[] qos = {1};
				mqttClient.Subscribe(topics, qos);
				mqttClient.MqttMsgPublishReceived -= subscriptionHandler;
				mqttClient.MqttMsgPublishReceived += subscriptionHandler;
				log.Info("Subscribes to topic [" +topics[0] + "]");
			}
		}

	
		
		public void subscriptionHandler(object sender, MqttMsgPublishEventArgs e)
        {
			try{
				
				string result = System.Text.Encoding.UTF8.GetString(e.Message);
	            var serializer  = new System.Web.Script.Serialization.JavaScriptSerializer();
				DeviceActionReq fwData;
				int rc;
				string msg;
				
				switch (e.Topic) {
					case DM_RESPONSE_TOPIC :
				            var response = serializer.Deserialize<DMResponse>(result);
				            var itm =  collection.Find( x => x.reqID == response.reqId );
				            if( itm is DMRequest)
				            {
				            	log.Info("["+response.rc+"]  "+itm.topic+" of ReqId  "+ itm.reqID);
				            	if(this.mgmtCallback !=null)
				            		this.mgmtCallback(itm.reqID,response.rc);
				            	collection.Remove(itm);
				            }
				             if(this.isSync){
					            oSignalEvent.Set();
				            	oSignalEvent.Dispose();
				            	oSignalEvent = new ManualResetEvent(false);
				            }
						break;
					
					case DM_REBOOT_TOPIC :
					 	var res = serializer.Deserialize<DMResponse>(result);
						log.Info("Device Rebot action called with ReqId : " +res.reqId );
						if(this.actionCallback != null)
							this.actionCallback(res.reqId , "reboot");
						break;

					case DM_FACTORY_RESET :
					 	var resetResponse = serializer.Deserialize<DMResponse>(result);
						log.Info("Device Factory rest action called with ReqId : " +resetResponse.reqId );
						if(this.actionCallback != null)
							this.actionCallback(resetResponse.reqId , "reset");
						break;
					case DM_UPDATE_TOPIC :
						 fwData = serializer.Deserialize<DeviceActionReq>(result);
						var fields = fwData.d.fields;
						for (int i = 0; i < fields.Length; i++) {
							if(fields[i].field == "mgmt.firmware" ){
								this.fw = fields[i].value;
								break;
							}
						}
						if(this.fw != null)
						{
							sendResponse(fwData.reqId,204,"");
						}

						break;
						
					case DM_OBSERVE_TOPIC :
						fwData = serializer.Deserialize<DeviceActionReq>(result);
						sendResponse(fwData.reqId,200,"");
						break;
						
					case DM_FIRMWARE_DOWNLOAD_TOPIC :
						fwData = serializer.Deserialize<DeviceActionReq>(result);
						 rc = RESPONSECODE_ACCEPTED;
						 msg ="";
						if(this.fw.state != UPDATESTATE_IDLE){
							rc = RESPONSECODE_BAD_REQUEST;
							msg = "Cannot download as the device is not in idle state";		
							sendResponse(fwData.reqId,rc,msg);
							break;
						}
						if(this.fwCallback != null)
						{
							this.fwCallback("download",this.fw);
						}
						
						break;
					case DM_FIRMWARE_UPDATE_TOPIC :
						fwData = serializer.Deserialize<DeviceActionReq>(result);
						 rc = RESPONSECODE_ACCEPTED;
						 msg ="";
						if(this.fw.state != UPDATESTATE_DOWNLOADED){
							rc = RESPONSECODE_BAD_REQUEST;
							msg = "Firmware is still not successfully downloaded.";		
							sendResponse(fwData.reqId,rc,msg);
							break;
						}
						if(this.fwCallback != null)
						{
							this.fwCallback("update",this.fw);
						}
						
						break;
					
					case DM_CANCEL_OBSERVE_TOPIC :
						fwData = serializer.Deserialize<DeviceActionReq>(result);
						sendResponse(fwData.reqId,200,"");
						break;

					default:
						
						break;
				}
	           
			}
        	catch(Exception ex)
        	{
        		log.Error("Execption has occer in subscriptionHandler ",ex);
        	}

        }
		
		/// <summary>
		///  To know the current connection status of the device
		/// </summary>
		/// <returns> bool value of status of connection </returns>
		public bool connectionStatus()
		{
			return mqttClient.IsConnected;
		}
		
		private void publishDM (string topic , object message,string reqId)
		{
			var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(message);
			log.Info("Device Management Request For Topic " + topic +" with payload " +json);
			collection.Add(new DMRequest(reqId,topic,json));
			mqttClient.Publish(topic, Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE , false);
			if(this.isSync)
				oSignalEvent.WaitOne();
		}
		/// <summary>
		///  To register the device as an managed device to Watson IoT Platform
		/// </summary>
		/// <param name="lifeTime">Time period of the device to remain as managed device</param>
		/// <param name="supportDeviceActions">bool value to represent the support for device actions for the device</param>
		/// <param name="supportFirmwareActions">bool value to represent the support for firmware action for the device</param>
		/// <returns>unique id of the current request</returns>
		public string manage(int lifeTime,bool supportDeviceActions,bool supportFirmwareActions)
		{
			try{
				string uid =Guid.NewGuid().ToString();
				var payload = new {
					lifetime =  lifeTime,
					supports = new {
						deviceActions =  supportDeviceActions,
						firmwareActions = supportFirmwareActions
					},
					deviceInfo = this.deviceInfo ,
					metadata = new {}
				};
				var message = new { reqId = uid , d = payload };
				publishDM(MANAGE_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		
		}
		/// <summary>
		///  To register the device as an managed device to Watson IoT Platform
		/// </summary>
		/// <param name="lifeTime">Time period of the device to remain as managed device</param>
		/// <param name="supportDeviceActions">bool value to represent the support for device actions for the device</param>
		/// <param name="supportFirmwareActions">bool value to represent the support for firmware action for the device</param>
		/// <param name="metaData"> Object that represent the meta information of the device </param>
		/// <returns>unique id of the current request</returns>
		public string manage(int lifeTime,bool supportDeviceActions,bool supportFirmwareActions, Object metaData)
		{
			try{
				string uid =Guid.NewGuid().ToString();
				var payload = new {
					lifetime =  lifeTime,
					supports = new {
						deviceActions =  supportDeviceActions,
						firmwareActions = supportFirmwareActions
					},
					deviceInfo = this.deviceInfo ,
					metadata = metaData
				};
				var message = new { reqId =uid , d = payload };
				publishDM(MANAGE_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
			
		}
		/// <summary>
		///  To register the device as an Unmanaged device to Watson Iot Platform
		/// </summary>
		/// <returns>unique id of the current request</returns>
		public string unmanage()
		{
			try{
				string uid =Guid.NewGuid().ToString();
				var message = new { reqId =uid };
				publishDM(UNMANAGE_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		}
		/// <summary>
		///  To Add error code to the Watson IoT platform for the device 
		/// </summary>
		/// <param name="errorCode">integer value of device error code </param>
		/// <returns>unique id of the current request</returns>
		public string addErrorCode(int errorCode)
		{
			try
			{
				string uid =Guid.NewGuid().ToString();
				var payload = new{
					errorCode = errorCode
				};
				var message = new { reqId =uid ,d = payload};
				publishDM(ADD_ERROR_CODE_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		}
		
		/// <summary>
		///  To Clear the error code added by the device in the Watson IoT Platform
		/// </summary>
		/// <returns>unique id of the current request</returns>
		public string clearErrorCode()
		{
			try
			{
				string uid =Guid.NewGuid().ToString();
				var message = new { reqId =uid};
				publishDM(CLEAR_ERROR_CODES_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		}
		/// <summary>
		///  To Add log of the device status to Watson IoT Platform 
		/// </summary>
		/// <param name="msg">String value of the log message </param>
		/// <param name="dataAsString"> String value of base64-encoded binary diagnostic data </param>
		/// <param name="severityValue">intiger value of severity (0: informational, 1: warning, 2: error)</param>
		/// <returns>unique id of the current request</returns>
		public string addLog(string msg, string dataAsString,int severityValue)
		{
			try
			{
				string uid =Guid.NewGuid().ToString();
				var payload = new{
					message = msg,
					timestamp = DateTime.UtcNow.ToString("o"),
					data = dataAsString,
					severity = severityValue
				};
				var message = new { reqId =uid ,d = payload};
				publishDM(ADD_LOG_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		}
		
		/// <summary>
		///  To clear the log present in Watson IoT platform for the current device
		/// </summary>
		/// <returns>unique id of the current request</returns>
		public string clearLog()
		{
			try
			{
				string uid =Guid.NewGuid().ToString();
				var message = new { reqId =uid};
				publishDM(CLEAR_LOG_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		}
		/// <summary>
		/// To Update the current location of the device to the Watson IoT Platform
		/// </summary>
		/// <param name="longitude">double value of longitude of the device </param>
		/// <param name="latitude">double value of latitude of the device</param>
		/// <param name="elevation">double value of elevation of the device</param>
		/// <param name="accuracy">double value of accuracy of the reading</param>
		/// <returns>unique id of the current request</returns>
		public string setLocation( double  longitude,double latitude,double elevation,double accuracy)
		{
			try
			{
				string uid =Guid.NewGuid().ToString();
				this.locationInfo.longitude = longitude;
				this.locationInfo.latitude = latitude;
				this.locationInfo.elevation = elevation;
				this.locationInfo.accuracy =accuracy;
				this.locationInfo.measuredDateTime = DateTime.Now.ToString("o");
				//this.locationInfo.updatedDateTime = this.locationInfo.measuredDateTime;
				var message = new { reqId =uid ,d = this.locationInfo };
				publishDM(UPDATE_LOCATION_TOPIC,message,uid);
				return uid;
			}
        	catch(Exception e)
        	{
        		log.Error("Execption has occer in manage ",e);
        		return "";
        	}
		}
		/// <summary>
		/// To send device Action respoce to the Watson IoT Platform
		/// </summary>
		/// <param name="reqId">String value of device action request id</param>
		/// <param name="rc">Int value of response code </param>
		/// <param name="msg">String value of response message</param>
		public void sendResponse ( string reqId, int rc ,string msg){
			var message = new { reqId =reqId , rc = Convert.ToString(rc) , message = msg};
			var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(message);
			log.Info("Sending DM response with payload " +json);
			mqttClient.Publish(RESPONSE_TOPIC, Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE , false);
			
		}
		/// <summary>
		/// To set the download state of the firmware download request
		/// 0 	Idle 	The device is currently not in the process of downloading firmware.
		/// 1 	Downloading 	The device is currently downloading firmware.
		/// 2 	Downloaded	The device successfully downloaded a firmware update and is ready to install it.
		/// </summary>
		/// <param name="state">Int value of the state</param>
		public void setState(int state){
			if(this.fw ==null)
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
			this.fw.state = state;
			string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(notify);
			mqttClient.Publish(NOTIFY_TOPIC, Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE , false);
		
		}
		
		/// <summary>
		/// To set the update state of the firmware update request
		/// 0 	Success 	The firmware was successfully updated.
		/// 1 	In Progress 	The firmware update was initiated but is not yet complete.
		/// 2 	Out of Memory 	An out-of-memory condition was detected during the operation.
		/// 3 	Connection Lost 	The connection was lost during the firmware download.
		/// 4 	Verification Failed 	The firmware did not pass verification.
		/// 5 	Unsupported Image 	The downloaded firmware image is not supported by the device.
		/// 6 	Invalid URI 	The device could not download the firmware from the provided URI.
		/// </summary>
		/// <param name="updateState">Int value of the state</param>
		public void setUpdateState(int updateState){
			if(this.fw ==null)
				return;
			object[] field = new object[1];
			field[0]= new {
					        	field="mgmt.firmware",
					        	value= new {
					        		state=UPDATESTATE_IDLE,
					        		updateStatus = updateState
					        	}
			           };
			var notify = new {
				d = new {
					fields=field
					}
				};
			
			this.fw.state = UPDATESTATE_IDLE;
			this.fw.updateStatus = updateState;
			string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(notify);
			mqttClient.Publish(NOTIFY_TOPIC, Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE , false);
		
		}
		
        public delegate void processMgmtResponse( string reqestId, string responseCode);

        public event processMgmtResponse mgmtCallback;
        
        public delegate void processDeviceAction( string reqestId,string action);

        public event processDeviceAction actionCallback;
        
        public delegate void processFirmwareAction(string action,DeviceFirmware firmware);

        public event processFirmwareAction fwCallback;
    
	
	}
}
