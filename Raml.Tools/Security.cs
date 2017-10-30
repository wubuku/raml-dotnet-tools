using System;
using System.Collections.Generic;

namespace Raml.Tools
{
    [Serializable]
    public class Security
    {
        public string AuthorizationUri { get; set; }
        public string AccessTokenUri { get; set; }

        public IEnumerable<string> AuthorizationGrants { get; set; }

        public IEnumerable<string> Scopes { get; set; }

        public string RequestTokenUri  { get; set; } // Oauth 1

        public string TokenCredentialsUri  { get; set; } // Oauth 1

        public IEnumerable<GeneratorParameter> Headers { get; set; }

        public IEnumerable<GeneratorParameter> QueryParameters { get; set; }

    }
}