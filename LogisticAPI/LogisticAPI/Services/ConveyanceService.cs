using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Models;
using LogisticAPI.Repositories;
using System.Text.RegularExpressions;

namespace LogisticAPI.Services
{
    public class ConveyanceService : IConveyanceService
    {
        private IConveyanceRepository @object;
        private readonly IMapper mapper;

        public ConveyanceService(IConveyanceRepository @object, IMapper mapper)
        {
            this.@object = @object;
            this.mapper = mapper;
        }

        public async Task<ConveyanceResponse> CreateConveyance(ConveyanceRequest request)
        {
            var entity = mapper.Map<Conveyance>(request);

            string GROUND_TRANSPORT = @"^[A-Za-z]{3}\d{3}$"; ;
            string MARINE_TRANSPORT = @"^[A-Za-z]{3}\d{4}[A-Za-z]$";

            if (request.TransportType == Enums.TransportEnum.MARINE_TRANSPORT)
            {

                if (!Regex.IsMatch(request.Id, MARINE_TRANSPORT))
                {
                    return new ConveyanceResponse() { Errors = new() { new() {Message = "Id is no valid" } } };
                }
            }
            else if (request.TransportType == Enums.TransportEnum.GROUND_TRANSPORT)
            {
                if (!Regex.IsMatch(request.Id, GROUND_TRANSPORT))
                {
                    return new ConveyanceResponse() { Errors = new() { new() { Message = "Id is no valid" } } };
                }
            }

            entity = await @object.CreateConveyance(entity);
            var result = mapper.Map<ConveyanceResponse>(entity);
            return result;
        }
    }
}