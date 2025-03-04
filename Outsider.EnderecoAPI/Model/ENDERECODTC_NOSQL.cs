using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Outsider.EnderecoAPI.Model
{
    
    public class ENDERECODTC_NOSQL
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public Guid Id { get; set; }
        [BsonElement]
        public string? Recebedor { get; set; }
        [BsonElement]
        [BsonGuidRepresentation(GuidRepresentation.CSharpLegacy)]
        public Guid UsuarioId { get; set; }
        [BsonElement]
        public string? Logradouro { get; set; }
        [BsonElement]
        public string? Bairro { get; set; }
        [BsonElement]
        public string? Estado { get; set; }
        [BsonElement]
        public string? Cidade { get; set; }
        [BsonElement]
        public short Numero { get; set; }
        [BsonElement]
        public string? Complemento { get; set; }
        [BsonElement]
        public string? CEP { get; set; }
        [BsonElement]
        public DateTime DataInclusao { get; set; }
        [BsonElement]
        public DateTime DataAlteracao { get; set; }
    }
}
