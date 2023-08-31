
using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
using LogisticAPI.models;
using LogisticAPI.Repositories;
using LogisticAPI.Repository;
using System.Security.Claims;
using System.Security.Principal;

namespace LogisticAPI.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository @object;
        private readonly IConveyanceRepository object1;
        private readonly IMapper _mapper;
        private IPrincipal _principal;
        public ProductService(IProductRepository @object, IConveyanceRepository object1, IMapper mapper, IPrincipal principal)
        {
            _mapper = mapper;
            this.@object = @object;
            this.object1 = object1;
            _principal = principal;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            Product entity = _mapper.Map<Product>(request);
            ClaimsIdentity? claim = _principal.Identity as ClaimsIdentity;
            entity.UserId = claim?.Claims.FirstOrDefault(c => c.Type == "name").Value;
            double percentage = 0;
            entity.Conveyance = await object1.GetById(request.ConveyanceId);
            if (entity.Amount > (int)DiscountAmountEnum.MORETHANTEN)
            {
                if (entity.Conveyance.TransportType == TransportEnum.GROUND_TRANSPORT)
                {
                    percentage = ((double)DiscountEnum.GROUND_TRANSPORT);
                    entity.FinalPrice = entity.Price - (percentage * entity.Price) / 100;
                    entity.Discount = DiscountEnum.GROUND_TRANSPORT;
                }
                else if (entity.Conveyance.TransportType == TransportEnum.MARINE_TRANSPORT)
                {
                    percentage = ((double)DiscountEnum.MARINE_TRANSPORT);
                    entity.FinalPrice = entity.Price - (percentage * entity.Price) / 100;
                    entity.Discount = DiscountEnum.MARINE_TRANSPORT;
                }


            }
            else
            {
                entity.FinalPrice = entity.Price;
                entity.Discount = DiscountEnum.NONE;
            }

            entity.Id = RandomAlfanumericId();
            entity.Created = new DateTime();
            Product productCreated = await @object.CreateProduct(entity);
            ProductResponse response = _mapper.Map<ProductResponse>(entity);
            return response;
        }

        public async Task<ProductResponse> GetById(string id)
        {

            var response = await @object.GetById(id);
            var result = _mapper.Map<ProductResponse>(response);
            return result;
        }

        public async Task<IEnumerable<ProductResponse>> GetProdutsByUserId(string userId)
        {
            var response = await @object.GetByUserIdAsync(userId);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(response);
            return result;
        }
        public static string RandomAlfanumericId()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IEnumerable<ProductResponse>> GetProdutsByUserId()
        {
            ClaimsIdentity? claim = _principal.Identity as ClaimsIdentity;
            string userId = claim?.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var response = await @object.GetByUserIdAsync(userId);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(response);
            return result;
        }
    }
}