/*
 *  Copyright (c) 2016 IBM Corporation and other Contributors.
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

namespace GatewayMgmt
{
	class SampleGatewayManagement
	{
		public static void Main(string[] args)
		{
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
			
			Console.WriteLine("Sample Managed Gateway Device");
			
			string orgId ="";
			string gatewayDeviceType = "";
			string gatewayDeviceID = "";
			string authMethod = "token"; 
			string authToken  = "";
			
			bool isSync = true;
			
			string deviceType = "";
			string deviceId = "";
			
			
			Console.Write("Enter your org id :");
        	orgId = Console.ReadLine();
        	
        	Console.Write("Enter your gateway type :");
        	gatewayDeviceType = Console.ReadLine();

        	Console.Write("Enter your gateway id :");
        	gatewayDeviceID = Console.ReadLine();

        	Console.Write("Enter your auth token :");
        	authToken = Console.ReadLine();

			Console.WriteLine("please enter connected device details:");
			Console.Write("Enter your device type :");
        	deviceType = Console.ReadLine();

        	Console.Write("Enter your device id :");
        	deviceId = Console.ReadLine();
        	
			DeviceInfo simpleDeviceInfo = new DeviceInfo();
		    simpleDeviceInfo.description = "My device";
		    simpleDeviceInfo.deviceClass = "My device class";
		    simpleDeviceInfo.manufacturer = "My device manufacturer";
		    simpleDeviceInfo.fwVersion = "Device Firmware Version";
		    simpleDeviceInfo.hwVersion = "Device HW Version";
		    simpleDeviceInfo.model = "My device model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My device location";
		    
		    
			GatewayManagement gwMgmtClient =new GatewayManagement(orgId,gatewayDeviceType,gatewayDeviceID,authMethod,authToken,isSync);
			gwMgmtClient.deviceInfo = simpleDeviceInfo;
			gwMgmtClient.mgmtCallback += processMgmtResponse;
			gwMgmtClient.connect();
			
			Console.WriteLine("Managed Gateway");
			gwMgmtClient.managedGateway(4000,true,true);
			Console.WriteLine("Managed Gateway With Meta");
			gwMgmtClient.managedGateway(4000,true,true,new{Key=""});
			Console.WriteLine("Add Gateway Error Code");
			gwMgmtClient.addGatewayErrorCode(12);
			Console.WriteLine("Clear Gateway Error Code");
			gwMgmtClient.clearGatewayErrorCode();
			Console.WriteLine("Add Gateway Log");
			string message = "test";
			string data="data";
			int severity= 1;
			gwMgmtClient.addGatewayLog(message,data,severity);
			Console.WriteLine("Clear Gateway Log");
			gwMgmtClient.clearGatewayLog();
			Console.WriteLine("Set Gateway Location");
			double longitude = 77.5667;
			double latitude =12.9667;
			double elevation=0;
			double accuracy =10;
			gwMgmtClient.setGatewayLocation(longitude,latitude,elevation,accuracy);
			Console.WriteLine("Unmanage Gateway");
			gwMgmtClient.unmanagedGateway();
			
			Console.WriteLine("Managed Device");
			gwMgmtClient.managedDevice(deviceType,deviceId,4000,true,true);
			Console.WriteLine("Managed Device With Meta");
			
			DeviceInfo attachedDeviceInfo = new DeviceInfo();
		    attachedDeviceInfo.description = "My device";
		    attachedDeviceInfo.deviceClass = "My device class";
		    attachedDeviceInfo.manufacturer = "My device manufacturer";
		    attachedDeviceInfo.fwVersion = "Device Firmware Version";
		    attachedDeviceInfo.hwVersion = "Device HW Version";
		    attachedDeviceInfo.model = "My device model";
		    attachedDeviceInfo.serialNumber = "1432";
		    attachedDeviceInfo.descriptiveLocation ="My device location";
		    
			gwMgmtClient.managedDevice(deviceType,deviceId,4000,true,true,attachedDeviceInfo,new{Key=""});
			Console.WriteLine("Add Device Error Code");
			gwMgmtClient.addDeviceErrorCode(deviceType,deviceId,12);
			Console.WriteLine("Clear Device Error Code");
			gwMgmtClient.clearDeviceErrorCode(deviceType,deviceId);
			Console.WriteLine("Add Device Log");
			gwMgmtClient.addDeviceLog(deviceType,deviceId,message,data,severity);
			Console.WriteLine("Clear Device Log");
			gwMgmtClient.clearDeviceLog(deviceType,deviceId);
			Console.WriteLine("Set Device Location");
			gwMgmtClient.setDeviceLocation(deviceType,deviceId,longitude,latitude,elevation,accuracy);
			Console.WriteLine("Unmanage Device");
			gwMgmtClient.unmanagedDevice(deviceType,deviceId);
			
			Console.Write("Press any key to exit . . . ");
			Console.ReadKey();
			gwMgmtClient.disconnect();
			
		}
		public static void processMgmtResponse( string reqestId, string responseCode){
			Console.WriteLine("req Id:" + reqestId +"	responseCode:"+ responseCode);
		}
	}
}