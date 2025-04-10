﻿namespace App.Domain.Entities.Common;

public class PaginatedResult<T>
{
    //old
    public List<T>? Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
