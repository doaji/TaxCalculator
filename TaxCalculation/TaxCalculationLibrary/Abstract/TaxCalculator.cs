using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculationLibrary.Helpers;
using TaxCalculationLibrary.Interface;
using TaxCalculationLibrary.Model;

namespace TaxCalculationLibrary.Abstract
{
    public abstract class TaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> : ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> where TTaxRateRequest : class where TTaxCalculationRequest : class
                 where TTaxRateResponse : class
        where TTaxCalculationResponse : class
    {
        public string TaxCalculationApiEndPoint { get; private set; }

        public string APIKEY { get; private set; }
        public string TaxRateApiEndPoint { get; private set; }

        public TaxCalculator(string apiKey, string taxCalculationApiEndPoint, string taxRateApiEndPoint)
        {
            APIKEY = apiKey;
            TaxCalculationApiEndPoint = taxCalculationApiEndPoint;
            TaxRateApiEndPoint = taxRateApiEndPoint;
        }

        public void Dispose()
        {
            APIKEY = TaxCalculationApiEndPoint = TaxRateApiEndPoint = null;
        }

        public virtual bool ValidateTaxRateRequest(TTaxRateRequest request)
        {
            return request != null && TaxRateApiEndPoint.IsNotEmpty() && APIKEY.IsNotEmpty();
        }

        public virtual bool ValidateTaxCalculationRequest(TTaxCalculationRequest request)
        {
            return request != null && TaxCalculationApiEndPoint.IsNotEmpty() && APIKEY.IsNotEmpty();
        }

        public abstract string BuildTaxRateAPIUrl(TTaxRateRequest request);
    }
}