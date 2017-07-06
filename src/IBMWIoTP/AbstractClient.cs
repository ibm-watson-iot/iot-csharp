/*
 *  Copyright (c) 2016-2017 IBM Corporation and other Contributors.
 *
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.eclipse.org/legal/epl-v10.html 
 *
 * Contributors:
 * 	kaberi Singh - Initial Contribution
 *	Hari hara prasad Viswanathan  - updations
 * 	Nikhil Chennakeshava Murthy and Hari hara prasad Viswanatha - Client certificate based authentication
 */
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using log4net;

namespace IBMWIoTP
{
    /// <summary>
    ///     A client that handles connections with the IBM Watson IoT Platform. <br>
    ///     This is an abstract class which has to be extended
    /// </summary>
    public abstract class AbstractClient
    {
        private string clientUsername;
        private string clientPassword;
        private string clientId;
        private string orgId;
        
        private string caCertificatePath;
        private string clientCertificatePath;
        private string caCertificatePassword;
        private string clientCertificatePassword;
        private bool _isSecureConnection;
        private ILog log = log4net.LogManager.GetLogger(typeof(DeviceManagement));
        
        protected static readonly String CLIENT_ID_DELIMITER = ":";
        protected static readonly String DOMAIN = ".messaging.internetofthings.ibmcloud.com";
        protected static readonly int MQTTS_PORT = 8883;
        protected MqttClient mqttClient;                                                             
        
        /// <summary>
        ///     Note that this class does not have a default constructor <br>
	    /// </summary>
        /// <param name="orgid">
        ///     object of String which denotes OrgId </param>
        /// <param name="clientId">
        ///     object of String which denotes clientId </param>
        /// <param name="userName">
        ///     object of String which denotes userName </param>
        /// <param name="password">
        ///     object of String which denotes password </param>
               
        public AbstractClient(string orgid, string clientId, string userName, string password)
        {
            this.clientId = clientId;
            this.clientUsername = userName;
            this.clientPassword = password;
            this.orgId = orgid;
            String now = DateTime.Now.ToString(".yyyy.MM.dd-THH.mm.fff");

            string hostName = orgid + DOMAIN;

            try {
            	X509Certificate cer = new X509Certificate();
            	if(File.Exists("message.pem")){
					cer.Import("message.pem");
            	}
	            log.Info("hostname is :" + hostName);
	            mqttClient = new MqttClient(hostName,MQTTS_PORT,true,cer,new X509Certificate(),MqttSslProtocols.TLSv1_2);
            	this._isSecureConnection = true;
            } catch (Exception) {
            	log.Warn("hostname is :" + hostName+"  with insecure connection");
            	this._isSecureConnection = false;
            	mqttClient = new MqttClient(hostName);
            	//throw;
            }
			//mqttClient = new MqttClient(hostName);


        }
        
                
        /// <param name="orgid">
        ///     object of String which denotes OrgId </param>
        /// <param name="clientId">
        ///     object of String which denotes clientId </param>
        /// <param name="userName">
        ///     object of String which denotes userName </param>
        /// <param name="password">
        ///     object of String which denotes password </param>
        ///	<param name="caCertificatePath">
        ///     object of String which denotes CA certificate path </param>
        /// <param name="caCertificatePassword">
        ///     object of String which denotes CA certificate password </param>
        /// <param name="clientCertificatePath">
        ///     object of String which denotes client certificate path </param>
        /// <param name="clientCertificatePassword">
        ///     object of String which denotes client certificate password </param>
                
        public AbstractClient(string orgid, string clientId, string userName, string password, string caCertificatePath, string caCertificatePassword, string clientCertificatePath, string clientCertificatePassword)
        {
        	
        	
            this.clientId = clientId;
            this.clientUsername = userName;
            this.clientPassword = password;
            this.orgId = orgid;
            
            this.caCertificatePath = caCertificatePath;
            this.caCertificatePassword = caCertificatePassword;
            this.clientCertificatePath = clientCertificatePath;
            this.clientCertificatePassword = clientCertificatePassword;
            
            String now = DateTime.Now.ToString(".yyyy.MM.dd-THH.mm.fff");

            string hostName = orgid + DOMAIN;
			if(String.IsNullOrEmpty(caCertificatePath) || String.IsNullOrEmpty(caCertificatePassword) || String.IsNullOrEmpty(clientCertificatePath) || String.IsNullOrEmpty(clientCertificatePassword)){ 		

	            try {
	            	X509Certificate cer = new X509Certificate();
	            	if(File.Exists("message.pem")){
						cer.Import("message.pem");
	            	}
		            log.Info("hostname is :" + hostName);
		            mqttClient = new MqttClient(hostName,MQTTS_PORT,true,cer,new X509Certificate(),MqttSslProtocols.TLSv1_2);
            		this._isSecureConnection = true;
	            
	            } catch (Exception) {
	            	log.Warn("hostname is :" + hostName+"  with insecure connection");
            		this._isSecureConnection = false;
	            	
	            	mqttClient = new MqttClient(hostName);
	            	//throw;
	            }
			        		
            }else{
	            try {
	            	X509Certificate2 caCert = new X509Certificate2(caCertificatePath, caCertificatePassword);
					X509Certificate2 clientCert = new X509Certificate2(clientCertificatePath, clientCertificatePassword);
	            	
					try{
						X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
						store.Open(OpenFlags.ReadWrite);
						store.Add(caCert); 
						store.Add(clientCert);
						store.Close();
					}
					catch(Exception e){
						log.Error("Unable to add certifiactes to the store");
						throw(new Exception("Unable to add certifiactes to the store", e.InnerException));
					}
					
	            	
		            log.Info("hostname is :" + hostName);
		            mqttClient = new MqttClient(hostName,MQTTS_PORT,true,caCert,clientCert,MqttSslProtocols.TLSv1_2);
            		this._isSecureConnection = true;
	            	
	            }catch (Exception e) {
		            log.Info("hostname is :" + hostName+"  with insecure connection");
		            log.Error("Unable to make secure connection" + e.ToString());
		            throw(new Exception("Unable to make secure connection" , e));
	            }
            
            }
			
        }
        
               
        /// <summary>
        ///     Connect the device from the IBM Watson IoT Platform
        /// </summary>
        public virtual void connect()
        {
            try 
            {
                
                if(orgId == "quickstart"){
                    mqttClient.Connect(clientId);
                }
                else
                {
                	mqttClient.Connect(clientId, clientUsername, clientPassword);
                }
                log.Info("Device Connected to IBM Watson IoT Platform");
            }
            catch (Exception e)
            {
            	log.Error("Execption has occurred in connecting to MQTT",e);
            	throw new Exception("Execption has occurred in connecting to MQTT",e);
            }
        }
		/// <summary>
        ///     Connect the device from the IBM Watson IoT Platform
        /// </summary>
        public virtual void connect(bool cleanSession , ushort keepAlivePeriod)
        {
            try 
            {
                
                if(orgId == "quickstart"){
                    mqttClient.Connect(clientId);
                }
                else
                {
                	mqttClient.Connect(clientId, clientUsername, clientPassword,cleanSession,keepAlivePeriod);
                }
                log.Info("Device Connected to IBM Watson IoT Platform");
            }
            catch (Exception e)
            {
            	log.Error("Execption has occurred in connecting to MQTT",e);
            	throw new Exception ("Execption has occurred in connecting to MQTT",e);
            }
        }
        /// <summary>
        ///     Disconnect the device from the IBM Watson IoT Platform
        /// </summary>
        public void disconnect()
        {
             log.Info("Disconnecting from the IBM Watson IoT Platform");
            try
            {
                mqttClient.Disconnect();

                log.Info("Successfully disconnected from the IBM Watson IoT Platform");
            }
            catch (Exception e)
            {
            	log.Error("Execption has occurred in disconnecting from MQTT",e);
            	//throw new Exception("Execption has occurred in disconnecting from MQTT",e);
            }
        }

        /// <summary>
        ///     Determine whether this device or application is currently connected to the IBM Internet
        ///     of Things Foundation.
        /// </summary>
        /// <returns>
        ///     Whether the device or application is connected to the IBM Watson IoT Platform
        /// </returns>
        public bool isConnected()
        {
            return mqttClient.IsConnected;
        }

        /// <summary>
        ///     Provides a human readable String representation of this Device, including the number
        ///     of messages sent and the current connect status.
        /// </summary>
        /// <returns>
        ///     String representation of the Device status
        /// </returns>
        public string toString()
        {
            return "[" + clientId + "] " +  "Connected = " + isConnected();
        }
        public bool isSecureConnection { 
        	get{
        		return this._isSecureConnection;
        	}
        }
		
        /// <summary>
        ///  Parse the given file and gives key value pared dictionary of parameters separated with "=",parsing starts after the lien with the given region and end with a new line 
        /// </summary>
        /// <param name="path">
        /// file path to be parsed</param>
        /// <param name="region">
        /// starting line of the requires segment</param>
        /// <returns>
        /// Dictionary of key,value pair in string,string format </returns>
        public static Dictionary<string, string> parseFile(string path , string region)
		{
			Dictionary<string, string>  myDictionary = new Dictionary<string, string>();
			string[] lines = System.IO.File.ReadAllLines(path);
			var idx =Array.FindIndex(lines, row => row == region);
			for (int i = idx+1; i < lines.Length; i++) {
				if(string.IsNullOrWhiteSpace(lines[i]))
				   break;
				string[] tokens = lines[i].Split('=');
				myDictionary.Add(tokens[0].Trim(),tokens[1].Trim());
			
			}
			return myDictionary;
		}
    }
}