C# for Application Developers
=============================

- See `iot-csharp <https://github.com/ibm-messaging/iot-csharp>`_ in GitHub


----


Constructor
------------

The constructor builds the client instance, and accepts arguments containing the following definitions:

- ``orgId`` - Your organization ID.
- ``appId`` - The unique ID of your application within your organization.
- ``auth-key`` - API key (required if auth-method is ``apikey``).
- ``auth-token`` - API key token (required if auth-method is ``apikey``).

If only ``appId`` is provided, the client will connect to the IoT Platform Quickstart service and default to an unregistered device. The argument lists create definitions which are used to interact with the IoT Platform module.

.. code:: C#

       ApplciationClient applicationClient = new ApplciationClient(orgId, appId, apiKey, authToken);
       applicationClient.connect();


----


Subscribing to device events
-----------------------------

Events are the mechanism by which devices publish data to the IoT Platform. The device controls the content of the event and assigns a name for each event it sends.

When an event is received by the IoT Foundation the credentials of the connection on which the event was received are used to determine from which device the event was sent. With this architecture it is impossible for a device to impersonate another device.

By default, applications will subscribe to all events from all connected devices. Use the type, id, event and msgFormat parameters to control the scope of the subscription. A single client can support multiple subscriptions. The code samples below give examples of how to subscribe to devices dependent on device type, id, event and msgFormat parameters.

To subscribe to all events from all devices
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: C#

        applicationClient.connect();
        applicationClient.subscribeToDeviceEvents();


To subscribe to all events from a device
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: C#

        applicationClient.connect();
        applicationClient.subscribeToDeviceEvents(deviceType,deviceId);



To subscribe to a specific event from two or more different devices
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: C#

        applicationClient.connect();
        applicationClient.subscribeToDeviceEvents(deviceType, deviceId, evtName);


To subscribe to all events published by a device in json format
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: C#

    applicationClient.connect();
    applicationClient.subscribeToDeviceEvents(deviceType, deviceId, evtName, eventFormat, qualityOfServies);


Handling events from devices
~~~~~~~~~~~~~~~~~~~~~~~~~~~~

To process the events received by your subscriptions you need to register an event callback method.

- ``device Type `` - string (identifies the device type across all types of devices in the organization)
- ``device Id `` - string (uniquely identifies the device across all devices of the above type in the organization)
- ``eventName`` - string
- ``eventFormat`` - string
- ``eventData`` - string

.. code:: C#

    public static void processEvent(string deviceType,string deviceId ,String eventName, string format, string data) {
        // Do something
    }

    applicationClient.connect();
    applicationClient.eventCallback += processEvent;
    applicationClient.subscribeToDeviceEvents();


----


Subscribing to device status
----------------------------

By default, this will subscribe to status updates for all connected devices. Use the type and id parameters to control the scope of the subscription. A single client can support multiple subscriptions.

Subscribe to status updates for all devices
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: C#

   applicationClient.connect();
   applicationClient.deviceStatusCallback  += processDeviceStatus;
   applicationClient.subscribeToDeviceStatus();


Subscribe to status updates for two different devices
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: C#

    applicationClient.connect();
    applicationClient.deviceStatusCallback += processDeviceStatus;
    applicationClient.subscribeToDeviceStatus(deviceType, deviceId);


Handling status updates from devices
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
To process the status updates received by your subscriptions you need to register an event callback method.

.. code:: C#

    public static void processDeviceStatus(String deviceType, string deviceId, string data)
        {
           //
        }


    applicationClient.connect();

    applicationClient.appStatusCallback += processAppStatus;
    applicationClient.subscribeToApplicationStatus();


----

Publishing events from devices
------------------------------

Applications can publish events as if they originated from a Device.

.. code:: C#

     applicationClient.connect();
     applicationClient.publishEvent(deviceType, deviceId, evt, data, qualityOfServies);


----


Publishing commands to devices
------------------------------

Applications can publish commands to connected devices.

.. code:: C#

     applicationClient.connect();
     applicationClient.publishCommand(deviceType, deviceId, cmdName, cmdFormat, data, qualityOfServies);
