﻿using System;
using System.Collections.Generic;

namespace User_App.Models;

public partial class OrOrganization
{
    public int OrganizationId { get; set; }

    public string OrganizationName { get; set; } = null!;

    public string? Email { get; set; }

    public string? CreatedOn { get; set; }
}
