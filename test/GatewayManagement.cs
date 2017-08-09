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
	public class GatewayManagement
	{
		IBMWIoTP.GatewayManagement testClient;
		string orgId,deviceType,deviceID,authmethod,authtoken;
		
		[SetUp]
		public void Setup() 
		{
			IBMWIoTP.GatewayManagement.AutoReconnect = false;
			Dictionary<string,string> data = IBMWIoTP.DeviceClient.parseFile("../../Resource/Gatewayprop.txt","## Gateway Registration detail");
	    	if(	!data.TryGetValue("Organization-ID",out orgId)||
	    		!data.TryGetValue("Device-Type",out deviceType)||
	    		!data.TryGetValue("Device-ID",out deviceID)||
	    		!data.TryGetValue("Authentication-Method",out authmethod)||
	    		!data.TryGetValue("Authentication-Token",out authtoken) )
	    	{
	    		throw new Exception("Invalid property file");
	    	}
			testClient = new IBMWIoTP.GatewayManagement("../../Resource/Gatewayprop.txt",true);
		}
		[Test]
		public void GatewayManagementObjectCreationWithFilePath()
		{
			testClient = new IBMWIoTP.GatewayManagement("../../Resource/Gatewayprop.txt",true);
		}
		[Test]
		public void GatewayManagementObjectCreationWithParams()
		{
			Dictionary<string,string> data = IBMWIoTP.DeviceClient.parseFile("../../Resource/Gatewayprop.txt","## Gateway Registration detail");
	    	if(	!data.TryGetValue("Organization-ID",out orgId)||
	    		!data.TryGetValue("Device-Type",out deviceType)||
	    		!data.TryGetValue("Device-ID",out deviceID)||
	    		!data.TryGetValue("Authentication-Method",out authmethod)||
	    		!data.TryGetValue("Authentication-Token",out authtoken) )
	    	{
	    		throw new Exception("Invalid property file");
	    	}
			testClient = new IBMWIoTP.GatewayManagement(orgId,deviceType,deviceID,authmethod,authtoken,true);
		}
		[Test, ExpectedException]
		public void GatewayManagementObjectCreationWithinvalidFilePath()
		{
			testClient = new IBMWIoTP.GatewayManagement("propInvalid.text",true);
		}
		[Test]
		public void GatewayManagementManagedConnect()
		{
			testClient.connect();
			Assert.IsTrue(testClient.connectionStatus());
		}
		[Test]
		public void GatewayManagementManagedRequest()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			IBMWIoTP.DeviceInfo simpleDeviceInfo = new IBMWIoTP.DeviceInfo();
		    simpleDeviceInfo.description = "My gateway";
		    simpleDeviceInfo.deviceClass = "My gateway class";
		    simpleDeviceInfo.manufacturer = "My gateway manufacturer";
		    simpleDeviceInfo.fwVersion = "gateway Firmware Version";
		    simpleDeviceInfo.hwVersion = "gateway HW Version";
		    simpleDeviceInfo.model = "My gateway model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My gateway location";
			testClient.deviceInfo = simpleDeviceInfo;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			uid = testClient.managedGateway(4000,true,true,new{type="gateway"});
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		
		[Test]
		public void GatewayManagementAddErrorCodeRequest()
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
			 uid = testClient.addGatewayErrorCode(12);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementClearErrorCodeRequest()
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
			 uid = testClient.clearGatewayErrorCode();
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementAddLogRequest()
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
			 uid = testClient.addGatewayLog("test","data",1);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementClearLogRequest()
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
			 uid = testClient.clearGatewayLog();
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementSetLocationRequest()
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
			 uid = testClient.setGatewayLocation(77.5667,12.9667, 0,10);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementUnManagedRequest()
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
			 uid = testClient.unmanagedGateway();
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementManagedRequestForDevice()
		{
			var status ="";
			var uid ="";
			var recivedId ="";
			var wasCalled = false;
			IBMWIoTP.DeviceInfo simpleDeviceInfo = new IBMWIoTP.DeviceInfo();
		    simpleDeviceInfo.description = "My connected device";
		    simpleDeviceInfo.deviceClass = "My connected device class";
		    simpleDeviceInfo.manufacturer = "My connected device manufacturer";
		    simpleDeviceInfo.fwVersion = "Connected Device Firmware Version";
		    simpleDeviceInfo.hwVersion = "Connected Device HW Version";
		    simpleDeviceInfo.model = "My Connected device model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My Connected device location";
			testClient.deviceInfo = simpleDeviceInfo;
			if(!testClient.isConnected())
				testClient.connect();
			testClient.mgmtCallback += (reqId,sts) => {
				recivedId= reqId;
				status =sts;
				wasCalled = true;
			};
			uid = testClient.managedDevice("CsharpTestGatewayConnectedDevice","csharpgwdev0",4000,true,true,simpleDeviceInfo,new{type="connected device"});
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		
		[Test]
		public void GatewayManagementAddErrorCodeRequestForDevice()
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
			 uid = testClient.addDeviceErrorCode("CsharpTestGatewayConnectedDevice","csharpgwdev0",12);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementClearErrorCodeRequestForDevice()
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
			 uid = testClient.clearDeviceErrorCode("CsharpTestGatewayConnectedDevice","csharpgwdev0");
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementAddLogRequestForDevice()
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
			 uid = testClient.addDeviceLog("CsharpTestGatewayConnectedDevice","csharpgwdev0","test","data",1);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementClearLogRequestForDevice()
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
			 uid = testClient.clearDeviceLog("CsharpTestGatewayConnectedDevice","csharpgwdev0");
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementSetLocationRequestForDevice()
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
			 uid = testClient.setDeviceLocation("CsharpTestGatewayConnectedDevice","csharpgwdev0",77.5667,12.9667, 0,10);
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		[Test]
		public void GatewayManagementUnManagedRequestForDevice()
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
			 uid = testClient.unmanagedDevice("CsharpTestGatewayConnectedDevice","csharpgwdev0");
			Assert.IsTrue(wasCalled);
			Assert.AreEqual(status,"200");
			Assert.AreEqual(recivedId,uid);
		}
		//Action
		[Test]
		public void GatewayActionReboot()
		{
			bool wasCalled = false;
			string recivedRequestId="",recivedAction="";
			testClient.actionCallback += (string reqestId,string action)=>{
				wasCalled = true;
				recivedRequestId = reqestId;
				recivedAction = action;
			};
			string msg = "{\"reqId\":\"b5f1b986-a8a4-4c92-aa11-97f532ee5947\"}";
			string topic ="iotdm-1/type/"+deviceType+"/id/"+deviceID+"/mgmt/initiate/device/reboot";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs(topic,System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			
			Assert.AreEqual(recivedRequestId ,"b5f1b986-a8a4-4c92-aa11-97f532ee5947");
			Assert.AreEqual(recivedAction,"reboot");
			Assert.IsTrue(wasCalled);
		}
	
		[Test]
		public void GatewayActionFactoryReset()
		{
			bool wasCalled = false;
			string recivedRequestId="",recivedAction="";
			testClient.actionCallback += (string reqestId,string action)=>{
				wasCalled = true;
				recivedRequestId = reqestId;
				recivedAction = action;
			};
			string msg = "{\"reqId\":\"50f20f47-25a1-4f91-9bfb-b4267b1500b9\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/mgmt/initiate/device/factory_reset",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			Assert.AreEqual(recivedRequestId ,"50f20f47-25a1-4f91-9bfb-b4267b1500b9");
			Assert.AreEqual(recivedAction,"reset");
			Assert.IsTrue(wasCalled);
		}
		[Test]
		public void GatewayActionFirmeare_a_Download()
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
			MqttMsgPublishEventArgs evtUpdate = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/device/update",System.Text.Encoding.UTF8.GetBytes(msgUpdate),false,1,true);
			testClient.subscriptionHandler(new {},evtUpdate);
			
			string msgObserve = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtObserve = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/observe",System.Text.Encoding.UTF8.GetBytes(msgObserve),false,1,true);
			testClient.subscriptionHandler(new {},evtObserve);
			
			
			string msg = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/mgmt/initiate/firmware/download",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			string msgCancel = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtCancel = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/cancel",System.Text.Encoding.UTF8.GetBytes(msgCancel),false,1,true);
			testClient.subscriptionHandler(new {},evtCancel);
			
			Assert.AreEqual(recivedAction,"download");
			Assert.AreEqual(uri,"https://test.com/file.dll");
			Assert.IsTrue(wasCalled);
		}
		
		[Test]
		public void GatewayActionFirmeare_b_Update()
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
			MqttMsgPublishEventArgs evtUpdate = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/device/update",System.Text.Encoding.UTF8.GetBytes(msgUpdate),false,1,true);
			testClient.subscriptionHandler(new {},evtUpdate);
			
			string msgObserve = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtObserve = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/observe",System.Text.Encoding.UTF8.GetBytes(msgObserve),false,1,true);
			testClient.subscriptionHandler(new {},evtObserve);
			
			
			string msg = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/mgmt/initiate/firmware/update",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			string msgCancel = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtCancel = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/cancel",System.Text.Encoding.UTF8.GetBytes(msgCancel),false,1,true);
			testClient.subscriptionHandler(new {},evtCancel);
		
			Assert.AreEqual(recivedAction,"update");
			Assert.AreEqual(uri,"https://test.com/file.dll");
			Assert.IsTrue(wasCalled);
		}
		
		[Test]
		public void GatewayActionFirmeare_connectedDevice_a_Download()
		{
			bool wasCalled = false;
			string recivedAction="",reqId="";
			string connectedDevice ="",connectedDeviceType ="";
			string deviceType ="connectedDevice";
			string deviceID = "conndectedId";
			testClient.connectedDevicefwActionCallback += (string devicetype ,string deviceid,string action, IBMWIoTP.DeviceActionReq fw)=>{
				wasCalled = true;
				recivedAction = action;
				reqId = fw.reqId;
				connectedDeviceType = devicetype;
				connectedDevice = deviceid;
				//testClient.setState(IBMWIoTP.DeviceManagement.UPDATESTATE_DOWNLOADED);
			};
			string msgUpdate = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\",\"value\":{\"version\":\"\",\"name\":\"\",\"uri\":\"https://test.com/file.dll\",\"verifier\":\"\",\"state\":0,\"updateStatus\":0,\"updatedDateTime\":\"\"}}],\"action\":\"firmware/download\"}}";
			MqttMsgPublishEventArgs evtUpdate = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/device/update",System.Text.Encoding.UTF8.GetBytes(msgUpdate),false,1,true);
			testClient.subscriptionHandler(new {},evtUpdate);
			
			string msgObserve = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtObserve = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/observe",System.Text.Encoding.UTF8.GetBytes(msgObserve),false,1,true);
			testClient.subscriptionHandler(new {},evtObserve);
			
			
			string msg = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/mgmt/initiate/firmware/download",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			string msgCancel = "{\"reqId\":\"852536dd-db36-43c9-bed0-ba76124a1b90\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtCancel = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/cancel",System.Text.Encoding.UTF8.GetBytes(msgCancel),false,1,true);
			testClient.subscriptionHandler(new {},evtCancel);
			
			Assert.AreEqual(recivedAction,"download");
			Assert.AreEqual(reqId,"852536dd-db36-43c9-bed0-ba76124a1b90");
			
			Assert.AreEqual(connectedDevice,deviceID);
			Assert.AreEqual(connectedDeviceType,deviceType);
			Assert.IsTrue(wasCalled);
		}
		
		[Test]
		public void GatewayActionFirmeare_connectedDevice_b_Update()
		{
			bool wasCalled = false;
			string recivedAction="",reqId="";
			string connectedDevice ="",connectedDeviceType ="";
			string deviceType ="connectedDevice";
			string deviceID = "conndectedId";
			testClient.connectedDevicefwActionCallback += (string devicetype ,string deviceid,string action, IBMWIoTP.DeviceActionReq fw)=>{
				wasCalled = true;
				recivedAction = action;
				reqId = fw.reqId;
				connectedDeviceType = devicetype;
				connectedDevice = deviceid;
				testClient.setUpdateState(IBMWIoTP.DeviceManagement.UPDATESTATE_SUCCESS);
				
			};
			string msgUpdate = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\",\"value\":{\"version\":\"\",\"name\":\"\",\"uri\":\"https://test.com/file.dll\",\"verifier\":\"\",\"state\":2,\"updateStatus\":0,\"updatedDateTime\":\"\"}}],\"action\":\"firmware/update\"}}";
			MqttMsgPublishEventArgs evtUpdate = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/device/update",System.Text.Encoding.UTF8.GetBytes(msgUpdate),false,1,true);
			testClient.subscriptionHandler(new {},evtUpdate);
			
			string msgObserve = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtObserve = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/observe",System.Text.Encoding.UTF8.GetBytes(msgObserve),false,1,true);
			testClient.subscriptionHandler(new {},evtObserve);
			
			
			string msg = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\"}";
			MqttMsgPublishEventArgs evt = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/mgmt/initiate/firmware/update",System.Text.Encoding.UTF8.GetBytes(msg),false,1,true);
			testClient.subscriptionHandler(new {},evt);
			
			string msgCancel = "{\"reqId\":\"980c9013-6e91-4d9c-898b-a9c4709a4708\",\"d\":{\"fields\":[{\"field\":\"mgmt.firmware\"}]}}";
			MqttMsgPublishEventArgs evtCancel = new MqttMsgPublishEventArgs("iotdm-1/type/"+deviceType+"/id/"+deviceID+"/cancel",System.Text.Encoding.UTF8.GetBytes(msgCancel),false,1,true);
			testClient.subscriptionHandler(new {},evtCancel);
		
			Assert.AreEqual(recivedAction,"update");
			Assert.AreEqual(reqId,"980c9013-6e91-4d9c-898b-a9c4709a4708");
			Assert.AreEqual(connectedDevice,deviceID);
			Assert.AreEqual(connectedDeviceType,deviceType);
			Assert.IsTrue(wasCalled);
		}
		
	}
}
