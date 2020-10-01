using System.Collections.Generic;

namespace TaxCalculationLibrary.Model
{
    public class TaxJarCalculationRequest
    {
        /// <summary>
        /// optional    Two-letter ISO country code of the country where the order shipped from.
        /// </summary>
        public string from_country { get; set; }

        /// <summary>
        /// optional    Postal code where the order shipped from (5-Digit ZIP or ZIP+4).
        /// </summary>
        public string from_zip { get; set; }

        /// <summary>
        /// optional    Two-letter ISO state code where the order shipped from.
        /// </summary>
        public string from_state { get; set; }

        /// <summary>
        /// optional    City where the order shipped from.
        /// </summary>
        public string from_city { get; set; }

        /// <summary>
        /// optional    Street address where the order shipped from.
        /// </summary>
        public string from_street { get; set; }

        /// <summary>
        /// required    Two-letter ISO country code of the country where the order shipped to.
        /// </summary>
        public string to_country { get; set; }

        /// <summary>
        /// conditional Postal code where the order shipped to (5-Digit ZIP or ZIP+4).
        /// </summary>
        public string to_zip { get; set; }

        /// <summary>
        /// conditional Two-letter ISO state code where the order shipped to.
        /// </summary>
        public string to_state { get; set; }

        /// <summary>
        ///  optional    City where the order shipped to.
        /// </summary>
        public string to_city { get; set; }

        /// <summary>
        /// optional    Street address where the order shipped to.
        /// </summary>
        public string to_street { get; set; }

        /// <summary>
        /// optional    Total amount of the order, excluding shipping.
        /// </summary>
        public float amount { get; set; }

        /// <summary>
        /// required  Total amount of shipping for the order.
        /// </summary>
        public float shipping { get; set; }

        /// <summary>
        ///  optional    Unique identifier of the given customer for exemptions.
        /// </summary>
        public string customer_id { get; set; }

        /// <summary>
        /// optional    Type of exemption for the order: wholesale, government, marketplace, other, or non_exempt.
        /// </summary>
        public string exemption_type { get; set; }

        public List<TaxJarAddress> nexus_addresses { get; set; }
        public List<TaxJarLineItem> line_items { get; set; }
    }

    public class TaxJarAddress
    {
        /// <summary>
        /// optional    Unique identifier of the given nexus address.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///  conditional Two-letter ISO country code for the nexus address.
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// optional    Postal code for the nexus address.
        /// </summary>
        public string zip { get; set; }

        /// <summary>
        /// conditional Two-letter ISO state code for the nexus address.
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// optional    City for the nexus address.
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// optional    Street address for the nexus address.
        /// </summary>
        public string street { get; set; }
    }

    public class TaxJarLineItem
    {
        /// <summary>
        /// optional    Unique identifier of the given line item.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// optional Quantity for the item.
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// optional    Product tax code for the item. If omitted, the item will remain fully taxable.
        /// </summary>
        public string product_tax_code { get; set; }

        /// <summary>
        /// optional    Unit price for the item.
        /// </summary>
        public float unit_price { get; set; }

        /// <summary>
        /// optional    Total discount (non-unit) for the item.
        /// </summary>
        public float discount { get; set; }
    }
}