using AutoMapper;
using Outsider.DTO;
using Outsider.TabelaGeralAPI.Model;

namespace Outsider.TabelaGeralAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TabelaGeralDTO, TabelaGeralModel>();
                config.CreateMap<TabelaGeralModel, TabelaGeralDTO>();

                config.CreateMap<TabelaGeralItemDTO, TabelaGeralItemModel>();
                config.CreateMap<TabelaGeralItemModel, TabelaGeralItemDTO>();
            });
            return mappingConfig;
        }
    }
}
