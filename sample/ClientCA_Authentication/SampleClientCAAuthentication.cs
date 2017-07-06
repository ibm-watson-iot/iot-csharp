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
 * 	 Nikhil Chennakeshava Murthy and Hari hara prasad Viswanatha - Client certificate based authentication
 */
 
using System;
using System.Configuration;
using System.Threading;

using IBMWIoTP;

namespace ClientCA_Authentication
{
	public class SampleClientCAAuthentication
	{
		
		private static DeviceClient deviceClient;
		private static GatewayClient gatewayClient;
		
		public static void Main(string[] args)
		{
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
			
			Console.WriteLine("Example for Client certificate authentication for Device and Gateway");

			int opt;
			Console.Write("1.Device\n2.Gateway\nEnter your choice:");
			
			opt = Convert.ToInt32(Console.ReadLine()); 
			
			switch(opt){
				case 1:
					deviceClient = new DeviceClient("prop.txt");
        		
		            try{
		           	 	deviceClient.connect();
		           	 	deviceClient.subscribeCommand("testcmd", "json", 0);
		            	deviceClient.commandCallback += processCommand;
		            }
		            catch(Exception ex)
		            {
		            		Console.WriteLine("ex:"+ex.Message);
		            }
		            for (int i = 0; i < 10; i++)
		            {
		            	string data = "{\"temp\":"+(i*5)+"}";
		                Console.WriteLine(data);
		                deviceClient.publishEvent("test", "json", data, 0);
		                Thread.Sleep(1000);
		            }
		            deviceClient.disconnect();
		            break;
		            
		          case 2:
		            gatewayClient = new GatewayClient("gatewayprop.txt");
				
					try{
						
						gatewayClient.connect();
						gatewayClient.commandCallback += processCommand;
						gatewayClient.errorCallback += processError;
						Console.WriteLine("Gateway connected");
						Console.WriteLine("publishing gateway events..");
						
						gatewayClient.publishGatewayEvent("test","{\"temp\":25}");
						Thread.Sleep(2000);
						gatewayClient.publishGatewayEvent("test","{\"temp\":22}",2);
						gatewayClient.disconnect();
					}
					catch(Exception ex)
		            {
		            		Console.WriteLine("ex:"+ex.Message);
		            }
					break;
					
					default: Console.WriteLine("Invalid");
					break;
					
			}
			
        	
		}
		public static void processCommand(string cmdName, string format, string data) {
             Console.WriteLine("Sample Device Client : Sample Command " + cmdName + " " + "format: " + format + "data: " + data);
        }
		public static void processCommand(string deviceType, string deviceId,string cmdName, string format, string data) {
             Console.WriteLine("Device Type: "+deviceType+" Device ID: "+deviceId +" Command: " + cmdName  + " format: " + format + " data: " + data);
        }
		public static void processError(string deviceType, string deviceId,GatewayError err) {
			Console.WriteLine("Device Type: "+deviceType+" Device ID: "+deviceId +" msg:"+ err.Message);
        }
	}
}