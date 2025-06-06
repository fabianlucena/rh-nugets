﻿namespace RFDapperDriverMySQL.Exceptions
{
    [Serializable]
    public class InvalidTableNameException(string? message)
        : Exception($"Invalid table name: {message}")
    {
    }
}