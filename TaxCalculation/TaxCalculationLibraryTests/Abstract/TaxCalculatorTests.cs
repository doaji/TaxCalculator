using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TaxCalculationLibrary.Model;

namespace TaxCalculationLibrary.Abstract.Tests
{
    [TestClass()]
    public class TaxCalculatorTests
    {
        [TestMethod()]
        public void GetTaxCalculationBadCalculatorNoAPIKeyTest()
        {
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
                nexus_addresses = new List<TaxJarAddress> {
    new TaxJarAddress {
      id = "Main Location",
      country = "US",
      zip = "92093",
      state = "CA",
      city = "La Jolla",
      street = "9500 Gilman Drive",
    }
  },
                line_items = new List<TaxJarLineItem> {
    new TaxJarLineItem {
      id = "1",
      quantity = 1,
      product_tax_code = "20010",
      unit_price = 15,
      discount = 0
    }
  }
            };
            Calculators.TaxJar taxJar = new Calculators.TaxJar("", "https://api.taxjar.com/v2/taxes", "");
            try
            {
                TaxJarCalculationResponse result2 = taxJar.CalculateTaxForOrder(jarCalculationRequest);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod()]
        public void GetTaxRateBadCalculatorNoAPIKeyTest()
        {
            string taxRateApiendpoint = "https://api.taxjar.com/v2/rates";
            TaxJarRateRequest taxRateRequest = new TaxJarRateRequest
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Country = "US",
                Zip = "05495-2086"
            };
            TaxCalculationLibrary.Calculators.TaxJar taxJar = new TaxCalculationLibrary.Calculators.TaxJar("", "", taxRateApiendpoint);
            try
            {
                TaxJarRateResponse result = taxJar.GetTaxRate(taxRateRequest);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod()]
        public void GetTaxCalculationGoodRequestTest()
        {
            string apikey = "5da2f821eee4035db4771edab942a4cc";

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
                nexus_addresses = new List<TaxJarAddress> {
    new TaxJarAddress {
      id = "Main Location",
      country = "US",
      zip = "92093",
      state = "CA",
      city = "La Jolla",
      street = "9500 Gilman Drive",
    }
  },
                line_items = new List<TaxJarLineItem> {
    new TaxJarLineItem {
      id = "1",
      quantity = 1,
      product_tax_code = "20010",
      unit_price = 15,
      discount = 0
    }
  }
            };
            Calculators.TaxJar taxJar = new Calculators.TaxJar(apikey, "https://api.taxjar.com/v2/taxes", "");
            try
            {
                TaxJarCalculationResponse result2 = taxJar.CalculateTaxForOrder(jarCalculationRequest);
                Assert.IsNotNull(result2);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void GetTaxRateGoodRequestTest()
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
            Calculators.TaxJar taxJar = new Calculators.TaxJar(apikey, "", taxRateApiendpoint);
            try
            {
                TaxJarRateResponse result = taxJar.GetTaxRate(taxRateRequest);
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void GetTaxCalculationNullRequestTest()
        {
            string apikey = "5da2f821eee4035db4771edab942a4cc";

            Calculators.TaxJar taxJar = new Calculators.TaxJar(apikey, "https://api.taxjar.com/v2/taxes", "");
            try
            {
                TaxJarCalculationResponse result2 = taxJar.CalculateTaxForOrder(null);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod()]
        public void GetTaxRateNullRequestTest()
        {
            string taxRateApiendpoint = "https://api.taxjar.com/v2/rates";
            string apikey = "5da2f821eee4035db4771edab942a4cc";
            Calculators.TaxJar taxJar = new Calculators.TaxJar(apikey, "", taxRateApiendpoint);
            try
            {
                TaxJarRateResponse result = taxJar.GetTaxRate(null);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod()]
        public void GetTaxCalculationBadRequestNoCountryTest()
        {
            string apikey = "5da2f821eee4035db4771edab942a4cc";

            TaxJarCalculationRequest jarCalculationRequest = new TaxJarCalculationRequest
            {
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                from_city = "La Jolla",
                from_street = "9500 Gilman Drive",
                to_zip = "90002",
                to_state = "CA",
                to_city = "Los Angeles",
                to_street = "1335 E 103rd St",
                amount = 15,
                shipping = 1.5F,
                nexus_addresses = new List<TaxJarAddress> {
    new TaxJarAddress {
      id = "Main Location",
      country = "US",
      zip = "92093",
      state = "CA",
      city = "La Jolla",
      street = "9500 Gilman Drive",
    }
  },
                line_items = new List<TaxJarLineItem> {
    new TaxJarLineItem {
      id = "1",
      quantity = 1,
      product_tax_code = "20010",
      unit_price = 15,
      discount = 0
    }
  }
            };
            Calculators.TaxJar taxJar = new Calculators.TaxJar(apikey, "https://api.taxjar.com/v2/taxes", "");
            try
            {
                TaxJarCalculationResponse result2 = taxJar.CalculateTaxForOrder(jarCalculationRequest);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod()]
        public void GetTaxRateBadRequestNoZipTest()
        {
            string taxRateApiendpoint = "https://api.taxjar.com/v2/rates";
            string apikey = "5da2f821eee4035db4771edab942a4cc";
            TaxJarRateRequest taxRateRequest = new TaxJarRateRequest
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Country = "US"
            };
            Calculators.TaxJar taxJar = new Calculators.TaxJar(apikey, "", taxRateApiendpoint);
            try
            {
                TaxJarRateResponse result = taxJar.GetTaxRate(taxRateRequest);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }
    }
}