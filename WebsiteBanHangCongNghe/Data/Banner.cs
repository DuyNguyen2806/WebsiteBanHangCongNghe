using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Banner
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Img { get; set; }
}
