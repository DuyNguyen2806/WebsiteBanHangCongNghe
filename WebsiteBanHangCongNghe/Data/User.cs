using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? RandomKey { get; set; }

    public int RoleId { get; set; }

    public DateTime? Birthday { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
