using AutoMapper;
using Outsider.DTO;
using Outsider.MarcaAPI.Model;
using static Outsider.MarcaAPI.Model.MarcaModel;

namespace Outsider.MarcaAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MarcaDTO, MarcaModel>();
                config.CreateMap<MarcaModel, MarcaDTO>();

                config.CreateMap<CategoriaModel, CategoriaDTO>();
                config.CreateMap<CategoriaDTO, CategoriaModel>();
            });
            return mappingConfig;
        }
    }
}
