using System.ComponentModel.DataAnnotations;

namespace PhoneCase.Shared.Enums;

public enum CategoryType
{
    [Display(Name ="Cihaz")]
    Device = 0,
    [Display(Name ="Malzeme")]
    Material = 1,
    [Display(Name ="Tarz")]
    Style = 2,
    [Display(Name ="Kampanya")]
    Campaign = 3
}
