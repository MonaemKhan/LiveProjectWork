using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralModels.Administration
{
    [Table("userSessions", Schema = "_session")]
    public class UserSession
    {
        [Key]
        public string SessionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
    }
}
