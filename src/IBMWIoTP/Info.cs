/*
 *  Copyright (c) 2016 IBM Corporation and other Contributors.
 *
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.eclipse.org/legal/epl-v10.html 
 *
 * Contributors:
 *   Hari hara prasad Viswanathan  - Initial Contribution
 */
 
using System;
using System.Security.Cryptography.X509Certificates;

namespace IBMWIoTP
{
	/// <summary>
	/// Description of info.
	/// </summary>
	public class DeviceInfo
	{
		public DeviceInfo()
		{
			this.serialNumber ="";
			this.manufacturer = "" ;
			this.model = "" ;
			this.deviceClass ="";
			this.description = "";
			this.fwVersion ="";
			this.hwVersion ="";
			this.descriptiveLocation = "";
		}
		public string serialNumber {get;set;}
		public string manufacturer {get;set;}
		public string model {get;set;}
		public string deviceClass {get;set;}
		public string description {get;set;}
		public string fwVersion {get;set;}
		public string hwVersion {get;set;}
		public string descriptiveLocation {get;set;}
	}
	
	public class DeviceFirmware
	{
		public DeviceFirmware(){
		}
		public string version{get;set;}
		public string name{get;set;}
		public string url{get;set;}
		public string uri{get;set;}
		public string verifier{get;set;}
		public int state{get;set;}
		public int updateStatus{get;set;}
		public string updatedDateTime{get;set;}
	}
	
	public class LocationInfo
	{
		public LocationInfo()
		{
			this.latitude = 0;
			this.longitude = 0;
			this.measuredDateTime="";
			this.elevation = 0;
			this.accuracy=0;
		}
		public double longitude {get;set;}
		public double latitude {get;set;}
		public double elevation {get;set;}
		public double accuracy {get;set;}
		public string measuredDateTime {get;set;}
		//public string updatedDateTime {get;set;}
	}
	
	
	public class GatewayError
	{
		public GatewayError()
		{
		}
		public string Request { get; set; }
		public string Time { get; set; }
		public string Topic { get; set; }
		public string Type { get; set; }
		public string Id { get; set; }
		public string Client { get; set; }
		public string RC { get; set; }
		public string Message { get; set; }

	}
	public class RegisterDevicesInfo	{
		public RegisterDevicesInfo(){
		}
		public string typeId {get;set;}
		public string deviceId {get;set;}
		public DeviceInfo deviceInfo {get;set;}
		public LocationInfo location {get;set;}
		public object metadata {get;set;}
		public string authToken {get;set;}
	}
	public class RegisterSingleDevicesInfo	{
		public RegisterSingleDevicesInfo(){
		}
		public string deviceId {get;set;}
		public DeviceInfo deviceInfo {get;set;}
		public LocationInfo location {get;set;}
		public object metadata {get;set;}
		public string authToken {get;set;}
	}
	public class UpdateDevicesInfo	{
		public UpdateDevicesInfo(){
			this.metadata =new {};
			this.status = new { alert= new{enabled = true}};
			this.extensions = new {};
		}
		public DeviceInfo deviceInfo {get;set;}
		public object metadata {get;set;}
		public object status {get;set;}
		public object extensions {get;set;}
	}
	
	public class DeviceListElement	{
		public DeviceListElement(){
		}
		public string typeId {get;set;}
		public string deviceId {get;set;}
	}
	
	public class DeviceTypeInfo {
		public DeviceTypeInfo(){}
		public string id {get;set;}
		public string description {get;set;}
		public string classId {get;set;}
		public DeviceInfo deviceInfo {get;set;}
		public object metadata {get;set;}
		
	}
	public class DeviceTypeInfoUpdate {
		public DeviceTypeInfoUpdate(){}
		public string description {get;set;}
		public DeviceInfo deviceInfo {get;set;}
		public object metadata {get;set;}
	}
	public class LogInfo {
		public LogInfo(){
			this.message ="";
			this.severity =0 ;
			this.timestamp ="";
			this.data = "" ;
		}
		public string message {get;set;}
		public int severity {get;set;}
		public string data {get;set;}
		public string timestamp {get;set;}

	}
	public class ErrorCodeInfo {
		public ErrorCodeInfo(){
			this.errorCode =0 ;
			this.timestamp ="";
		}
		public int errorCode {get;set;}
		public string timestamp {get;set;}
	}
	public class DeviceMgmtparameter {
		public DeviceMgmtparameter(){
			this.name = "";
			this.value ="";
		}
		public string name {get;set;}
		public string value {get;set;}
	}
	
	
	
	public class DMRequest
	{
		public  DMRequest()
		{
		}
		public  DMRequest(string reqId, string topic ,string json)
		{
			this.reqID = reqId;
			this.topic = topic;
			this.json =json;
		}
		public string reqID {get;set;}
		public string topic {get;set;}
		public string json {get;set;}
	}
	
	public class DMResponse
	{
		public DMResponse()
		{
		}
		public string reqId {get;set;}
		public string rc {get;set;}
		
	}

	public class DMField {
		public DMField(){
		}
	
		public string field {get;set;}
		public DeviceFirmware value {get;set;}
	}
	public class DMFields {
		public DMFields(){
		}
		public DMField [] fields;
	}
	public class DeviceActionReq{
	
		public DeviceActionReq(){
		}
		public string reqId {get;set;}
		public DMFields d {get;set;}
	}
	
	
	
//	public class OrgInfo {
//	
//		public OrgInfo(){}
//		
//
//		public string id {get;set;}
//		public string name {get;set;}
//		public string enabled {get;set;}
//		public string type {get;set;}
//		public BluemixInfo bluemix {get;set;}
//		public ConfigInfo config {get;set;}
//		public string created {get;set;}
//		public string updated {get;set;}
//	}
//	
//	public class BluemixInfo {
//		public BluemixInfo(){}
//		public string region {get;set;}
//		public string organizationGuid {get;set;}
//		public string serviceInstanceGuid {get;set;}
//		public string spaceGuid {get;set;}
//		public string planId {get;set;}
//		
//	}
//	
//	public class BlockChainState{
//	
//		public BlockChainState(){}
//		public bool activated {get;set;}
//		public bool enabled {get;set;}
//	
//	}
//	public class ConfigInfo{
//		public ConfigInfo(){}
//		public BlockChainState blockchain {get;set;}
//	}

}
