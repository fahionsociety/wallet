using System.ComponentModel.DataAnnotations;

namespace Wallet.Api.Models;

public class CreateEmployeeCardRequest
{
    [Required]
    public string EmployeeSurname { get; set; } = "";

    [Required]
    public string EmployeeLastName { get; set; } = "";

    [Required]
    public string EmployeeNumber { get; set; } = "";

    [Required]
    [RegularExpression("ZEB|PCR|TFS", ErrorMessage = "Company must be ZEB, PCR or TFS.")]
    public string Company { get; set; } = "";

    [Required]
    [EmailAddress]
    public string EmployeeEmail { get; set; } = "";

    public bool PartnerCard { get; set; }

    
}