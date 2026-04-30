var users = new List<User>();

// Requesting dates
Console.WriteLine("Write your name: ");
string name = Console.ReadLine();

Console.WriteLine("Write your age: ");
int age = int.Parse(Console.ReadLine());

Console.WriteLine("Write you email: ");
string email = Console.ReadLine();

// Adding user
User newUser = new User(Name = name, Age = age, Email = email);

users.Add(newUser);

// The system assume that the class is public when it's not defined
class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}