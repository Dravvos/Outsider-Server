using AutoMapper;
using Outside.PedidoAPI.Model;
using Outsider.DTO;
using Outsider.PedidoAPI.Model;

namespace Outsider.PedidoAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PedidoDTO, PedidoModel>().ReverseMap();
                config.CreateMap<PedidoItemDTO, PedidoItemModel>().ReverseMap();
                config.CreateMap<EnderecoDTO, EnderecoModel>().ReverseMap();
                config.CreateMap<ProdutoModel, ProdutoDTO>().ReverseMap();
                config.CreateMap<TabelaGeralModel, TabelaGeralDTO>().ReverseMap();
                config.CreateMap<TabelaGeralItemDTO, TabelaGeralItemModel>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
