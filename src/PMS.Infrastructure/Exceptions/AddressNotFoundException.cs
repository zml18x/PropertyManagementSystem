﻿namespace PMS.Infrastructure.Exceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(string? message) : base(message) { }
    }
}