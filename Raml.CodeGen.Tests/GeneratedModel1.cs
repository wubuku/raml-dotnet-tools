
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RAML.Api.Core;
using Raml.Common;

namespace Sales
{
    public partial class Preferred
    {
        private readonly BoxApi proxy;

        internal Preferred(BoxApi proxy)
        {
            this.proxy = proxy;
        }

        
        public virtual async Task<ApiResponse> Get()
        {

            var url = "contacts/preferred";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };

        }

        		/// <param name="request">ApiRequest</param>
        public virtual async Task<ApiResponse> Get(ApiRequest request)
        {

            var url = "contacts/preferred";
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };
        }

    }

    public partial class Contacts
    {
        private readonly BoxApi proxy;

        internal Contacts(BoxApi proxy)
        {
            this.proxy = proxy;
        }
        public virtual Preferred Preferred
        {
            get { return new Preferred(proxy); }
        }

        		/// <param name="id"></param>
        public virtual async Task<Models.ContactsGetResponse> Get(string id)
        {

            var url = "contacts/{id}";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					SchemaValidator.ValidateWithExceptions("{ \"$schema\": \"http://json-schema.org/schema\",  \"type\": \"object\",  \"description\": \"a contact\",  \"properties\": {    \"name\": { \"type\": \"string\", \"required\" : \"true\" },    \"email\": { \"type\": \"string\", \"required\" : \"true\" },    \"phone\": { \"type\": \"string\", \"required\" : \"true\" }  }}", response.Content);
				}
					
			}

            return new Models.ContactsGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => SchemaValidator.IsValidJSON("{ \"$schema\": \"http://json-schema.org/schema\",  \"type\": \"object\",  \"description\": \"a contact\",  \"properties\": {    \"name\": { \"type\": \"string\", \"required\" : \"true\" },    \"email\": { \"type\": \"string\", \"required\" : \"true\" },    \"phone\": { \"type\": \"string\", \"required\" : \"true\" }  }}", response.Content), true)
                                            };

        }

        		/// <param name="request">Models.ContactsGetRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.ContactsGetResponse> Get(Models.ContactsGetRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "contacts/{id}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
	        var response = await proxy.Client.SendAsync(req);
			if (proxy.SchemaValidation.Enabled && proxy.SchemaValidation.RaiseExceptions)
            {
				if(proxy.SchemaValidation.RaiseExceptions)
				{
					SchemaValidator.ValidateWithExceptions("{ \"$schema\": \"http://json-schema.org/schema\",  \"type\": \"object\",  \"description\": \"a contact\",  \"properties\": {    \"name\": { \"type\": \"string\", \"required\" : \"true\" },    \"email\": { \"type\": \"string\", \"required\" : \"true\" },    \"phone\": { \"type\": \"string\", \"required\" : \"true\" }  }}", response.Content);
				}
				
            }
            return new Models.ContactsGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => SchemaValidator.IsValidJSON("{ \"$schema\": \"http://json-schema.org/schema\",  \"type\": \"object\",  \"description\": \"a contact\",  \"properties\": {    \"name\": { \"type\": \"string\", \"required\" : \"true\" },    \"email\": { \"type\": \"string\", \"required\" : \"true\" },    \"phone\": { \"type\": \"string\", \"required\" : \"true\" }  }}", response.Content), true)
                                            };
        }

        		/// <param name="company"></param>
        public virtual async Task<ApiResponse> GetByCompany(string company)
        {

            var url = "contacts/{company}";
            url = url.Replace("{company}", company.ToString());
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };

        }

        		/// <param name="request">Models.ContactsGetByCompanyRequest</param>
        public virtual async Task<ApiResponse> GetByCompany(Models.ContactsGetByCompanyRequest request)
        {

            var url = "contacts/{company}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Company == null)
				throw new InvalidOperationException("Uri Parameter Company cannot be null");

            url = url.Replace("{company}", request.UriParameters.Company.ToString());
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };
        }

    }

    public partial class CompaniesPreferred
    {
        private readonly BoxApi proxy;

        internal CompaniesPreferred(BoxApi proxy)
        {
            this.proxy = proxy;
        }

        
        public virtual async Task<ApiResponse> Get()
        {

            var url = "companies/preferred";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };

        }

        		/// <param name="request">ApiRequest</param>
        public virtual async Task<ApiResponse> Get(ApiRequest request)
        {

            var url = "companies/preferred";
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };
        }

    }

    public partial class Companies
    {
        private readonly BoxApi proxy;

        internal Companies(BoxApi proxy)
        {
            this.proxy = proxy;
        }
        public virtual CompaniesPreferred CompaniesPreferred
        {
            get { return new CompaniesPreferred(proxy); }
        }

        
        public virtual async Task<ApiResponse> Get()
        {

            var url = "companies";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };

        }

        		/// <param name="request">ApiRequest</param>
        public virtual async Task<ApiResponse> Get(ApiRequest request)
        {

            var url = "companies";
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												IsValid = new Lazy<bool>(() => true, true)
                                            };
        }

    }

    /// <summary>
    /// Main class for grouping root resources. Nested resources are defined as properties. The constructor can optionally receive an URL and HttpClient instance to override the default ones.
    /// </summary>
    public partial class BoxApi
    {
		public SchemaValidationSettings SchemaValidation; 
        protected readonly HttpClient client;
        public const string BaseUri = "http://localhost:8081/api/";

        internal HttpClient Client { get { return client; } }


        public BoxApi(string endpointUrl)
        {
			SchemaValidation = new SchemaValidationSettings
			{
				Enabled = true,
				RaiseExceptions = true
			};
			

            if(string.IsNullOrWhiteSpace(endpointUrl))
                throw new ArgumentException("You must specify the endpoint URL", "endpointUrl");

			if (endpointUrl.Contains("{"))
			{
				var regex = new Regex(@"\{([^\}]+)\}");
				var matches = regex.Matches(endpointUrl);
				var parameters = new List<string>();
				foreach (Match match in matches)
				{
					parameters.Add(match.Groups[1].Value);
				}
				throw new InvalidOperationException("Please replace parameter/s " + string.Join(", ", parameters) + " in the URL before passing it to the constructor ");
			}

            client = new HttpClient {BaseAddress = new Uri(endpointUrl)};
        }

        public BoxApi(HttpClient httpClient)
        {
            if(httpClient.BaseAddress == null)
                throw new InvalidOperationException("You must set the BaseAddress property of the HttpClient instance");

            client = httpClient;
        }

        
        public virtual Contacts Contacts
        {
            get { return new Contacts(this); }
        }
                
        public virtual Companies Companies
        {
            get { return new Companies(this); }
        }
                


		public void AddDefaultRequestHeader(string name, string value)
		{
			client.DefaultRequestHeaders.Add(name, value);
		}

		public void AddDefaultRequestHeader(string name, IEnumerable<string> values)
		{
			client.DefaultRequestHeaders.Add(name, values);
		}


    }

} // end namespace


namespace Sales.Models
{
    public partial class IdGetOKResponseContent 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    } // end class

    /// <summary>
    /// Uri Parameters for resource /{id}
    /// </summary>
    public partial class ContactsIdUriParameters 
    {
        public string Id { get; set; }

    } // end class

    /// <summary>
    /// Uri Parameters for resource /{company}
    /// </summary>
    public partial class ContactsCompanyUriParameters 
    {
        public string Company { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Get of class Contacts
    /// </summary>
    public partial class ContactsGetRequest : ApiRequest
    {
        public ContactsGetRequest(ContactsIdUriParameters UriParameters)
        {
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public ContactsIdUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method GetByCompany of class Contacts
    /// </summary>
    public partial class ContactsGetByCompanyRequest : ApiRequest
    {
        public ContactsGetByCompanyRequest(ContactsCompanyUriParameters UriParameters)
        {
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public ContactsCompanyUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Response object for method Get of class Contacts
    /// </summary>

    public partial class ContactsGetResponse : ApiResponse
    {


	    private IdGetOKResponseContent typedContent;
        /// <summary>
        /// Typed Response content
        /// </summary>
        public IdGetOKResponseContent Content 
    	{
	        get
	        {
		        if (typedContent != null)
			        return typedContent;

		        var task =  Formatters != null && Formatters.Any() 
                            ? RawContent.ReadAsAsync<IdGetOKResponseContent>(Formatters)
                            : RawContent.ReadAsAsync<IdGetOKResponseContent>();
		        task.Wait();
		        typedContent = task.GetAwaiter().GetResult();
		        return typedContent;
	        }
	    }

    } // end class


} // end Models namespace