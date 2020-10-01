using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculationLibrary.Interface;

namespace TaxCalculationLibrary.Model
{
    public class TaxJarRateRequest
    {
        /// <summary>
        /// Two-letter ISO country code for given location.
        /// For international locations outside of US, `country` is required.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Postal code for given location (5-Digit ZIP or ZIP+4).
        /// </summary>
        ///
        [JsonProperty("zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Two-letter ISO state code for given location.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// City for given location.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Street address for given location
        /// </summary>
        [JsonProperty("street")]
        public string Street { get; set; }

        /// <summary>
        /// Default is false
        /// </summary>
        public bool IsInternationalAddress { get; set; } = false;
    }
}