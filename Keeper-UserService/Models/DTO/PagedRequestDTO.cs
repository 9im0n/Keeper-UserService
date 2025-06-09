﻿namespace Keeper_UserService.Models.DTO
{
    public class PagedRequestDTO<TFilter>
    {
        public TFilter? Filter { get; set; }
        public string Sort { get; set; } = "Id";
        public string Direction { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
