using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Payment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double? Fee { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
