using MediatR;
using Server.Application.Interfaces;
using Server.Application.Models;
using static Server.Core.ResponseModels;

namespace Server.Application.Command_Operations.Products
{
    public class DeleteProduct_Command : IRequest<DeleteProduct_Result>
    {
        public int ProductID { get; set; }
    }

    public class DeleteProduct_CommandHandler : IRequestHandler<DeleteProduct_Command, DeleteProduct_Result>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProduct_CommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DeleteProduct_Result> Handle(DeleteProduct_Command request, CancellationToken ct)
        {
            Product? searchedProduct = await _productRepository.GetProductAsync(request.ProductID);

            if(searchedProduct is null) return new DeleteProduct_Result() { IsSuccessful = false, Message = "WARNING: ProductID does not exist!"};

            await _productRepository.DeleteProductAsync(searchedProduct);

            return new DeleteProduct_Result() { IsSuccessful = true, Message = $"Product({searchedProduct.ProductName}) deleted successfully!" };
        }
    }
}
