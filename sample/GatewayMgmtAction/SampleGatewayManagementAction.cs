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

namespace GatewayMgmtAction
{
	class SampleGatewayManagementAction
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
			
			Console.WriteLine("Gateway Management Action Sample");
		 	
			Console.Write("Enter your org id :");
        	orgID = Console.ReadLine();
        	
        	Console.Write("Enter your gateway type :");
        	deviceType = Console.ReadLine();

        	Console.Write("Enter your gateway id :");
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
		    
		    GatewayManagement	gwMgmtClient = new GatewayManagement(orgID,deviceType,deviceId,authType,authKey,isSync);
			gwMgmtClient.deviceInfo = simpleDeviceInfo;
			gwMgmtClient.mgmtCallback += processMgmtResponse;
			gwMgmtClient.actionCallback += (string reqestId,string action)=>{
				Console.WriteLine("req Id:" + reqestId +"	Action:"+ action +" called");
				if(action == "reboot"){
					gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_ACCEPTED,"");

					Thread.Sleep(2000);
					gwMgmtClient.disconnect();
					
					Console.WriteLine("disconnected");
					Thread.Sleep(5000);
					
					Console.WriteLine("Re connected");	
					gwMgmtClient.connect();
					
					gwMgmtClient.managedGateway(4000,true,true);
				}
				if(action == "reset"){
					gwMgmtClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_FUNCTION_NOT_SUPPORTED,"");
				}
		
			};
			gwMgmtClient.fwCallback += (string action , DeviceFirmware fw)=>{
				if(action == "download"){
					gwMgmtClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADING);
					Console.WriteLine("Start downloading new Firmware from "+fw.uri);
					Thread.Sleep(2000);
					Console.WriteLine("completed Download");
					gwMgmtClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);
				
				}
				if(action == "update"){
					gwMgmtClient.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
					Console.WriteLine("Start Updating new Firmware ");
					Thread.Sleep(2000);
					Console.WriteLine("Updated new Firmware ");
					gwMgmtClient.setUpdateState(DeviceManagement.UPDATESTATE_SUCCESS);
				}
			};
			gwMgmtClient.connect();
			Console.WriteLine("Manage");
			gwMgmtClient.managedGateway(4000,true,true);
		
//			Console.Write("Press any key to exit . . . ");
			Console.ReadKey();
//			gwMgmtClient.disconnect();
			
			
		}
		public static void processMgmtResponse( string reqestId, string responseCode){
			Console.WriteLine("req Id:" + reqestId +"	responseCode:"+ responseCode);
		}
		
        
	}
	
}