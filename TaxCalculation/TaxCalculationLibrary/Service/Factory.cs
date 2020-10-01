using TaxCalculationLibrary.Interface;

namespace TaxCalculationLibrary.Service
{
    public static class Factory
    {
        #region Public Methods

        public static ITaxService<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> GetTaxService<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse>(ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> taxCalculator) where TTaxRateRequest : class where TTaxCalculationRequest : class where TTaxRateResponse : class
        where TTaxCalculationResponse : class
        {
            return new TaxService<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse>(taxCalculator);
        }

        #endregion Public Methods
    }
}