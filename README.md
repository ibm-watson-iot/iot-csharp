C# Client Library - Introduction
============================================

This C# Client Library can be used to simplify interactions with the [IBM Watson IoT Platform] (https://internetofthings.ibmcloud.com). The documentation is divided into following sections:  

- The [Device section](docs/Device.rst) contains information on how devices publish events and handle commands using the C# IBMWIoTP Client Library.
- The [Managed Device section](docs/DeviceManagement.rst) contains information on how devices can connect to the Watson IoT Platform Device Management service using C# IBMWIoTP Client Library and perform device management operations like firmware update, location update, and diagnostics update.
- The [Gateway section](docs/Gateway.rst) contains information on how gateways publish events and handle commands for itself and for the attached devices using the C# IBMWIoTP Client Library.
- The [Gateway Management section](docs/GatewayManagement.rst) contains information on how to connect the gateway as Managed Gateway to IBM Watson IoT Platform and manage the attached devices.
- The [Application section](docs/Application.rst) details how applications can use the C# IBMWIoTP Client Library to interact with devices.
- The [API section](docs/API.rst) contains information on how applications can use the C# IBMWIoTP Client Library to interact with the organization in the Watson IoT Platform through REST APIs
- The [Client Certificate section](docs/CACertificate.md) contains information on how to connect devices/gateway with Client side Certificate based authentication.
- The [Information Management section](docs/IMApi.md) contains information onhow applications can use the C# IBMWIoTP Client Library to interact with the organization in the Watson IoT Platform Information Management APIs

Supported Features
------------------

| Feature   |      Supported?      | Description |
|----------|:-------------:|:-------------|
| Device connectivity |  &#10004; | Connect your device(s) to Watson IoT Platform with ease using this library.
| Gateway connectivity |    &#10004;   |  Connect your gateway(s) to Watson IoT Platform with ease using this library.
| Application connectivity | &#10004; | Connect your application(s) to Watson IoT Platform with ease using this library.
| Watson IoT API | &#10004; | Shows how applications can use this library to interact with the Watson IoT Platform through REST APIs.
| Information Management API | &#10004; | Shows how applications can use this library to configure and implement Information Management APIs in the Watson IoT Platform.
| SSL/TLS | &#10004; | By default, this library connects your devices, gateways and applications **securely** to Watson IoT Platform registered service.
| Client side Certificate based authentication | &#10004; | Client side Certificate based authentication
| Device Management | &#10004; | Connects your device/gateway as managed device/gateway to Watson IoT Platform.
| Device Management Extension(DME) | &#10008; | Provides support for custom device management actions.
| Scalable Application | &#10004; | Provides support for load balancing for applications.
| Auto reconnect | &#10004; | Enables device/gateway/application to automatically reconnect to Watson IoT Platform while they are in a disconnected state.
| Websocket | &#10008; | Enables device/gateway/application to connect to Watson IoT Platform using WebSockets.
| Event/Command publish using MQTT| &#10004; |  Enables device/gateway/application to publish messages using MQTT.
| Event/Command publish using HTTP| &#10008; | Enables device/gateway/application to publish messages using HTTP.

NuGet Package
--------------------------------
 C# library is available in [nuget](https://www.nuget.org/packages/IBMWIoTP/)

To install IBMWIoTP, run the following command in the Package Manager Console

```
  PM> Install-Package IBMWIoTP
```


----
Dependencies
-------------------------------------------------------------------------------

-  [Paho M2MQTT] (https://www.nuget.org/packages/M2Mqtt/) - provides a client class which enable applications to connect to an MQTT broker
-  [log4net] (https://www.nuget.org/packages/log4net/) - library for creating log.
-  [RestSharp] (https://www.nuget.org/packages/RestSharp/) - Simple REST and HTTP API Client

----

Samples
-------------------------------------------------------------------------------
You can find samples in each of the corresponding repositories as follows:

* [Device samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/DeviceIoTF) - Repository contains samples for connecting device to IBM Watson Internet of Things Platform .
* [Gateway Samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/Gateway) -  Repository contains samples for connecting gateway to IBM Watson Internet of Things Platform .
* [DeviceManagement samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/DeviceManagement) - Repository contains samples for connecting device as managed device to IBM Watson Internet of Things Platform .
* [GatewayManagement Samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/GatewayManagement) - Repository contains samples for connecting gateway as managed device to IBM Watson Internet of Things Platform .
* [DeviceManagement Action Samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/DeviceManagementAction) - Repository contains samples for DeviceManagement Action like reboot,rest,firmware download and update.
* [GatewayManagement Action Samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/GatewayMgmtAction) - Repository contains samples for GatewayManagement Action like reboot,rest,firmware download and update.
* [GatewayManagement Action For the connected device Samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/GatewayConnectedDeviceMgntAction) - Repository contains samples for GatewayManagement Action like reboot,rest,firmware download and update for the managed device connected to the gateway.
* [Application samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/GatewayMgmtAction) - Repository contains samples for developing the application(s) in IBM Watson Internet of Things Platform in different languages.
* [Watson IoT Platform API V002 samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/ApiClient) - Repository contains samples that interacts with IBM Watson IoT Platform using the platform API Version 2.
* [Client Certificate samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/ClentCA_Authentication) - Repository contains samples for connecting devices/gateway with Client side Certificate based authentication to IBM Watson IoT Platform.
* [Information Management samples](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/IMClient) - Repository contains samples for Information Management of device events in IBM Watson IoT Platform.

----

License
-----------------------

The library is shipped with Eclipse Public License and refer to the [License file](LICENSE) for more information about the licensing.
