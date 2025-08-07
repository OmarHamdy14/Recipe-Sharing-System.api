﻿namespace RecipeSharingAPI.Models
{
    public class AuthModel
    {
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string? Message { get; set; }

    }
}
