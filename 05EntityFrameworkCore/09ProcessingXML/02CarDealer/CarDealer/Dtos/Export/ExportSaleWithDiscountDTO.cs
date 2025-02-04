﻿namespace CarDealer.Dtos.Export
{   
    using System.Xml.Serialization;
    
    [XmlType("sale")]
    public class ExportSaleWithDiscountDTO
    {
        [XmlElement("car")]
        public ExportCarDTO CarInfo { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }

        [XmlElement("customer-name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount { get; set; }
    }
}
