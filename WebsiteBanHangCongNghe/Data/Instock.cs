using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Instock
{
    /// <summary>
    /// 0: Hết hàng, 1:Còn hàng
    /// </summary>
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
