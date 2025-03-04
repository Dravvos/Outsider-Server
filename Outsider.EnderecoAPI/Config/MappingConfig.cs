using AutoMapper;
using Outside.EnderecoAPI.Model;
using Outsider.DTO;
using Outsider.EnderecoAPI.Model;

namespace Outsider.EnderecoAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<EnderecoDTO, EnderecoModel>().ReverseMap();
                config.CreateMap<ENDERECODTC_NOSQL, EnderecoDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
