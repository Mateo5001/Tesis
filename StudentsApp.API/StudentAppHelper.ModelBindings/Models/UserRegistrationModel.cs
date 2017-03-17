
using StudentAppHelper.ModelBindings.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentAppHelper.ModelBindings.Models
{
  public class UserRegistrationModel
  {
    [Required]
    [Display(Name = "Nombre Primero")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Nombre Segundo")]
    public string SecondName { get; set; }

    [Required]
    [Display(Name = "Apellido Primero")]
    public string FirstLastName { get; set; }

    [Required]
    [Display(Name = "Apellido Segundo")]
    public string SecondLastName { get; set; }

    [Required]
    [Display(Name = "Tipo TipoDocumento")]
    public EDocumentType DocumentType { get; set; }

    [Required]
    [Display(Name = "Identificacion")]
    public string DocumentNumber { get; set; }

    [Required]
    [Display(Name = "User name")]
    public string UserNick { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmationPassword { get; set; }
  }
}