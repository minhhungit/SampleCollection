using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WcfConsoleMessages.Contracts;
using WcfConsoleMessages.Models;

namespace WcfConsoleMessages.Services
{
    public class FooWebService : IFooService
    {
        public Task<FooResponse> Foo(FooRequest request)
        {
            return Task.Run(() =>
            {
                return new FooResponse
                {
                    Foo = new FooResponseWrapper
                    {
                        Header = new FooResponseHeader
                        {
                            Status = "yes"
                        },
                        QuotedParts = new FooResponseQuotedParts
                        {
                            Suppliers = new List<FooResponseSupplier> {
                                new FooResponseSupplier{
                                    SupplierId = Guid.NewGuid(),
                                    SupplierName = "Jin Demo Foo Wcf",
                                    Quotes = new List<FooResponseQuoteItem>{
                                        new FooResponseQuoteItem{
                                            PartRef = 1,
                                            Quotes = new List<FooResponseQuote>{
                                               new FooResponseQuote{
                                                   PartNum = "1",
                                                   Desc = "item 01",
                                                   List = 1,
                                                   Net = 2
                                               },
                                               new FooResponseQuote{
                                                   PartNum = "2",
                                                   Desc = "item 02",
                                                   List = 3,
                                                   Net = 4
                                               }
                                            }
                                        },
                                        new FooResponseQuoteItem{
                                            PartRef = 2,
                                            Message = "no quote"                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            });
        }
    }
}