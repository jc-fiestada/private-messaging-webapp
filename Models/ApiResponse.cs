namespace PrivateChat.Models
{
    class ApiResponse<T>
    {
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}