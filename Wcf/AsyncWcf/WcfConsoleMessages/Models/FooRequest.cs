using System.ServiceModel;
using System.Xml.Serialization;

namespace WcfConsoleMessages.Models
{
    [MessageContract(IsWrapped = true, WrapperName = "Request")]
    public class FooRequest
    {
        [MessageBodyMember]
        public FooRepairer Repairer { get; set; }
    }

    public class FooRepairer
    {
        [XmlElement(ElementName = "RepairerName")]
        public string RepairerName { get; set; }

        [XmlElement(ElementName = "RepairerStreet")]
        public string RepairerStreet { get; set; }

        [XmlElement(ElementName = "RepairerCity")]
        public string RepairerCity { get; set; }

        [XmlElement(ElementName = "RepairerState")]
        public string RepairerState { get; set; }

        [XmlElement(ElementName = "RepairerZip", IsNullable = true)]
        public string RepairerZip { get; set; }

        [XmlElement(ElementName = "RepairerZipExt", IsNullable = true)]
        public string RepairerZipExt { get; set; }
    }

}
