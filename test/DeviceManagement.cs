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
	public class DeviceManagement
	{
		IBMWIoTP.DeviceManagement testClient;
		string orgId,deviceType,deviceID,authmethod,authtoken;
		
		[SetUp]
		public void Setup() 
		{
			testClient = new IBMWIoTP.DeviceManagement("../../Resource/prop.txt",true);
		}
		[Test]
		public void DeviceManagementObjectCreationWithFilePath()
		{
			testClient = new IBMWIoTP.DeviceManagement("../../Resource/prop.txt",true);
		}
		[Test]
		public void DeviceManagementObjectCreationWithParams()
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
			testClient = new IBMWIoTP.DeviceManagement(orgId,deviceType,deviceID,authmethod,authtoken,true);
		}
		[Test, ExpectedException]
		public void DeviceManagementObjectCreationWithinvalidFilePath()
		{
			testClient = new IBMWIoTP.DeviceManagement("propInvalid.text",true);
		}
		[Test]
		public void DeviceManagementManagedConnect()
		{
			testClient.connect();
			Assert.IsTrue(testClient.connectionStatus());
		}
		[Test]
		public void DeviceManagementManagedRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			DeviceInfo simpleDeviceInfo = new DeviceInfo();
		    simpleDeviceInfo.description = "My device";
		    simpleDeviceInfo.deviceClass = "My device class";
		    simpleDeviceInfo.manufacturer = "My device manufacturer";
		    simpleDeviceInfo.fwVersion = "Device Firmware Version";
		    simpleDeviceInfo.hwVersion = "Device HW Version";
		    simpleDeviceInfo.model = "My device model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My device location";
			testClient.deviceInfo = simpleDeviceInfo;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};

			
			uid = testClient.manage(4000,true,true,new {test=1});
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		
		[Test]
		public void DeviceManagementAddErrorCodeRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			 uid = testClient.addErrorCode(12);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void DeviceManagementClearErrorCodeRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			 uid = testClient.clearErrorCode();
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void DeviceManagementAddLogRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			 uid = testClient.addLog("test","data",1);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void DeviceManagementClearLogRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			 uid = testClient.clearLog();
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void DeviceManagementSetLocationRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			 uid = testClient.setLocation(77.5667,12.9667, 0,10);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void DeviceManagementUnManagedRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			 uid = testClient.unmanage();
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		
		//Action
		[Test]
		public void DeviceActionReboot()
		{
			bool wasCalled = false;
			string recivedRequestId="",recivedAction="";
			testClient.actionCallback += (string reqestId,string action)=>{
				wasCalled = true;
				recivedRequestId = reqestId;
				recivedAction = action;
			};
			string msg = "{\"reqId\":\"b5f1b986-a8a4-4c92-aa11-97f532ee5947\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/mgmt/initiate/device/reboot",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			Assert.AreEqual(recivedRequestId ,"b5f1b986-a8a4-4c92-aa11-97f532ee5947");
			Assert.AreEqual(recivedAction,"reboot");
			Assert.IsTrue(wasCalled);
		}
	
		[Test]
		public void DeviceActionFactoryReset()
		{
			bool wasCalled = false;
			string recivedRequestId="",recivedAction="";
			testClient.actionCallback += (string reqestId,string action)=>{
				wasCalled = true;
				recivedRequestId = reqestId;
				recivedAction = action;
			};
			string msg = "{\"reqId\":\"50f20f47-25a1-4f91-9bfb-b4267b1500b9\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/mgmt/initiate/device/factory_reset",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			Assert.AreEqual(recivedRequestId ,"50f20f47-25a1-4f91-9bfb-b4267b1500b9");
			Assert.AreEqual(recivedAction,"reset");
			Assert.IsTrue(wasCalled);
		}
		[Test]
		public void DeviceActionFirmeare_a_Download()
		{
			bool wasCalled = false;
			string recivedAction="",uri="";
			testClient.fwCallback += (string action, IBMWIoTP.DeviceFirmware fw)=>{
				wasCalled = true;
				recivedAction = action;
				uri = fw.uri;
				testClient.setState(IBMWIoTP.DeviceManagement.UPDATESTATE_DOWNLOADED);
			};
			string msgUpdate = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\",\"value\":{\"version\":\"\",\"name\":\"\",\"uri\":\"https://test.com/file.dll\",\"verifier\":\"\",\"state\":0,\"updateStatus\":0,\"updatedDateTime\":\"\"}}],\"action\":\"firmware/download\"}}";
			MqttMsgPublishEventArgs evtUpdate = new MqttMsgPublishEventArgs("iotdm-1/device/update",System.Text.Encoding.UTF8.GetBytes(msgUpdate),false,1,true);
			testClient.subscriptionHandler(new {},evtUpdate);
			
			string msgObserve = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtObserve = new MqttMsgPublishEventArgs("iotdm-1/observe",System.Text.Encoding.UTF8.GetBytes(msgObserve),false,1,true);
			testClient.subscriptionHandler(new {},evtObserve);
			
			
			string msg = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/mgmt/initiate/firmware/download",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			string msgCancel = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtCancel = new MqttMsgPublishEventArgs("iotdm-1/cancel",System.Text.Encoding.UTF8.GetBytes(msgCancel),false,1,true);
			testClient.subscriptionHandler(new {},evtCancel);
			
			Assert.AreEqual(recivedAction,"download");
			Assert.AreEqual(uri,"https://test.com/file.dll");
			Assert.IsTrue(wasCalled);
		}
		
		[Test]
		public void DeviceActionFirmeare_b_Update()
		{
			bool wasCalled = false;
			string recivedAction="",uri="";
			testClient.fwCallback += (string action, IBMWIoTP.DeviceFirmware fw)=>{
				wasCalled = true;
				recivedAction = action;
				uri = fw.uri;
				testClient.setUpdateState(IBMWIoTP.DeviceManagement.UPDATESTATE_SUCCESS);
				
			};
			string msgUpdate = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\",\"value\":{\"version\":\"\",\"name\":\"\",\"uri\":\"https://test.com/file.dll\",\"verifier\":\"\",\"state\":2,\"updateStatus\":0,\"updatedDateTime\":\"\"}}],\"action\":\"firmware/update\"}}";
			MqttMsgPublishEventArgs evtUpdate = new MqttMsgPublishEventArgs("iotdm-1/device/update",System.Text.Encoding.UTF8.GetBytes(msgUpdate),false,1,true);
			testClient.subscriptionHandler(new {},evtUpdate);
			
			string msgObserve = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtObserve = new MqttMsgPublishEventArgs("iotdm-1/observe",System.Text.Encoding.UTF8.GetBytes(msgObserve),false,1,true);
			testClient.subscriptionHandler(new {},evtObserve);
			
			
			string msg = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/mgmt/initiate/firmware/update",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			string msgCancel = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtCancel = new MqttMsgPublishEventArgs("iotdm-1/cancel",System.Text.Encoding.UTF8.GetBytes(msgCancel),false,1,true);
			testClient.subscriptionHandler(new {},evtCancel);
		
			Assert.AreEqual(recivedAction,"update");
			Assert.AreEqual(uri,"https://test.com/file.dll");
			Assert.IsTrue(wasCalled);
		}
		
	}
}
