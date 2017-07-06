using System;
using System.Configuration;
using System.Threading;

using IBMWIoTP;

namespace com.ibm.iotf.client.device.sample
{
    /// <author>Kaberi Singh, kabsingh@in.ibm.com</author>
    /// <date>28/08/2015 09:05:05 </date>
    /// <summary>
    ///     A sample device client
    /// </summary>
    public class SampleDeviceClient 
    {
       private static DeviceClient deviceClient;
        static void Main(string[] args)
        {
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
        	Console.WriteLine("Check Out Following Samples For devices");
        	Console.WriteLine("1.Quick Start");
        	Console.WriteLine("2.Registered Flow");
        	Console.WriteLine("3.Any key to exit");
        	Console.Write("Please enter your choice :");
        	int val = int.Parse(Console.ReadLine());
        	switch(val){
    			case 1: Console.WriteLine ("Starting Quick Start Flow");
        			 	QuickStart();
        				break;
        			
        		case 2 : Console.WriteLine ("Starting Registered Flow");
        				RegisteredFlow();
        				break;
			default : break; 
        	}
        	Console.ReadLine();
       }

        private static void QuickStart (){
        	string deviceType = "";
        	string uuid = "";
    		
        	Console.Write("Enter your event type :");
		   	deviceType = Console.ReadLine();

    		Console.Write("Enter your Quick Start Uid :");
		   	uuid = Console.ReadLine();
		   	
    		deviceClient = new DeviceClient(deviceType, uuid);
            try{
           	 	deviceClient.connect();
            }
            catch(Exception ex)
            {
            		Console.WriteLine("ex:"+ex.Message);
            }
            for (int i = 0; i < 10; i++)
            {
            		String data = "{\"temp\":"+(i*5)+"}";
                Console.WriteLine(data);
                deviceClient.publishEvent("test", "json", data, 0);
                Thread.Sleep(1000);
            }
            deviceClient.disconnect();
        }
        
        public static void RegisteredFlow(){
        	string orgId ="";
        	string deviceType = "";
        	string deviceId = "";
        	string authToken = "";
        	
        	Console.Write("Enter your org id :");
        	orgId = Console.ReadLine();
        	
        	Console.Write("Enter your device type :");
        	deviceType = Console.ReadLine();

        	Console.Write("Enter your device id :");
        	deviceId = Console.ReadLine();

        	Console.Write("Enter your auth token :");
        	authToken = Console.ReadLine();

        	deviceClient = new DeviceClient(orgId,deviceType,deviceId,"token",authToken);
        	
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
        }
        public static void processCommand(string cmdName, string format, string data) {
             Console.WriteLine("Sample Device Client : Sample Command " + cmdName + " " + "format: " + format + "data: " + data);
        }
     
    }

}
