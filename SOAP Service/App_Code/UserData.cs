using System;
using System.Linq;
using System.Collections.Generic;

public class UserData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public string Email { get; set; }
    public bool IsRetired { get; set; }

    public UserData() { }
    public UserData(string firstName, string lastName, int age, string gender, string email, bool isRetired)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        Email = email;
        IsRetired = isRetired;
    }

    public override string ToString()
    {
        IEnumerable<string> formatedProperties =
            GetType()
            .GetProperties()
            .Skip(1)
            .Select(p => p.Name + ": " +  p.GetValue(this).ToString());

        return string.Join(Environment.NewLine, formatedProperties);
    }
}