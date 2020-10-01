using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculationLibrary.Interface
{
    public interface ITaxCalculator<in TTaxRateRequest, in TTaxCalculationRequest, out TTaxRateResponse, out TTaxCalculationResponse> : IDisposable
        where TTaxRateRequest : class
        where TTaxCalculationRequest : class
         where TTaxRateResponse : class
        where TTaxCalculationResponse : class
    {
        string TaxCalculationApiEndPoint { get; }
        string TaxRateApiEndPoint { get; }
        string APIKEY { get; }

        bool ValidateTaxRateRequest(TTaxRateRequest request);

        bool ValidateTaxCalculationRequest(TTaxCalculationRequest request);

        string BuildTaxRateAPIUrl(TTaxRateRequest request);
    }
}