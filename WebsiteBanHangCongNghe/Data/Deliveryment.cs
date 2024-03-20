using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Deliveryment
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
