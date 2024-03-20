using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string? Imgs { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
