using System;

namespace TaxCalculationLibrary.Interface
{
    public interface ITaxCalculator<in TTaxRateRequest, in TTaxCalculationRequest, out TTaxRateResponse, out TTaxCalculationResponse> : IDisposable
        where TTaxRateRequest : class
        where TTaxCalculationRequest : class
         where TTaxRateResponse : class
        where TTaxCalculationResponse : class
    {
        #region Public Methods

        TTaxCalculationResponse CalculateTaxForOrder(TTaxCalculationRequest request);

        TTaxRateResponse GetTaxRate(TTaxRateRequest request);

        #endregion Public Methods
    }
}