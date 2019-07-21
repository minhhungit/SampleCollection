using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Xml.Serialization;

namespace WcfConsoleMessages.Models
{
    //[DataContract(Namespace = "http://tempuri.org/FooWs/v3/hoste")]
    [MessageContract(IsWrapped = false, WrapperNamespace = "http://tempuri.org/FooWs/v3/hosted")]
    public class FooResponse
    {
        [MessageBodyMember(Name = "Response")]
        public FooResponseWrapper Foo;
    }


    public class FooResponseWrapper
    {
        [MessageBodyMember] public FooResponseHeader Header;
        [MessageBodyMember] public FooResponseQuotedParts QuotedParts { get; set; }
    }

    public class FooResponseHeader
    {
        [XmlAttribute("status")]
        public string Status;
    }

    public class FooResponseQuotedParts
    {
        public FooResponseQuotedParts()
        {
            this.Suppliers = new List<FooResponseSupplier>();
        }

        [XmlElement(ElementName = "SupplierResponse")]
        public List<FooResponseSupplier> Suppliers { get; set; }
    }

    public class FooResponseSupplier
    {
        public FooResponseSupplier()
        {
            this.Quotes = new List<FooResponseQuoteItem>();
        }

        [XmlElement(ElementName = "SupplierID")]
        public Guid SupplierId { get; set; }

        [XmlElement(ElementName = "Quote")]
        public List<FooResponseQuoteItem> Quotes { get; set; }

        [XmlElement(ElementName = "SupplierName")]
        public string SupplierName { get; set; }
    }

    public class FooResponseQuoteItem
    {
        public FooResponseQuoteItem()
        {
            this.Quotes = new List<FooResponseQuote>();
        }

        public int PartRef { get; set; }

        public List<FooResponseQuote> Quotes { get; set; }

        public string Message { get; set; }
    }

    public class FooResponseQuote
    {
        [XmlElement(ElementName = "PartType")]
        public string PartType { get; set; }

        [XmlElement(ElementName = "DeliveryType")]
        public int DeliveryType { get; set; }

        [XmlElement(ElementName = "Grade")]
        public string Grade { get; set; }

        [XmlElement(ElementName = "PartNum")]
        public string PartNum { get; set; }

        [XmlElement(ElementName = "Desc")]
        public string Desc { get; set; }

        [XmlIgnore]
        public double Net { get; set; }

        [XmlElement(ElementName = "Net")]
        public string NetString
        {
            get
            {
                return Net.ToString("F2");
            }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    Net = amount;
            }
        }

        [XmlIgnore]
        public double List { get; set; }

        [XmlElement(ElementName = "List")]
        public string ListString
        {
            get
            {
                return List.ToString("F2");
            }
            set
            {
                double amount = 0;
                if (double.TryParse(value, out amount))
                    List = amount;
            }
        }

        [XmlElement(ElementName = "Avail")]
        public string Avail { get; set; }
    }
}
