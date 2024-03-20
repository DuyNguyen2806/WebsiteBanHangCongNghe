using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Admin
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
