using System.Text.Json.Serialization;

namespace LedgerAPIV2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<Ledger> Ledgers { get; set; } = new List<Ledger>();
    }
}
