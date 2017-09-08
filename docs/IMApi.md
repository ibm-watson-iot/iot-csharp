C# Client Library - Watson IoT Platform Information Management Support
===============================================================
Introduction
-------------------------------------------------------------------------------

This client library describes how to use Watson IoT Platform API with the C# client library. For help with getting started with this module, see [C# Client Library - Introduction](https://github.com/ibm-watson-iot/iot-csharp/blob/master/README.md).

This section contains information about the Information Management capabilities of IBM Watson IoT Platform help you to organize and integrate data coming in to and going out of Watson IoT Platform.

Constructor
------------

The constructor builds the client instance, and accepts apiKey and authToken as parameters:

* apikey - API key
* authToken - API key token

The following code snippet shows how to construct the ApiClient,

```csharp

using IBMWIoTP;    
...

IBMWIoTP.ApiClient client = new IBMWIoTP.ApiClient(apiKey,authToken);    
```


## Schema :

### GetAllActiveSchemas
#### ` SchemaCollection GetAllActiveSchemas()`

Query active schema definitions,Schemas are used to define the structure of Events, Device State and Thing State in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas)]

#### Returns
SchemaCollection object

### GetActiveSchemaMetadata
#### ` SchemaInfo GetActiveSchemaMetadata(string id)`

Retrieves the metadata for the active schema definition with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas_schemaId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas_schemaId)]

#### Parameters
* `id` String value of Schema id

#### Returns
SchemaInfo object

### GetActiveSchemaContent
#### ` dynamic GetActiveSchemaContent(string id)`

Retrieves the content of the active schema definition file with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas_schemaId_content](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_schemas_schemaId_content)]

#### Parameters
* `id` String value of Schema id

#### Returns
dynamic object

### GetAllDraftSchemas
#### ` SchemaCollection GetAllDraftSchemas()`

Query draft schema definitions For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas)]

#### Returns
SchemaCollection object

### AddDraftSchema
#### ` SchemaInfo AddDraftSchema(SchemaDraft draft)`

Create a draft schema definition For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/post_draft_schemas](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/post_draft_schemas)]

#### Parameters
* `draft` SchemaDraft object filled with valid properties

#### Returns
SchemaInfo object

### DeleteDraftSchema
#### ` void DeleteDraftSchema(string id)`

Deletes the draft schema definition with the specified id from the organization in the Watson IoT Platform. Deleting the schema definition deletes both the metadata and the actual schema definition file from the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/delete_draft_schemas_schemaId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/delete_draft_schemas_schemaId)]

#### Parameters
* `id` String value of Schema id

### GetDraftSchemaMetadata
#### ` SchemaInfo GetDraftSchemaMetadata(string id)`

Retrieves the metadata for the draft schema definition with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas_schemaId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas_schemaId)]

#### Parameters
* `id` String value of Schema id

#### Returns
SchemaInfo object

### UpdateDraftSchemaMetadata
#### ` SchemaInfo UpdateDraftSchemaMetadata(SchemaInfo updated)`

Updates the metadata for the draft schema definition with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/put_draft_schemas_schemaId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/put_draft_schemas_schemaId)]

#### Parameters
* `updated` updated SchemaInfo object

#### Returns
SchemaInfo object

### GetDraftSchemaContent
#### ` dynamic GetDraftSchemaContent(string id)`

Retrieves the content of the draft schema definition file with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas_schemaId_content](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/get_draft_schemas_schemaId_content)]

#### Parameters
* `id` String value of Schema id

#### Returns
dynamic object

### UpdateDraftSchemaContent
#### ` SchemaInfo UpdateDraftSchemaContent(string schemaId,string schemaFilePath)`

Updates the content of a draft schema definition file with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/put_draft_schemas_schemaId_content](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Schemas/put_draft_schemas_schemaId_content)]

#### Parameters
* `schemaId` String value of Schema id

* `schemaFilePath` String value of schema file path

#### Returns
SchemaInfo object

## PhysicalInterfaces :

### GetAllActivePhysicalInterfaces
#### ` PhysicalInterfacesCollection GetAllActivePhysicalInterfaces()`

Query active phyiscal interfaces For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces)]

#### Returns
PhysicalInterfacesCollection object

### GetActivePhysicalInterfaces
#### ` PhysicalInterfacesInfo GetActivePhysicalInterfaces(string id)`

Retrieve the active physical interface with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces_physicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces_physicalInterfaceId)]

#### Parameters
* `id` String value of physicalInterface Id

#### Returns
PhysicalInterfacesInfo object

### GetActivePhysicalInterfacesEvents
#### ` EventTypeBind[] GetActivePhysicalInterfacesEvents(string id)`

Retrieve the list of event mappings for the active physical interface. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces_physicalInterfaceId_events](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_physicalinterfaces_physicalInterfaceId_events)]

#### Parameters
* `id` String value of physicalInterface Id

#### Returns
Array of EventTypeBind object

### GetAllDraftPhysicalInterfaces
#### ` PhysicalInterfacesCollection GetAllDraftPhysicalInterfaces()`

Query draft phyiscal interfaces For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces)]

#### Returns
PhysicalInterfacesCollection object

### AddDraftPhysicalInterfaces
#### ` PhysicalInterfacesInfo AddDraftPhysicalInterfaces(PhysicalInterfaceDraft draft)`

Creates a new draft physical interface for the organization in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/post_draft_physicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/post_draft_physicalinterfaces)]

#### Parameters
* `draft` PhysicalInterfaceDraft object

#### Returns
PhysicalInterfacesInfo object

### GetDraftPhysicalInterfaces
#### ` PhysicalInterfacesInfo GetDraftPhysicalInterfaces(string id)`

Retrieve the draft physical interface with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces_physicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces_physicalInterfaceId)]

#### Parameters
* `id` String value of physicalInterface Id

#### Returns
PhysicalInterfacesInfo object

### DeleteDraftPhysicalInterfaces
#### ` void DeleteDraftPhysicalInterfaces(string id)`

Deletes the draft physical interface with the specified id from the organization in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/delete_draft_physicalinterfaces_physicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/delete_draft_physicalinterfaces_physicalInterfaceId)]

#### Parameters
* `id` String value of physicalInterface Id

### UpdateDraftPhysicalInterfaces
#### ` PhysicalInterfacesInfo UpdateDraftPhysicalInterfaces(PhysicalInterfacesInfo draft)`

Updates the draft physical interface with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/put_draft_physicalinterfaces_physicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/put_draft_physicalinterfaces_physicalInterfaceId)]

#### Parameters
* `draft` updated PhysicalInterfacesInfo object

#### Returns
PhysicalInterfacesInfo object

### MapDraftPhysicalInterfacesEvent
#### ` EventTypeBind MapDraftPhysicalInterfacesEvent(string id,EventTypeBind evnt)`

Maps an event id to a specific event type for the draft specified physical interface. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/post_draft_physicalinterfaces_physicalInterfaceId_events](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/post_draft_physicalinterfaces_physicalInterfaceId_events)]

#### Parameters
* `id` String value of physicalInterface Id

* `evnt` EventTypeBind object for the event

#### Returns
EventTypeBind object

### GetAllDraftPhysicalInterfacesMappedEvents
#### ` EventTypeBind[] GetAllDraftPhysicalInterfacesMappedEvents(string id)`

Retrieve the list of event mappings for the draft physical interface. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces_physicalInterfaceId_events](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/get_draft_physicalinterfaces_physicalInterfaceId_events)]

#### Parameters
* `id` String value of physicalInterface Id

#### Returns
Array of EventTypeBind object

### DeleteDraftPhysicalInterfacesMappedEvents
#### ` void DeleteDraftPhysicalInterfacesMappedEvents(string id,string eventId)`

Removes the event mapping with the specified id from the draft physical interface. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/delete_draft_physicalinterfaces_physicalInterfaceId_events_eventId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Physical_Interfaces/delete_draft_physicalinterfaces_physicalInterfaceId_events_eventId)]

#### Parameters
* `id` String value of physicalInterface Id

* `eventId` String value of event Id in EventTypeBind object

## LogicalInterface:

### GetAllActiveLogicalInterfaces
#### ` LogicalInterfaceCollection GetAllActiveLogicalInterfaces()`

Query active logical interfaces For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_logicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_logicalinterfaces)]

#### Returns
LogicalInterfaceCollection object

### GetActiveLogicalInterfaces
#### ` LogicalInterfaceInfo GetActiveLogicalInterfaces(string id)`

Retrieve the active logical interface with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `id` String value of LogicalInterfaces Id

#### Returns
LogicalInterfaceInfo object

### OperateLogicalInterfaces
#### ` OperationResponse OperateLogicalInterfaces(string id,OperationInfo operate)`

Performs the specified operation against the logical interface For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/patch_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/patch_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `id` String value of LogicalInterfaces Id

* `operate` OperationInfo object with specified operation

#### Returns
OperationResponse object

### GetAllDraftLogicalInterfaces
#### ` LogicalInterfaceCollection GetAllDraftLogicalInterfaces()`

Query draft logical interfaces For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_draft_logicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_draft_logicalinterfaces)]

#### Returns
LogicalInterfaceCollection object

### AddDraftLogicalInterfaces
#### ` LogicalInterfaceInfo AddDraftLogicalInterfaces(LogicalInterfaceDraft draft)`

Creates a new draft logical interface for the organization in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/post_draft_logicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/post_draft_logicalinterfaces)]

#### Parameters
* `draft` LogicalInterfaceDraft object

#### Returns
LogicalInterfaceInfo object

### DeleteDraftLogicalInterfaces
#### ` void DeleteDraftLogicalInterfaces(string id)`

Deletes the draft logical interface with the specified id from the organization in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/delete_draft_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/delete_draft_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `id` String value of LogicalInterfaces Id

### GetDraftLogicalInterfaces
#### ` LogicalInterfaceInfo GetDraftLogicalInterfaces(string id)`

Retrieve the draft logical interface with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_draft_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/get_draft_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `id` String value of LogicalInterfaces Id

#### Returns
LogicalInterfaceInfo object

### OperateDraftLogicalInterfaces
#### ` OperationDraftResponse OperateDraftLogicalInterfaces(string id,OperationInfo operate)`

Performs the specified operation against the draft logical interface. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/patch_draft_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/patch_draft_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `id` String value of LogicalInterfaces Id

* `operate` OperationInfo with specified operataion

#### Returns
OperationDraftResponse object

### UpdateDraftLogicalInterfaces
#### ` LogicalInterfaceInfo UpdateDraftLogicalInterfaces(LogicalInterfaceInfo update)`

Updates the draft logical interface with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/put_draft_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Logical_Interfaces/put_draft_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `update` updated LogicalInterfaceInfo object

#### Returns
LogicalInterfaceInfo object

## EventType:

### GetAllActiveEventType
#### ` EventTypeCollection GetAllActiveEventType()`

Query active event types For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_event_types](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_event_types)]

#### Returns
EventTypeCollection object

### GetActiveEventType
#### ` EventTypeInfo GetActiveEventType(string id)`

Retrieve the active event type with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_event_types_eventTypeId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_event_types_eventTypeId)]

#### Parameters
* `id` String value of EventType Id

#### Returns
EventTypeInfo object

### GetAllDraftEventType
#### ` EventTypeCollection GetAllDraftEventType()`

Query draft event types For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_draft_event_types](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_draft_event_types)]

#### Returns
EventTypeCollection object

### AddDraftEventType
#### ` EventTypeInfo AddDraftEventType(EventTypeDraft draft)`

Creates a new draft event type for the organization in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/post_draft_event_types](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/post_draft_event_types)]

#### Parameters
* `draft` EventTypeDraft object

#### Returns
EventTypeInfo object

### DeleteDraftEventType
#### ` void DeleteDraftEventType(string id)`

Deletes the draft event type with the specified id from the organization in the Watson IoT Platform. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/delete_draft_event_types_eventTypeId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/delete_draft_event_types_eventTypeId)]

#### Parameters
* `id` String value of EventType draft's Id

### GetDraftEventType
#### ` EventTypeInfo GetDraftEventType(string id)`

Retrieve the draft event type with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_draft_event_types_eventTypeId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/get_draft_event_types_eventTypeId)]

#### Parameters
* `id` String value of EventType draft's Id

#### Returns
EventTypeInfo object

### UpdateDraftEventType
#### ` EventTypeInfo UpdateDraftEventType(EventTypeInfo info)`

Updates the draft event type with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/put_draft_event_types_eventTypeId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Event_Types/put_draft_event_types_eventTypeId)]

#### Parameters
* `info` updated EventTypeInfo object

#### Returns
EventTypeInfo object

## Device

### GetCurrentDeviceState using https
#### ` dynamic GetCurrentDeviceState(string typeId,string deviceId,string logicalInterfaceId)`

Retrieve the current state of the device with the specified id. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Devices/get_device_types_typeId_devices_deviceId_state_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Devices/get_device_types_typeId_devices_deviceId_state_logicalInterfaceId)]

#### Parameters
* `typeId` String value of device type Id

* `deviceId` String value of device Id

* `logicalInterfaceId` String value of logicalInterfaceId

#### Returns
dynamic object

### Get Current Device State using MQTT Topic
Retrieve the current state of the device with the specified id using MQTT handler
```
ApplicationClient client =  new ApplicationClient(orgId, appId, apiKey, authToken);
client.connect();

...

//MQTT Topic subscription for IM State
client.subscribeToIMState(deviceType,deviceID);
client.IMStateCallback+=processIM;

...

public  void processIM(string deviceType, string deviceId, string logicalInterfaceId , string data)
		{
				Console.WriteLine("IM Status of Device " +deviceType +"/"+deviceId + "For Logical interface of ID " + logicalInterfaceId +"data"+data);
		}

```


## DeviceType

### OperateDeviceType
#### ` OperationResponse OperateDeviceType(string id,OperationInfo operate)`

Performs the specified operation against the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/patch_device_types_typeId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/patch_device_types_typeId)]

#### Parameters
* `id` String value of device type Id

* `operate` OperationInfo object with operation specified

#### Returns
OperationResponse object

### GetAllActiveDeviceTypeLI
#### ` LogicalInterfaceInfo[] GetAllActiveDeviceTypeLI(string id)`

Retrieve the list of active logical interfaces that have been associated with the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_logicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_logicalinterfaces)]

#### Parameters
* `id` String value of device type Id

#### Returns
Array of LogicalInterfaceInfo object

### GetAllActiveDeviceTypeMappings
#### ` MappingInfo[] GetAllActiveDeviceTypeMappings(string id)`

Retrieve the list of active property mappings for the specified device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_mappings](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_mappings)]

#### Parameters
* `id` String value of device type Id

#### Returns
Array of MappingInfo object

### GetActiveDeviceTypeMappingLI
#### ` MappingInfo GetActiveDeviceTypeMappingLI(string id,string logicalInterfaceId)`

Retrieves the active property mappings for a specific logical interface for the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_mappings_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_mappings_logicalInterfaceId)]

#### Parameters
* `id` String value of device type Id

* `logicalInterfaceId` String value of logicalInterface Id

#### Returns
MappingInfo object

### GetActiveDeviceTypePI
#### ` PhysicalInterfacesInfo GetActiveDeviceTypePI(string id)`

Retrieve the active physical interface that has been associated with the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_physicalinterface](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_device_types_typeId_physicalinterface)]

#### Parameters
* `id` String value of device type Id

#### Returns
PhysicalInterfacesInfo object

### GetAllDraftDeviceType
#### ` dynamic GetAllDraftDeviceType(string physicalInterfaceId,string logicalInterfaceId)`

Retrieves the list of device types that are associated with the logical interface and/or physical interface with the ids specified using the corresponding query parameters. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types)]

#### Parameters
* `physicalInterfaceId` String value of physicalInterfaceId

* `logicalInterfaceId` String value of logicalInterfaceId

#### Returns
dynamic object

### OperateDraftDeviceType
#### ` OperationDraftResponse OperateDraftDeviceType(string id,OperationInfo operate)`

Performs the specified operation against the draft device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/patch_draft_device_types_typeId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/patch_draft_device_types_typeId)]

#### Parameters
* `id` String value of device type Id

* `operate` OperationInfo object with operation specified

#### Returns
OperationDraftResponse object

### GetAllDraftDeviceTypeLI
#### ` LogicalInterfaceInfo[] GetAllDraftDeviceTypeLI(string typeId)`

Retrieve the list of draft logical interfaces that have been associated with the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_logicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_logicalinterfaces)]

#### Parameters
* `typeId` String value of device type Id

#### Returns
Array of LogicalInterfaceInfo object

### AddDraftDeviceTypeLI
#### ` LogicalInterfaceInfo AddDraftDeviceTypeLI(string typeId,LogicalInterfaceInfo info)`

Associates a draft logical interface with the specified device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_logicalinterfaces](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_logicalinterfaces)]

#### Parameters
* `typeId` String value of device type Id

* `info` LogicalInterfaceInfo object

#### Returns
LogicalInterfaceInfo object

### DeleteDraftDeviceTypeLI
#### ` void DeleteDraftDeviceTypeLI(string id,string logicalInterfaceId)`

Disassociates the draft logical interface with the specified id from the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_logicalinterfaces_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_logicalinterfaces_logicalInterfaceId)]

#### Parameters
* `id` String value of device type Id

* `logicalInterfaceId` String value of device logicalInterface Id

### GetAllDraftDeviceTypeMapping
#### ` MappingInfo[] GetAllDraftDeviceTypeMapping(string id)`

Retrieve the list of draft property mappings for the specified device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_mappings](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_mappings)]

#### Parameters
* `id` String value of device type Id

#### Returns
Array of MappingInfo object

### AddDraftDeviceTypeMapping
#### ` MappingInfo AddDraftDeviceTypeMapping(string id,MappingDraft draft)`

Creates the draft property mappings for an logical interface for the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_mappings](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_mappings)]

#### Parameters
* `id` String value of device type Id

* `draft` MappingDraft object with all values

#### Returns
MappingInfo object

### DeleteDraftDeviceTypeMapping
#### ` void DeleteDraftDeviceTypeMapping(string id,string logicalInterfaceId)`

Deletes the draft property mappings for a specific logical interface for the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_mappings_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_mappings_logicalInterfaceId)]

#### Parameters
* `id` String value of device type Id

* `logicalInterfaceId` String value of logicalInterface Id

### GetDraftDeviceTypeMapping
#### ` MappingInfo GetDraftDeviceTypeMapping(string id,string logicalInterfaceId)`

Retrieves the draft property mappings for a specific logical interface for the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_mappings_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/get_draft_device_types_typeId_mappings_logicalInterfaceId)]

#### Parameters
* `id` String value of device type Id

* `logicalInterfaceId` String value of logicalInterface Id

#### Returns
MappingInfo object

### UpdatedDraftDeviceTypeMapping
#### ` LogicalInterfaceInfo UpdatedDraftDeviceTypeMapping(string id,LogicalInterfaceInfo info)`

Updates the draft property mappings for a specific logical interface for the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/put_draft_device_types_typeId_mappings_logicalInterfaceId](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/put_draft_device_types_typeId_mappings_logicalInterfaceId)]

#### Parameters
* `id` String value of device type Id

* `info` Updated LogicalInterfaceInfo object

#### Returns
LogicalInterfaceInfo object

### AddDraftDeviceTypePI
#### ` PhysicalInterfacesInfo AddDraftDeviceTypePI(string deviceTypeId,PhysicalInterfacesInfo info)`

Associates a draft physical interface with the specified device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_physicalinterface](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/post_draft_device_types_typeId_physicalinterface)]

#### Parameters
* `deviceTypeId` String value of device type Id

* `info` PhysicalInterfacesInfo object

#### Returns
PhysicalInterfacesInfo object

### DeleteDraftDeviceTypePI
#### ` void DeleteDraftDeviceTypePI(string deviceTypeId)`

Disassociates the draft physical interface from the device type. For more info [[https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_physicalinterface](https://docs.internetofthings.ibmcloud.com/apis/swagger/v0002/state-mgmt.html#!/Device_Types/delete_draft_device_types_typeId_physicalinterface)]

#### Parameters
* `deviceTypeId` String value of device type Id

### GetDraftDeviceTypePI
#### ` PhysicalInterfacesInfo GetDraftDeviceTypePI(string deviceTypeId)`

Retrieve the draft physical interface that has been associated with the device type.

#### Parameters
* `deviceTypeId` String value of device type Id

#### Returns
PhysicalInterfacesInfo object
