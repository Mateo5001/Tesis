using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Formatting;


namespace StudentAppHelper.ModelBindings.Models
{
  public class logInModel
  {
    [Required]
    public string User { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

  }
}


