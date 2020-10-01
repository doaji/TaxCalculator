using System.Net.Http;
using TaxCalculationLibrary.Helpers;
using TaxCalculationLibrary.Interface;

namespace TaxCalculationLibrary.Abstract
{
    public abstract class TaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> : ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> where TTaxRateRequest : class where TTaxCalculationRequest : class
                 where TTaxRateResponse : class
        where TTaxCalculationResponse : class
    {
        #region Protected Fields

        protected static readonly HttpClient httpClient = new HttpClient();

        #endregion Protected Fields

        #region Public Constructors

        public TaxCalculator(string apiKey, string taxCalculationApiEndPoint, string taxRateApiEndPoint)
        {
            APIKEY = apiKey;
            TaxCalculationApiEndPoint = taxCalculationApiEndPoint;
            TaxRateApiEndPoint = taxRateApiEndPoint;
        }

        #endregion Public Constructors

        #region Protected Properties

        protected string APIKEY { get; private set; }
        protected string TaxCalculationApiEndPoint { get; private set; }
        protected string TaxRateApiEndPoint { get; private set; }

        #endregion Protected Properties

        #region Public Methods

        public abstract TTaxCalculationResponse CalculateTaxForOrder(TTaxCalculationRequest request);

        public virtual void Dispose()
        {
            APIKEY = TaxCalculationApiEndPoint = TaxRateApiEndPoint = null;
        }

        public abstract TTaxRateResponse GetTaxRate(TTaxRateRequest request);

        #endregion Public Methods

        #region Protected Methods

        protected virtual bool ValidateTaxCalculationRequest(TTaxCalculationRequest request)
        {
            return request != null && TaxCalculationApiEndPoint.IsNotEmpty() && APIKEY.IsNotEmpty();
        }

        protected virtual bool ValidateTaxRateRequest(TTaxRateRequest request)
        {
            return request != null && TaxRateApiEndPoint.IsNotEmpty() && APIKEY.IsNotEmpty();
        }

        #endregion Protected Methods
    }
}