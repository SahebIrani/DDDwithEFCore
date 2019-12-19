using DDDEfCore.ProductCatalog.Core.DomainModels.Products;
using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.UpdatePerson
{
    public class UpdateProductCommand : IRequest
    {
        public ProductId ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
