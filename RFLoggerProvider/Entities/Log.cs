using RFService.Entities;
using RFAuth.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFLoggerProvider.Entities
{
    [Table("TransactionsLog", Schema = "log")]
    public class Log
        : EntityIdUuid
    {
        [Required]
        public DateTime LogTimestamp { get; set; } = default;

        [ForeignKey("Level")]
        public Int64? LevelId { get; set; }
        public LogLevel? Level { get; set; }

        [ForeignKey("Module")]
        public Int64? ModuleId { get; set; }
        public LogModule? Module { get; set; }

        [ForeignKey("Action")]
        public Int64? ActionId { get; set; }
        public LogAction? Action { get; set; }

        [ForeignKey("Session")]
        public Int64? SessionId { get; set; }
        public Session? Session { get; set; }

        [ForeignKey("User")]
        public User? User { get; set; }

        [MaxLength(-1)]
        public string? Message { get; set; }

        [MaxLength(-1)]
        public string? JsonData { get; set; }
    }
}