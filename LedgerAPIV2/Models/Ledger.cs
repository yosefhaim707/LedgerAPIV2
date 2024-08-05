using System.Text.Json.Serialization;

namespace LedgerAPIV2.Models
{
    public class Ledger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; } = new List<User>();
    }
}
