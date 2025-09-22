namespace PrivateChat.Models
{
    class ApiResponse<T>
    {
        public List<string>? Errors;
        public T? Data;
        public string? Message;
    }
}