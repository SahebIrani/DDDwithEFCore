using DDDEfCore.ProductCatalog.Core.DomainModels.Products;
using FluentValidation;
using System;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
    public class GetProductDetailRequestValidator : AbstractValidator<GetProductDetailRequest>
    {
        public GetProductDetailRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEqual((ProductId) Guid.Empty);
        }
    }
}
