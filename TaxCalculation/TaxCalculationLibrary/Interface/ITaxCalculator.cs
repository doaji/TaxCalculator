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
        TTaxCalculationResponse CalculateTaxForOrder(TTaxCalculationRequest request);

        TTaxRateResponse GetTaxRate(TTaxRateRequest request);
    }
}