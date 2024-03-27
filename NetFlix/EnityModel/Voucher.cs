using System;
using System.Collections.Generic;

namespace NetFlix.EnityModel;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string? VoucherCode { get; set; }

    public string? VoucherType { get; set; }

    public decimal? DiscountValue { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public int? UsageLimit { get; set; }

    public int? RemainingUsage { get; set; }
}
