using API_Challenge.Modelos;
using API_Challenge.Modelos.DTO;
using AutoMapper;

namespace API_Challenge
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClienteDto, Cliente>();
                config.CreateMap<Cliente, ClienteDto>();
            });    
            return mappingConfig;
        }
    }
}
