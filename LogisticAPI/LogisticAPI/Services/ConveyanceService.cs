using AutoMapper;
using LogisticAPI.Entities;
using LogisticAPI.Enums;
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


            if (request.TransportType == Enums.TransportEnum.MARINE_TRANSPORT)
            {

                if (!Regex.IsMatch(request.Id, TransportEnumRegex.MARINE_TRANSPORT))
                {
                    return new ConveyanceResponse() { Errors = new() { ErrorTypeEnum.IdFormatTransportMaritimeIsRequired } };
                }
            }
            else if (request.TransportType == Enums.TransportEnum.GROUND_TRANSPORT)
            {
                if (!Regex.IsMatch(request.Id, TransportEnumRegex.GROUND_TRANSPORT))
                {
                    return new ConveyanceResponse() { Errors = new() { ErrorTypeEnum.IdFormatTransportGroundIsRequired } };

                }
            }
            else
            {
                return new ConveyanceResponse() { Errors = new() { ErrorTypeEnum.TypeTransportIsRequired } };
            }

            entity = await @object.CreateConveyance(entity);
            var result = mapper.Map<ConveyanceResponse>(entity);
            return result;
        }

        public async Task<List<ConveyanceResponse>> GetConveyances()
        {
            var entities = await @object.GetConveyances();
            var result = mapper.Map<List<ConveyanceResponse>>(entities);
            return result;
        }


    }
}