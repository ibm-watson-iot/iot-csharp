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

namespace DeviceManagementSample
{
	class SampleDeviceManagement
	{
		public static void Main(string[] args)
		{
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
			
			string orgID = "";
			string deviceType = "";
			string deviceId = "";
			string authType = "";
			string authKey = "";
			bool isSync = true;
			
			Console.WriteLine("Device Management Sample");
		 	
			Console.Write("Enter your org id :");
        	orgID = Console.ReadLine();
        	
        	Console.Write("Enter your device type :");
        	deviceType = Console.ReadLine();

        	Console.Write("Enter your device id :");
        	deviceId = Console.ReadLine();

        	Console.Write("Enter your auth key :");
        	authKey = Console.ReadLine();


			
			DeviceInfo simpleDeviceInfo = new DeviceInfo();
		    simpleDeviceInfo.description = "My device";
		    simpleDeviceInfo.deviceClass = "My device class";
		    simpleDeviceInfo.manufacturer = "My device manufacturer";
		    simpleDeviceInfo.fwVersion = "Device Firmware Version";
		    simpleDeviceInfo.hwVersion = "Device HW Version";
		    simpleDeviceInfo.model = "My device model";
		    simpleDeviceInfo.serialNumber = "12345";
		    simpleDeviceInfo.descriptiveLocation ="My device location";
		    
			DeviceManagement	deviceClient = new DeviceManagement(orgID,deviceType,deviceId,authType,authKey,isSync);
			deviceClient.deviceInfo = simpleDeviceInfo;
			deviceClient.mgmtCallback += processMgmtResponse;
			deviceClient.connect();
			Console.WriteLine("Manage");
			deviceClient.manage(4000,true,true);
			Console.WriteLine("Manage With Meta");
			deviceClient.manage(4000,true,true,new{Key=""});
			Console.WriteLine("Add Error Code");
			deviceClient.addErrorCode(12);
			Console.WriteLine("Clear Error Code");
			deviceClient.clearErrorCode();
			Console.WriteLine("Add Log");
			deviceClient.addLog("test","data",1);
			Console.WriteLine("Clear Log");
			deviceClient.clearLog();
			Console.WriteLine("Set Location");
			deviceClient.setLocation(77.5667,12.9667, 0,10);
			Console.WriteLine("Unmanage");
			deviceClient.unmanage();
			Console.Write("Press any key to exit . . . ");
			Console.ReadKey();
			deviceClient.disconnect();
		}
		public static void processMgmtResponse( string reqestId, string responseCode){
			Console.WriteLine("req Id:" + reqestId +"	responseCode:"+ responseCode);
		}
	}
}