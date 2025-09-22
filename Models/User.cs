using BCrypt.Net;

namespace PrivateChat.Models
{
    class User
    {
        public int? ID { get; }
        public string Name { get; }
        public string Username { get; }
        public string? HashedPassword { get; }
        public string Sex { get; }
        public int Age { get; }

        private User(int? id, string name, string username, string sex, int age, string hashedPassword = "")
        {
            ID = id;
            Name = name;
            Username = username;
            HashedPassword = hashedPassword;
            Sex = sex;
            Age = age;
        }

        public static (User?, List<string>) ValidateUser(UserDTO u)
        {
            List<string> errorList = new List<string>();

            if(u.Name is not null) u.Name = u.Name.Trim().ToUpper();
            if(u.Sex is not null) u.Sex = u.Sex.Trim().ToUpper();


            if (u.ID is not null && u.ID <= 0) errorList.Add("Invalid user Id");

            if (string.IsNullOrWhiteSpace(u.Name)) errorList.Add("Name should not be empty");
            else if (u.Name.Length < 5) errorList.Add("Name should not be less than 5 characters");
            else if (u.Name.Length > 60) errorList.Add("Name should not exceed more than 60 characters");

            if (string.IsNullOrWhiteSpace(u.Username)) errorList.Add("Username should not be empty");
            else if (u.Username.Length < 5) errorList.Add("Username should not be less than 5 characters");
            else if (u.Username.Length > 15) errorList.Add("Username should not exceed more than 15 characters");

            if (string.IsNullOrWhiteSpace(u.Password)) errorList.Add("Password should not be empty");
            else if (u.Password.Length < 5) errorList.Add("Password should not be less than 5 characters");
            else if (u.Password.Length > 15) errorList.Add("Password should not exceed more than 15 characters");

            if (string.IsNullOrWhiteSpace(u.Sex)) errorList.Add("Sex should not be empty");
            else if (u.Sex != "MALE" && u.Sex != "FEMALE") errorList.Add("Invalid sex value");

            if (u.Age is null) errorList.Add("Age should not be empty");
            else if (u.Age < 18) errorList.Add("Underage users are not allowed to use this software");
            else if (u.Age > 99) errorList.Add("Overage users are not allowed to use this software");


            if (errorList.Count() != 0) return (null, errorList);

            User user = new User(u.ID, u.Name!, u.Username!,  u.Sex!, (int)u.Age!, BCrypt.Net.BCrypt.HashPassword(u.Password));

            return (user, errorList);
        }

    }
}