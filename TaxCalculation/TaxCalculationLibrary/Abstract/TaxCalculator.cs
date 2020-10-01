using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        protected static readonly HttpClient httpClient = new HttpClient();
        protected string TaxCalculationApiEndPoint { get; private set; }

        protected string APIKEY { get; private set; }
        protected string TaxRateApiEndPoint { get; private set; }

        public TaxCalculator(string apiKey, string taxCalculationApiEndPoint, string taxRateApiEndPoint)
        {
            APIKEY = apiKey;
            TaxCalculationApiEndPoint = taxCalculationApiEndPoint;
            TaxRateApiEndPoint = taxRateApiEndPoint;
        }

        public virtual void Dispose()
        {
            APIKEY = TaxCalculationApiEndPoint = TaxRateApiEndPoint = null;
        }

        protected virtual bool ValidateTaxRateRequest(TTaxRateRequest request)
        {
            return request != null && TaxRateApiEndPoint.IsNotEmpty() && APIKEY.IsNotEmpty();
        }

        protected virtual bool ValidateTaxCalculationRequest(TTaxCalculationRequest request)
        {
            return request != null && TaxCalculationApiEndPoint.IsNotEmpty() && APIKEY.IsNotEmpty();
        }

        public abstract TTaxCalculationResponse CalculateTaxForOrder(TTaxCalculationRequest request);

        public abstract TTaxRateResponse GetTaxRate(TTaxRateRequest request);
    }
}