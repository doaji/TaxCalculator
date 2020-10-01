using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaxCalculationLibrary.Abstract;
using TaxCalculationLibrary.Helpers;
using TaxCalculationLibrary.Model;

namespace TaxCalculationLibrary.Calculators
{
    public class TaxJar : TaxCalculator<TaxJarRateRequest, TaxJarCalculationRequest, TaxJarRateResponse, TaxJarCalculationResponse>
    {
        #region Public Constructors

        public TaxJar(string apiKey, string taxCalculationApiEndPoint, string taxRateApiEndPoint) : base(apiKey, taxCalculationApiEndPoint, taxRateApiEndPoint)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override TaxJarCalculationResponse CalculateTaxForOrder(TaxJarCalculationRequest request)
        {
            if (ValidateTaxCalculationRequest(request))
            {
                try
                {
                    return CalculateTax(request).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Bad Request");
            }
        }

        public override TaxJarRateResponse GetTaxRate(TaxJarRateRequest request)
        {
            if (ValidateTaxRateRequest(request))
            {
                try
                {
                    return CalculateTaxRate(request).Result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Bad Request");
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override bool ValidateTaxCalculationRequest(TaxJarCalculationRequest request)
        {
            if (base.ValidateTaxCalculationRequest(request))
            {
                bool IsUS = request.to_country.ToLower().Trim() == "us";
                bool IsCA = request.to_country.ToLower().Trim() == "ca";
                return request.to_country.IsNotEmpty() && request.shipping >= 0 && (IsUS ? (request.to_state.IsNotEmpty() && request.to_zip.IsNotEmpty()) : true) && (IsCA ? request.to_state.IsNotEmpty() : true) && ValidateAddressList(request.nexus_addresses);
            }
            return false;
        }

        protected override bool ValidateTaxRateRequest(TaxJarRateRequest request)
        {
            if (base.ValidateTaxRateRequest(request))
            {
                return (request.IsInternationalAddress ? request.Country.IsNotEmpty() : true) && request.Zip.IsNotEmpty();
            }
            return false;
        }

        #endregion Protected Methods

        #region Private Methods

        private string BuildTaxRateAPIUrl(TaxJarRateRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            StringBuilder url = new StringBuilder(TaxRateApiEndPoint);
            url.Append($"?zip={request.Zip}");
            PropertyInfo[] properties = typeof(TaxJarRateRequest).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "Zip" && property.Name != "IsInternationalAddress" && property.GetValue(request).ToString().IsNotEmpty())
                {
                    url.Append($"&{property.Name}={property.GetValue(request).ToString()}");
                }
            }
            return url.ToString();
        }

        private async Task<TaxJarCalculationResponse> CalculateTax(TaxJarCalculationRequest request)
        {
            string json = request.Jsonify();
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKEY);
            string url = TaxCalculationApiEndPoint;

            using (HttpResponseMessage response = await httpClient.PostAsync(url, data))
            {
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result.UnJsonify<TaxJarCalculationResponse>();
            }
        }

        private async Task<TaxJarRateResponse> CalculateTaxRate(TaxJarRateRequest request)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = BuildTaxRateAPIUrl(request);

            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result.UnJsonify<TaxJarRateResponse>();
            }
        }

        private bool ValidateAddressList(List<TaxJarAddress> nexus_addresses)
        {
            return !nexus_addresses?.Any(item => item.country.IsEmpty() || item.state.IsEmpty()) ?? true;
        }

        #endregion Private Methods
    }
}