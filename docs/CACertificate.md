
# C# Client Library - Client Certificate
- See [iot-csharp](https://github.com/ibm-messaging/iot-csharp) in GitHub


## Introduction

This client library describes how to use devices with the C# IBMWIoTP client library. For a basic introduction to the broader module, see [C# Client Library - Introduction](https://github.com/ibm-messaging/iot-csharp).

This section contains information on how devices can connect to the IBM Watson IoT Platform with Client certificate.

#### Requirement
* OpenSSL

## Generate certificates using OpenSSL

We need a self-signed Certificate Authority (CA) Certificate and a Client Certificate signed by the Certificate Authority (CA).

### CA certificate

* Generate the key for the CA certificate:

```
    openssl genrsa -out rootCA.key 2048
```

* Create the self-signed CA certificate using the above generated key:

```
    openssl req -x509 -new -nodes -key rootCA.key -sha256 -days 1024 -out rootCA.pem
```

* Then the created CA certificate has to be combined with it’s key to generate a single Personal Information Exchange (pfx) file:

```
openssl pkcs12 -export -inkey rootCA.key -in rootCA.pem -name root -out rootCA.pfx
```
  Note: The password you provide will be required when installing the certificate.

### Client Certificate

* Generate the private key for the Client certificate:

```
openssl genrsa -out client.key 2048
```

* Create a Certificate Signing Request (CSR) to generate client certificate:

```
openssl req -new -key client.key -out client.csr

```

* Generate the Client certificate by signing the Certificate Signing Request (CSR) using CA certificate and CA key.

```
openssl x509 -req -in client.csr -CA rootCA.pem -CAkey rootCA.key -CAcreateserial -out client.pem -days 500 -sha256
```

Fill in the details for the CSR fields. For Common Name (CN), we need to provide the identifier in the following format.
##### device
format – `d:deviceType:deviceId`, which depicts, we are going to connect this client as Device (d) under deviceType with deviceId as registered on Watson IoT platform.
##### gateway
format – `g:typeId:deviceId`, which depicts, we are going to connect this client as Gateway (g) under typeId with deviceId as registered on Watson IoT platform.

* Then the created Client certificate has to be combined with it’s key to generate a single Personal Information Exchange (pfx) file:

```
openssl pkcs12 -export -inkey client.key -in client.pem -name root -out client.pfx
```

Note: The password you provide will be required when installing the certificate.

## Connecting with CA certificate

##### Add certificate in the Watson IoT Platfrom:
<b>
Select Settings -> CA Certificates -> Add Certificate -> Select rootCA.pem -> Comments -> Save
</b>

##### Enable certificate validation in platform:
<b>
Select Security -> Connection Security -> Configure -> Select Default Security Level as TLS with Client Certificate and Token Authentication -> Click on Save
</b>

##### Create device config file
Create a device config file (prop.txt) with the following format:

```
## Device Registration detail
Organization-ID = <Your Organization ID>
Device-Type = <Your device Type>
Device-ID = <Your device ID>
Authentication-Method = token
Authentication-Token = <Your device Token>
CA-Certificate-Path = <Your root CA certificate path >
CA-Certificate-Password = <Your root CA certificate password>
Client-Certificate-Path =  <Your Client CA certificate path >
Client-Certificate-Password = <Your Client CA certificate password >
```
##### Connecting to Watson IoT Platform

```
DeviceClient deviceClient = new DeviceClient("prop.txt");
deviceClient.connect();
```
##### Create gateway config file
Create a gateway config file (gatewayprop.txt) with the following format:

```
## Gateway Registration detail
Organization-ID = <Your Organization ID>
Device-Type = <Your Gateway Type>
Device-ID = <Your Gateway ID>
Authentication-Method = token
Authentication-Token = <Your Gateway Token>
CA-Certificate-Path = <Your root CA certificate path >
CA-Certificate-Password = <Your root CA certificate password>
Client-Certificate-Path =  <Your Client CA certificate path >
Client-Certificate-Password = <Your Client CA certificate password >
```
##### Connecting to Watson IoT Platform

```
GatewayClient gatewayClient = new GatewayClient("gatewayprop.txt");
gatewayClient.connect();
```
### [Sample](https://github.com/ibm-watson-iot/iot-csharp/tree/master/sample/ClientCA_Authentication)
