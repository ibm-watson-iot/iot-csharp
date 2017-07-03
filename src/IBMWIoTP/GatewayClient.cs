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
 * 	 Nikhil Chennakeshava Murthy and Hari hara prasad Viswanatha - Client certificate based authentication
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using log4net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IBMWIoTP
{
	/// <summary>
	/// A client, used by Gateway, that simplifies the Gateway interactions with IBM Watson IoT Platform. 
	/// 
	/// Gateways are a specialized class of devices in Watson IoT Platform which serve as access points to the 
	/// Watson IoT Platform for other devices. Gateway devices have additional permission when compared to 
	/// regular devices and can perform the following  functions:
	/// 
	/// 1)Register new devices to Watson IoT Platform
	/// 2)Send and receive its own sensor data like a directly connected device,
	/// 3)Send and receive data on behalf of the devices connected to it
	/// 4)Run a device management agent, so that it can be managed, also manage the devices connected to it
	/// 
	/// Refer to the "https://docs.internetofthings.ibmcloud.com/gateways/mqtt.html"documentation for more information about the 
	/// Gateway support in Watson IoT Platform.
	/// </summary>
	public class GatewayClient : AbstractClient
	{
		protected string gatewayDeviceType ;
		protected string gatewayDeviceID;
		private string authtoken ;
		private string GATEWAY_COMMAND_PATTERN = "iot-2/type/(.+)/id/(.+)/cmd/(.+)/fmt/(.+)";
		private string GATEWAY_NOTIFICATION_PATTERN = "iot-2/type/(.+)/id/(.+)/notify";
		ILog log = log4net.LogManager.GetLogger(typeof(GatewayClient));
		
		static string _orgId ="";
        static string _deviceType ="";
        static string _deviceID ="";
        static string _authmethod ="";
        static string _authtoken ="";
        
        static string _caCertificatePath = "";
        static string _caCertificatePassword = "";
        static string _clientCertificatePath = "";
        static string _clientCertificatePassword = "";
        
        /// <summary>
		/// Constructor of Gateway Client, helps to connect a gateway to The Watson IoT Platform
        /// </summary>
        /// <param name="orgId">object of String which denotes your organization Id</param>
        /// <param name="gatewayDeviceType">object of String which denotes your gateway type</param>
        /// <param name="gatewayDeviceID">object of String which denotes gateway Id</param>
        /// <param name="authMethod">object of String which denotes your authentication method</param>
        /// <param name="authToken">object of String which denotes your authentication token</param>
        
		public GatewayClient(string orgId, string gatewayDeviceType, string gatewayDeviceID, string authMethod, string authToken)
            : base(orgId, "g" + CLIENT_ID_DELIMITER + orgId + CLIENT_ID_DELIMITER + gatewayDeviceType + CLIENT_ID_DELIMITER + gatewayDeviceID, "use-token-auth", authToken)
		{
			this.gatewayDeviceID =gatewayDeviceID;
			this.gatewayDeviceType =gatewayDeviceType;
			this.authtoken = authToken;
		}
		
		
		public GatewayClient(string orgId, string gatewayDeviceType, string gatewayDeviceID, string authMethod, string authToken, string caCertificatePath, string caCertificatePassword, string clientCertificatePath, string clientCertificatePassword)
            : base(orgId, "g" + CLIENT_ID_DELIMITER + orgId + CLIENT_ID_DELIMITER + gatewayDeviceType + CLIENT_ID_DELIMITER + gatewayDeviceID, "use-token-auth", authToken, caCertificatePath,  caCertificatePassword,  clientCertificatePath,  clientCertificatePassword)
		{
			this.gatewayDeviceID =gatewayDeviceID;
			this.gatewayDeviceType =gatewayDeviceType;
			this.authtoken = authToken;
		}
		
		
		
		
		
		/// <summary>
		/// Constructor of Gateway Client, helps to connect a gateway to The Watson IoT Platform
		/// </summary>
		/// <param name="filePath">object of String which denotes file path that contains gateway credentials in specified format</param>
		public GatewayClient(string filePath)
            : base(parseFromFile( filePath), "g" + CLIENT_ID_DELIMITER + _orgId + CLIENT_ID_DELIMITER + _deviceType + CLIENT_ID_DELIMITER + _deviceID, "use-token-auth", _authtoken, _caCertificatePath, _caCertificatePassword, _clientCertificatePath, _clientCertificatePassword)
		{
			this.gatewayDeviceID =_deviceID;
			this.gatewayDeviceType =_deviceType;
			this.authtoken = _authtoken;
		}
		private static string parseFromFile(string filePath)
        {
        	Dictionary<string,string> data = parseFile(filePath,"## Gateway Registration detail");
        	if(	!data.TryGetValue("Organization-ID",out _orgId)||
        		!data.TryGetValue("Device-Type",out _deviceType)||
        		!data.TryGetValue("Device-ID",out _deviceID)||
        		!data.TryGetValue("Authentication-Method",out _authmethod)||
        		!data.TryGetValue("Authentication-Token",out _authtoken) )
        	{
        		throw new Exception("Invalid property file");
        	}
        	if(	!data.TryGetValue("CA-Certificate-Path",out _caCertificatePath)||
				!data.TryGetValue("CA-Certificate-Password",out _caCertificatePassword)||
				!data.TryGetValue("Client-Certificate-Path",out _clientCertificatePath)||
				!data.TryGetValue("Client-Certificate-Password",out _clientCertificatePassword))
        	{
        		//log.Info("Certificate's Not found in the given config file")
        	}
        	return _orgId;
        }
		private void subscrieToGatewayCommands(){
			this.subscribeToDeviceCommands(this.gatewayDeviceType,this.gatewayDeviceID);
		}
		/// <summary>
		/// To subscribe to all commands for the device connected to gateway  
		/// </summary>
		/// <param name="deviceType">string value of your device Type</param>
		/// <param name="deviceId">string value of your device id</param>
		public void subscribeToDeviceCommands(string deviceType, string deviceId) {
			this.subscribeToDeviceCommands(deviceType,deviceId,"+");
		}
		/// <summary>
		/// To subscribe to a specific command for the device connected to gateway  
		/// </summary>
		/// <param name="deviceType">string value of your device Type</param>
		/// <param name="deviceId">string value of your device id</param>
		/// <param name="command">string value of the command name to be subscriberd</param>
		public void subscribeToDeviceCommands(string deviceType, string deviceId, string command) {
			this.subscribeToDeviceCommands(deviceType,deviceId,command,0);
		}
		/// <summary>
		/// To subscribe to a specific command for the device connected to gateway with custom quality of services
		/// </summary>
		/// <param name="deviceType">string value of your device Type</param>
		/// <param name="deviceId">string value of your device id</param>
		/// <param name="command">string value of the command name to be subscriberd</param>
		/// <param name="qos"> int value of the quality of services 0,1,2</param>
		public void subscribeToDeviceCommands(string deviceType, string deviceId, string command, int qos) {
			try {
				mqttClient.MqttMsgPublishReceived -= client_MqttMsgPublishReceived;
				mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
				string newTopic = "iot-2/type/"+deviceType+"/id/"+deviceId+"/cmd/" + command + "/fmt/json";
				string[] topics = { newTopic };
				byte[] qosLevels = { (byte) qos };
		        mqttClient.Subscribe(topics, qosLevels);
		        
			} catch (Exception e) {
                log.Error("Execption has occer in subscribeToDeviceCommands",e);
			}
		}
		/// <summary>
		/// To connect the device to the Watson IoT Platform
		/// </summary>
		public override void connect()
		{
			base.connect();
			this.subscrieToGatewayCommands();
			this.subscribeToGatewayNotification();
		} 	
		/// <summary>
		/// To subscribe to notifications from the platform to the gateway 
		/// </summary>
		public void subscribeToGatewayNotification() {
			mqttClient.MqttMsgPublishReceived -= client_MqttMsgPublishReceived;
			mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			
			string newTopic = "iot-2/type/"+this.gatewayDeviceType +"/id/" +this.gatewayDeviceID + "/notify";
			string[] topics = { newTopic };
			byte[] qosLevels = { 2 };
			mqttClient.Subscribe(topics, qosLevels);

		}
		/// <summary>
		///  To publish an event to the Watson IoT platform
		/// </summary>
		/// <param name="evt">string value of event name</param>
		/// <param name="data">object of event data</param>
		/// <returns>bool value of result of publish status </returns>
		public bool publishGatewayEvent(string evt, object data) {
			return publishDeviceEvent(this.gatewayDeviceType, this.gatewayDeviceID, evt, data, 0);
		}
		/// <summary>
		/// To publish an event to the Watson IoT platform with custom quality of services
		/// </summary>
		/// <param name="evt">string value of event name</param>
		/// <param name="data">object of event data</param>
		/// <param name="qos">int value of the quality of services 0,1,2</param>
		/// <returns>bool value of result of publish status </returns>
		public bool publishGatewayEvent(string evt, object data, int qos) {
			return publishDeviceEvent(this.gatewayDeviceType, this.gatewayDeviceID, evt, data, qos);
		}
		
		/// <summary>
		///  To Publish the event on behalf of the connected device to the Watson IoT Platform
		/// </summary>
		/// <param name="deviceType">string value of your device Type</param>
		/// <param name="deviceId">string value of your device id</param>
		/// <param name="evt">string value of event name</param>
		/// <param name="data">object of event data</param>
		/// <returns>bool value of result of publish status </returns>
		public bool publishDeviceEvent(string deviceType, string deviceId, string evt, object data) {
			return publishDeviceEvent(deviceType, deviceId, evt, data, 0);
		}
	
		/// <summary>
		/// To Publish the event on behalf of the connected device to the Watson IoT Platform with custom quality of services
		/// </summary>
		/// <param name="deviceType">string value of your device Type</param>
		/// <param name="deviceId">string value of your device id</param>
		/// <param name="evt">string value of event name</param>
		/// <param name="data">object of event data</param>
		/// <param name="qos">int value of the quality of services 0,1,2</param>
		/// <returns>bool value of result of publish status </returns>
		public bool publishDeviceEvent(string deviceType, string deviceId, string evt, object data, int qos) {
			if (!isConnected()) {
				return false;
			}
			var payload = new{
				ts = DateTime.Now.ToString("o"),
				d = data
			};
			string topic = "iot-2/type/" + deviceType + "/id/" + deviceId + "/evt/" + evt + "/fmt/json";
			var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(payload);
			mqttClient.Publish(topic, Encoding.UTF8.GetBytes(json), 0, false);
			mqttClient.MqttMsgPublished += client_MqttMsgPublished;
			return true;
		}
		[Obsolete]
		private void MqttMsgReceived(MqttMsgPublishEventArgs e)
        {
          log.Info("*** Message Received.");
          log.Info("*** Topic: " + e.Topic);
          log.Info("*** Message: " + System.Text.UTF8Encoding.UTF8.GetString(e.Message));
          log.Info("");
        }
		[Obsolete]
		void client_EventPublished(Object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            try
            {
              log.Info("*** Message Received.");
              log.Info("*** Topic: " + e.Topic);
              log.Info("*** Message: " + System.Text.UTF8Encoding.UTF8.GetString(e.Message));
              log.Info("");
            }
            catch (Exception ex)
            {
                log.Error("Execption has occer in client_EventPublished",ex);
            }
        }
		[Obsolete]
        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
          log.Info("*** Message subscribed : " + e.MessageId);
        }
		[Obsolete]
        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
          log.Info("*** Message published : " + e.MessageId);
        }
        
		public delegate void processCommand(string deviceType, string deviceId,string cmdName, string format, string data);
		
		public delegate void processErrorNotification(string deviceType, string deviceId,GatewayError err);

        public event processCommand commandCallback =null;
        public event processErrorNotification errorCallback =null;
        
        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
        	try
        	{
	            // handle message received
	            string result = System.Text.Encoding.UTF8.GetString(e.Message);
	            
	            string topic = e.Topic;
	           //log.Info(topic);
	            Match matchCmd = Regex.Match(topic, GATEWAY_COMMAND_PATTERN);
	            if (matchCmd.Success)
	            {
	            	string[] tokens = topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
	          		if(this.commandCallback != null)
	           			this.commandCallback(tokens[2], tokens[4],tokens[6],tokens[8], result);
	            }
	             Match matchNotification = Regex.Match(topic, GATEWAY_NOTIFICATION_PATTERN);
	            if (matchNotification.Success)
	            {
	            	string[] tokens = topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
          		 	var serializer  = new System.Web.Script.Serialization.JavaScriptSerializer();
        			GatewayError response = serializer.Deserialize<GatewayError>(result);
	            	if(this.errorCallback != null)
	           			this.errorCallback(tokens[2], tokens[4],response);
	            	
	            }
	            
            }
        	catch(Exception ex)
        	{
        		log.Error("Execption has occer in client_MqttMsgPublishReceived ",ex);
        	}
        }

	}
}
