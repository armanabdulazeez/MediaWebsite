namespace WebAppFreelanceMediaApi.EntryModels
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}
