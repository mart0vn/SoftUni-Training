﻿namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("partId")]
    public class ImportCarPartDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
