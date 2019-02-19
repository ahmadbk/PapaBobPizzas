using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PapaBobMegaChallengeMVC.Models
{
    public enum Size : int
    {
        [Display(Name ="Small (12 Inch - $12)")]
        Small = 0,
        [Display(Name = "Medium (14 Inch - $14)")]
        Medium = 1,
        [Display(Name = "Large (16 Inch - $16)")]
        Large = 2
    }
}