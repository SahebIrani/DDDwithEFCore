using System;
using System.Collections.Generic;
using System.Text;
using DDDEfCore.ProductCatalog.Core.DomainModels.Products;
using MediatR;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
    public class GetProductDetailRequest : IRequest<GetProductDetailResult>
    {
        public ProductId ProductId { get; set; }
    }
}
