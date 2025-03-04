using AutoMapper;
using Outsider.CupomAPI.Model;
using Outsider.DTO;

namespace Outsider.TabelaGeralAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CupomModel, CupomDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
