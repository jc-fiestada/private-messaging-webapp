using Microsoft.VisualBasic;
using PrivateChat.Models;
using PrivateChat.DatabaseHelpers;

namespace PrivateChat.Endpoints
{
    class UserEndpoints
    {
        

        public IResult CheckUserSession(HttpContext context)
        {
            int? userId = context.Session.GetInt32("id");

            if (userId is null)
            return Results.Unauthorized();

            return Results.Ok();
        }
        public async Task<IResult> SignUp(UserDTO user, HttpContext context)
        {
            UserDatabase database = new UserDatabase();
            (User? verifiedUser, List<string> errors) = User.ValidateUser(user);

            ApiResponse<string> results = new ApiResponse<string>();

            if (errors.Count() != 0 && verifiedUser is null)
            {
                results.Errors = errors;
                return Results.Json(results, statusCode: 422);
            }

            await database.InsertUser(verifiedUser!);
            int userId = await database.GetUserId(verifiedUser!.Name);

            context.Session.SetInt32("id", userId);
            return Results.Ok();
        }

        public async Task<IResult> SignIn(UserDTO dto, HttpContext context)
        {
            UserDatabase database = new UserDatabase();
            ApiResponse<string> response = new ApiResponse<string>();

            if (dto.Username is null || dto.Password is null)
            return Results.BadRequest();

            (UserDTO? validatedDto, List<string> errors) = ModifyAndAccessUser.VerifyNullableUser(dto);

            if (validatedDto is null && errors.Count() != 0)
            {
                response.Errors = errors;
                return Results.Json(response, statusCode: 422);
            }

            (bool isUserCredentialsValid, int? userId) = await database.ValidateUserSignin(dto.Username, dto.Password);

            if (!isUserCredentialsValid || userId is null)
            return Results.Unauthorized();

            context.Session.SetInt32("id", (int)userId);
            return Results.Ok();
        }
    }
}