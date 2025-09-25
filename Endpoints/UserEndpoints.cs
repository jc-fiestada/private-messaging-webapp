using Microsoft.VisualBasic;
using PrivateChat.Models;
using PrivateChat.DatabaseHelpers;

namespace PrivateChat.Endpoints
{
    class UserEndpoints
    {
        public async Task<IResult> SignUp(UserDTO user)
        {
            (User? verifiedUser, List<string> errors) = User.ValidateUser(user);

            ApiResponse<string> results = new ApiResponse<string>();

            if (errors.Count() != 0 && verifiedUser is null)
            {
                results.Errors = errors;
                return Results.Json(results, statusCode: 400);
            }

            UserDatabase database = new UserDatabase();
            await database.InsertUser(verifiedUser!);
            results.Message = "Success";
            return Results.Json(results, statusCode: 200);
        }
    }
}