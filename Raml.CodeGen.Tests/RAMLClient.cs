




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


namespace XMLPurchaseOrder
{
    public partial class Orders
    {
        private readonly MoviesApi proxy;

        internal Orders(MoviesApi proxy)
        {
            this.proxy = proxy;
        }

        
        public virtual async Task<Models.OrdersGetResponse> Get()
        {

            var url = "orders";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
	        var response = await proxy.Client.SendAsync(req);
			
			if (proxy.SchemaValidation.Enabled)
		    {
				if(proxy.SchemaValidation.RaiseExceptions) 
				{
					await SchemaValidator.ValidateWithExceptionAsync("", response.Content);
				}
					
			}

            return new Models.OrdersGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers, 
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("", response.Content), true)
                                            };

        }

        		/// <param name="request">ApiRequest</param>
		/// <param name="responseFormatters">response formmaters</param>
        public virtual async Task<Models.OrdersGetResponse> Get(ApiRequest request, IEnumerable<MediaTypeFormatter> responseFormatters = null)
        {

            var url = "orders";
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
					await SchemaValidator.ValidateWithExceptionAsync("", response.Content);
				}
				
            }
            return new Models.OrdersGetResponse  
                                            {
                                                RawContent = response.Content,
                                                RawHeaders = response.Headers,
	                                            Formatters = responseFormatters,
                                                StatusCode = response.StatusCode,
                                                ReasonPhrase = response.ReasonPhrase,
												SchemaValidation = new Lazy<SchemaValidationResults>(() => SchemaValidator.IsValid("", response.Content), true)
                                            };
        }

        		/// <param name="purchaseordertype"></param>
        public virtual async Task<ApiResponse> Post(Models.PurchaseOrderType purchaseordertype)
        {

            var url = "orders";
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var stringWriter = new StringWriter();
        	new XmlSerializer(typeof (PurchaseOrderType)).Serialize(stringWriter, purchaseordertype);
            req.Content = new  StringContent(stringWriter.GetStringBuilder().ToString(), Encoding.UTF8, "application/xml");     
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

        		/// <param name="request">Models.OrdersPostRequest</param>
        public virtual async Task<ApiResponse> Post(Models.OrdersPostRequest request)
        {

            var url = "orders";
            var req = new HttpRequestMessage(HttpMethod.Post, url);

            if(request.RawHeaders != null)
            {
                foreach(var header in request.RawHeaders)
                {
                    req.Headers.TryAddWithoutValidation(header.Key, string.Join(",", header.Value));
                }
            }
            var stringWriter = new StringWriter();
        	new XmlSerializer(typeof (PurchaseOrderType)).Serialize(stringWriter, request.Content);
            req.Content = new  StringContent(stringWriter.GetStringBuilder().ToString(), Encoding.UTF8, "application/xml");     
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
    /// Main class for grouping root resources. Nested resources are defined as properties. The constructor can optionally receive an URL and HttpClient instance to override the default ones.
    /// </summary>
    public partial class MoviesApi
    {

		public SchemaValidationSettings SchemaValidation { get; private set; } 

        protected readonly HttpClient client;
        public const string BaseUri = "http://mocksvc.mulesoft.com/mocks/328d4294-376c-4655-8315-d1dccd58a5d1/";

        internal HttpClient Client { get { return client; } }




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

        
        public virtual Orders Orders
        {
            get { return new Orders(this); }
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




namespace XMLPurchaseOrder {
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO")]
    [System.Xml.Serialization.XmlRootAttribute("purchaseOrder", Namespace="http://www.example.com/IPO", IsNullable=false)]
    public partial class PurchaseOrderType {
        
        private AddressType[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        private string itemField;
        
        private ItemChoiceType itemElementNameField;
        
        private ItemsType itemsField1;
        
        private System.DateTime orderDateField;
        
        private bool orderDateFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("billTo", typeof(AddressType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("shipTo", typeof(AddressType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("singleAddress", typeof(AddressType), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public AddressType[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("comment", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("customerComment", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("shipComment", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ItemsType items {
            get {
                return this.itemsField1;
            }
            set {
                this.itemsField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="date")]
        public System.DateTime orderDate {
            get {
                return this.orderDateField;
            }
            set {
                this.orderDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool orderDateSpecified {
            get {
                return this.orderDateFieldSpecified;
            }
            set {
                this.orderDateFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UKAddress))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(USAddress))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO")]
    public partial class AddressType {
        
        private string nameField;
        
        private string streetField;
        
        private string cityField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string street {
            get {
                return this.streetField;
            }
            set {
                this.streetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string city {
            get {
                return this.cityField;
            }
            set {
                this.cityField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO")]
    public partial class ItemsType {
        
        private ItemsTypeItem[] itemField;
        
        private string[] textField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ItemsTypeItem[] item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.example.com/IPO")]
    public partial class ItemsTypeItem {
        
        private string productNameField;
        
        private string quantityField;
        
        private decimal uSPriceField;
        
        private string[] itemsField;
        
        private ItemsChoiceType1[] itemsElementNameField;
        
        private System.DateTime shipDateField;
        
        private bool shipDateFieldSpecified;
        
        private string partNumField;
        
        private decimal weightKgField;
        
        private bool weightKgFieldSpecified;
        
        private ItemsTypeItemShipBy shipByField;
        
        private bool shipByFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string productName {
            get {
                return this.productNameField;
            }
            set {
                this.productNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="positiveInteger")]
        public string quantity {
            get {
                return this.quantityField;
            }
            set {
                this.quantityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal USPrice {
            get {
                return this.uSPriceField;
            }
            set {
                this.uSPriceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("comment", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("customerComment", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("shipComment", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date")]
        public System.DateTime shipDate {
            get {
                return this.shipDateField;
            }
            set {
                this.shipDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool shipDateSpecified {
            get {
                return this.shipDateFieldSpecified;
            }
            set {
                this.shipDateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string partNum {
            get {
                return this.partNumField;
            }
            set {
                this.partNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal weightKg {
            get {
                return this.weightKgField;
            }
            set {
                this.weightKgField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool weightKgSpecified {
            get {
                return this.weightKgFieldSpecified;
            }
            set {
                this.weightKgFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ItemsTypeItemShipBy shipBy {
            get {
                return this.shipByField;
            }
            set {
                this.shipByField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool shipBySpecified {
            get {
                return this.shipByFieldSpecified;
            }
            set {
                this.shipByFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO", IncludeInSchema=false)]
    public enum ItemsChoiceType1 {
        
        /// <remarks/>
        comment,
        
        /// <remarks/>
        customerComment,
        
        /// <remarks/>
        shipComment,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.example.com/IPO")]
    public enum ItemsTypeItemShipBy {
        
        /// <remarks/>
        air,
        
        /// <remarks/>
        land,
        
        /// <remarks/>
        any,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO")]
    public partial class UKAddress : AddressType {
        
        private string postcodeField;
        
        private string exportCodeField;
        
        public UKAddress() {
            this.exportCodeField = "1";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string postcode {
            get {
                return this.postcodeField;
            }
            set {
                this.postcodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string exportCode {
            get {
                return this.exportCodeField;
            }
            set {
                this.exportCodeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO")]
    public partial class USAddress : AddressType {
        
        private USState stateField;
        
        private string zipField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public USState state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="positiveInteger")]
        public string zip {
            get {
                return this.zipField;
            }
            set {
                this.zipField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO")]
    public enum USState {
        
        /// <remarks/>
        AK,
        
        /// <remarks/>
        AL,
        
        /// <remarks/>
        AR,
        
        /// <remarks/>
        CA,
        
        /// <remarks/>
        PA,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO", IncludeInSchema=false)]
    public enum ItemsChoiceType {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":billTo")]
        billTo,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":shipTo")]
        shipTo,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(":singleAddress")]
        singleAddress,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.com/IPO", IncludeInSchema=false)]
    public enum ItemChoiceType {
        
        /// <remarks/>
        comment,
        
        /// <remarks/>
        customerComment,
        
        /// <remarks/>
        shipComment,
    }
}






namespace XMLPurchaseOrder.Models
{
    public abstract class  PurchaseOrderType 
    {

    } // end class

    /// <summary>
    /// Request object for method Post of class Orders
    /// </summary>
    public partial class OrdersPostRequest : ApiRequest
    {
        public OrdersPostRequest(PurchaseOrderType Content = null, MediaTypeFormatter Formatter = null)
        {
            this.Content = Content;
            this.Formatter = Formatter;
        }

        /// <summary>
        /// Request content
        /// </summary>
        public PurchaseOrderType Content { get; set; }
        /// <summary>
        /// Request formatter
        /// </summary>
        public MediaTypeFormatter Formatter { get; set; }

    } // end class

    /// <summary>
    /// Response object for method Get of class Orders
    /// </summary>

    public partial class OrdersGetResponse : ApiResponse
    {


	    private PurchaseOrderType typedContent;
        /// <summary>
        /// Typed Response content
        /// </summary>
        public PurchaseOrderType Content 
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
                    typedContent = (PurchaseOrderType)new XmlSerializer(typeof(PurchaseOrderType)).Deserialize(xmlStream);
                }
                else
                {
                    var task =  Formatters != null && Formatters.Any() 
                                ? RawContent.ReadAsAsync<PurchaseOrderType>(Formatters).ConfigureAwait(false)
                                : RawContent.ReadAsAsync<PurchaseOrderType>().ConfigureAwait(false);
		        
		            typedContent = task.GetAwaiter().GetResult();
                }

		        return typedContent;
	        }
	    }

		


    } // end class


} // end Models namespace
