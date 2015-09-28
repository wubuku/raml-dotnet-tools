




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RAML.Api.Core;
using Raml.Common;


namespace Movies
{
    /// <summary>
    /// rent a movie
    /// </summary>
    public partial class Rent
    {
        private readonly MoviesApi proxy;

        internal Rent(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
		/// rent a movie
		/// </summary>
		/// <param name="content"></param>
		/// <param name="id"></param>
        public virtual async Task<ApiResponse> Put(string content, string id)
        {

            var url = "movies/{id}/rent";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Put, url);
	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            req.Content = new StringContent(content);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// rent a movie
		/// </summary>
		/// <param name="request">Models.RentPutRequest</param>
        public virtual async Task<ApiResponse> Put(Models.RentPutRequest request)
        {

            var url = "movies/{id}/rent";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Put, url);

	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            req.Content = request.Content;
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

    }

    /// <summary>
    /// return a movie
    /// </summary>
    public partial class Return
    {
        private readonly MoviesApi proxy;

        internal Return(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
		/// return a movie
		/// </summary>
		/// <param name="content"></param>
		/// <param name="id"></param>
        public virtual async Task<ApiResponse> Put(string content, string id)
        {

            var url = "movies/{id}/return";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Put, url);
	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            req.Content = new StringContent(content);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// return a movie
		/// </summary>
		/// <param name="request">Models.ReturnPutRequest</param>
        public virtual async Task<ApiResponse> Put(Models.ReturnPutRequest request)
        {

            var url = "movies/{id}/return";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Put, url);

	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            req.Content = request.Content;
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

    }

    public partial class Wishlist
    {
        private readonly MoviesApi proxy;

        internal Wishlist(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
		/// gets the current user movies wishlist
		/// </summary>
        public virtual async Task<Models.WishlistGetResponse> Get()
        {

            var url = "movies/wishlist";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
					
			}

            return new Models.WishlistGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };

        }

        /// <summary>
		/// gets the current user movies wishlist
		/// </summary>
		/// <param name="request">ApiRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.WishlistGetResponse> Get(ApiRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "movies/wishlist";
            var req = new HttpRequestMessage(HttpMethod.Get, url);

	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
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
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
				
            }
            return new Models.WishlistGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };
        }

        /// <summary>
		/// add a movie to the current user movies wishlist
		/// </summary>
		/// <param name="content"></param>
		/// <param name="id"></param>
        public virtual async Task<ApiResponse> Post(string content, string id)
        {

            var url = "movies/wishlist/{id}";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Post, url);
	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            req.Content = new StringContent(content);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// add a movie to the current user movies wishlist
		/// </summary>
		/// <param name="request">Models.WishlistPostRequest</param>
        public virtual async Task<ApiResponse> Post(Models.WishlistPostRequest request)
        {

            var url = "movies/wishlist/{id}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Post, url);

	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            req.Content = request.Content;
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

        /// <summary>
		/// removes a movie from the current user movies wishlist
		/// </summary>
		/// <param name="id"></param>
        public virtual async Task<ApiResponse> Delete(string id)
        {

            var url = "movies/wishlist/{id}";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// removes a movie from the current user movies wishlist
		/// </summary>
		/// <param name="request">Models.WishlistDeleteRequest</param>
        public virtual async Task<ApiResponse> Delete(Models.WishlistDeleteRequest request)
        {

            var url = "movies/wishlist/{id}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Delete, url);

	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
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
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

    }

    public partial class Rented
    {
        private readonly MoviesApi proxy;

        internal Rented(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
		/// gets the user rented movies
		/// </summary>
        public virtual async Task<Models.RentedGetResponse> Get()
        {

            var url = "movies/rented";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
					
			}

            return new Models.RentedGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };

        }

        /// <summary>
		/// gets the user rented movies
		/// </summary>
		/// <param name="request">ApiRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.RentedGetResponse> Get(ApiRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "movies/rented";
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
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
				
            }
            return new Models.RentedGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };
        }

    }

    public partial class Available
    {
        private readonly MoviesApi proxy;

        internal Available(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
		/// get all movies that are not currently rented
		/// </summary>
        public virtual async Task<Models.AvailableGetResponse> Get()
        {

            var url = "movies/available";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
					
			}

            return new Models.AvailableGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };

        }

        /// <summary>
		/// get all movies that are not currently rented
		/// </summary>
		/// <param name="request">ApiRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.AvailableGetResponse> Get(ApiRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "movies/available";
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
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
				
            }
            return new Models.AvailableGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };
        }

    }

    public partial class Movies
    {
        private readonly MoviesApi proxy;

        internal Movies(MoviesApi proxy)
        {
            this.proxy = proxy;
        }
        public virtual Wishlist Wishlist
        {
            get { return new Wishlist(proxy); }
        }
        public virtual Rented Rented
        {
            get { return new Rented(proxy); }
        }
        public virtual Available Available
        {
            get { return new Available(proxy); }
        }
        public virtual Rent Rent
        {
            get { return new Rent(proxy); }
        }
        public virtual Return Return
        {
            get { return new Return(proxy); }
        }

        /// <summary>
		/// gets all movies in the catalogue
		/// </summary>
        public virtual async Task<Models.MoviesGetResponse> Get()
        {

            var url = "movies";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync(Models.MoviesGetResponse.GetSchema(response.StatusCode), response.Content);
				}
					
			}

            return new Models.MoviesGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid(Models.MoviesGetResponse.GetSchema(response.StatusCode), response.Content), true)
                                            };

        }

        /// <summary>
		/// gets all movies in the catalogue
		/// </summary>
		/// <param name="request">Models.MoviesGetRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.MoviesGetResponse> Get(Models.MoviesGetRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "movies";
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            if(request.Headers != null)
            {
                foreach(var header in request.Headers.Headers)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
	        var response = await proxy.Client.SendAsync(req);
			if (proxy.SchemaValidation.Enabled && proxy.SchemaValidation.RaiseExceptions)
            {
				if(proxy.SchemaValidation.RaiseExceptions)
				{
					await SchemaValidator.ValidateWithExceptionAsync(Models.MoviesGetResponse.GetSchema(response.StatusCode), response.Content);
				}
				
            }
            return new Models.MoviesGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid(Models.MoviesGetResponse.GetSchema(response.StatusCode), response.Content), true)
                                            };
        }

        /// <summary>
		/// adds a movie to the catalogue
		/// </summary>
		/// <param name="moviespostrequestcontent"></param>
        public virtual async Task<ApiResponse> Post(Models.MoviesPostRequestContent moviespostrequestcontent)
        {

            var url = "movies";
            var req = new HttpRequestMessage(HttpMethod.Post, url);
	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            req.Content = new ObjectContent(typeof(Models.MoviesPostRequestContent), moviespostrequestcontent, new JsonMediaTypeFormatter());                           
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// adds a movie to the catalogue
		/// </summary>
		/// <param name="request">Models.MoviesPostRequest</param>
        public virtual async Task<ApiResponse> Post(Models.MoviesPostRequest request)
        {

            var url = "movies";
            var req = new HttpRequestMessage(HttpMethod.Post, url);

	        if (string.IsNullOrEmpty(proxy.OAuthAccessToken))
				throw new InvalidOperationException("This API call is secured with OAuth, you must provide an access token (set OAuthAccessToken before calling this method)");
            req.Headers.Add("Authorization", "Bearer " + proxy.OAuthAccessToken);
            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            if(request.Formatter == null)
                request.Formatter = new JsonMediaTypeFormatter();
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

        /// <summary>
		/// get the info of a movie
		/// </summary>
		/// <param name="id"></param>
        public virtual async Task<Models.MoviesGetByIdResponse> GetById(string id)
        {

            var url = "movies/{id}";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync(Models.MoviesGetByIdResponse.GetSchema(response.StatusCode), response.Content);
				}
					
			}

            var headers = new Models.MultipleGetByIdHeader();
            headers.SetProperties(response.Headers, response.StatusCode);
            return new Models.MoviesGetByIdResponse  
                                            {
                                                RawContent = response.Content,
                                                Headers = headers, 
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid(Models.MoviesGetByIdResponse.GetSchema(response.StatusCode), response.Content), true)
                                            };

        }

        /// <summary>
		/// get the info of a movie
		/// </summary>
		/// <param name="request">Models.MoviesGetByIdRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.MoviesGetByIdResponse> GetById(Models.MoviesGetByIdRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "movies/{id}";
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
					await SchemaValidator.ValidateWithExceptionAsync(Models.MoviesGetByIdResponse.GetSchema(response.StatusCode), response.Content);
				}
				
            }
            var headers = new Models.MultipleGetByIdHeader();
            headers.SetProperties(response.Headers, response.StatusCode);
            return new Models.MoviesGetByIdResponse  
                                            {
                                                RawContent = response.Content,
                                                Headers = headers, 
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid(Models.MoviesGetByIdResponse.GetSchema(response.StatusCode), response.Content), true)
                                            };
        }

        /// <summary>
		/// update the info of a movie
		/// </summary>
		/// <param name="moviesidputrequestcontent"></param>
		/// <param name="id"></param>
        public virtual async Task<ApiResponse> Put(Models.MoviesIdPutRequestContent moviesidputrequestcontent, string id)
        {

            var url = "movies/{id}";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Put, url);
            req.Content = new ObjectContent(typeof(Models.MoviesIdPutRequestContent), moviesidputrequestcontent, new JsonMediaTypeFormatter());                           
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// update the info of a movie
		/// </summary>
		/// <param name="request">Models.MoviesPutRequest</param>
        public virtual async Task<ApiResponse> Put(Models.MoviesPutRequest request)
        {

            var url = "movies/{id}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Put, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            if(request.Formatter == null)
                request.Formatter = new JsonMediaTypeFormatter();
	        var response = await proxy.Client.SendAsync(req);
            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

        /// <summary>
		/// remove a movie from the catalogue
		/// </summary>
		/// <param name="id"></param>
        public virtual async Task<ApiResponse> Delete(string id)
        {

            var url = "movies/{id}";
            url = url.Replace("{id}", id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
	        var response = await proxy.Client.SendAsync(req);

            return new ApiResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };

        }

        /// <summary>
		/// remove a movie from the catalogue
		/// </summary>
		/// <param name="request">Models.MoviesDeleteRequest</param>
        public virtual async Task<ApiResponse> Delete(Models.MoviesDeleteRequest request)
        {

            var url = "movies/{id}";
			if(request.UriParameters == null)
				throw new InvalidOperationException("Uri Parameters cannot be null");               

			if(request.UriParameters.Id == null)
				throw new InvalidOperationException("Uri Parameter Id cannot be null");

            url = url.Replace("{id}", request.UriParameters.Id.ToString());
            var req = new HttpRequestMessage(HttpMethod.Delete, url);

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
												SchemaValidation = new Lazy<SchemaValidationResults>(() => new SchemaValidationResults(true), true)
                                            };
        }

    }

    public partial class Search
    {
        private readonly MoviesApi proxy;

        internal Search(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
		/// search movies by name or director
		/// </summary>
		/// <param name="content"></param>
		/// <param name="postsearchquery">query properties</param>
        public virtual async Task<Models.SearchPostResponse> Post(string content, Models.PostSearchQuery postsearchquery)
        {

            var url = "search";
            if(postsearchquery != null)
            {
                url += "?";
                if(postsearchquery.Name != null)
					url += "&name=" + postsearchquery.Name;
                if(postsearchquery.Director != null)
					url += "&director=" + postsearchquery.Director;
                if(postsearchquery.Rented != null)
					url += "&rented=" + postsearchquery.Rented.ToString().ToLower();
            }
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = new StringContent(content);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
					
			}

            return new Models.SearchPostResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };

        }

        /// <summary>
		/// search movies by name or director
		/// </summary>
		/// <param name="request">Models.SearchPostRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.SearchPostResponse> Post(Models.SearchPostRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "search";
            if(request.Query != null)
            {
                url += "?";
                if(request.Query.Name != null)
                    url += "&name=" + request.Query.Name;
                if(request.Query.Director != null)
                    url += "&director=" + request.Query.Director;
                if(request.Query.Rented != null)
                    url += "&rented=" + request.Query.Rented.ToString().ToLower();
            }
            var req = new HttpRequestMessage(HttpMethod.Post, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            req.Content = request.Content;
	        var response = await proxy.Client.SendAsync(req);
			if (proxy.SchemaValidation.Enabled && proxy.SchemaValidation.RaiseExceptions)
            {
				if(proxy.SchemaValidation.RaiseExceptions)
				{
					await SchemaValidator.ValidateWithExceptionAsync("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content);
				}
				
            }
            return new Models.SearchPostResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}", response.Content), true)
                                            };
        }

    }

    /// <summary>
    /// Main class for grouping root resources. Nested resources are defined as properties. The constructor can optionally receive an URL and HttpClient instance to override the default ones.
    /// </summary>
    public partial class MoviesApi
    {

		public SchemaValidationSettings SchemaValidation { get; private set; } 

        protected readonly HttpClient client;
        public const string BaseUri = "http://movies.com/api/";

        internal HttpClient Client { get { return client; } }




        public string OAuthAccessToken { get; set; }

		private string oauthAuthorizeUrl = "https://localhost:8081/oauth/authorize";
		public string OAuthAuthorizeUrl { get { return oauthAuthorizeUrl; } set { oauthAuthorizeUrl = value; } }

   		private string oauthAccessTokenUrl = "https://localhost:8081/oauth/access_token";
		public string OAuthAccessTokenUrl { get { return oauthAccessTokenUrl; } set { oauthAccessTokenUrl = value; } }

        public MoviesApi(string endpointUrl)
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

        public MoviesApi(HttpClient httpClient)
        {
            if(httpClient.BaseAddress == null)
                throw new InvalidOperationException("You must set the BaseAddress property of the HttpClient instance");

            client = httpClient;

			SchemaValidation = new SchemaValidationSettings
			{
				Enabled = true,
				RaiseExceptions = true
			};
        }

        
        public virtual Movies Movies
        {
            get { return new Movies(this); }
        }
                
        public virtual Search Search
        {
            get { return new Search(this); }
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








namespace Movies.Models
{
    public partial class  MoviesPostRequestContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }


    } // end class

    public partial class  MoviesIdPutRequestContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }


    } // end class

    public partial class  MoviesGetOKResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }

		[JsonProperty("rented")]
        public bool Rented { get; set; }


    } // end class

    public partial class  MoviesGetBadRequestResponseContent 
    {
		[JsonProperty("error")]
        public string Error { get; set; }

		[JsonProperty("code")]
        public int Code { get; set; }


    } // end class

    public partial class  MoviesIdGetOKResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }

		[JsonProperty("rented")]
        public bool Rented { get; set; }


    } // end class

    public partial class  MoviesIdGetBadRequestResponseContent 
    {
		[JsonProperty("error")]
        public string Error { get; set; }

		[JsonProperty("code")]
        public int Code { get; set; }


    } // end class

    public partial class  MoviesIdGetNotFoundResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("error")]
        public string Error { get; set; }

		[JsonProperty("code")]
        public int Code { get; set; }


    } // end class

    public partial class  MoviesWishlistGetOKResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }

		[JsonProperty("rented")]
        public bool Rented { get; set; }


    } // end class

    public partial class  MoviesRentedGetOKResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }

		[JsonProperty("rented")]
        public bool Rented { get; set; }


    } // end class

    public partial class  MoviesAvailableGetOKResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }

		[JsonProperty("rented")]
        public bool Rented { get; set; }


    } // end class

    public partial class  SearchPostOKResponseContent 
    {
		[JsonProperty("id")]
        public int Id { get; set; }

		[JsonProperty("name")]
        public string Name { get; set; }

		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("genre")]
        public string Genre { get; set; }

		[JsonProperty("cast")]
        public string Cast { get; set; }

		[JsonProperty("duration")]
        public decimal Duration { get; set; }

		[JsonProperty("storyline")]
        public string Storyline { get; set; }

		[JsonProperty("language")]
        public string Language { get; set; }

		[JsonProperty("rented")]
        public bool Rented { get; set; }


    } // end class

    /// <summary>
    /// Multiple Response Types MoviesGetOKResponseContent, MoviesGetBadRequestResponseContent
    /// </summary>
    public partial class  MultipleMoviesGet : ApiMultipleResponse
    {
        static readonly Dictionary<HttpStatusCode, string> schemas = new Dictionary<HttpStatusCode, string>
        {
			{ (HttpStatusCode)200, "{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"array\",  \"description\": \"movies\",  \"items\":     {      \"type\": \"object\",        \"properties\": {        \"id\": { \"type\": \"integer\" },        \"name\": { \"type\": \"string\"},        \"director\": { \"type\": \"string\"},        \"genre\": { \"type\": \"string\" },        \"cast\":{ \"type\": \"string\" },        \"duration\":{ \"type\": \"number\" },        \"storyline\":{ \"type\": \"string\" },        \"language\":{ \"type\": \"string\" },        \"rented\":{ \"type\": \"boolean\" }    }  }}"},
			{ (HttpStatusCode)400, "{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"object\",  \"description\": \"bad request error\",  \"properties\":     {      \"error\": { \"type\": \"string\"},      \"code\": { \"type\": \"integer\" }    }  }"},
		};
        
		public static string GetSchema(HttpStatusCode statusCode)
        {
            return schemas.ContainsKey(statusCode) ? schemas[statusCode] : string.Empty;
        }
        
        public MultipleMoviesGet()
        {
            names.Add((HttpStatusCode)200, "MoviesGetOKResponseContent");
            types.Add((HttpStatusCode)200, typeof(IList<MoviesGetOKResponseContent>));
            names.Add((HttpStatusCode)400, "MoviesGetBadRequestResponseContent");
            types.Add((HttpStatusCode)400, typeof(MoviesGetBadRequestResponseContent));
        }
        public IList<MoviesGetOKResponseContent> MoviesGetOKResponseContent { get; set; }

        public MoviesGetBadRequestResponseContent MoviesGetBadRequestResponseContent { get; set; }


    } // end class

    /// <summary>
    /// Multiple Response Types MoviesIdGetOKResponseContent, MoviesIdGetBadRequestResponseContent, MoviesIdGetNotFoundResponseContent
    /// </summary>
    public partial class  MultipleMoviesIdGet : ApiMultipleResponse
    {
        static readonly Dictionary<HttpStatusCode, string> schemas = new Dictionary<HttpStatusCode, string>
        {
			{ (HttpStatusCode)200, "{   \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"object\",  \"description\": \"a movie\",  \"properties\": {    \"id\": { \"type\": \"integer\"},    \"name\": { \"type\": \"string\"},    \"director\": { \"type\": \"string\"},    \"genre\": { \"type\": \"string\" },    \"cast\":{ \"type\": \"string\" },    \"duration\":{ \"type\": \"number\" },    \"storyline\":{ \"type\": \"string\" },    \"language\":{ \"type\": \"string\" },    \"rented\":{ \"type\": \"boolean\" }  }}"},
			{ (HttpStatusCode)400, "{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"object\",  \"description\": \"bad request error\",  \"properties\":     {      \"error\": { \"type\": \"string\"},      \"code\": { \"type\": \"integer\" }    }  }"},
			{ (HttpStatusCode)404, "{ \"$schema\": \"http://json-schema.org/draft-03/schema\",  \"type\": \"object\",  \"description\": \"not found\",  \"properties\":     {      \"id\": { \"type\": \"integer\" },      \"error\": { \"type\": \"string\"},      \"code\": { \"type\": \"integer\" }    }  }"},
		};
        
		public static string GetSchema(HttpStatusCode statusCode)
        {
            return schemas.ContainsKey(statusCode) ? schemas[statusCode] : string.Empty;
        }
        
        public MultipleMoviesIdGet()
        {
            names.Add((HttpStatusCode)200, "MoviesIdGetOKResponseContent");
            types.Add((HttpStatusCode)200, typeof(MoviesIdGetOKResponseContent));
            names.Add((HttpStatusCode)400, "MoviesIdGetBadRequestResponseContent");
            types.Add((HttpStatusCode)400, typeof(MoviesIdGetBadRequestResponseContent));
            names.Add((HttpStatusCode)404, "MoviesIdGetNotFoundResponseContent");
            types.Add((HttpStatusCode)404, typeof(MoviesIdGetNotFoundResponseContent));
        }
        public MoviesIdGetOKResponseContent MoviesIdGetOKResponseContent { get; set; }

        public MoviesIdGetBadRequestResponseContent MoviesIdGetBadRequestResponseContent { get; set; }

        public MoviesIdGetNotFoundResponseContent MoviesIdGetNotFoundResponseContent { get; set; }


    } // end class

    public partial class  PostSearchQuery 
    {
        /// <summary>
        /// Name of the movie
        /// </summary>
		[JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Director of the movie
        /// </summary>
		[JsonProperty("director")]
        public string Director { get; set; }

		[JsonProperty("rented")]
        public bool? Rented { get; set; }


    } // end class

    /// <summary>
    /// Uri Parameters for resource /{id}
    /// </summary>
    public partial class  MoviesIdUriParameters 
    {
		[JsonProperty("id")]
        public string Id { get; set; }


    } // end class

    /// <summary>
    /// Uri Parameters for resource /rent
    /// </summary>
    public partial class  MoviesIdRentUriParameters 
    {
		[JsonProperty("id")]
        public string Id { get; set; }


    } // end class

    /// <summary>
    /// Uri Parameters for resource /return
    /// </summary>
    public partial class  MoviesIdReturnUriParameters 
    {
		[JsonProperty("id")]
        public string Id { get; set; }


    } // end class

    /// <summary>
    /// Uri Parameters for resource /{id}
    /// </summary>
    public partial class  MoviesWishlistIdUriParameters 
    {
		[JsonProperty("id")]
        public string Id { get; set; }


    } // end class

    public partial class GetMoviesHeader : ApiHeader
    {

		[JsonProperty("user")]
        public string User { get; set; }
		[JsonProperty("code")]
        public int? Code { get; set; }

    } // end class

    public partial class GetByIdMoviesOKResponseHeader : ApiResponseHeader
    {
		[JsonProperty("code")]
        public int? Code { get; set; }
		[JsonProperty("created")]
        public DateTime? Created { get; set; }

    } // end class

    public partial class GetByIdMoviesBadRequestResponseHeader : ApiResponseHeader
    {
		[JsonProperty("code")]
        public int? Code { get; set; }

    } // end class

    /// <summary>
    /// Multiple Header Types GetByIdMoviesOKResponseHeader, GetByIdMoviesBadRequestResponseHeader
    /// </summary>
    public partial class MultipleGetByIdHeader : ApiMultipleObject
    {
        
        public MultipleGetByIdHeader()
        {
            names.Add((HttpStatusCode)200, "GetByIdMoviesOKResponseHeader");
            types.Add((HttpStatusCode)200, typeof(GetByIdMoviesOKResponseHeader));
            names.Add((HttpStatusCode)400, "GetByIdMoviesBadRequestResponseHeader");
            types.Add((HttpStatusCode)400, typeof(GetByIdMoviesBadRequestResponseHeader));
        }

		public void SetProperties(HttpResponseHeaders headers, HttpStatusCode statusCode)
	    {
		    if (statusCode == (HttpStatusCode)200)
		    {
				var header = new GetByIdMoviesOKResponseHeader();
			    foreach (var responseHeader in headers)
			    {
				    var prop = header.GetType().GetProperties().FirstOrDefault(p => p.Name == NetNamingMapper.GetPropertyName(responseHeader.Key));
					if(prop != null)
						prop.SetValue(header, responseHeader.Value);
			    }
			    this.GetByIdMoviesOKResponseHeader = header;
				return;
		    }
		    if (statusCode == (HttpStatusCode)400)
		    {
				var header = new GetByIdMoviesBadRequestResponseHeader();
			    foreach (var responseHeader in headers)
			    {
				    var prop = header.GetType().GetProperties().FirstOrDefault(p => p.Name == NetNamingMapper.GetPropertyName(responseHeader.Key));
					if(prop != null)
						prop.SetValue(header, responseHeader.Value);
			    }
			    this.GetByIdMoviesBadRequestResponseHeader = header;
				return;
		    }
	    }
        public GetByIdMoviesOKResponseHeader GetByIdMoviesOKResponseHeader { get; set; }
        public GetByIdMoviesBadRequestResponseHeader GetByIdMoviesBadRequestResponseHeader { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Put of class Rent
    /// </summary>
    public partial class RentPutRequest : ApiRequest
    {
        public RentPutRequest(MoviesIdRentUriParameters UriParameters, HttpContent Content = null, MediaTypeFormatter Formatter = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public HttpContent Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }
        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesIdRentUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Put of class Return
    /// </summary>
    public partial class ReturnPutRequest : ApiRequest
    {
        public ReturnPutRequest(MoviesIdReturnUriParameters UriParameters, HttpContent Content = null, MediaTypeFormatter Formatter = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public HttpContent Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }
        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesIdReturnUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Post of class Wishlist
    /// </summary>
    public partial class WishlistPostRequest : ApiRequest
    {
        public WishlistPostRequest(MoviesWishlistIdUriParameters UriParameters, HttpContent Content = null, MediaTypeFormatter Formatter = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public HttpContent Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }
        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesWishlistIdUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Delete of class Wishlist
    /// </summary>
    public partial class WishlistDeleteRequest : ApiRequest
    {
        public WishlistDeleteRequest(MoviesWishlistIdUriParameters UriParameters)
        {
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesWishlistIdUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Get of class Movies
    /// </summary>
    public partial class MoviesGetRequest : ApiRequest
    {
        public MoviesGetRequest(GetMoviesHeader Headers = null)
        {
            this.Headers = Headers;
        }

        /// <summary>
        /// Typed Request headers
        /// </summary>
        public GetMoviesHeader Headers { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Post of class Movies
    /// </summary>
    public partial class MoviesPostRequest : ApiRequest
    {
        public MoviesPostRequest(MoviesPostRequestContent Content = null, MediaTypeFormatter Formatter = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public MoviesPostRequestContent Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }

    } // end class

    /// <summary>
    /// Request object for method GetById of class Movies
    /// </summary>
    public partial class MoviesGetByIdRequest : ApiRequest
    {
        public MoviesGetByIdRequest(MoviesIdUriParameters UriParameters)
        {
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesIdUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Put of class Movies
    /// </summary>
    public partial class MoviesPutRequest : ApiRequest
    {
        public MoviesPutRequest(MoviesIdUriParameters UriParameters, MoviesIdPutRequestContent Content = null, MediaTypeFormatter Formatter = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public MoviesIdPutRequestContent Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }
        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesIdUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Delete of class Movies
    /// </summary>
    public partial class MoviesDeleteRequest : ApiRequest
    {
        public MoviesDeleteRequest(MoviesIdUriParameters UriParameters)
        {
            this.UriParameters = UriParameters;
        }

        /// <summary>
        /// Request Uri Parameters
        /// </summary>
        public MoviesIdUriParameters UriParameters { get; set; }

    } // end class

    /// <summary>
    /// Request object for method Post of class Search
    /// </summary>
    public partial class SearchPostRequest : ApiRequest
    {
        public SearchPostRequest(HttpContent Content = null, MediaTypeFormatter Formatter = null, PostSearchQuery Query = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
            this.Query = Query;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public HttpContent Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }
        /// <summary>
        /// Request query string properties
        /// </summary>
        public PostSearchQuery Query { get; set; }

    } // end class

    /// <summary>
    /// Response object for method Get of class Wishlist
    /// </summary>

    public partial class WishlistGetResponse : ApiResponse
    {


	    private IList<MoviesWishlistGetOKResponseContent> typedContent;
        /// <summary>
        /// Typed Response content
        /// </summary>
        public IList<MoviesWishlistGetOKResponseContent> Content 
    	{
	        get
	        {
		        if (typedContent != null)
			        return typedContent;

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    typedContent = (IList<MoviesWishlistGetOKResponseContent>)new XmlSerializer(typeof(IList<MoviesWishlistGetOKResponseContent>)).Deserialize(xmlStream);
                }
                else
                {
                    var task =  Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync<IList<MoviesWishlistGetOKResponseContent>>(Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync<IList<MoviesWishlistGetOKResponseContent>>().ConfigureAwait(false);
		        
		            typedContent = task.GetAwaiter().GetResult();
                }

		        return typedContent;
	        }
	    }

		


    } // end class

    /// <summary>
    /// Response object for method Get of class Rented
    /// </summary>

    public partial class RentedGetResponse : ApiResponse
    {


	    private IList<MoviesRentedGetOKResponseContent> typedContent;
        /// <summary>
        /// Typed Response content
        /// </summary>
        public IList<MoviesRentedGetOKResponseContent> Content 
    	{
	        get
	        {
		        if (typedContent != null)
			        return typedContent;

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    typedContent = (IList<MoviesRentedGetOKResponseContent>)new XmlSerializer(typeof(IList<MoviesRentedGetOKResponseContent>)).Deserialize(xmlStream);
                }
                else
                {
                    var task =  Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync<IList<MoviesRentedGetOKResponseContent>>(Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync<IList<MoviesRentedGetOKResponseContent>>().ConfigureAwait(false);
		        
		            typedContent = task.GetAwaiter().GetResult();
                }

		        return typedContent;
	        }
	    }

		


    } // end class

    /// <summary>
    /// Response object for method Get of class Available
    /// </summary>

    public partial class AvailableGetResponse : ApiResponse
    {


	    private IList<MoviesAvailableGetOKResponseContent> typedContent;
        /// <summary>
        /// Typed Response content
        /// </summary>
        public IList<MoviesAvailableGetOKResponseContent> Content 
    	{
	        get
	        {
		        if (typedContent != null)
			        return typedContent;

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    typedContent = (IList<MoviesAvailableGetOKResponseContent>)new XmlSerializer(typeof(IList<MoviesAvailableGetOKResponseContent>)).Deserialize(xmlStream);
                }
                else
                {
                    var task =  Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync<IList<MoviesAvailableGetOKResponseContent>>(Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync<IList<MoviesAvailableGetOKResponseContent>>().ConfigureAwait(false);
		        
		            typedContent = task.GetAwaiter().GetResult();
                }

		        return typedContent;
	        }
	    }

		


    } // end class

    /// <summary>
    /// Response object for method Get of class Movies
    /// </summary>

    public partial class MoviesGetResponse : ApiResponse
    {

	    private MultipleMoviesGet typedContent;
        /// <summary>
        /// Typed response content
        /// </summary>
        public MultipleMoviesGet Content 
	    {
	        get
	        {
		        if (typedContent != null) 
					return typedContent;

		        typedContent = new MultipleMoviesGet();

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    var content = new XmlSerializer(typedContent.GetTypeByStatusCode(StatusCode)).Deserialize(xmlStream);
                    typedContent.SetPropertyByStatusCode(StatusCode, content);
                }
                else
                {
		            var task = Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync(typedContent.GetTypeByStatusCode(StatusCode), Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync(typedContent.GetTypeByStatusCode(StatusCode)).ConfigureAwait(false);
		        
		            var content = task.GetAwaiter().GetResult();
                    typedContent.SetPropertyByStatusCode(StatusCode, content);
                }

		        return typedContent;
	        }
    	}  
		
		public static string GetSchema(HttpStatusCode statusCode)
        {
            return MultipleMoviesGet.GetSchema(statusCode);
        }      

    } // end class

    /// <summary>
    /// Response object for method GetById of class Movies
    /// </summary>

    public partial class MoviesGetByIdResponse : ApiResponse
    {

        /// <summary>
        /// Typed Response headers (defined in RAML)
        /// </summary>
        public Models.MultipleGetByIdHeader Headers { get; set; }
	    private MultipleMoviesIdGet typedContent;
        /// <summary>
        /// Typed response content
        /// </summary>
        public MultipleMoviesIdGet Content 
	    {
	        get
	        {
		        if (typedContent != null) 
					return typedContent;

		        typedContent = new MultipleMoviesIdGet();

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    var content = new XmlSerializer(typedContent.GetTypeByStatusCode(StatusCode)).Deserialize(xmlStream);
                    typedContent.SetPropertyByStatusCode(StatusCode, content);
                }
                else
                {
		            var task = Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync(typedContent.GetTypeByStatusCode(StatusCode), Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync(typedContent.GetTypeByStatusCode(StatusCode)).ConfigureAwait(false);
		        
		            var content = task.GetAwaiter().GetResult();
                    typedContent.SetPropertyByStatusCode(StatusCode, content);
                }

		        return typedContent;
	        }
    	}  
		
		public static string GetSchema(HttpStatusCode statusCode)
        {
            return MultipleMoviesIdGet.GetSchema(statusCode);
        }      

    } // end class

    /// <summary>
    /// Response object for method Post of class Search
    /// </summary>

    public partial class SearchPostResponse : ApiResponse
    {


	    private IList<SearchPostOKResponseContent> typedContent;
        /// <summary>
        /// Typed Response content
        /// </summary>
        public IList<SearchPostOKResponseContent> Content 
    	{
	        get
	        {
		        if (typedContent != null)
			        return typedContent;

                IEnumerable<string> values = new List<string>();
                if (RawContent != null && RawContent.Headers != null)
                    RawContent.Headers.TryGetValues("Content-Type", out values);

                if (values.Any(hv => hv.ToLowerInvariant().Contains("xml")) &&
                    !values.Any(hv => hv.ToLowerInvariant().Contains("json")))
                {
                    var task = RawContent.ReadAsStreamAsync();

                    var xmlStream = task.GetAwaiter().GetResult();
                    typedContent = (IList<SearchPostOKResponseContent>)new XmlSerializer(typeof(IList<SearchPostOKResponseContent>)).Deserialize(xmlStream);
                }
                else
                {
                    var task =  Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync<IList<SearchPostOKResponseContent>>(Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync<IList<SearchPostOKResponseContent>>().ConfigureAwait(false);
		        
		            typedContent = task.GetAwaiter().GetResult();
                }

		        return typedContent;
	        }
	    }

		


    } // end class


} // end Models namespace
