﻿namespace Profile.ResponsResults
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}
