using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaxCalculationLibrary.Abstract;
using TaxCalculationLibrary.Helpers;
using TaxCalculationLibrary.Interface;
using TaxCalculationLibrary.Model;

namespace TaxCalculationLibrary.Calculators
{
    public class TaxJar : TaxCalculator<TaxJarRateRequest, TaxJarCalculationRequest, TaxJarRateResponse, TaxJarCalculationResponse>
    {
        public TaxJar(string apiKey, string taxCalculationApiEndPoint, string taxRateApiEndPoint) : base(apiKey, taxCalculationApiEndPoint, taxRateApiEndPoint)
        {
        }

        public override string BuildTaxRateAPIUrl(TaxJarRateRequest request)
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

        public override bool ValidateTaxCalculationRequest(TaxJarCalculationRequest request)
        {
            if (base.ValidateTaxCalculationRequest(request))
            {
                bool IsUS = request.to_country.ToLower().Trim() == "us";
                bool IsCA = request.to_country.ToLower().Trim() == "ca";
                return request.to_country.IsNotEmpty() && request.shipping >= 0 && (IsUS ? (request.to_state.IsNotEmpty() && request.to_zip.IsNotEmpty()) : true) && (IsCA ? request.to_state.IsNotEmpty() : true) && ValidateAddressList(request.nexus_addresses);
            }
            return false;
        }

        private bool ValidateAddressList(List<TaxJarAddress> nexus_addresses)
        {
            return !nexus_addresses?.Any(item => item.country.IsEmpty() || item.state.IsEmpty()) ?? true;
        }

        public override bool ValidateTaxRateRequest(TaxJarRateRequest request)
        {
            if (base.ValidateTaxRateRequest(request))
            {
                return (request.IsInternationalAddress ? request.Country.IsNotEmpty() : true) && request.Zip.IsNotEmpty();
            }
            return false;
        }
    }
}