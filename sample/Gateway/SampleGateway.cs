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

namespace Gateway
{
	class SampleGateway
	{
		public static void Main(string[] args)
		{
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
			
			Console.WriteLine("Example for gateway");
			string orgId ="";
			string gatewayDeviceType = "";
			string gatewayDeviceID = "";
			string authMethod = "token"; 
			string authToken  = "";
			
			Console.Write("Enter your org id :");
        	orgId = Console.ReadLine();
        	
        	Console.Write("Enter your gateway type :");
        	gatewayDeviceType = Console.ReadLine();

        	Console.Write("Enter your gateway id :");
        	gatewayDeviceID = Console.ReadLine();

        	Console.Write("Enter your auth token :");
        	authToken = Console.ReadLine();
        	
        	
			GatewayClient gw =new GatewayClient(orgId,gatewayDeviceType,gatewayDeviceID,authMethod,authToken);
			gw.commandCallback += processCommand;
			gw.errorCallback += processError;
			gw.connect();
			Console.WriteLine("Gateway connected");
			Console.WriteLine("publishing gateway events..");
			
			gw.publishGatewayEvent("test","{\"temp\":25}");
			gw.publishGatewayEvent("test","{\"temp\":22}",2);
			
			string deviceType = "";
			string deviceId = "";
			string deviceEvent = "testdevevt";
			string deviceEventValue = "{\"temp\":100}";
			
			Console.WriteLine("please enter connected device details:");
			Console.Write("Enter your device type :");
        	deviceType = Console.ReadLine();

        	Console.Write("Enter your device id :");
        	deviceId = Console.ReadLine();
        	Console.WriteLine("publishing device events..");
			gw.publishDeviceEvent(deviceType,deviceId,deviceEvent,deviceEventValue);
			gw.subscribeToDeviceCommands(deviceType,deviceId);
			Console.WriteLine("Press any key to exit . . . ");
			Console.ReadKey();
			gw.disconnect();
			
		}
		public static void processCommand(string deviceType, string deviceId,string cmdName, string format, string data) {
             Console.WriteLine("Device Type: "+deviceType+" Device ID: "+deviceId +" Command: " + cmdName  + " format: " + format + " data: " + data);
        }
		public static void processError(string deviceType, string deviceId,GatewayError err) {
			Console.WriteLine("Device Type: "+deviceType+" Device ID: "+deviceId +" msg:"+ err.Message);
        }
	}
}