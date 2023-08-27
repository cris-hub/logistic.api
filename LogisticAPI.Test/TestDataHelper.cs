using LogisticAPI.Entities;
using LogisticAPI.models;

namespace AuthenticationAPI.test
{
    public class TestDataHelper
    {
        public static List<Product> GetFakeProductsList()
        {
            return new List<Product>()
                {
                    new Product(){Amount=4 ,UserId="1234"},
                    new Product(){Amount=4 ,UserId="1234",Id="ABC1234D"},

                };
        }
        public static List<ProductResponse> GetFakeProductsResponseList()
        {
            return new List<ProductResponse>()
                {
                    new ProductResponse(){Amount=4 ,UserId="1234"},
                    new ProductResponse(){Amount=4 ,UserId="1234",Id="ABC1234D"},

                };
        }
    }
}
