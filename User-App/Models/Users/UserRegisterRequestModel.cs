namespace User_App.Models.Users
{
    public class UserRegisterRequestModel
    {
        public int UserId { get; set; }

        public int OrganizationId { get; set; }

        public string UserName { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public DateOnly? CreatedOn { get; set; }

        public virtual OrOrganization Organization { get; set; } = null!;
    }
}
