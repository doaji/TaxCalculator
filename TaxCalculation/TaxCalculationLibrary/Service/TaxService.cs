﻿using System;
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
        private readonly ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> _taxCalculator;

        public TaxService(ITaxCalculator<TTaxRateRequest, TTaxCalculationRequest, TTaxRateResponse, TTaxCalculationResponse> taxCalculator)
        {
            _taxCalculator = taxCalculator;
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
        public TTaxCalculationResponse GetTaxCalculationForOrder(TTaxCalculationRequest request)
        {
            if (_taxCalculator is null)
            {
                throw new Exception("Tax calculator cannot be null");
            }
            return _taxCalculator.CalculateTaxForOrder(request);
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
            return _taxCalculator.GetTaxRate(request);
        }
    }
}