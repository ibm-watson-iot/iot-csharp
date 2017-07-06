using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMWIoTP;
namespace com.ibm.iotf.client.app.sample
{
    /// <author>Kaberi Singh, kabsingh@in.ibm.com</author>
    /// <date>28/08/2015 09:05:05 </date>
    /// <summary>
    ///     Sample application client
    /// </summary>
    class SampleAppClient
    {

        private static string orgId = "";
        private static string appId = "";
        private static string apiKey = "";
        private static string authToken = "";


        static void Main(string[] args)
        {
        	Console.WriteLine("============================ IBM WatsonIoTP Sample ============================");
            try
            {
                string data = "{\"name\":\"foo\",\"cpu\":60,\"mem\":50}";
                string deviceType = "";
                string deviceId = "";
                string evt = "test";
                string format = "json";

				Console.Write("Enter your org id :");
				orgId = Console.ReadLine();
				
				Console.Write("Enter your app id :");
				appId = Console.ReadLine();
				
				Console.Write("Enter your api Key :");
				apiKey = Console.ReadLine();
				
				Console.Write("Enter your auth token :");
				authToken = Console.ReadLine();
				
				Console.WriteLine("Please enter device details to which you want to subscribe event and send command...");
				
                Console.Write("Enter your device type :");
				deviceType = Console.ReadLine();
				
				Console.Write("Enter your device id :");
				deviceId = Console.ReadLine();
				

                
                ApplicationClient applicationClient = new ApplicationClient(orgId, appId, apiKey, authToken);
                applicationClient.connect();

                Console.WriteLine("Connected sucessfully to app id : " + appId);

                applicationClient.commandCallback += processCommand;
                applicationClient.eventCallback += processEvent;
                applicationClient.deviceStatusCallback += processDeviceStatus;
                applicationClient.appStatusCallback += processAppStatus;

                applicationClient.subscribeToDeviceStatus();
                applicationClient.subscribeToApplicationStatus();
                
                

                applicationClient.subscribeToDeviceEvents(deviceType, deviceId, evt, format, 0);
            
                
			
                applicationClient.publishCommand(deviceType, deviceId, "testcmd", "json", data, 0);
                
                Console.ReadKey();

                applicationClient.disconnect();
            }
            catch (Exception)
            {
                // ignore
            }
        }

        public static void processCommand(string cmdName, string format, string data)
        {
             Console.WriteLine("Sample Application Client : Sample Command : " + cmdName + " format : " + format + " data : " + data);
        }

        public static void processEvent(string deviceType,string deviceId ,string eventName, string format, string data)
        {
             Console.WriteLine("Sample Application Client : Device Type"+deviceType+" Device ID:"+deviceId+"Sample Event : " + eventName + " format : " + format + " data : " + data);
        }

        public static void processDeviceStatus(string deviceType, string deviceId, string data)
        {
             Console.WriteLine("Sample Application Client : Sample Device status : " + deviceType + " format : " + deviceId + " data : " + data);
        }

        public static void processAppStatus(string appId, String data)
        {
             Console.WriteLine("Sample Application Client : Sample App Status : " + appId + " data : " + data);
        }
    }
}
