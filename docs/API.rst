===============================================================
C# Client Library - Watson IoT Platform API Support 
===============================================================
Introduction
-------------------------------------------------------------------------------

This client library describes how to use Watson IoT Platform API with the C# client library. For help with getting started with this module, see `C# Client Library - Introduction <https://github.com/ibm-watson-iot/iot-csharp/blob/master/README.md>`__. 

Constructor
-------------------------------------------------------------------------------

The constructor builds the client instance, and accepts apiKey and authToken as parameters:

* apikey - API key
* authToken - API key token

The following code snippet shows how to construct the ApiClient,

.. code:: c#
    
    using IBMWIoTP;    
    ...
    
    IBMWIoTP.ApiClient client = new IBMWIoTP.ApiClient(apiKey,authToken);    
    ...

----

Organization details
----------------------------------------------------

Application can use method GetOrganizationDetail() to view the Organization details:

.. code:: c#

    var orgDetail = client.GetOrganizationDetail();

Refer to the Organization Configuration section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

----

Bulk device operations
----------------------------------------------------

Applications can use bulk operations to get, add or remove devices in bulk from Internet of Things Platform.

Refer to the Bulk Operations section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

Get Devices in bulk
~~~~~~~~~~~~~~~~~~~

Method GetAllDevices() can be used to retrieve all the registered devices in an organization from Internet of Things Platform, each request can contain a maximum of 512KB. 

.. code:: c#

    var response = client.GetAllDevices();
    

Register Devices in bulk
~~~~~~~~~~~~~~~~~~~

Method RegisterMultipleDevices() can be used to register one or more devices to Internet of Things Platform, each request can contain a maximum of 512KB. For example, the following sample shows how to add a device using the bulk operation.

.. code:: c#

  //Create an array of RegisterDevicesInfo object and add all the devices to be registered
  IBMWIoTP.RegisterDevicesInfo [] bulk = new IBMWIoTP.RegisterDevicesInfo[1];
  var info = new IBMWIoTP.RegisterDevicesInfo();
  info.deviceId="123qwe";
  info.typeId = managedDeviceType;
  info.authToken ="1qaz2wsx3edc4rfv";
  info.deviceInfo = new IBMWIoTP.DeviceInfo();
  info.location = new IBMWIoTP.LocationInfo();
  info.metadata = new {};
  bulk[0] = info;
  ...
  
  //Call the RegisterMultipleDevices method using the created RegisterDevicesInfo object
  boolean status = client.RegisterMultipleDevices(bulk);
  ...
		
		
Delete Devices in bulk
~~~~~~~~~~~~~~~~~~~~~~~~

Method DeleteMultipleDevices() can be used to delete multiple devices from Internet of Things Platform, each request can contain a maximum of 512KB.

.. code:: c#

    // Create an array of IBMWIoTP.DeviceListElement object and add all the devices to be deleted
    IBMWIoTP.DeviceListElement [] removeBulk = new IBMWIoTP.DeviceListElement[1];
    var del = new IBMWIoTP.DeviceListElement();
    del.deviceId ="123qwe";
    del.typeId=managedDeviceType;
    removeBulk[0]=del;
    ...

    //Call the DeleteMultipleDevices method using the created IBMWIoTP.DeviceListElement object
    boolean status = client.DeleteMultipleDevices(removeBulk);
    ...
    
----

Device Type operations
----------------------------------------------------

Applications can use device type operations to list all, create, delete, view and update device types in Internet of Things Platform.

Refer to the Device Types section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

Get all Device Types
~~~~~~~~~~~~~~~~~~~~~~~~

Method GetAllDeviceTypes() can be used to retrieve all the registered device types in an organization from Internet of Things Platform. For example,

.. code:: c#

    var response = client.GetAllDeviceTypes();
    

Add a Device Type
~~~~~~~~~~~~~~~~~~~~~~~~

Method RegisterDeviceType() can be used to register a device type to Internet of Things Platform. For example,

.. code:: c#

    // A sample respresentation of a device type to be added    
    DeviceTypeInfo devty = new DeviceTypeInfo();
    devty.classId="Gateway";
    devty.deviceInfo = new DeviceInfo();
    devty.id = "gatewaypi";
    devty.metadata= new {};
    ...
    
    //Call the RegisterDeviceType method using the created DeviceTypeInfo object
    boolean status = client.RegisterDeviceType(devty)
    ...
        
Delete a Device Type
~~~~~~~~~~~~~~~~~~~~~~~~

Method DeleteDeviceType() can be used to delete a device type from Internet of Things Platform. For example,

.. code:: c#

    boolean status = client.DeleteDeviceType("gatewaypi");
    
Get a Device Type
~~~~~~~~~~~~~~~~~~~~~~~~

In order to retrieve information about a given device type, use the method GetDeviceType() and pass the deviceTypeId as a parameter as shown below,

.. code:: c#

    var response = client.GetDeviceType("gatewaypi");
    
Update a Device Type
~~~~~~~~~~~~~~~~~~~~~~~~

Method UpdateDeviceType() can be used to modify one or more properties of a device type. The properties that needs to be modified should be passed as a parameter. For example, following sample shows how to update the *description* of a device type,

.. code:: c#
    
  var info = new IBMWIoTP.DeviceTypeInfoUpdate();
  info.description="test";
  ...
  
  boolean status = client.UpdateDeviceType("gatewaypi",info);
  ...

----

Device operations
----------------------------------------------------

Applications can use device operations to list, add, remove, view, update, view location and view management information of a device in Internet of Things Platform.

Refer to the Device section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

Get Devices of a particular Device Type
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method ListDevices() can be used to retrieve all the devices of a particular device type in an organization from Internet of Things Platform. For example,

.. code:: c#

    var response = client.ListDevices(managedDeviceType);
    
Add a Device
~~~~~~~~~~~~~~~~~~~~~~~

Method RegisterDevice() can be used to register a device to Internet of Things Platform. For example,

.. code:: c#

    // A sample respresentation of different properties of a Device to be added    
    string newDeviceId= DateTime.Now.ToString("yyyyMMddHHmmssffff");
    var newdevice = new RegisterSingleDevicesInfo();
    newdevice.deviceId = newDeviceId;
    newdevice.authToken = "testtest";
    newdevice.deviceInfo = new IBMWIoTP.DeviceInfo();
    newdevice.location = new IBMWIoTP.LocationInfo();
    newdevice.metadata = new {};
    ...
    
    //Call the RegisterDevice method using the created RegisterSingleDevicesInfo object
    boolean status = client.RegisterDevice("gatewaypi",newdevice);
    ...
    
Delete a Device
~~~~~~~~~~~~~~~~~~~~~~~~

Method UnregisterDevice() can be used to delete a device from Internet of Things Platform. For example,

.. code:: c#

    boolean status = client.UnregisterDevice("gatewaypi",newDeviceId);
    
Get a Device
~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceInfo() can be used to retrieve a device from Internet of Things Platform. For example,

.. code:: c#

    var response = client.GetDeviceInfo("gatewaypi",newDeviceId);
    
Update a Device
~~~~~~~~~~~~~~~~~~~~~~~~

Method UpdateDeviceInfo() can be used to modify one or more properties of a device. For example, following sample shows how to update a device metadata,

.. code:: c#
    
    var update  = new IBMWIoTP.UpdateDevicesInfo();
    update.deviceInfo =new IBMWIoTP.DeviceInfo();
    ...
    
    boolean status = client.UpdateDeviceInfo("gatewaypi",newDeviceId,update);
    ...

Get Location Information
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceLocationInfo() can be used to get the location information of a device. For example, 

.. code:: c#
    
    var response = client.GetDeviceLocationInfo("gatewaypi",newDeviceId);

Update Location Information
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method UpdateDeviceLocationInfo() can be used to modify the location information for a device. For example,

.. code:: c#
    
    var loc = new IBMWIoTP.LocationInfo();
    loc.accuracy=1;
    loc.measuredDateTime = DateTime.Now.ToString("o");
    ...
    
    var status = client.UpdateDeviceLocationInfo("gatewaypi",newDeviceId,loc);
    ...

Get Device Management Information
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceManagementInfo() can be used to get the device management information for a device. For example, 

.. code:: c#
    
    var response = client.GetDeviceManagementInfo(managedDeviceType,managedDeviceId);

----

Device diagnostic operations
----------------------------------------------------

Applications can use Device diagnostic operations to clear logs, retrieve logs, add log information, delete logs, get specific log, clear error codes, get device error codes and add an error code to Internet of Things Platform.

Refer to the Device Diagnostics section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

Get Diagnostic logs
~~~~~~~~~~~~~~~~~~~~~~

Method GetAllDiagnosticLogs() can be used to get all diagnostic logs of the device. For example,

.. code:: c#

    var response = client.GetAllDiagnosticLogs(managedDeviceType,managedDeviceId);
    
Clear Diagnostic logs 
~~~~~~~~~~~~~~~~~~~~~~

Method ClearAllDiagnosticLogs() can be used to clear the diagnostic logs of the device. For example,

.. code:: c#

    boolean status = client.ClearAllDiagnosticLogs(managedDeviceType,managedDeviceId);
    
Add a Diagnostic log
~~~~~~~~~~~~~~~~~~~~~~

Method AddDeviceDiagLogs() can be used to add an entry in the log of diagnostic information for the device. The log may be pruned as the new entry is added. For example,

.. code:: c#

    Console.WriteLine("AddDeviceDiagLogs");
    var log =new IBMWIoTP.LogInfo();
    log.message="test";
    log.severity =1;
    ...
    
    client.AddDeviceDiagLogs(managedDeviceType,managedDeviceId,log);
    ...

Get a Diagnostic log
~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDiagnosticLog() can be used to retrieve a diagnostic log based on the log id. For example,

.. code:: c#

    var log = client.GetDiagnosticLog(managedDeviceType,managedDeviceId,"<logid>");
    
Delete a Diagnostic log
~~~~~~~~~~~~~~~~~~~~~~~~~~

Method DeleteDiagnosticLog() can be used to delete a diagnostic log based on the log id. For example,

.. code:: c#

    client.DeleteDiagnosticLog(managedDeviceType,managedDeviceId,"<logid>");
    

Clear Diagnostic ErrorCodes
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method ClearDeviceErrorCodes() can be used to clear the list of error codes of the device. The list is replaced with a single error code of zero. For example,

.. code:: c#

    client.ClearDeviceErrorCodes(managedDeviceType,managedDeviceId);
    
Get Diagnostic ErrorCodes
~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceErrorCodes() can be used to retrieve all diagnostic ErrorCodes of the device. For example,

.. code:: c#

    var response = client.GetDeviceErrorCodes(managedDeviceType,managedDeviceId);

Add a Diagnostic ErrorCode
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method AddErrorCode() can be used to add an error code to the list of error codes for the device. The list may be pruned as the new entry is added. For example,

.. code:: c#

    var err =  new IBMWIoTP.ErrorCodeInfo();
    err.errorCode = 0;
    err.timestamp =  "";
    ...
    
    client.AddErrorCode(managedDeviceType,managedDeviceId,err);
    ...

----

Connection problem determination
----------------------------------

Method GetDeviceConnectionLogs() can be used to list connection log events for a device to aid in diagnosing connectivity problems. The entries record successful connection, unsuccessful connection attempts, intentional disconnection and server-initiated disconnection.

.. code:: c#

    var response = client.GetDeviceConnectionLogs(managedDeviceType,managedDeviceId);

Refer to the Problem Determination section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

----

Device Management request operations
----------------------------------------------------

Applications can use the device management operations to list all device management requests, initiate a request, clear request status, get details of a request, get list of request statuses for each affected device and get request status for a specific device.

Refer to the Device Management Requests section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

Get all Device management requests
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetAllDeviceManagementRequests() can be used to retrieve the list of device management requests, which can be in progress or recently completed. For example,

.. code:: c#

    var response = client.GetAllDeviceManagementRequests();
    
Initiate a Device management request
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method InitiateDeviceManagementRequest() can be used to initiate a device management request, such as reboot. For example,

.. code:: c#

    IBMWIoTP.DeviceMgmtparameter [] param = new IBMWIoTP.DeviceMgmtparameter[1];
    IBMWIoTP.DeviceMgmtparameter p = new IBMWIoTP.DeviceMgmtparameter();
    p.name="rebootAfter";
    p.value = "100";
    param[0] = p;
    IBMWIoTP.DeviceListElement [] deviceList = new IBMWIoTP.DeviceListElement[1];
    IBMWIoTP.DeviceListElement ele = new IBMWIoTP.DeviceListElement();
    ele.typeId = managedDeviceType;
    ele.deviceId= managedDeviceId;
    deviceList[0] = ele;
    ...
    
    var response =client.InitiateDeviceManagementRequest("device/reboot",param,deviceList);
    ...

Delete a Device management request
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method DeleteDeviceManagementRequest() can be used to clear the status of a device management request. Application can use this operation to clear the status of a completed request, or an in-progress request which may never complete due to a problem. For example,

.. code:: c#

    // Pass the Request ID of a device management request
    boolean status = client.DeleteDeviceManagementRequest(id);
    
Get details of a Device management request
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceManagementRequest() can be used to get the details of the device management request. For example,

.. code:: c#

    // Pass the Request ID of a device management request
    var details = client.GetDeviceManagementRequest(id);
    

Get status of a Device management request
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceManagementRequestStatus() can be used to get a list of device management request device statuses. For example,

.. code:: c#

    // Pass the Request ID of a device management request
    var details = client.GetDeviceManagementRequestStatus(id);

Get status of a Device management request by Device
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDeviceManagementRequestStatus() can also be overloaded to get an individual device management request device status. For example,

.. code:: c#

    // Pass the Request ID of a device management request along with Device type & Id
    var details = client.GetDeviceManagementRequestStatus(id,devicetypeId,deviceId);

----

Usage management
----------------------------------------------------

Applications can use the usage management operations to retrieve the number of active devices over a period of time, retrieve amount of storage used by historical event data, retrieve total amount of data used.

Refer to the Usage management section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the list of query parameters, the request & response model and http status code.

Get data traffic
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetDataUsage() can be used to retrieve the amount of data used for a specified period of time. For example,

.. code:: c#
    
    var response = client.GetDataUsage("2016","2016",false);

----

Service status
----------------------------------------------------

Method GetServiceStatus() can be used to retrieve the organization-specific status of each of the services offered by the Internet of Things Platform. 

.. code:: c#
    
    var response = client.GetServiceStatus();

Refer to the Service status section of the `IBM Watson IoT Platform API <https://docs.internetofthings.ibmcloud.com/swagger/v0002.html>`__ for information about the response model and http status code.

----

Last event cache
----------------------------------------------------

Applications can use the Watson IoT Platform Last Event Cache operations to retrieve the last event that was sent by a device. You can retrieve the last recorded value of an event ID for a specific device, or the last recorded value for each event ID that was reported by a specific device. Last event data of a device can be retrieved for any specific event that occurred up to 365 days ago.


Refer to the Last event cache of the `IBM Watson IoT Platform API <https://console.bluemix.net/docs/services/IoT/devices/api.html#api>`__ for information.

Get last event of a specific device
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetLastEvents() can be used to retrieve the last cached event from a specific device. For example,

.. code:: c#

    client.GetLastEvents(managedDeviceType,managedDeviceId);
    
Get last event of a specific device of given event type
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Method GetLastEventsByEventType() can be used to retrieve the last cached event from a specific device of given event type. For example,

.. code:: c#

    client.GetLastEventsByEventType(managedDeviceType,managedDeviceId,"test");
