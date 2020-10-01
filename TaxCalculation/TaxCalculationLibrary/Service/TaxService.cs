using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaxCalculationLibrary.Helpers;
using TaxCalculationLibrary.Interface;

namespace TaxCalculationLibrary.Service
{
    public class TaxService<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> : ITaxService<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> where TTaxRateRequest : class where TTaxCalculationRequest : class
                         where TTaxRateResponse : class
        where TTaxCalculationResponse : class
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> _taxCalculator;

        public TaxService(ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        private async Task<TTaxCalculationResponse> CalculateTax(TTaxCalculationRequest request)
        {
            string json = request.Jsonify();
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _taxCalculator.APIKEY);
            string url = _taxCalculator.TaxCalculationApiEndPoint;

            using (HttpResponseMessage response = await httpClient.PostAsync(url, data))
            {
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result.UnJsonify<TTaxCalculationResponse>();
            }
        }

        private async Task<TTaxRateResponse> CalculateTaxRate(TTaxRateRequest request)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _taxCalculator.APIKEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = _taxCalculator.BuildTaxRateAPIUrl(request);

            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result.UnJsonify<TTaxRateResponse>();
            }
        }

        public void Dispose()
        {
            _taxCalculator?.Dispose();
        }

        /// <summary>
        /// Get the sales tax that should be collected for a given order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TTaxCalculationResponse GetTaxCalculation(TTaxCalculationRequest request)
        {
            if (_taxCalculator is null)
            {
                throw new Exception("Tax calculator cannot be null");
            }
            if (_taxCalculator.ValidateTaxCalculationRequest(request))
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

        /// <summary>
        /// Get the sales tax rates for a given location.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></r
        public TTaxRateResponse GetTaxRate(TTaxRateRequest request)
        {
            if (_taxCalculator is null)
            {
                throw new Exception("Tax calculator cannot be null");
            }
            if (_taxCalculator.ValidateTaxRateRequest(request))
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
    }
}