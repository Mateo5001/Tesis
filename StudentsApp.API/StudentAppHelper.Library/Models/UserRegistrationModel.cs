using StudentAppHelper.Library.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAppHelper.Library.Models
{
  public class UserRegistrationModel
  {
    [Required]
    [Display(Name = "Nombre Primero")]
    public string NombreP { get; set; }

    [Required]
    [Display(Name = "Nombre Segundo")]
    public string NombreS { get; set; }

    [Required]
    [Display(Name = "Apellido Primero")]
    public string ApellidoP { get; set; }

    [Required]
    [Display(Name = "Apellido Segundo")]
    public string ApellidoS { get; set; }

    [Required]
    [Display(Name = "Tipo TipoDocumento")]
    public TipoDocumento TipoDocumento { get; set; }

    [Required]
    [Display(Name = "Identificacion")]
    public string Identificacion { get; set; }

    [Required]
    [Display(Name = "User name")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}