﻿namespace ApiJWT.Models;

public record SignUpRequest
{
    public required string Email
    {
        get; init;
    }
    public required string Name
    {
        get; init;
    }
    public required string Surname
    {
        get; init;
    }
    public required string Phone
    {
        get; init;
    }
    public required string Password
    {
        get; init;
    }
}