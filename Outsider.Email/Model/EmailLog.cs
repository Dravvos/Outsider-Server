using Outsider.Email.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsider.Email.Model
{
    [Table("EmailLogs")]
    public class EmailLog:BaseModel
    {
        [Required]
        [Column("Email")]
        public string? Email { get; set; }
        
        [Column("Log")]
        public string? Log { get; set; }

        [Required]
        [Column("DataEnvio")]
        public DateTime DataEnvio { get; set; }
    }
}
