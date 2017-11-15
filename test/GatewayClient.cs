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
using NUnit.Framework;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace test
{
	[TestFixture]
	public class GatewayClient
	{
		IBMWIoTP.GatewayClient testClient;
		string orgId,deviceType,deviceID,authmethod,authtoken,caCertificatePath,caCertificatePassword,clientCertificatePath,clientCertificatePassword;
		
		[SetUp]
		public void Setup() 
		{
			IBMWIoTP.GatewayClient.AutoReconnect = false;
			testClient = new IBMWIoTP.GatewayClient("../../Resource/Gatewayprop.txt");
		}
		[Test]
		public void GatewayClientObjectCreationWithFilePath()
		{
			testClient = new IBMWIoTP.GatewayClient("../../Resource/Gatewayprop.txt");
		}
		[Test]
		public void GatewayClientObjectCreationWithParams()
		{
			Dictionary<string,string> data = IBMWIoTP.GatewayClient.parseFile("../../Resource/Gatewayprop.txt","## Gateway Registration detail");
	    	if(	!data.TryGetValue("Organization-ID",out orgId)||
	    		!data.TryGetValue("Device-Type",out deviceType)||
	    		!data.TryGetValue("Device-ID",out deviceID)||
	    		!data.TryGetValue("Authentication-Method",out authmethod)||
	    		!data.TryGetValue("Authentication-Token",out authtoken) )
	    	{
	    		throw new Exception("Invalid property file");
	    	}
			testClient = new IBMWIoTP.GatewayClient(orgId,deviceType,deviceID,authmethod,authtoken);
		}
		[Test]
		public void DeviceClientObjectCreationWithValuesWithCA()
		{
			Dictionary<string,string> data = IBMWIoTP.GatewayClient.parseFile("../../Resource/Gatewayprop.txt","## Gateway Registration detail");
	    	if(	!data.TryGetValue("Organization-ID",out orgId)||
	    		!data.TryGetValue("Device-Type",out deviceType)||
	    		!data.TryGetValue("Device-ID",out deviceID)||
	    		!data.TryGetValue("Authentication-Method",out authmethod)||
	    		!data.TryGetValue("Authentication-Token",out authtoken) ||
//	    		!data.TryGetValue("CA-Certificate-Path",out caCertificatePath)||
//				!data.TryGetValue("CA-Certificate-Password",out caCertificatePassword)||
				!data.TryGetValue("Client-Certificate-Path",out clientCertificatePath)||
				!data.TryGetValue("Client-Certificate-Password",out clientCertificatePassword))
        	{
        		throw new Exception("Invalid property file");
        	}
			 testClient  = new IBMWIoTP.GatewayClient(orgId,deviceType,deviceID,authmethod,authtoken,caCertificatePath,caCertificatePassword,clientCertificatePath,clientCertificatePassword);
		}
		[Test, ExpectedException]
		public void GatewayClientObjectCreationWithinvalidFilePath()
		{
			testClient = new IBMWIoTP.GatewayClient("propInvalid.text");
		}
		[Test]
		public void GatewayClientConnectionStatus()
		{
			testClient.connect();
			Assert.IsTrue(testClient.isConnected());
		}
		
		[Test]
		public void GatewayClientSendGatewayEvent()
		{
			bool result= testClient.publishGatewayEvent("test","{\"temp\":25}");
			if(!result){
				testClient.connect();
				result= testClient.publishGatewayEvent("test","{\"temp\":25}");
			}
			Assert.IsTrue(result);
		}
		
		[Test]
		public void GatewayClientSendGatewayEventWithQos()
		{
			bool result=testClient.publishGatewayEvent("test","{\"temp\":22}",2);
			if(!result){
				testClient.connect();
				result=testClient.publishGatewayEvent("test","{\"temp\":22}",2);
			}
			Assert.IsTrue(result);
		}
		
		[Test]
		public void GatewayClientSendDeviceEvent()
		{
			bool result=testClient.publishDeviceEvent("CsharpTestGatewayConnectedDevice","csharpgwdev0","testdevevt","{\"temp\":100}");
			if(!result){
				testClient.connect();
				result=testClient.publishDeviceEvent("CsharpTestGatewayConnectedDevice","csharpgwdev0","testdevevt","{\"temp\":100}");
			}
			Assert.IsTrue(result);
		}
		[Test]
		public void GatewayClientCommandReciveing()
		{
			var wasCalled = false;
			var cmd ="";
			var fmt="";
			var data = "";
			var devicetype= "";
			var deviceid = "";
			testClient.commandCallback += (t,i,c,f,d) => {
				devicetype = t;
				deviceid = i;
				cmd = c;
				fmt=f;
				data = d;
				wasCalled = true;
			};
			string msg = "name:foo,cpu:60,mem:50";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iot-2/type/testdev/id/1234/cmd/testcmd/fmt/json",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.client_MqttMsgPublishReceived(new {},evt);
			Assert.AreEqual(devicetype ,"testdev");
			Assert.AreEqual(deviceid ,"1234");
			Assert.AreEqual(cmd ,"testcmd");
			Assert.AreEqual(fmt,"json");
			Assert.AreEqual(data,"name:foo,cpu:60,mem:50");
			Assert.IsTrue(wasCalled);
		}
		
		[Test]
		public void GatewayClientDisconnect()
		{
			testClient.disconnect();
			Assert.IsFalse(testClient.isConnected());
		}
		
	}
}
