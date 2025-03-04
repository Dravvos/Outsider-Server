using AutoMapper;
using Outsider.AvaliacaoAPI.Model;
using Outsider.AvaliacaoAPI.MongoDB;
using Outsider.DTO;

namespace Outsider.AvaliacaoAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AvaliacaoDTO, AvaliacaoModel>().ReverseMap();
                config.CreateMap<AVALIACAODTC_NOSQL, EnderecoDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
