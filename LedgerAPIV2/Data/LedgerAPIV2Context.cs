using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LedgerAPIV2.Models;

namespace LedgerAPIV2.Data
{
    public class LedgerAPIV2Context : DbContext
    {
        public LedgerAPIV2Context (DbContextOptions<LedgerAPIV2Context> options)
            : base(options)
        {
        }

        public DbSet<LedgerAPIV2.Models.Ledger> Ledger { get; set; } = default!;
        public DbSet<LedgerAPIV2.Models.User> User { get; set; } = default!;
    }
}
