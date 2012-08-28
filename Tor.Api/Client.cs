using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using RestSharp;
using System;

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

            using (MD5 md5Hash = MD5.Create())
            {
                string ApiKeyHash = GetMd5Hash(md5Hash, this.ApiKey).ToLower();
                ApiToken = GetMd5Hash(md5Hash, string.Concat(this.ProjectDomain.ToLower(), ApiKeyHash)).ToLower();
            }
        }

        private IRestResponse MakeRequest(string url, RestSharp.Method method, Dictionary<string, string> parameters = null, string attachment = null)
        {
            string requestUrl = string.Format("v{0}{1}", this.ApiVersion, url);
            var client = new RestClient(this.ServerUrl);
            var request = new RestRequest(requestUrl, method);

            //adding parameters
            foreach (KeyValuePair<string, string> param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }
            //token
            request.AddParameter("token", this.ApiToken);

            //file
            if (!string.IsNullOrEmpty(attachment))
            {
                if (File.Exists(attachment))
                {
                    request.AddFile("attachment", attachment);
                }
            }

            return client.Execute(request);
        }

        public Response NewTicket(Ticket ticket)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ticket", ticket.ToJson<Ticket>());

            // execute the request
            var apiresponse = this.MakeRequest("/tickets", RestSharp.Method.POST, parameters, ticket.Attachment);
            var content = apiresponse.Content;
            Response response = null;
            try
            {
                if ((int)apiresponse.StatusCode >= 400)
                {
                    throw new Exception(content);
                }
                else
                {
                    response = content.FromJson<Response>();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(content);
            }

            return response;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}

