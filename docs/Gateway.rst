======================================
C# Client Library - Gateway Devices
======================================
- See `iot-csharp <https://github.com/ibm-messaging/iot-csharp>`_ in GitHub


----

Constructor
-------------------------------------------------------------------------------

The constructor builds the Gateway client instance, and accepts a Properties object containing the following definitions:

* ``org`` - Your organization ID.
* ``type`` - The type of your Gateway device.
* ``id`` - The ID of your Gateway.
* ``auth-method`` - Method of authentication (The only value currently supported is "token").
* ``auth-token`` - API key token.

The Properties object creates definitions which are used to interact with the Watson Internet of Things Platform module.

The following code snippet shows how to construct the GatewayClient instance,

.. code:: C#

    string orgId ="<your org id>";
    string gatewayDeviceType = "<your gateway device type>";
    string gatewayDeviceID = "<your gateway id>";
    string authMethod = "token";
    string authToken  = "<your gateway auth token>";

    GatewayClient gw =new GatewayClient(orgId,gatewayDeviceType,gatewayDeviceID,authMethod,authToken);


Connecting to the Watson Internet of Things Platform
----------------------------------------------------

Connect to the Watson Internet of Things Platform by calling the *connect* function.

.. code:: C#

  GatewayClient gw =new GatewayClient(orgId,gatewayDeviceType,gatewayDeviceID,authMethod,authToken);
  gw.connect();


After the successful connection to the IBM Watson IoT Platform, the Gateway client can perform the following operations,

* Publish events for itself and on behalf of devices connected behind the Gateway.
* Subscribe to commands for itself and on behalf of devices behind the Gateway.



Register devices using the Watson IoT Platform API
-------------------------------------------------------------------------
There are different ways to register the devices behind the Gateway to IBM Watson IoT Platform,

* **Auto registration**: The device gets added automatically in IBM Watson IoT Platform when Gateway publishes any event/subscribes to any commands for the devices connected to it.

Publishing events
-------------------------------------------------------------------------------
Events are the mechanism by which Gateways/devices publish data to the Watson IoT Platform. The Gateway/device controls the content of the event and assigns a name for each event it sends.

**The Gateway can publish events from itself and on behalf of any device connected via the Gateway**.

When an event is received by the IBM Watson IoT Platform the credentials of the connection on which the event was received are used to determine from which Gateway the event was sent. With this architecture it is impossible for a Gateway to impersonate another device.

Events can be published at any of the three `quality of service levels <../messaging/mqtt.html#/>`__ defined by the MQTT protocol.  By default events will be published as qos level 0.

Publish Gateway event using default quality of service
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
.. code:: C#

    gw.connect();
    gw.publishGatewayEvent("test","{\"temp\":25}");


Publish Gateway event using user-defined quality of service
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Events can be published at higher MQTT quality of service levels, but these events may take slower than QoS level 0, because of the extra confirmation of receipt.

.. code:: C#

  gw.connect();
  gw.publishGatewayEvent("test","{\"temp\":22}",2);


Publishing events from devices
-------------------------------------------------------------------------------

The Gateway can publish events on behalf of any device connected via the Gateway by passing the appropriate typeId and deviceId based on the origin of the event:

.. code:: C#

    string deviceType = "<your device type>";
    string deviceId = "<your device id>";
    string deviceEvent = "<your device event>";
    string deviceEventValue = "{\"temp\":100}"
    gw.publishDeviceEvent(deviceType,deviceId,deviceEvent,deviceEventValue);

One can use the overloaded publishDeviceEvent() method to publish the device event in the desired quality of service. Refer to `MQTT Connectivity for Gateways <https://docs.internetofthings.ibmcloud.com/gateways/mqtt.html>`__ documentation to know more about the topic structure used.

----


Handling commands
-------------------------------------------------------------------------------
The Gateway can subscribe to commands directed at the gateway itself and to any device connected via the gateway. When the Gateway client connects, it automatically subscribes to any commands for this Gateway. But to subscribe to any commands for the devices connected via the Gateway, use one of the overloaded subscribeToDeviceCommands() method, for example,

.. code:: C#

    gw.subscribeToDeviceCommands(deviceType,deviceId);

To process specific commands you need to register a command callback method. The messages are returned as an instance of the Command class which has the following properties:

* ``deviceType`` - The device type for which the command is received.
* ``deviceId`` - The device id for which the command is received, Could be the Gateway or any device connected via the Gateway.
* ``payload`` - The command payload.
* ``format`` - The format of the command payload, currently only JSON format is supported in the C# Client Library.
* ``command`` - The name of the command.


A sample implementation of the Command callback is shown below,

.. code:: C#

      public static void processCommand(string deviceType, string deviceId,string cmdName, string format, string data) {
                 Console.WriteLine("Device Type: "+deviceType+" Device ID: "+deviceId +" Command: " + cmdName  + " format: " + format + " data: " + data);
      }

Once the Command callback is added to the GatewayClient, the processCommand() method is invoked whenever any command is published on the subscribed criteria, The following snippet shows how to add the command call back into GatewayClient instance,

.. code:: C#

  GatewayClient gw =new GatewayClient(orgId,gatewayDeviceType,gatewayDeviceID,authMethod,authToken);
  gw.commandCallback += processCommand;
  gw.connect();


Handling Errors
-------------------------------------------------------------------------------
When errors occur during the validation of the publish or subscribe topic, or during automatic registration, a notification will be sent to the gateway device.
For consuming those notification an callback should be registered, this callback method will be called whenever the notification is received.Callback method has three parameters,

* ``deviceType``
* ``deviceId``
* ``GatewayError object``

GatewayError object contains following properties describing the error occurred,

*    ``Request``: Request type Either publish or subscribe
*    ``Time``: Timestamp in ISO 8601 Format
*    ``Topic``: The request topic from the gateway
*    ``Type``: The device type from the topic
*    ``Id``: The device id from the topic
*    ``Client``: The client id of the request
*    ``RC``: The return code
*    ``Message``: The error message

A sample implementation of the error callback is shown below,

.. code:: C#

  public static void processError(string deviceType, string deviceId,GatewayError err) {
    Console.WriteLine("Device Type: "+deviceType+" Device ID: "+deviceId +" msg:"+ err.Message);
  }

Once the Command callback is added to the GatewayClient, the processError() method is invoked whenever any error notification comes , The following snippet shows how to add the error call back into GatewayClient instance,

.. code:: C#

 GatewayClient gw =new GatewayClient(orgId,gatewayDeviceType,gatewayDeviceID,authMethod,authToken);
 gw.commandCallback += processCommand;
 gw.errorCallback += processError;
 gw.connect();

Refer to the `documentation <https://docs.internetofthings.ibmcloud.com/gateways/mqtt.html#/gateway-notifications#gateway-notifications>`__ for more information about the error notification.
