using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfConsoleMessages.Models;

namespace WcfConsoleMessages.Contracts
{
    [ServiceContract(Namespace = "http://tempuri.org/FooWs/v3/hosted")]
    public interface IFooService
    {
        [OperationContract(Action = "Foo")]
        [XmlSerializerFormat]
        Task<FooResponse> Foo(FooRequest request);
    }
}
