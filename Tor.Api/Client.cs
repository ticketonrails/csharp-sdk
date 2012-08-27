using System;
using System.Collections.Generic;
using RestSharp;

namespace Tor.Api
{
	public class Client
	{
		public string ApiKey { get; set; }
		public string ProjectDomain { get; set; }
		public string ApiToken { get; set; }
    	public string ApiVersion { get; set; }
    	public string ServerUrl { get; set; }
		
		public Client(string apiKey, string projectDomain)
		{
			this.ApiKey = apiKey;
			this.ProjectDomain = projectDomain;
			this.ServerUrl = "http://api.ticketonrails.com";
			this.ApiVersion = "1";
		}
		
		private Response MakeRequest(string url, string method = "GET", Dictionary<string, string> parameters = null){
			string requestUrl = string.Format("{0}/v{1}{2}", this.ServerUrl, this.ApiVersion, url);
			var client = new RestClient(this.ServerUrl);
			var request = new RestRequest(requestUrl, Method.POST);
			
			// execute the request
			var apiresponse = client.Execute(request);
			var content = apiresponse.Content;
			
			Response response = new Response();
			return response;
		}
		
		public Response NewTicket(Ticket ticket){
			return this.MakeRequest("/tickets", "POST", null);
		}
	}
}

