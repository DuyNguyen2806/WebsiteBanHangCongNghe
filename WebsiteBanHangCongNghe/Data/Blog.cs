using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ShortDesc { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Img { get; set; } = null!;
}
