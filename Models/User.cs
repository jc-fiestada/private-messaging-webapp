using BCrypt.Net;

namespace PrivateChat.Models
{
    class User
    {
        int ID { get; }
        string Name { get; }
        string Username { get; }
        string Password { get; }
        string HashedPassword { get; }
        string Sex { get; }
        int Age { get; }
        List<string> ErrorList { get; } = new List<string>();

        private User(int id, string name, string username, string password, string hashedPassword, string sex, int age)
        {
            ID = id;
            Name = name;
            Username = username;
            Password = password;
            HashedPassword = hashedPassword;
            Sex = sex;
            Age = age;
        }


        public User? CreateUser(int? id, string name, string username, string password, string sex, int age)
        {
            List<string> errorList = new List<string>();
            
            name = name.Trim().ToUpper();
            sex = sex.Trim().ToUpper();

            if (id is null)
            {

            }

            if (id < 0)
            {

            }



            if (string.IsNullOrWhiteSpace(name))
            {

            }
            else if (name.Length < 5)
            {

            }
            else if (name.Length > 50)
            {
                
            }

            if (string.IsNullOrWhiteSpace(username))
            {

            }
            else if (username.Length < 5)
            {

            }
            else if (username.Length > 15)
            {
                
            }

            if (string.IsNullOrWhiteSpace(password))
            {

            }
            else if (password.Length < 5)
            {

            }
            else if (password.Length > 15)
            {
                
            }









            if (ErrorList.Count() == 0)
            {
                string convertedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                User user = new User((int)id!, name, username, password, convertedPassword, sex, age);
                return user;
            }
            return null;
        }
        
        
    }
}