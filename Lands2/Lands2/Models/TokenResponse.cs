using System;
using System.Collections.Generic;
using System.Text;

namespace Lands2.Models
{
    using System;
    using Newtonsoft.Json;
    class TokenResponse
    {
        #region Properties
        [JsonProperty(PropertyName = "acces_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "issued")]
        public string isused { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public string Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
        #endregion
    }
}
