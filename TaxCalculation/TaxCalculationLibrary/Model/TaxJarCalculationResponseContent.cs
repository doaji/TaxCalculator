namespace TaxCalculationLibrary.Model
{
    public class TaxJarCalculationResponseContent
    {
        /// <summary>
        /// Total amount of the order.
        /// </summary>
        public float order_total_amount { get; set; }

        /// <summary>
        /// Total amount of shipping for the order.
        /// </summary>
        public float shipping { get; set; }

        /// <summary>
        /// Amount of the order to be taxed.
        /// </summary>
        public float taxable_amount { get; set; }

        /// <summary>
        /// Amount of sales tax to collect.
        /// </summary>
        public float amount_to_collect { get; set; }

        /// <summary>
        /// Overall sales tax rate of the order (amount_to_collect ÷ taxable_amount).
        /// </summary>
        public float rate { get; set; }

        /// <summary>
        /// Whether or not you have nexus for the order based on an address on file, nexus_addresses parameter, or from_ parameters.
        /// </summary>
        public bool has_nexus { get; set; }

        /// <summary>
        /// Freight taxability for the order.
        /// </summary>
        public bool freight_taxable { get; set; }

        /// <summary>
        /// Origin-based or destination-based sales tax collection.
        /// </summary>
        public string tax_source { get; set; }

        /// <summary>
        /// Type of exemption for the order: wholesale, government, marketplace, other, or non_exempt. If no customer_id or exemption_type is provided, no exemption_type is returned in the response.
        /// </summary>
        public string exemption_type { get; set; }

        /// <summary>
        ///  Jurisdiction names for the order.
        /// </summary>
        public object jurisdictions { get; set; }

        /// <summary>
        /// Breakdown of rates by jurisdiction for the order, shipping, and individual line items. If has_nexus is false or no line items are provided, no breakdown is returned in the response.
        /// </summary>
        public object breakdown { get; set; }
    }
}