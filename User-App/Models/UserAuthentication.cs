using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_App.Models;

[Keyless]
[Table("User_Authentication")]
public partial class UserAuthentication
{
    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("userName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Password { get; set; }
}
