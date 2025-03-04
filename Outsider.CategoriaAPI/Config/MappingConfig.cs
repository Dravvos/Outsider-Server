using AutoMapper;
using Outsider.CategoriaAPI.Model;
using Outsider.DTO;

namespace Outsider.CategoriaAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CategoriaDTO, CategoriaModel>();
                config.CreateMap<CategoriaModel, CategoriaDTO>();
            });
            return mappingConfig;
        }
    }
}
