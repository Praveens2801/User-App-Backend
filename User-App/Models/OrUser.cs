using System;
using System.Collections.Generic;

namespace User_App.Models;

public partial class OrUser
{
    public int UserId { get; set; }

    public int OrganizationId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? CreatedOn { get; set; }
}
