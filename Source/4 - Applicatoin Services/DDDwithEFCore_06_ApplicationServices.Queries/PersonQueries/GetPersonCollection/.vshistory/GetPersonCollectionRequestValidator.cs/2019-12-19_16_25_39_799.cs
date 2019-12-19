﻿using FluentValidation;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonCollection
{
    public class GetProductCollectionRequestValidator : AbstractValidator<GetProductCollectionRequest>
    {
        public GetProductCollectionRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .LessThan(int.MaxValue);

            RuleFor(x => x.PageSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .LessThan(int.MaxValue);
        }
    }
}
