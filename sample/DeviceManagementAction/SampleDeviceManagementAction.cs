/*
 *  Copyright (c) 2017 IBM Corporation and other Contributors.
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
using System.Threading;

namespace DeviceManagementAction
{
	class SampleDeviceManagementAction
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
			deviceClient.actionCallback += (string reqestId,string action)=>{
				Console.WriteLine("req Id:" + reqestId +"	Action:"+ action +" called");
				if(action == "reboot"){
					deviceClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_ACCEPTED,"");

					Thread.Sleep(2000);
					deviceClient.disconnect();
					
					Console.WriteLine("disconnected");
					Thread.Sleep(5000);
					
					Console.WriteLine("Re connected");	
					deviceClient.connect();
					
					deviceClient.manage(4000,true,true);
				}
				if(action == "reset"){
					deviceClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_FUNCTION_NOT_SUPPORTED,"");
				}
		
			};
			deviceClient.fwCallback += (string action , DeviceFirmware fw)=>{
				if(action == "download"){
					deviceClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADING);
					Console.WriteLine("Start downloading new Firmware from "+fw.uri);
					Thread.Sleep(2000);
					Console.WriteLine("completed Download");
					deviceClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);
				
				}
				if(action == "update"){
					deviceClient.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
					Console.WriteLine("Start Updating new Firmware ");
					Thread.Sleep(2000);
					Console.WriteLine("Updated new Firmware ");
					deviceClient.setUpdateState(DeviceManagement.UPDATESTATE_SUCCESS);
				}
			};
			deviceClient.connect();
			deviceClient.subscribeCommand("testcmd", "json", 2);
           	deviceClient.commandCallback += processCommand;
			Console.WriteLine("Manage");
			deviceClient.manage(4000,true,true);
			Console.WriteLine("Set Location");
			deviceClient.setLocation(77.5667,12.9667, 0,10);

			//Console.Write("Press any key to exit . . . ");
			Console.ReadKey();
//			deviceClient.disconnect();
			
			
		}
		public static void processMgmtResponse( string reqestId, string responseCode){
			Console.WriteLine("req Id:" + reqestId +"	responseCode:"+ responseCode);
		}
		
        public static void processCommand(string cmdName, string format, string data) {
             Console.WriteLine("Sample Device Client : Sample Command " + cmdName + " " + "format: " + format + "data: " + data);
        }
	}
}