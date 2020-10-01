using System;
using TaxCalculationLibrary.Helpers;
using TaxCalculationLibrary.Model;

namespace TaxCalculationConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunTaxJarSample();
        }

        private static void RunTaxJarSample()
        {
            string taxRateApiendpoint = "https://api.taxjar.com/v2/rates";
            string apikey = "5da2f821eee4035db4771edab942a4cc";
            TaxJarRateRequest taxRateRequest = new TaxJarRateRequest
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Country = "US",
                Zip = "05495-2086"
            };

            TaxJarCalculationRequest jarCalculationRequest = new TaxJarCalculationRequest
            {
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                from_city = "La Jolla",
                from_street = "9500 Gilman Drive",
                to_country = "US",
                to_zip = "90002",
                to_state = "CA",
                to_city = "Los Angeles",
                to_street = "1335 E 103rd St",
                amount = 15,
                shipping = 1.5F,
                nexus_addresses = new System.Collections.Generic.List<TaxJarAddress> {
    new TaxJarAddress {
      id = "Main Location",
      country = "US",
      zip = "92093",
      state = "CA",
      city = "La Jolla",
      street = "9500 Gilman Drive",
    }
  },
                line_items = new System.Collections.Generic.List<TaxJarLineItem> {
    new TaxJarLineItem {
      id = "1",
      quantity = 1,
      product_tax_code = "20010",
      unit_price = 15,
      discount = 0
    }
  }
            };
            TaxCalculationLibrary.Calculators.TaxJar taxJar = new TaxCalculationLibrary.Calculators.TaxJar(apikey, "https://api.taxjar.com/v2/taxes", taxRateApiendpoint);
            using (var service = TaxCalculationLibrary.Service.Factory.GetTaxService(taxJar))
            {
                try
                {
                    TaxJarRateResponse result = service.GetTaxRate(taxRateRequest);
                    Console.WriteLine(result.Jsonify());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    TaxJarCalculationResponse result2 = service.GetTaxCalculation(jarCalculationRequest);
                    Console.WriteLine(result2.Jsonify());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}