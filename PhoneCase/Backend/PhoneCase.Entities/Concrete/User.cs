using System;
using Microsoft.AspNetCore.Identity;
using PhoneCase.Entities.Abstract;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Entities.Concrete;

public class User : IdentityUser,IEntity
{
    private User()
    {
    }

    public User(string? firstname, string? lastName, string? address, string? city, Gender? gender)
    {
        FirstName = firstname;
        LastName = lastName;
        Address = address;
        City = city;
        Gender = gender;
}
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public Gender? Gender { get; set; }
    public DateTimeOffset RegistrationDate { get; set; } = DateTimeOffset.UtcNow;
}
