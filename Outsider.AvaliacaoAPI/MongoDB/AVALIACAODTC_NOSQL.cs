using MongoDB.Bson.Serialization.Attributes;

namespace Outsider.AvaliacaoAPI.MongoDB
{
    public class AVALIACAODTC_NOSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public Guid ProdutoId { get; set; }
        [BsonElement]
        public Guid UsuarioId { get; set; }
        [BsonElement]
        public byte Estrelas { get; set; }
        [BsonElement]
        public string? Comentario { get; set; }
        [BsonElement]
        public DateTime DataAvaliacao { get; set; }
        [BsonElement]
        public DateTime DataInclusao { get; set; }
        [BsonElement]
        public DateTime DataAlteracao { get; set; }
    }
}
