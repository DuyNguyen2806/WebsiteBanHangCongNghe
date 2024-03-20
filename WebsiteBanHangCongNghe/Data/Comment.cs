﻿using System;
using System.Collections.Generic;

namespace WebsiteBanHangCongNghe.Data;

public partial class Comment
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}