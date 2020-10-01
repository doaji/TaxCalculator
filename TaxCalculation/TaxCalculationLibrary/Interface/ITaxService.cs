using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculationLibrary.Interface
{
    public interface ITaxService<in TTaxRateRequest, in TTaxCalculationRequest, out TTaxRateResponse, out TTaxCalculationResponse> : IDisposable where TTaxRateRequest : class where TTaxCalculationRequest : class
                 where TTaxRateResponse : class
        where TTaxCalculationResponse : class
    {
        /// <summary>
        /// Get the sales tax that should be collected for a given order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        TTaxCalculationResponse GetTaxCalculationForOrder(TTaxCalculationRequest request);

        /// <summary>
        /// Get the sales tax rates for a given location.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        TTaxRateResponse GetTaxRate(TTaxRateRequest request);
    }
}