using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Model
{
    public class Voucher
    {
        public string VoucherCode { get; set; }
        public VoucherType Type { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public int UsageLimit { get; set; }
        public int RemainingUsage { get; set; }
    }

    public enum VoucherType
    {
        Percentage,
        FixedAmount
    }
}
