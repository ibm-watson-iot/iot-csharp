======================================
C# Client Library - Managed Device
======================================
- See `iot-csharp <https://github.com/ibm-messaging/iot-csharp>`_ in GitHub


----

Introduction
-------------

This client library describes how to use devices with the C# IBMWIoTP client library. For a basic introduction to the broader module, see `C# Client Library - Introduction <https://github.com/ibm-messaging/iot-Csharp>`__.

This section contains information on how devices can connect to the IBM Watson IoT Platform Device Management service using C# and perform device management operations like firmware update, location update, and diagnostics update.

The Device section contains information on how devices can publish events and handle commands using the C# IBMWIoTP Client Library.

The Applications section contains information on how applications can use the C# IBMWIoTP Client Library to interact with devices.


Device Management
-------------------------------------------------------------------------------
The `device management <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/index.html>`__ feature enhances the IBM Watson IoT Platform Connect service with new capabilities for managing devices. Device management makes a distinction between managed and unmanaged devices:

* **Managed Devices** are defined as devices which have a management agent installed. The management agent sends and receives device metadata and responds to device management commands from the IBM Watson IoT Platform Connect.
* **Unmanaged Devices** are any devices which do not have a device management agent. All devices begin their life-cycle as unmanaged devices, and can transition to managed devices by sending a message from a device management agent to the IBM Watson IoT Platform Connect.


Connecting to the IBM Watson IoT Platform Connect Device Management Service
---------------------------------------------------------------------------

Create DeviceData
------------------------------------------------------------------------
The `device model <https://docs.internetofthings.ibmcloud.com/reference/device_model.html>`__ describes the metadata and management characteristics of a device. The device database in the IBM Watson IoT Platform Connect is the master source of device information. Applications and managed devices are able to send updates to the database such as a location or the progress of a firmware update. Once these updates are received by the IBM Watson IoT Platform Connect, the device database is updated, making the information available to applications.

The device model in the IBMWIoTP client library is represented as DeviceInfo.

The following code snippet shows how to create the mandatory object DeviceInfo along with the DeviceManagement:

.. code:: C#

  DeviceInfo simpleDeviceInfo = new DeviceInfo();
  simpleDeviceInfo.description = "My device";
  simpleDeviceInfo.deviceClass = "My device class";
  simpleDeviceInfo.manufacturer = "My device manufacturer";
  simpleDeviceInfo.fwVersion = "Device Firmware Version";
  simpleDeviceInfo.hwVersion = "Device HW Version";
  simpleDeviceInfo.model = "My device model";
  simpleDeviceInfo.serialNumber = "<your device id>5";

  DeviceManagement deviceClient = new DeviceManagement(orgID,deviceType,deviceId,authType,authKey,isSync);
  deviceClient.deviceInfo = simpleDeviceInfo;
  deviceClient.connect();

Construct DeviceManagement
-------------------------------------------------------------------------------
DeviceManagement - A device class that connects the device as managed device to IBM Watson IoT Platform Connect and enables the device to perform one or more Device Management operations. Also the DeviceManagement instance can be used to do normal device operations like publishing device events and listening for commands from application.

DeviceManagement exposes the following constructor to support different user patterns by accepting the following

Constructs a DeviceManagement instance by accepting the following parameters,

* ``org`` - Your organization ID.
* ``type`` - The type of your device.
* ``id`` - The ID of your device.
* ``auth-method`` - Method of authentication (The only value currently supported is "token").
* ``auth-token`` - Auth Token

And ``isSync`` as optional parameters which in case of true all managed request will be performed synchronously.

All these properties are required to interact with the IBM Watson IoT Platform Connect.

The following code shows how to create a DeviceManagement instance:

.. code:: C#

	string orgID = "<your org id>";
	string deviceType = "<your device type>";
	string deviceId = "<your gateway auth token>";
	string authType = "token";
	string authKey = "<your auth key >";
	bool isSync = true;

	DeviceManagement deviceClient = new DeviceManagement(orgID,deviceType,deviceId,authType,authKey,isSync);
	deviceClient.deviceInfo = simpleDeviceInfo;
	deviceClient.connect();

Register Callback
------------------------------------------------
In order to track the response of the management request we need to register a call back method.When ever an response for the manage request comes this call back function is called with two parameters

* request Id - To identify the management request
* response status - status of response

Each device management request method will return an unique request id which helps to identify the corresponding response.
Following are the status code for the device Management response,

* 200: The operation was successful.
* 400: The input message does not match the expected format, or one of the values is out of the valid range.
* 404: The topic name is incorrect, or the device is not in the database.
* 409: A conflict occurred during the device database update. To resolve this, simplify the operation is necessary.

The following code shows how to create a callback instance:

.. code:: C#

	DeviceManagement deviceClient = new DeviceManagement(orgID,deviceType,deviceId,authType,authKey,isSync);
	deviceClient.deviceInfo = simpleDeviceInfo;
	deviceClient.mgmtCallback += processMgmtResponce;
	deviceClient.connect();

	.........
	.........
	.........

	public static void processMgmtResponce( string reqestId, string responceCode){
		Console.WriteLine("req Id:" + reqestId +"responceCode:"+ responceCode);
	}


Manage
------------------------------------------------------------------
The device can invoke manage() method to participate in device management activities, manage method will take following parameters,

* lifeTime - The timeframe specifies the length of time within which the device must send another **Manage device** request in order to avoid being reverted to an unmanaged device and marked as dormant.
* supportDeviceActions - bool value for the device action support of the device.
* supportFirmwareActions -  bool value for the Firmware action support of the device.
* metaData(Optional) - meta data object of the device that provide device meta information

.. code:: C#

    deviceClient.manage(4000,true,true);

with meta data object:

.. code:: C#

      deviceClient.manage(4000,true,true,new{Key=""});

Refer to the `documentation <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/index.html#/manage-device#manage-device>`__ for more information about the manage operation.

Unmanage
-----------------------------------------------------

A device can invoke unmanage() method when it no longer needs to be managed. The Internet of Things Platform Connect will no longer send new device management requests to this device and all device management requests from this device will be rejected other than a **Manage device** request.

.. code:: C#

	deviceClient.unmanage();

Refer to the `documentation <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/index.html#/unmanage-device#unmanage-device>`__ for more information about the Unmanage operation.

Location Update
-----------------------------------------------------

Devices that can determine their location can choose to notify the Internet of Things Platform Connect about location changes. In order to update the location, the device needs to call setLocation method in client object with longitude,latitude ,elevation and accuracy as parameters.

.. code:: C#

  deviceClient.setLocation(77.5667,12.9667, 0,10);

Refer to the `documentation <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/index.html#/update-location#update-location>`__ for more information about the Location update.

Append/Clear ErrorCodes
-----------------------------------------------

Devices can choose to notify the Internet of Things Platform Connect about changes in their error status. In order to send the ErrorCodes the device needs to call addErrorCode() method in client object  as follows:

.. code:: C#

	deviceClient.addErrorCode(12);

Also, the ErrorCodes can be cleared from Internet of Things Platform Connect by calling the clearErrorCodes() method as follows:

.. code:: C#

  deviceClient.clearErrorCode();

Refer to the `documentation <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/index.html#/add-error-code#add-error-code>`__ for more information about the error code operations.

Append/Clear Log messages
-----------------------------
Devices can choose to notify the Internet of Things Platform Connect about changes by adding a new log entry. Log entry includes a log messages and severity, as well as an optional base64-encoded binary diagnostic data as string. In order to send log messages, the device needs to to call addLog() method in client object as follows:

.. code:: C#

  deviceClient.addLog("test","data",1);

Also, the log messages can be cleared from Internet of Things Platform Connect by calling the clear method as follows:

.. code:: C#

  deviceClient.clearLog()

The device diagnostics operations are intended to provide information on device errors, and does not provide diagnostic information relating to the devices connection to the Internet of Things Platform Connect.

Refer to the `documentation <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/index.html#/add-log#add-log>`__ for more information about the log operations.


----

Firmware Actions
-------------------------------------------------------------
The firmware update process is separated into two distinct actions:

* Downloading Firmware
* Updating Firmware.

The device needs to do the following activities to support Firmware Actions:

**1. Inform the server about the Firmware action support**

The device needs to set the firmware action flag to true in order for the server to initiate the firmware request. This can be achieved by invoking the manage() method with a true value for supportFirmwareActions parameter,

.. code:: C#

    deviceClient.manage(4000,false,true);

Once the support is informed to the DM server, the server then forwards the firmware actions to the device.

**2. Create the Firmware Action Handler**

In order to support the Firmware action, the device needs to create a handler and add it to ManagedDevice.

.. code:: C#

  public delegate void processFirmwareAction(string action,DeviceFirmware firmware){
  ...
  };

**3.1 Sample implementation of downloadFirmware**

The implementation must create a separate thread and add a logic to download the firmware and report the status of the download via DeviceManagement object. If the Firmware Download operation is successful, then the state of the firmware to be set to DOWNLOADED and UpdateStatus should be set to SUCCESS.

If an error occurs during Firmware Download the state should be set to IDLE and updateStatus should be set to one of the error status values:

* UPDATESTATE_OUT_OF_MEMORY
* UPDATESTATE_CONNECTION_LOST
* UPDATESTATE_INVALID_URI
* UPDATESTATE_VERIFICATION_FAILED

A sample Firmware Download implementation is shown below:

.. code:: C#

  public  void processFirmwareAction (string action , DeviceFirmware fw){
        if(action == "download"){
          deviceClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADING);
          Console.WriteLine("Start downloading new Firmware form "+fw.uri);
          //perform your firmware download
          Thread.Sleep(2000);
          Console.WriteLine("completed Download");
          deviceClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);

        }
        if(action == "update"){
          deviceClient.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
          Console.WriteLine("Start Updateting new Firmware ");
          //perform your firmware download
          Thread.Sleep(2000);
          Console.WriteLine("Updated new Firmware ");
          deviceClient.setUpdateState(DeviceManagement.UPDATESTATE_SUCCESS);
        }
      };

Device can check the integrity of the downloaded firmware image using the verifier and report the status back to IBM Watson Internet of Things Platform. The verifier can be set by the device during the startup (while creating the DeviceManagement object) or as part of the Download Firmware request by the application.

The complete code can be found in the device management sample `DeviceManagement samples <https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/DeviceManagement>`__.

**3.2 Sample implementation of updateFirmware**

The implementation must create a separate thread and add a logic to install the downloaded firmware and report the status of the update via DeviceManagement object. If the Firmware Update operation is successful, then the state of the firmware should to be set to IDLE and UpdateStatus should be set to SUCCESS.

If an error occurs during Firmware Update, updateStatus should be set to one of the error status values:

* UPDATESTATE_OUT_OF_MEMORY
* UPDATESTATE_UNSUPPORTED_IMAGE

A sample Firmware Update implementation is shown below:

.. code:: C#

  public void processFirmwareAction (string action , DeviceFirmware fw){
      if(action == "download"){
        deviceClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADING);
        Console.WriteLine("Start downloading new Firmware form "+fw.uri);
        //perform your firmware download
        Thread.Sleep(2000);
        Console.WriteLine("completed Download");
        deviceClient.setState(DeviceManagement.UPDATESTATE_DOWNLOADED);

      }
      if(action == "update"){
        deviceClient.setUpdateState(DeviceManagement.UPDATESTATE_IN_PROGRESS);
        Console.WriteLine("Start Updateting new Firmware ");
        //perform your firmware download
        Thread.Sleep(2000);
        Console.WriteLine("Updated new Firmware ");
        deviceClient.setUpdateState(DeviceManagement.UPDATESTATE_SUCCESS);
      }
    };


The complete code can be found in the device management sample `DeviceManagement samples <https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/DeviceManagement>`__.

**4. Add the handler to ManagedDevice**

The created handler needs to be added to the ManagedDevice instance so that the WIoTP client library invokes the corresponding method when there is a Firmware action request from IBM Watson Internet of Things Platform.

.. code:: C#

	deviceClient.fwCallback +=processFirmwareAction

Refer to `this page <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/requests.html#/firmware-actions#firmware-actions>`__ for more information about the Firmware action.

----

Device Actions
------------------------------------
The IBM Watson Internet of Things Platform supports the following device actions:

* Reboot
* Factory Reset

The device needs to do the following activities to support Device Actions:

**1. Inform server about the Device Actions support**

In order to perform Reboot and Factory Reset, the device needs to inform the IBM Watson Internet of Things Platform about its support first. This can be achieved by invoking the sendManageRequest() method with a true value for supportDeviceActions parameter,

.. code:: C#
	// Second parameter represents the device action support
  deviceClient.manage(4000,true,false);

Once the support is informed to the DM server, the server then forwards the device action requests to the device.

**2. Create the Device Action Handler**

In order to support the device action, the device needs to create a handler and add it to ManagedDevice.

.. code:: C#

  public void processDeviceAction( string reqestId,string action){

  }

**2 Sample implementation of handles**

The implementation must create a separate thread and add a logic to reboot or reset the device and report the status of the reboot via deviceClient object. Upon receiving the request, the device first needs to inform the server about the support(or failure) before proceeding with the actual reboot or reset . And if the device can not reboot the device or any other error during the reboot or reset, the device can update the status along with an optional message. A sample reboot implementation of a device is shown below:

.. code:: C#

  public void processDeviceAction( string reqestId,string action){

      Console.WriteLine("req Id:" + reqestId +"	Action:"+ action +" called");
      if(action == "reboot"){
      deviceClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_ACCEPTED,"");

      Thread.Sleep(2000);
      deviceClient.disconnect();

      Console.WriteLine("disconnected");
      Thread.Sleep(5000);

      Console.WriteLine("Re connected");
      deviceClient.connect();

      deviceClient.manage(4000,true,true);
      }
      if(action == "reset"){
      deviceClient.sendResponse(reqestId,DeviceManagement.RESPONSECODE_FUNCTION_NOT_SUPPORTED,"");
      }
  }


The complete code can be found in the device management sample `DeviceManagement samples <https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/DeviceManagement>`__.

**3. Add the handler to ManagedDevice**

The created handler needs to be added to the ManagedDevice instance so that the WIoTP client library invokes the corresponding method when there is a device action request from IBM Watson Internet of Things Platform.

.. code:: C#

	deviceClient.actionCallback += processDeviceAction;

Refer to `this page <https://docs.internetofthings.ibmcloud.com/devices/device_mgmt/requests.html#/device-actions-reboot#device-actions-reboot>`__ for more information about the Device Action.

----
