namespace PrivateChat.Models
{
    class ModifyAndAccessUser
    {
        public static (UserDTO?, List<string>) VerifyNullableUser(UserDTO u)
        {
           List<string> errorList = new List<string>();

            if (u.Name is not null) u.Name = u.Name.Trim().ToUpper();
            if (u.Sex is not null) u.Sex = u.Sex.Trim().ToUpper();

            if (u.Name is not null)
            {
                if (u.Name.Length < 5) errorList.Add("Name should not be less than 5 characters");
                else if (u.Name.Length > 60) errorList.Add("Name should not exceed more than 60 characters");
            }

            if (u.Username is not null)
            {
                if (u.Username.Length < 5) errorList.Add("Username should not be less than 5 characters");
                else if (u.Username.Length > 15) errorList.Add("Username should not exceed more than 15 characters");
            }

            if (u.Password is not null)
            {
                if (u.Password.Length < 5) errorList.Add("Password should not be less than 5 characters");
                else if (u.Password.Length > 15) errorList.Add("Password should not exceed more than 15 characters");
            }

            if (u.Sex is not null)
            {
                if (u.Sex != "MALE" && u.Sex != "FEMALE") errorList.Add("Invalid sex value");
            }

            if (u.Age is not null)
            {
                if (u.Age < 18) errorList.Add("Underage users are not allowed to use this software");
                else if (u.Age > 99) errorList.Add("Overage users are not allowed to use this software");
            }

            UserDTO userDTO = new UserDTO()
            {
                ID = u.ID,
                Name = u.Name,
                Username = u.Username,
                Password = u.Password,
                HashedPassword = u.HashedPassword,
                Age = u.Age,
                Sex = u.Sex
            };

            if (errorList.Count() != 0) return (null, errorList);

        
            return (userDTO, errorList);
        

        }
    }
}