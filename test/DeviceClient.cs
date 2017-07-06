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
 * 
 */
using System;
using System.Collections.Generic;
using System.Threading;

using IBMWIoTP;
using NUnit.Framework;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace test
{
	[TestFixture]
	public class DeviceClient 
	{
		IBMWIoTP.DeviceClient testClient;
		string orgId,deviceType,deviceID,authmethod,authtoken,caCertificatePath,caCertificatePassword,clientCertificatePath,clientCertificatePassword;
		
		[SetUp]
		public void Setup() 
		{
			testClient = new IBMWIoTP.DeviceClient("../../Resource/prop.txt");
		}
		[Test,ExpectedException (typeof(System.Net.Sockets.SocketException))]
		public void DeviceClientObjectCreationWithError()
		{
			 testClient  = new IBMWIoTP.DeviceClient("","","","","");
			
		}
		[Test]
		public void DeviceClientObjectCreationQuickStart()
		{
			 testClient  = new IBMWIoTP.DeviceClient("testdev","12345678");
			
		}
		[Test]
		public void DeviceClientObjectCreationWithValues()
		{
			Dictionary<string,string> data = IBMWIoTP.DeviceClient.parseFile("../../Resource/prop.txt","## Device Registration detail");
	    	if(	!data.TryGetValue("Organization-ID",out orgId)||
	    		!data.TryGetValue("Device-Type",out deviceType)||
	    		!data.TryGetValue("Device-ID",out deviceID)||
	    		!data.TryGetValue("Authentication-Method",out authmethod)||
	    		!data.TryGetValue("Authentication-Token",out authtoken) )
	    	{
	    		throw new Exception("Invalid property file");
	    	}
			 testClient  = new IBMWIoTP.DeviceClient(orgId,deviceType,deviceID,authmethod,authtoken);
		}
		[Test]
		public void DeviceClientObjectCreationWithValuesWithCA()
		{
			Dictionary<string,string> data = IBMWIoTP.DeviceClient.parseFile("../../Resource/prop.txt","## Device Registration detail");
	    	if(	!data.TryGetValue("Organization-ID",out orgId)||
	    		!data.TryGetValue("Device-Type",out deviceType)||
	    		!data.TryGetValue("Device-ID",out deviceID)||
	    		!data.TryGetValue("Authentication-Method",out authmethod)||
	    		!data.TryGetValue("Authentication-Token",out authtoken) ||
	    		!data.TryGetValue("CA-Certificate-Path",out caCertificatePath)||
				!data.TryGetValue("CA-Certificate-Password",out caCertificatePassword)||
				!data.TryGetValue("Client-Certificate-Path",out clientCertificatePath)||
				!data.TryGetValue("Client-Certificate-Password",out clientCertificatePassword))
        	{
        		throw new Exception("Invalid property file");
        	}
			 testClient  = new IBMWIoTP.DeviceClient(orgId,deviceType,deviceID,authmethod,authtoken,caCertificatePath,caCertificatePassword,clientCertificatePath,clientCertificatePassword);
		}
		[Test]
		public void DeviceClientObjectCreationWithFilePath()
		{
			 testClient  = new IBMWIoTP.DeviceClient("../../Resource/prop.txt");
		}
		[Test,ExpectedException (typeof(System.Exception))]
		public void DeviceClientObjectCreationWithFilePathInvaidFile()
		{
			 testClient  = new IBMWIoTP.DeviceClient("../../Resource/propInvalid.txt");
		}
		[Test]
		public void DeviceClientConnectionStatus()
		{
			testClient.connect();
			Assert.IsTrue(testClient.isConnected());
		}
		[Test]
		public void DeviceClientTostring()
		{
			testClient.connect();
			var str = testClient.toString();
			Assert.AreEqual(str,"[d:"+orgId+":"+deviceType+":"+deviceID+"] Connected = True" );
		}
		[Test]
		public void DeviceClientCommandSubscription()
		{
			testClient.subscribeCommand("testcmd", "json", 0);
		}
		[Test]
		public void DeviceClientCommandReciveing()
		{
			var wasCalled = false;
			var cmd ="";
			var fmt="";
			var data = "";
			testClient.commandCallback += (c,f,d) => {
				cmd = c;
				fmt=f;
				data = d;
				wasCalled = true;
			};
			string msg = "name:foo,cpu:60,mem:50";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iot-2/cmd/testcmd/fmt/json",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.client_MqttMsgPublishReceived(new {},evt);
			Assert.AreEqual(cmd ,"testcmd");
			Assert.AreEqual(fmt,"json");
			Assert.AreEqual(data,"name:foo,cpu:60,mem:50");
			Assert.IsTrue(wasCalled);
		}
		[Test]
		public void DeviceClientSendEvent()
		{
			string data = "{\"temp\":25}";
			bool result=testClient.publishEvent("test", "json", data);
			if(!result){
				testClient.connect();
				result=testClient.publishEvent("test", "json", data);
			}
			Assert.IsTrue(result);
		}
		[Test]
		public void DeviceClientSendEventWithQOS()
		{
			string data = "{\"temp\":25}";
			bool result=testClient.publishEvent("test", "json", data, 0);
			if(!result){
				testClient.connect();
				result=testClient.publishEvent("test", "json", data, 0);
			}
			Assert.IsTrue(result);
		}
		[Test]
		public void DeviceClientDisconnect()
		{
			testClient.disconnect();
			Assert.IsFalse(testClient.isConnected());
		}
		
	}
}
