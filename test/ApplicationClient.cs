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
using System.Collections.Generic;
using IBMWIoTP;
using NUnit.Framework;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace test
{
	[TestFixture]
	public class ApplicationClient
	{
		public IBMWIoTP.ApplicationClient testApp = null;
		[SetUp]
		public void Setup() 
		{
			IBMWIoTP.AbstractClient.AutoReconnect = false;
			testApp = new IBMWIoTP.ApplicationClient("../../Resource/AppProp.txt");
		}
		[Test,ExpectedException (typeof(System.Net.Sockets.SocketException))]
		public void ApplicationClientObjectCreationWithException()
		{
			testApp = new IBMWIoTP.ApplicationClient("","","","");
		}
		[Test]
		public void ApplicationClientObjectCreation()
		{
			string orgId,appID,apiKey,authToken;
			Dictionary<string,string> data = IBMWIoTP.ApplicationClient.parseFile("../../Resource/AppProp.txt","## Application Registration detail");
        	if(	!data.TryGetValue("Organization-ID",out orgId)||
        		!data.TryGetValue("App-ID",out appID)||
        		!data.TryGetValue("Api-Key",out apiKey)||
        		!data.TryGetValue("Authentication-Token",out authToken) )
        	{
        		throw new Exception("Invalid property file");
        	}
			testApp = new IBMWIoTP.ApplicationClient(orgId,appID,apiKey,authToken);
		}
		
		object parseFile(string string0, string string1)
		{
			throw new NotImplementedException();
		}
		[Test]
		public void ApplicationClientObjectCreationWithFilePath()
		{
			testApp = new IBMWIoTP.ApplicationClient("../../Resource/AppProp.txt");
		}
		[Test,ExpectedException]
		public void ApplicationClientObjectCreationWithFilePathInvaidFile()
		{
			 testApp  = new IBMWIoTP.ApplicationClient("propInvalid.txt");
		}
		[Test]
		public void ApplicationClientConnect()
		{
			testApp.connect();
			Assert.IsTrue(testApp.isConnected());
		}
		[Test]
		public void ApplicationClientDeviceStatusSubscription()
		{
			testApp.subscribeToDeviceStatus();
		}
		[Test]
		public void ApplicationClientApplicationStatusSubscription()
		{
			testApp.subscribeToApplicationStatus();
		}
		[Test]
		public void ApplicationClientDeviceEventSubscription()
		{
			testApp.subscribeToDeviceEvents("demotest", "12345678", "test", "josn", 0);
		}
		[Test]
		public void ApplicationClientDeviceCommandSubscription()
		{
			testApp.subscribeToDeviceCommands("demotest", "12345678", "test", "josn", 0);
		}
		
		[Test]
		public void ApplicationClientDeviceStatusUnSubscription()
		{
			testApp.unsubscribeToDeviceStatus("demotest","12345678");
		}
		[Test]
		public void ApplicationClientApplicationStatusUnSubscription()
		{
			testApp.unsubscribeToApplicationStatus("+");
		}
		[Test]
		public void ApplicationClientDeviceEventUnSubscription()
		{
			testApp.unsubscribeToDeviceEvents("demotest", "12345678", "test", "josn");
		}
		[Test]
		public void ApplicationClientDeviceCommandUnSubscription()
		{
			testApp.unsubscribeToDeviceCommands("demotest", "12345678", "test", "josn");
		}
		
		[Test]
		public void ApplicationClientPublishCommand()
		{
			bool result = testApp.publishCommand("demotest", "12345678", "test", "josn","{\"temp\":25}" ,0);
			if(!result){
				testApp.connect();
			 	result = testApp.publishCommand("demotest", "12345678", "test", "josn","{\"temp\":25}" ,0);
			}
			Assert.IsTrue(result);
		}

		[Test]
		public void ApplicationClientPublishEvent()
		{
			bool result =  testApp.publishEvent("demotest", "12345678", "test", "\"d\":{\"temp\":25}");
			if(!result){
				testApp.connect();
				result =  testApp.publishEvent("demotest", "12345678", "test", "\"d\":{\"temp\":25}");
			}
			Assert.IsTrue(result);
		}
		[Test]
		public void ApplicationClientDisconnect()
		{
			testApp.disconnect();
			Assert.IsFalse(testApp.isConnected());
		}
		[Test]
		public void ApplicationClientgetApiClient(){
			IBMWIoTP.ApiClient cli = testApp.GetAPIClient();
			Assert.IsInstanceOf<IBMWIoTP.ApiClient>(cli);
		}
		[Test]
		public void  ApplicationClientSubscribeToIMState(){
			testApp.subscribeToIMState();
			string typeId = "";
			string deviceId = "";
			string LIId ="";
			string json = "";
			testApp.IMStateCallback += (dt,did,liid,data)=>{
				typeId =dt;
				deviceId = did;
				LIId = liid;
				json = data;
			};
			string msg = "name:foo,cpu:60,mem:50";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iot-2/type/demotest/id/1qaz2wsx/intf/59a6c41052faff002c332a3b/evt/state",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testApp.client_MqttMsgArrived(new {},evt);
			Assert.AreEqual("demotest",typeId);
			Assert.AreEqual("1qaz2wsx",deviceId);
			Assert.AreEqual("59a6c41052faff002c332a3b",LIId);
			Assert.AreEqual(msg,json);
			
		}
		[Test]
		public void ApplicationClientUnSubscribeToIMState()
		{
			testApp.unsubscribeToIMState("demotest","1qaz2wsx","59a6c41052faff002c332a3b");
		}
	}
}
