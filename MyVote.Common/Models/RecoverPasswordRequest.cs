﻿using System.ComponentModel.DataAnnotations;

public class RecoverPasswordRequest
{
    [Required]
    public string Email { get; set; }
}

