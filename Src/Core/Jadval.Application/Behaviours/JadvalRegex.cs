﻿namespace Jadval.Application.Behaviours
{
    public static class JadvalRegex
    {
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$";
        public const string Email = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string PhoneNumber = "^09\\d{9}$";
        public const string UserName = "^[a-zA-Z_][a-zA-Z0-9_.]*";
    }
}
