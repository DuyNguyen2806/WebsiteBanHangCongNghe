using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string? Slug { get; set; }

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public string Imgs { get; set; } = null!;

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public double? Discount { get; set; }

    public int? Rating { get; set; }

    public bool? IsTrend { get; set; }

    public bool? IsVisible { get; set; }

    public int InstockId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Instock Instock { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
