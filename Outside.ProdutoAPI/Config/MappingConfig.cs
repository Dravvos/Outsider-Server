using AutoMapper;
using Outsider.DTO;
using Outsider.ProdutoAPI.Model;

namespace Outsider.ProdutoAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProdutoDTO, ProdutoModel>();
                config.CreateMap<ProdutoModel, ProdutoDTO>();

                config.CreateMap<TabelaGeralDTO, TabelaGeralModel>();
                config.CreateMap<TabelaGeralModel, TabelaGeralDTO>();

                config.CreateMap<TabelaGeralItemDTO, TabelaGeralItemModel>();
                config.CreateMap<TabelaGeralItemModel, TabelaGeralItemDTO>();


            });
            return mappingConfig;
        }
    }
}
