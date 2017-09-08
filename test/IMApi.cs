/*
 *  Copyright (c) 2017 IBM Corporation and other Contributors.
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
using System.Collections.Generic;
using System.Net;
using IBMWIoTP;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;


namespace IBMWIoTP
{
	
	public class mockResponse :IRestResponse{
		public IRestRequest Request
		{
			get;
			set;
		}
		public string ContentType
		{
			get;
			set;
		}
		public long ContentLength
		{
			get;
			set;
		}
		public string ContentEncoding
		{
			get;
			set;
		}
		public string Content
		{
			get;
			set;
		}
		public HttpStatusCode StatusCode
		{
			get;
			set;
		}
		public string StatusDescription
		{
			get;
			set;
		}
		public byte[] RawBytes
		{
			get;
			set;
		}
		public Uri ResponseUri
		{
			get;
			set;
		}
		public string Server
		{
			get;
			set;
		}
		public IList<RestResponseCookie> Cookies
		{
			get;
			set;
			
		}
		public IList<Parameter> Headers
		{
			get;
			set;
			
		}
		public ResponseStatus ResponseStatus
		{
			get;
			set;
		}
		public string ErrorMessage
		{
			get;
			set;
		}
		public Exception ErrorException
		{
			get;
			set;
		}
		public mockResponse(){
		}
	}
	public class mockRestClient :IRestClient{
		HttpStatusCode Status;
		string Content;
		public IRestRequest request;
		public mockRestClient(HttpStatusCode status, string content){
			this.Status = status;
			this.Content = content;
		}
		
		public CookieContainer CookieContainer {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public Nullable<int> MaxRedirects {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public string UserAgent {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public int Timeout {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public int ReadWriteTimeout {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public bool UseSynchronizationContext {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public IAuthenticator Authenticator {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public Uri BaseUrl {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public System.Text.Encoding Encoding {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public bool PreAuthenticate {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public IList<Parameter> DefaultParameters {
			get {
				throw new NotImplementedException();
			}
		}
		
		public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public IWebProxy Proxy {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public System.Net.Cache.RequestCachePolicy CachePolicy {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public bool FollowRedirects {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			throw new NotImplementedException();
		}
		
		public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
		{
			throw new NotImplementedException();
		}
		
		public IRestResponse Execute(IRestRequest request)
		{
			this.request=request;
			mockResponse res = new mockResponse() ;
			res.Content = this.Content;
			res.StatusCode = this.Status;
			return res as IRestResponse;
		}
		
		IRestResponse<T> IRestClient.Execute<T>(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		public byte[] DownloadData(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		public Uri BuildUri(IRestRequest request)
		{
			this.request=request;
			return new Uri("http://dummy.url.com");
		}
		
		public RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		public RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		public RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		public RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		public void AddHandler(string contentType, RestSharp.Deserializers.IDeserializer deserializer)
		{
			throw new NotImplementedException();
		}
		
		public void RemoveHandler(string contentType)
		{
			throw new NotImplementedException();
		}
		
		public void ClearHandlers()
		{
			throw new NotImplementedException();
		}
		
		public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		IRestResponse<T> IRestClient.ExecuteAsGet<T>(IRestRequest request, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		IRestResponse<T> IRestClient.ExecuteAsPost<T>(IRestRequest request, string httpMethod)
		{
			throw new NotImplementedException();
		}
		
		public System.Threading.Tasks.Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, System.Threading.CancellationToken token)
		{
			throw new NotImplementedException();
		}
		
		public System.Threading.Tasks.Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		public System.Threading.Tasks.Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		public System.Threading.Tasks.Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, System.Threading.CancellationToken token)
		{
			throw new NotImplementedException();
		}
		
		public System.Threading.Tasks.Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		public System.Threading.Tasks.Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, System.Threading.CancellationToken token)
		{
			throw new NotImplementedException();
		}
		
		System.Threading.Tasks.Task<IRestResponse> IRestClient.ExecuteTaskAsync(IRestRequest request, System.Threading.CancellationToken token)
		{
			throw new NotImplementedException();
		}
		
		System.Threading.Tasks.Task<IRestResponse> IRestClient.ExecuteTaskAsync(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		System.Threading.Tasks.Task<IRestResponse> IRestClient.ExecuteGetTaskAsync(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		System.Threading.Tasks.Task<IRestResponse> IRestClient.ExecuteGetTaskAsync(IRestRequest request, System.Threading.CancellationToken token)
		{
			throw new NotImplementedException();
		}
		
		System.Threading.Tasks.Task<IRestResponse> IRestClient.ExecutePostTaskAsync(IRestRequest request)
		{
			throw new NotImplementedException();
		}
		
		System.Threading.Tasks.Task<IRestResponse> IRestClient.ExecutePostTaskAsync(IRestRequest request, System.Threading.CancellationToken token)
		{
			throw new NotImplementedException();
		}
	}
	public class mockApiClient :ApiClient
	{
		public mockApiClient(string apiKey , string authToken):
			base(apiKey,authToken)
		{
			
		}
		public void SetResponse(HttpStatusCode statusCode, string data){
			this._client = new IBMWIoTP.mockRestClient(statusCode,data) as IRestClient;
		}
		public IRestRequest GetRequest(){
			var client = this._client as IBMWIoTP.mockRestClient;
			return client.request;
		}
		
		
		}

}
namespace test
{
	[TestFixture]
	public class IMApi
	{
		//IBMWIoTP.ApiClient client = null;
		IBMWIoTP.mockApiClient client = null;
		string orgId,appID,apiKey,authToken,gatewayType,gatwayId,deviceType,deviceId;
		SchemaInfo draftSchemaForPhysicalInterface = null;
		PhysicalInterfacesInfo draftPI;
		[SetUp]
		public void Setup() 
		{
			
			Dictionary<string,string> data = IBMWIoTP.ApplicationClient.parseFile("../../Resource/AppProp.txt","## Application Registration detail");
        	if(	!data.TryGetValue("Organization-ID",out orgId)||
        		!data.TryGetValue("App-ID",out appID)||
        		!data.TryGetValue("Api-Key",out apiKey)||
        		!data.TryGetValue("Authentication-Token",out authToken) )
        	{
        		throw new Exception("Invalid property file");
        	}
			//client = new IBMWIoTP.ApiClient(apiKey,authToken);
			client = new mockApiClient(apiKey,authToken);
			client.Timeout=100000;
			Dictionary<string,string> device = IBMWIoTP.DeviceClient.parseFile("../../Resource/prop.txt","## Device Registration detail");
			device.TryGetValue("Device-Type",out deviceType);
			device.TryGetValue("Device-ID",out deviceId);
			Dictionary<string,string> gw = IBMWIoTP.GatewayClient.parseFile("../../Resource/Gatewayprop.txt","## Gateway Registration detail");
			gw.TryGetValue("Device-Type",out gatewayType);
			gw.TryGetValue("Device-ID",out gatwayId);
		}
		[Test]
		public void Schema_a_AddDraftSchema(){
			string json ="{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c/content\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			SchemaDraft sd = new SchemaDraft();
			sd.name ="tempEventSchema";
			sd.schemaFile="tempSchemaPi.json";
			draftSchemaForPhysicalInterface = client.AddDraftSchema(sd);
			Assert.IsNotNull(draftSchemaForPhysicalInterface);
			Assert.AreEqual("58258b8146e0fb0001458b7c",draftSchemaForPhysicalInterface.id);
			Assert.AreEqual("Temperature Event Schema",draftSchemaForPhysicalInterface.name);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/schemas",request.Resource);
			Assert.IsTrue(request.AlwaysMultipartFormData);
		}
		[Test]
		public void Schema_b_GetAllDraftSchemas(){
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/draft/schemas/582aea2946e0fb0001a9db97/content\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			SchemaCollection coll = client.GetAllDraftSchemas();
			Assert.IsNotNull(coll);
			Assert.AreEqual("aaaaaaaaaaaaaaaaaaaaaaa",coll.bookmark);
			Assert.AreEqual(coll.results.Length,1);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/schemas",request.Resource);
		}
		[Test]
		public void Schema_c_GetDraftSchemas(){
			string json ="{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c/content\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			SchemaInfo info = client.GetDraftSchemaMetadata("58258b8146e0fb0001458b7c");
			Assert.IsNotNull(info);
			Assert.AreEqual(info.id,"58258b8146e0fb0001458b7c");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/schemas/58258b8146e0fb0001458b7c",request.Resource);
			
		}
		[Test]
		public void Schema_d_DeleteDraftSchemas(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftSchema("someId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/schemas/someId",request.Resource);
		}
		[Test]
		public void Schema_e_UpdateDraftSchemaMetadata(){
			string json ="{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c/content\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			SchemaInfo updated = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<SchemaInfo>(json);
			SchemaInfo info = client.UpdateDraftSchemaMetadata(updated);
			Assert.IsNotNull(info);
			Assert.AreEqual("58258b8146e0fb0001458b7c",info.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PUT,request.Method);
			Assert.AreEqual("/draft/schemas/58258b8146e0fb0001458b7c",request.Resource);
		}
		
		[Test]
		public void Schema_f_GetDraftSchemaContent(){
			string json ="{\"type\":\"object\",\"properties\":{\"temp\":{\"description\":\"temperature in degrees Celsius\",\"type\":\"number\",\"minimum\":-273.15,\"default\":0.0}}}";
			client.SetResponse(HttpStatusCode.OK,json);
			var info = client.GetDraftSchemaContent("schemaId");
			Assert.IsNotNull(info);
			Assert.AreEqual(info["type"],"object");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/schemas/schemaId/content",request.Resource);
		}
		[Test]
		public void Schema_g_UpdateDraftSchemaContent(){
			string json ="{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c/content\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			var info = client.UpdateDraftSchemaContent("schemaId","../../Resource/AppProp.txt");
			Assert.IsNotNull(info);
			Assert.AreEqual("58258b8146e0fb0001458b7c",info.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PUT,request.Method);
			Assert.AreEqual("/draft/schemas/schemaId/content",request.Resource);
			Assert.IsTrue(request.AlwaysMultipartFormData);
		}
		[Test]
		public void Schema_h_GetAllActiveSchemas(){
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"active\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/schemas/582aea2946e0fb0001a9db97/content\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			SchemaCollection coll = client.GetAllActiveSchemas();
			Assert.IsNotNull(coll);
			Assert.AreEqual("aaaaaaaaaaaaaaaaaaaaaaa",coll.bookmark);
			Assert.AreEqual(coll.results.Length,1);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/schemas",request.Resource);
		}
		[Test]
		public void Schema_i_GetActiveSchemaMetadata(){
			string json ="{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Temperature Event Schema\",\"description\":\"Schema that defines the structure of temperature events emitted by temperature sensors\",\"schemaType\":\"json-schema\",\"schemaFileName\":\"tempSensor.json\",\"contentType\":\"application/json\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"content\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c/content\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			SchemaInfo info = client.GetActiveSchemaMetadata("58258b8146e0fb0001458b7c");
			Assert.IsNotNull(info);
			Assert.AreEqual("58258b8146e0fb0001458b7c",info.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/schemas/58258b8146e0fb0001458b7c",request.Resource);
			
		}
		[Test]
		public void Schema_j_GetActiveSchemaContent(){
			string json ="{\"type\":\"object\",\"properties\":{\"temp\":{\"description\":\"temperature in degrees Celsius\",\"type\":\"number\",\"minimum\":-273.15,\"default\":0.0}}}";
			client.SetResponse(HttpStatusCode.OK,json);
			var info = client.GetActiveSchemaContent("schemaId");
			Assert.IsNotNull(info);
			Assert.AreEqual("object",info["type"]);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/schemas/schemaId/content",request.Resource);
		}
		
		
		
		
		[Test]
		public void PhysicalInterface_a_AddDraftPhysicalInterfaces()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"Physical Interface for MyBulbCo light bulbs\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfaceDraft draft = new PhysicalInterfaceDraft();
			draft.name="TempPI";
			draft.description="new PhysicalInterfaces for temp";
			draftPI =  client.AddDraftPhysicalInterfaces(draft);
			Assert.NotNull(draftPI);
			Assert.AreEqual("582add5746e0fb000174",draftPI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces",request.Resource);
			Assert.AreEqual( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draft) ,request.Parameters[0].Value);
		}
		[Test]
		public void PhysicalInterface_b_GetAllDraftPhysicalInterfaces()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"Physical Interface for MyBulbCo light bulbs\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesCollection result =  client.GetAllDraftPhysicalInterfaces();
			Assert.NotNull(result);
			Assert.That(result.results.Count,Is.Not.EqualTo(0));
			var draftPI = result.results[0];
			Assert.NotNull(draftPI);
			Assert.AreEqual("582add5746e0fb000174",draftPI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces",request.Resource);
		}
		[Test]
		public void PhysicalInterface_c_GetDraftPhysicalInterfaces()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"Physical Interface for MyBulbCo light bulbs\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesInfo result =  client.GetDraftPhysicalInterfaces("582add5746e0fb000174");
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb000174",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces/582add5746e0fb000174",request.Resource);
		}
		[Test]
		public void PhysicalInterface_d_UpdateDraftPhysicalInterfaces()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"changed to new discription\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesInfo draftPI = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(json);
			draftPI.description="changed to new discription";
			PhysicalInterfacesInfo result =  client.UpdateDraftPhysicalInterfaces(draftPI);
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb000174",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PUT,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces/582add5746e0fb000174",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draftPI) ,request.Parameters[0].Value);
		}
		[Test]
		public void PhysicalInterface_e_DeleteDraftPhysicalInterfaces(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftPhysicalInterfaces("someId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces/someId",request.Resource);
		}
		[Test]
		public void PhysicalInterface_f_MapDraftPhysicalInterfacesEvent()
		{
			string json ="{\"eventId\":1234,\"eventTypeId\":\"582add5746e0fb0001741b38\"}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeBind bind = new EventTypeBind();
			bind.eventTypeId = "582add5746e0fb0001741b38";
			bind.eventId="temperature";
			EventTypeBind result =  client.MapDraftPhysicalInterfacesEvent("582add5746e0fb000174",bind);
			Assert.NotNull(result);
			Assert.AreEqual("1234",result.eventId);
			Assert.AreEqual("582add5746e0fb0001741b38",result.eventTypeId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces/582add5746e0fb000174/events",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(bind) ,request.Parameters[0].Value);
		}
		[Test]
		public void PhysicalInterface_g_GetAllDraftPhysicalInterfacesMappedEvents()
		{
			string json ="[{\"eventId\":1234,\"eventTypeId\":\"582add5746e0fb0001741b38\"}]";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeBind[] result =  client.GetAllDraftPhysicalInterfacesMappedEvents("582add5746e0fb000174");
			Assert.NotNull(result);
			Assert.AreEqual("1234",result[0].eventId);
			Assert.AreEqual("582add5746e0fb0001741b38",result[0].eventTypeId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces/582add5746e0fb000174/events",request.Resource);
		}
		[Test]
		public void PhysicalInterface_h_DeleteDraftPhysicalInterfaces(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftPhysicalInterfacesMappedEvents("physicalInterfaceId","eventId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/physicalinterfaces/physicalInterfaceId/events/eventId",request.Resource);
		}
		[Test]
		public void PhysicalInterface_i_GetAllActivePhysicalInterfaces()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"Physical Interface for MyBulbCo light bulbs\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesCollection result =  client.GetAllActivePhysicalInterfaces();
			Assert.NotNull(result);
			Assert.That(result.results.Count,Is.Not.EqualTo(0));
			var draftPI = result.results[0];
			Assert.NotNull(draftPI);
			Assert.AreEqual("582add5746e0fb000174",draftPI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/physicalinterfaces",request.Resource);
		}
		[Test]
		public void PhysicalInterface_j_GetActivePhysicalInterfaces()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"Physical Interface for MyBulbCo light bulbs\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesInfo result =  client.GetActivePhysicalInterfaces("582add5746e0fb000174");
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb000174",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/physicalinterfaces/582add5746e0fb000174",request.Resource);
		}
		[Test]
		public void PhysicalInterface_k_GetActivePhysicalInterfacesEvents()
		{
			string json ="[{\"eventId\":1234,\"eventTypeId\":\"582add5746e0fb0001741b38\"}]";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeBind[] result =  client.GetActivePhysicalInterfacesEvents("582add5746e0fb000174");
			Assert.NotNull(result);
			Assert.AreEqual("1234",result[0].eventId);
			Assert.AreEqual("582add5746e0fb0001741b38",result[0].eventTypeId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/physicalinterfaces/582add5746e0fb000174/events",request.Resource);
		}
		
		
		
		[Test]
		public void LogicalInterface_a_AddDraftLogicalInterfaces()
		{
			string json ="{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"5829e2c546e0fb0001cf0ac4\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/5829e2c546e0fb0001cf0ac4\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceDraft draft =  new LogicalInterfaceDraft();
			draft.name="lidraft";
			draft.schemaId = "schemaId";
			draft.description="some thing";
			var draftLI =  client.AddDraftLogicalInterfaces(draft);
			Assert.NotNull(draftLI);
			Assert.AreEqual("582aea2946e0fb0001a9db97",draftLI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/logicalinterfaces",request.Resource);
			Assert.AreEqual( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draft) ,request.Parameters[0].Value);
		}
		[Test]
		public void LogicalInterface_b_GetAllDraftLogicalInterfaces()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"5829e2c546e0fb0001cf0ac4\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/5829e2c546e0fb0001cf0ac4\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceCollection result =  client.GetAllDraftLogicalInterfaces();
			Assert.NotNull(result);
			Assert.That(result.results.Count,Is.Not.EqualTo(0));
			var draftLI = result.results[0];
			Assert.NotNull(draftLI);
			Assert.AreEqual("582aea2946e0fb0001a9db97",draftLI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/logicalinterfaces",request.Resource);
		}
		[Test]
		public void LogicalInterface_c_GetDraftLogicalInterfaces()
		{
			string json ="{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"5829e2c546e0fb0001cf0ac4\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/5829e2c546e0fb0001cf0ac4\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceInfo result =  client.GetDraftLogicalInterfaces("582aea2946e0fb0001a9db97");
			Assert.NotNull(result);
			Assert.AreEqual("582aea2946e0fb0001a9db97",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/logicalinterfaces/582aea2946e0fb0001a9db97",request.Resource);
		}
		[Test]
		public void LogicalInterface_d_UpdateDraftLogicalInterfaces()
		{
			string json ="{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"5829e2c546e0fb0001cf0ac4\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/5829e2c546e0fb0001cf0ac4\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceInfo draftLI = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(json);
			draftLI.description="changed to new discription";
			LogicalInterfaceInfo result =  client.UpdateDraftLogicalInterfaces(draftLI);
			Assert.NotNull(result);
			Assert.AreEqual("582aea2946e0fb0001a9db97",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PUT,request.Method);
			Assert.AreEqual("/draft/logicalinterfaces/582aea2946e0fb0001a9db97",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draftLI) ,request.Parameters[0].Value);
		}
		[Test]
		public void LogicalInterface_e_DeleteDraftLogicalInterfaces(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftLogicalInterfaces("someId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/logicalinterfaces/someId",request.Resource);
		}
		[Test]
		public void LogicalInterface_f_OperateDraftLogicalInterfaces()
		{
			string json ="{\"message\":\"CUDIM0303I: State update configuration for Logical Interface 'Light Bulb Interface' is not valid. \",\"details\":{\"id\":\"CUDIM0303I\",\"properties\":[\"Logical Interface\",\"Light Bulb Interface\"]},\"failures\":{\"message\":\"CUDVS0302E: There are no mappings defined from any device types for the targeted logical interface\",\"details\":{\"id\":\"CUDVS0302E\",\"properties\":[]}}}";
			client.SetResponse(HttpStatusCode.OK,json);
			OperationInfo work = new OperationInfo(OperationInfo.Validate);
			OperationDraftResponse result =  client.OperateDraftLogicalInterfaces("582aea2946e0fb0001a9db97",work);
			Assert.NotNull(result);
			Assert.AreEqual("CUDIM0303I",result.details.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PATCH,request.Method);
			Assert.AreEqual("/draft/logicalinterfaces/582aea2946e0fb0001a9db97",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(work),request.Parameters[0].Value);
		}
		[Test]
		public void LogicalInterface_g_GetAllActiveLogicalInterfaces()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"5829e2c546e0fb0001cf0ac4\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/5829e2c546e0fb0001cf0ac4\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceCollection result =  client.GetAllActiveLogicalInterfaces();
			Assert.NotNull(result);
			Assert.That(result.results.Count,Is.Not.EqualTo(0));
			var draftLI = result.results[0];
			Assert.NotNull(draftLI);
			Assert.AreEqual("582aea2946e0fb0001a9db97",draftLI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/logicalinterfaces",request.Resource);
		}
		[Test]
		public void LogicalInterface_h_GetActiveLogicalInterfaces()
		{
			string json ="{\"id\":\"582aea2946e0fb0001a9db97\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"5829e2c546e0fb0001cf0ac4\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/5829e2c546e0fb0001cf0ac4\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceInfo result =  client.GetActiveLogicalInterfaces("582aea2946e0fb0001a9db97");
			Assert.NotNull(result);
			Assert.AreEqual("582aea2946e0fb0001a9db97",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/logicalinterfaces/582aea2946e0fb0001a9db97",request.Resource);
		}
		[Test]
		public void LogicalInterface_i_OperateLogicalInterfaces()
		{
			string json ="{\"message\":\"CUDIM0305I: State update configuration for Device Type  'Light Bulb Interface' has been successfully submitted for deactivation. \",\"details\":{\"id\":\"CUDIM0305I\",\"properties\":[\"Device Type\",\"Light Bulb Interface\"]},\"failures\":[]}";
			client.SetResponse(HttpStatusCode.OK,json);
			OperationInfo work = new OperationInfo(OperationInfo.Validate);
			OperationResponse result =  client.OperateLogicalInterfaces("582aea2946e0fb0001a9db97",work);
			Assert.NotNull(result);
			Assert.AreEqual("CUDIM0305I",result.details.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PATCH,request.Method);
			Assert.AreEqual("/logicalinterfaces/582aea2946e0fb0001a9db97",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(work),request.Parameters[0].Value);
		}
		
		
		
		
		
		
		[Test]
		public void EventType_a_AddDraftEventType()
		{
			string json ="{\"id\":\"582add5746e0fb0001741b38\",\"name\":\"MyBulbCo Light Bulb On Event Type\",\"description\":\"Native event definition for the MyBulbCo light bulb on event\",\"schemaId\":\"58258b8146e0fb0001458b7c\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeDraft draft = new EventTypeDraft();
			draft.name="Temprature";
			draft.description="eventType for temperature";
			draft.schemaId = "schemaId";
			EventTypeInfo info =  client.AddDraftEventType(draft);
			Assert.NotNull(info);
			Assert.AreEqual("582add5746e0fb0001741b38",info.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/event/types",request.Resource);
			Assert.AreEqual( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draft) ,request.Parameters[0].Value);
		}
		[Test]
		public void EventType_b_GetAllDraftLogicalInterfaces()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582add5746e0fb0001741b38\",\"name\":\"MyBulbCo Light Bulb On Event Type\",\"description\":\"Native event definition for the MyBulbCo light bulb on event\",\"schemaId\":\"58258b8146e0fb0001458b7c\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeCollection result =  client.GetAllDraftEventType();
			Assert.NotNull(result);
			Assert.That(result.results.Count,Is.Not.EqualTo(0));
			var draft = result.results[0];
			Assert.NotNull(draft);
			Assert.AreEqual("582add5746e0fb0001741b38",draft.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/event/types",request.Resource);
		}
		[Test]
		public void EventType_c_GetDraftEventType()
		{
			string json ="{\"id\":\"582add5746e0fb0001741b38\",\"name\":\"MyBulbCo Light Bulb On Event Type\",\"description\":\"Native event definition for the MyBulbCo light bulb on event\",\"schemaId\":\"58258b8146e0fb0001458b7c\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeInfo result =  client.GetDraftEventType("582add5746e0fb0001741b38");
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb0001741b38",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/event/types/582add5746e0fb0001741b38",request.Resource);
		}
		[Test]
		public void EventType_d_UpdateDraftEventType()
		{
			string json ="{\"id\":\"582add5746e0fb0001741b38\",\"name\":\"MyBulbCo Light Bulb On Event Type\",\"description\":\"Native event definition for the MyBulbCo light bulb on event\",\"schemaId\":\"58258b8146e0fb0001458b7c\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeInfo draft = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<EventTypeInfo>(json);
			draft.description="changed to new discription";
			EventTypeInfo result =  client.UpdateDraftEventType(draft);
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb0001741b38",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PUT,request.Method);
			Assert.AreEqual("/draft/event/types/582add5746e0fb0001741b38",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draft) ,request.Parameters[0].Value);
		}
		[Test]
		public void EventType_e_DeleteDraftEventType(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftEventType("someId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/event/types/someId",request.Resource);
		}
		[Test]
		public void EventType_f_GetAllActiveEventType()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"facets\":\"\",\"totalRows\":1},\"results\":[{\"id\":\"582add5746e0fb0001741b38\",\"name\":\"MyBulbCo Light Bulb On Event Type\",\"description\":\"Native event definition for the MyBulbCo light bulb on event\",\"schemaId\":\"58258b8146e0fb0001458b7c\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeCollection result =  client.GetAllActiveEventType();
			Assert.NotNull(result);
			Assert.That(result.results.Count,Is.Not.EqualTo(0));
			var draft = result.results[0];
			Assert.NotNull(draft);
			Assert.AreEqual("582add5746e0fb0001741b38",draft.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/event/types",request.Resource);
		}
		[Test]
		public void EventType_g_GetActiveEventType()
		{
			string json ="{\"id\":\"582add5746e0fb0001741b38\",\"name\":\"MyBulbCo Light Bulb On Event Type\",\"description\":\"Native event definition for the MyBulbCo light bulb on event\",\"schemaId\":\"58258b8146e0fb0001458b7c\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/draft/schemas/58258b8146e0fb0001458b7c\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			EventTypeInfo result =  client.GetActiveEventType("582add5746e0fb0001741b38");
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb0001741b38",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/event/types/582add5746e0fb0001741b38",request.Resource);
		}
		
		
		[Test]
		public void Device_a_GetCurrentDeviceState()
		{
			string json ="{\"timestamp\":\"2016-09-16T17:32:10Z\",\"updated\":\"2016-09-16T15:26:12Z\",\"state\":{\"temperature\":38.4}}";
			client.SetResponse(HttpStatusCode.OK,json);
			var result =  client.GetCurrentDeviceState("typeId","deviceId","logicalInterfaceId");
			Assert.NotNull(result);
			Assert.AreEqual("2016-09-16T17:32:10Z",result["timestamp"]);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/device/types/typeId/devices/deviceId/state/logicalInterfaceId",request.Resource);
		}
		
		[Test]
		public void DeviceType_a_OperateDeviceType()
		{
			string json ="{\"message\":\"CUDIM0305I: State update configuration for Device Type 'myDeviceType' has been successfully submitted for deactivation\",\"details\":{\"id\":\"CUDIM0305I\",\"properties\":[\"Device Type\",\"myDeviceType\"]},\"failures\":[]}";
			client.SetResponse(HttpStatusCode.OK,json);
			OperationInfo action =  new OperationInfo(OperationInfo.Validate);
			OperationResponse result =  client.OperateDeviceType("typeId",action);
			Assert.NotNull(result);
			Assert.AreEqual("CUDIM0305I",result.details.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PATCH,request.Method);
			Assert.AreEqual("/device/types/typeId",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(action),request.Parameters[0].Value);
		}
		[Test]
		public void DeviceType_b_GetAllActiveDeviceTypeLI()
		{
			string json ="[{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"582add5746e0fb0001741b38\",\"version\":\"active\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/schemas/582add5746e0fb0001741b38\"}}]";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceInfo[] coll = client.GetAllActiveDeviceTypeLI("typeId");
			var draftLI = coll[0];
			Assert.NotNull(draftLI);
			Assert.AreEqual("58258b8146e0fb0001458b7c",draftLI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/device/types/typeId/logicalinterfaces",request.Resource);
		}
		[Test]
		public void DeviceType_d_GetAllActiveDeviceTypeMappings()
		{
			string json = "[{\"logicalInterfaceId\":\"58258b8146e0fb0001458b7c\",\"notificationStrategy\":\"on-state-change\",\"propertyMappings\":{\"1234\":{\"temperature\":\"$event.t\",\"humidity\":\"$event.h\"}},\"version\":\"active\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\"}]";
			client.SetResponse(HttpStatusCode.OK,json);
			MappingInfo[] coll = client.GetAllActiveDeviceTypeMappings("typeId");
			var draft = coll[0];
			Assert.NotNull(draft);
			Assert.AreEqual("58258b8146e0fb0001458b7c",draft.logicalInterfaceId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/device/types/typeId/mappings",request.Resource);
		}
		[Test]
		public void DeviceType_e_GetActiveDeviceTypePI()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"Physical Interface for MyBulbCo light bulbs\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesInfo result= client.GetActiveDeviceTypePI("typeId");
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb000174",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/device/types/typeId/physicalinterface",request.Resource);
		}
		[Test]
		public void DeviceType_f_GetActiveDeviceTypeMappingLI()
		{
			string json ="{\"logicalInterfaceId\":\"58258b8146e0fb0001458b7c\",\"notificationStrategy\":\"on-state-change\",\"propertyMappings\":{\"event1234\":{\"temperature\":\"$event.temp\",\"eventCount\":\"$state.eventCount + 1\"}},\"version\":\"active\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\"}";
			client.SetResponse(HttpStatusCode.OK,json);
			MappingInfo result= client.GetActiveDeviceTypeMappingLI("typeId","58258b8146e0fb0001458b7c");
			Assert.NotNull(result);
			Assert.AreEqual("58258b8146e0fb0001458b7c",result.logicalInterfaceId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/device/types/typeId/mappings/58258b8146e0fb0001458b7c",request.Resource);
		}
		[Test]
		public void DeviceType_g_GetAllDraftDeviceType()
		{
			string json ="{\"bookmark\":\"aaaaaaaaaaaaaaaaaaaaaaa\",\"meta\":{\"id\":{\"myDeviceType\":1,\"myOtherDeviceType\":2},\"totalRows\":2},\"results\":[{\"id\":\"myDeviceType\",\"description\":\"My first device type\",\"deviceInfo\":{\"serialNumber\":\"100087\",\"manufacturer\":\"ACME Co.\",\"model\":\"7865\",\"deviceClass\":\"A\",\"description\":\"My shiny device\",\"fwVersion\":\"1.0.0\",\"hwVersion\":\"1.0\",\"descriptiveLocation\":\"Office 5, D Block\"},\"metadata\":{\"customField1\":\"customValue1\",\"customField2\":\"customValue2\"},\"refs\":{\"physicalInterface\":\"/api/v0002/device/types/myDeviceType/physicalinterface\",\"logicalInterfaces\":\"/api/v0002/device/types/myDeviceType/logicalinterfaces\",\"mappings\":\"/api/v0002/device/types/myDeviceType/mappings\"}}]}";
			client.SetResponse(HttpStatusCode.OK,json);
			var result= client.GetAllDraftDeviceType("","58258b8146e0fb0001458b7c");
			Assert.NotNull(result);
			Assert.NotNull(result["results"]);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/device/types?logicalInterfaceId=58258b8146e0fb0001458b7c",request.Resource);
		}
		[Test]
		public void DeviceType_h_OperateDraftDeviceType()
		{
			string json ="{\"message\":\"CUDIM0303I: State update configuration for Device Type 'myDeviceType' is not valid\",\"details\":{\"id\":\"CUDIM0303I\",\"properties\":[\"Device Type\",\"myDeviceType\"]},\"failures\":{\"message\":\"CUDVS0301E: The device type 'myDeviceType' does not have any mappings defined for it\",\"details\":{\"id\":\"CUDVS0301E\",\"properties\":[\"myDeviceType\"]}}}";
			//string json = "{\"message\":\"CUDIM0300I: State update configuration for Device Type 'demotest' has been successfully submitted for activation.\",\"details\":{\"id\":\"CUDIM0300I\",\"properties\":[\"Device Type\",\"demotest\"]},\"failures\":[]}";
			client.SetResponse(HttpStatusCode.OK,json);
			OperationInfo action =  new OperationInfo(OperationInfo.Validate);
			OperationDraftResponse result =  client.OperateDraftDeviceType("typeId",action);
			Assert.NotNull(result);
			Assert.AreEqual("CUDIM0303I",result.details.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PATCH,request.Method);
			Assert.AreEqual("/draft/device/types/typeId",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(action),request.Parameters[0].Value);
		}
		[Test]
		public void DeviceType_i_GetAllDraftDeviceTypeLI()
		{
			string json ="[{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"582add5746e0fb0001741b38\",\"version\":\"active\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/schemas/582add5746e0fb0001741b38\"}}]";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceInfo[] coll = client.GetAllDraftDeviceTypeLI("typeId");
			var draftLI = coll[0];
			Assert.NotNull(draftLI);
			Assert.AreEqual("58258b8146e0fb0001458b7c",draftLI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/logicalinterfaces",request.Resource);
		}
		[Test]
		public void DeviceType_j_AddDraftDeviceTypeLI()
		{
			string json ="{\"id\":\"58258b8146e0fb0001458b7c\",\"name\":\"Light Bulb Interface\",\"description\":\"Canonical Light Bulb Interface\",\"schemaId\":\"582add5746e0fb0001741b38\",\"version\":\"active\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"schema\":\"/api/v0002/schemas/582add5746e0fb0001741b38\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			LogicalInterfaceInfo draftLI = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<LogicalInterfaceInfo>(json);
			draftLI = client.AddDraftDeviceTypeLI("typeId",draftLI);
			Assert.NotNull(draftLI);
			Assert.AreEqual("58258b8146e0fb0001458b7c",draftLI.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/logicalinterfaces",request.Resource);
		}
		[Test]
		public void DeviceType_k_DeleteDraftDeviceTypeLI(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftDeviceTypeLI("typeId","logicalinterfacesId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/logicalinterfaces/logicalinterfacesId",request.Resource);
		}
		[Test]
		public void DeviceType_l_GetAllDraftDeviceTypeMapping()
		{
			string json = "[{\"logicalInterfaceId\":\"58258b8146e0fb0001458b7c\",\"notificationStrategy\":\"on-state-change\",\"propertyMappings\":{\"1234\":{\"temperature\":\"$event.t\",\"humidity\":\"$event.h\"}},\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\"}]";
			client.SetResponse(HttpStatusCode.OK,json);
			MappingInfo[] coll = client.GetAllDraftDeviceTypeMapping("typeId");
			var draft = coll[0];
			Assert.NotNull(draft);
			Assert.AreEqual("58258b8146e0fb0001458b7c",draft.logicalInterfaceId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/mappings",request.Resource);
		}
		[Test]
		public void DeviceType_m_AddDraftDeviceTypeMapping()
		{
			string json = "{\"logicalInterfaceId\":\"58258b8146e0fb0001458b7c\",\"notificationStrategy\":\"on-state-change\",\"propertyMappings\":{\"1234\":{\"temperature\":\"$event.t\",\"humidity\":\"$event.h\"}},\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\"}";
			client.SetResponse(HttpStatusCode.OK,json);
			MappingDraft dft = new MappingDraft();
			dft.logicalInterfaceId = "58258b8146e0fb0001458b7c";
			dft.notificationStrategy = "never";
			dft.propertyMappings =  new {};
			MappingInfo result = client.AddDraftDeviceTypeMapping("typeId",dft);
			Assert.NotNull(result);
			Assert.AreEqual("58258b8146e0fb0001458b7c",result.logicalInterfaceId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/mappings",request.Resource);
			Assert.AreEqual( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dft) ,request.Parameters[0].Value);
		}
		[Test]
		public void DeviceType_n_DeleteDraftDeviceTypeMapping(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftDeviceTypeMapping("typeId","logicalinterfacesId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/mappings/logicalinterfacesId",request.Resource);
		}
		[Test]
		public void DeviceType_o_GetDraftDeviceTypeMapping()
		{
			string json = "{\"logicalInterfaceId\":\"58258b8146e0fb0001458b7c\",\"notificationStrategy\":\"on-state-change\",\"propertyMappings\":{\"1234\":{\"temperature\":\"$event.t\",\"humidity\":\"$event.h\"}},\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\"}";
			client.SetResponse(HttpStatusCode.OK,json);
			MappingInfo result= client.GetDraftDeviceTypeMapping("typeId","58258b8146e0fb0001458b7c");
			Assert.NotNull(result);
			Assert.AreEqual("58258b8146e0fb0001458b7c",result.logicalInterfaceId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/mappings/58258b8146e0fb0001458b7c",request.Resource);
		}
		[Test]
		public void DeviceType_o_UpdatedDraftDeviceTypeMapping()
		{
			
			string json = "{\"logicalInterfaceId\":\"58258b8146e0fb0001458b7c\",\"notificationStrategy\":\"on-state-change\",\"propertyMappings\":{\"1234\":{\"temperature\":\"$event.t\",\"humidity\":\"$event.h\"}},\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\"}";
			client.SetResponse(HttpStatusCode.OK,json);
			MappingDraft dft = new MappingDraft();
			dft.logicalInterfaceId = "58258b8146e0fb0001458b7c";
			dft.notificationStrategy = "never";
			dft.propertyMappings =  new {};
			MappingInfo result = client.UpdatedDraftDeviceTypeMapping("typeId",dft);
			Assert.NotNull(result);
			Assert.AreEqual("58258b8146e0fb0001458b7c",result.logicalInterfaceId);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.PUT,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/mappings/58258b8146e0fb0001458b7c",request.Resource);
			Assert.AreEqual( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dft) ,request.Parameters[0].Value);
		
		}
		[Test]
		public void DeviceType_p_AddDraftDeviceTypePI()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"changed to new discription\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesInfo draftPI = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PhysicalInterfacesInfo>(json);
			PhysicalInterfacesInfo result =  client.AddDraftDeviceTypePI("typeId",draftPI);
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb000174",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.POST,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/physicalinterface",request.Resource);
			Assert.AreEqual(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(draftPI) ,request.Parameters[0].Value);
		}
		[Test]
		public void DeviceType_q_GetDraftDeviceTypePI()
		{
			string json ="{\"id\":\"582add5746e0fb000174\",\"name\":\"MyBulbCo Light Bulb Interface\",\"description\":\"changed to new discription\",\"version\":\"draft\",\"created\":\"2016-09-16T13:59:22Z\",\"createdBy\":\"john.doe@us.ibm.com\",\"updated\":\"2016-09-16T15:26:12Z\",\"updatedBy\":\"fred.bloggs@uk.ibm.com\",\"refs\":{\"events\":\"/api/v0002/draft/physicalinterfaces/582add5746e0fb000174/events\"}}";
			client.SetResponse(HttpStatusCode.OK,json);
			PhysicalInterfacesInfo result =  client.GetDraftDeviceTypePI("typeId");
			Assert.NotNull(result);
			Assert.AreEqual("582add5746e0fb000174",result.id);
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.GET,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/physicalinterface",request.Resource);
		}
		[Test]
		public void DeviceType_r_DeleteDraftDeviceTypePI(){
			client.SetResponse(HttpStatusCode.OK,"");
			client.DeleteDraftDeviceTypePI("typeId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/physicalinterface",request.Resource);
		}
		
		[Test,ExpectedException (typeof (System.Net.WebException))]
		public void RestHandler_exception(){
			client.SetResponse(HttpStatusCode.InternalServerError,"");
			client.DeleteDraftDeviceTypePI("typeId");
			IRestRequest request = client.GetRequest();
			Assert.AreEqual(Method.DELETE,request.Method);
			Assert.AreEqual("/draft/device/types/typeId/physicalinterface",request.Resource);
		}
	}
}
