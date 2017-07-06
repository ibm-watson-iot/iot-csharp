C# for Device Developers
========================

- See `iot-csharp <https://github.com/ibm-messaging/iot-csharp>`_ in GitHub


----


Constructor
-----------
The constructor builds the client instance, and there are two different ways of connecting a device to IBM Watson IoT Platform. Accordingly, we have two overloaded constructors for that

**Quick Start**

To connect device in quick start mode, we need to create client instance with two arguments
* device Type
* device id

.. code:: C#

	DeviceClient deviceClient = new DeviceClient(deviceType, uuid);
 	deviceClient.connect();


**Registered**

To connect device in Registered mode,we need to create client instance with the following definitions:

- ``orgId`` - Your organization ID.
- ``deviceType`` - The type of your device.
- ``deviceId`` - The ID of your device.
- ``auth-method`` - Method of authentication (the only value currently supported is "token").
- ``auth-token`` - API key token (required if auth-method is "token").


.. code:: C#

	deviceClient = new DeviceClient(orgId,deviceType,deviceId,"token",authToken);
	deviceClient.connect();



Publishing events
------------------
Events are the mechanism by which devices publish data to the IoT Platform. The device controls the content of the event and assigns a name for each event it sends.

When an event is received by the IoT Platform the credentials of the connection on which the event was received are used to determine which device sent the event. With this architecture, it is impossible for a device to impersonate another device.

Events can be published at any of the three `quality of service (QoS) levels <../mqtt.html#/qoslevels>`_, defined by the MQTT protocol. By default events will be published as QoS level 0.

Publish event using default quality of service
----------------------------------------------
.. code:: C#

	deviceClient.connect();
	deviceClient.publishEvent("event", "json", "{temp:23}");


Publish event using user-defined quality of service
-----------------------------------------------------
Events can be published at higher MQTT quality of service levels, but events published at QoS levels above 0 may be slower then QoS 0 events, because of the extra confirmation of receipt used in QoS levels above 0.

.. code:: C#

	deviceClient.connect();
	deviceClient.publishEvent("event", "json", "{temp:23}", 2);


Handling commands
-------------------
To process specific commands, you must register a command callback method also subscribe to that command.


.. code:: C#

	public static void processCommand(string cmdName, string format, string data) {
	    ...
	}
        ...
	deviceClient.connect();
	deviceClient.subscribeCommand("your command name", "your command format", 0);
	deviceClient.commandCallback += processCommand;

If you want to subscribe to all the commands of all the type we need to use '+' for command name and format.
