using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Dateorder { get; set; }

    public string? Phone { get; set; }

    public DateTime? Datedelivery { get; set; }

    public string Username { get; set; } = null!;

    public string? Address { get; set; }

    public int StatusId { get; set; }

    public string? Note { get; set; }

    public double Fee { get; set; }

    public int PayId { get; set; }

    public int? AdminId { get; set; }

    public int DeliveryId { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual Deliveryment Delivery { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Payment Pay { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
