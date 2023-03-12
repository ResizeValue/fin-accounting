using FinAccountingApi.Application.PaymentChecks.Products.Tags;
using FinAccountingApi.Domain.PaymentCheck;

namespace FinAccountingApi.Application.PaymentChecks.Products
{
    public class ProductMapper
    {
        public static ProductModel GetProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Tags = TagMapper.GetListTagModel(product.Tags)
            };
        }

        public static IEnumerable<ProductModel>? GetListProductModel(IEnumerable<Product>? products)
        {
            if (products == null) return null;

            return products.Select(x => GetProductModel(x));
        }

        public static Product GetProduct(ProductModel product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Tags = TagMapper.GetListTag(product.Tags)?.ToList(),
            };
        }

        public static IEnumerable<Product>? GetListProduct(IEnumerable<ProductModel>? products)
        {
            if (products == null) return null;

            return products.Select(x => GetProduct(x));
        }
    }
}
