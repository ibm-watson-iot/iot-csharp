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
using IBMWIoTP;
using NUnit.Framework;

namespace test
{
	[TestFixture]
	public class Info
	{
		[Test]
		public void DeviceInfoObjectTest()
		{
			DeviceInfo simpleDeviceInfo = new DeviceInfo();
		    simpleDeviceInfo.description = "My device";
		    simpleDeviceInfo.deviceClass = "My device class";
		    simpleDeviceInfo.manufacturer = "My device manufacturer";
		    simpleDeviceInfo.fwVersion = "Device Firmware Version";
		    simpleDeviceInfo.hwVersion = "Device HW Version";
		    simpleDeviceInfo.model = "My device model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My device location";
		    
		    Assert.AreEqual(simpleDeviceInfo.description , "My device");
		    Assert.AreEqual(simpleDeviceInfo.deviceClass , "My device class");
		    Assert.AreEqual(simpleDeviceInfo.manufacturer , "My device manufacturer");
		    Assert.AreEqual(simpleDeviceInfo.fwVersion , "Device Firmware Version");
		    Assert.AreEqual(simpleDeviceInfo.hwVersion , "Device HW Version");
		    Assert.AreEqual(simpleDeviceInfo.model , "My device model");
		    Assert.AreEqual(simpleDeviceInfo.serialNumber , "12345");
		    Assert.AreEqual(simpleDeviceInfo.descriptiveLocation ,"My device location");
		}
		[Test]
		public void DeviceFirmwareObjectTest()
		{
			DeviceFirmware sample = new DeviceFirmware();
			sample.version = "1.0.0"; 
			sample.name = "test"; 
			sample.url = "http://url.com"; 
			sample.uri = "http://url.com"; 
			sample.verifier = "1q2w4e"; 
			sample.state = 0; 
			sample.updateStatus = 0; 
			sample.updatedDateTime = "time"; 
			
			Assert.AreEqual(sample.version , "1.0.0"); 
			Assert.AreEqual(sample.name , "test"); 
			Assert.AreEqual(sample.url , "http://url.com"); 
			Assert.AreEqual(sample.uri , "http://url.com"); 
			Assert.AreEqual(sample.verifier , "1q2w4e"); 
			Assert.AreEqual(sample.state ,0); 
			Assert.AreEqual(sample.updateStatus , 0); 
			Assert.AreEqual(sample.updatedDateTime , "time"); 
		}
		
		[Test]
		public void LocationInfoObjectTest()
		{
			LocationInfo sample = new LocationInfo();
			sample.latitude = 10.213;
			sample.longitude = 145.213;
			sample.measuredDateTime="now";
			sample.elevation = 0;
			sample.accuracy=6;
			
			Assert.AreEqual(sample.latitude , 10.213);
			Assert.AreEqual(sample.longitude , 145.213);
			Assert.AreEqual(sample.measuredDateTime,"now");
			Assert.AreEqual(sample.elevation , 0);
			Assert.AreEqual(sample.accuracy,6);
		}
		
		[Test]
		public void GatewayErrorObjectTest()
		{
			GatewayError sample = new GatewayError();
			sample.Request = "my request";
			sample.Time = "now";
			sample.Topic = "my topic";
			sample.Type = "my type";
			sample.Id = "my id";
			sample.Client = "my client";
			sample.RC = "200";
			sample.Message = "test msg";
			
			Assert.AreEqual(sample.Request , "my request");
			Assert.AreEqual(sample.Time , "now");
			Assert.AreEqual(sample.Topic , "my topic");
			Assert.AreEqual(sample.Type , "my type");
			Assert.AreEqual(sample.Id , "my id");
			Assert.AreEqual(sample.Client , "my client");
			Assert.AreEqual(sample.RC , "200");
			Assert.AreEqual(sample.Message , "test msg");
		}
		
		[Test]
		public void RegisterDevicesInfoObjectTest()
		{
			RegisterDevicesInfo sample = new RegisterDevicesInfo();
			sample.typeId = "my id";
			sample.deviceId = "my device id";
			sample.metadata = "test" ;
			sample.authToken = "my token";
			sample.deviceInfo = new DeviceInfo();
			sample.deviceInfo.description ="my device";
			sample.location = new LocationInfo();
			sample.location.accuracy = 1;
			
			Assert.AreEqual("my id",sample.typeId);
			Assert.AreEqual("my device id",sample.deviceId);
			Assert.AreEqual("test",sample.metadata);
			Assert.AreEqual("my token",sample.authToken);
			Assert.AreEqual("my device",sample.deviceInfo.description);
			Assert.AreEqual(1,sample.location.accuracy);
		}
		[Test]
		public void RegisterSingleDevicesInfoObjectTest()
		{
			RegisterSingleDevicesInfo sample = new RegisterSingleDevicesInfo();
			sample.deviceId = "my device id";
			sample.metadata = "test" ;
			sample.authToken = "my token";
			sample.deviceInfo = new DeviceInfo();
			sample.deviceInfo.description ="my device";
			sample.location = new LocationInfo();
			sample.location.accuracy = 1;
			
			Assert.AreEqual("my device id",sample.deviceId);
			Assert.AreEqual("test",sample.metadata);
			Assert.AreEqual("my token",sample.authToken);
			Assert.AreEqual("my device",sample.deviceInfo.description);
			Assert.AreEqual(1,sample.location.accuracy);
		}
		[Test]
		public void UpdateDevicesInfoObjectTest()
		{
			UpdateDevicesInfo sample = new UpdateDevicesInfo();
			sample.metadata = "test" ;
			sample.deviceInfo = new DeviceInfo();
			sample.deviceInfo.description ="my device";
			sample.extensions = "test";
			sample.status = "test";
			
			Assert.AreEqual("test",sample.metadata);
			Assert.AreEqual("test",sample.extensions);
			Assert.AreEqual("test",sample.status);
			Assert.AreEqual("my device",sample.deviceInfo.description);
		}
		
		[Test]
		public void DeviceListElementObjectTest()
		{
			DeviceListElement sample = new DeviceListElement();
			sample.typeId = "my id";
			sample.deviceId = "my device id";
			
			Assert.AreEqual("my id",sample.typeId);
			Assert.AreEqual("my device id",sample.deviceId);
		}
		
		[Test]
		public void DeviceTypeInfoObjectTest()
		{
			DeviceTypeInfo sample = new DeviceTypeInfo();
			sample.id = "my id";
			sample.classId = "device";
			sample.description ="my device";
			sample.metadata = "test" ;
			sample.deviceInfo = new DeviceInfo();
			sample.deviceInfo.description ="my device";
			
			Assert.AreEqual("my id",sample.id);
			Assert.AreEqual("device",sample.classId);
			Assert.AreEqual("my device",sample.description);
			Assert.AreEqual("my device",sample.deviceInfo.description);
			Assert.AreEqual("test",sample.metadata);
			
		}
		
		[Test]
		public void DeviceTypeInfoUpdateObjectTest()
		{
			DeviceTypeInfoUpdate sample = new DeviceTypeInfoUpdate();
			sample.description ="my device";
			sample.metadata = "test" ;
			sample.deviceInfo = new DeviceInfo();
			sample.deviceInfo.description ="my device";
			
			Assert.AreEqual("my device",sample.description);
			Assert.AreEqual("my device",sample.deviceInfo.description);
			Assert.AreEqual("test",sample.metadata);
			
		}
		[Test]
		public void LogInfoObjectTest()
		{
			LogInfo sample = new LogInfo();
			sample.message ="msg";
			sample.severity = 1;
			sample.timestamp = "time";
			sample.data="my data";

			
			Assert.AreEqual("my data",sample.data);
			Assert.AreEqual("time",sample.timestamp);
			Assert.AreEqual("msg",sample.message);
			Assert.AreEqual(1,sample.severity);
			
		}
		[Test]
		public void ErrorCodeInfoObjectTest()
		{
			ErrorCodeInfo sample = new ErrorCodeInfo();
			sample.errorCode = 1;
			sample.timestamp = "time";

			
			Assert.AreEqual("time",sample.timestamp);
			Assert.AreEqual(1,sample.errorCode);
			
		}
		
		[Test]
		public void DeviceMgmtparameterObjectTest()
		{
			DeviceMgmtparameter sample = new DeviceMgmtparameter();
			sample.name = "param1";
			sample.value = "value";

			
			Assert.AreEqual("param1",sample.name);
			Assert.AreEqual("value",sample.value);
			
		}
		[Test]
		public void DMRequestObjectTest(){
		
			DMRequest sample = new DMRequest();
			sample.json = "{}";
			sample.reqID = "id";
			sample.topic = "topic";
			
			Assert.AreEqual("{}",sample.json );
			Assert.AreEqual("id",sample.reqID );
			Assert.AreEqual("topic",sample.topic );
		}
		[Test]
		public void DMResponseObjectTest(){
		
			DMResponse sample = new DMResponse();
			sample.rc = "1";
			sample.reqId = "id";
			
			Assert.AreEqual("1",sample.rc);
			Assert.AreEqual("id",sample.reqId);
			
		}
		[Test]
		public void DMFieldObjectTest(){
		
			DMField sample = new DMField();
			sample.value = new DeviceFirmware();
			sample.value.version = "1.1.1";
			sample.field = "field";
			
			Assert.AreEqual("field",sample.field);
			Assert.AreEqual("1.1.1",sample.value.version);
		}
		[Test]
		public void DMFieldsObjectTest(){
		
			DMFields sample = new DMFields();
			sample.fields = new DMField[1];
			
			Assert.AreEqual(1,sample.fields.Length);
		}
		[Test]
		public void DeviceActionReqObjectTest(){
		
			DeviceActionReq sample = new DeviceActionReq();
			sample.reqId = "id";
			sample.d = new DMFields();
			sample.d.fields = new DMField[1];
			
			Assert.AreEqual(1,sample.d.fields.Length);
			Assert.AreEqual("id",sample.reqId);
		}
	}
}
