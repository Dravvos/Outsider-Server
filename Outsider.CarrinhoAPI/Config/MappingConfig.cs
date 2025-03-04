using AutoMapper;
using Outsider.CarrinhoAPI.Model;
using Outsider.DTO;

namespace Outsider.CarrinhoAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProdutoDTO, ProdutoModel>().ReverseMap();
                config.CreateMap<TabelaGeralDTO,TabelaGeralModel>().ReverseMap();
                config.CreateMap<TabelaGeralItemDTO, TabelaGeralItemModel>().ReverseMap();
                config.CreateMap<CarrinhoDTO, CarrinhoModel>().ReverseMap();
                config.CreateMap<ItemCarrinhoModel, ItemCarrinhoDTO>().ReverseMap();

                config.CreateMap<CarrinhoRedisDTO, CarrinhoDTO>().ReverseMap();

                config.CreateMap<CarrinhoItemRedisDTO, ItemCarrinhoDTO>()
                    .ForMember(dest => dest.Carrinho, opt => opt.Ignore()).ReverseMap();
                    
            });
            return mappingConfig;
        }
    }
}
