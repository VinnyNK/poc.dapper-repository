namespace poc.dapper_repository.Domain.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    
    public string LastName { get; private set; }

    public int Age { get; private set; }

    public User(string name, string lastName, int age)
    {
        Name = name;
        LastName = lastName;
        Age = age;
    }
}
