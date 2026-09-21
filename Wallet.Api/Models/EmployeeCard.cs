namespace Wallet.Api.Models;

public class EmployeeCard
{
    public Guid ID { get; set; }

    public string? EmployeeSurname { get; set; }

    public string? EmployeeLastName { get; set; }

    public string? EmployeeNumber { get; set; }

    public string? Company { get; set; }

    public string? EmployeeEmail { get; set; }

    public bool PartnerCard { get; set; }

    public string? WalletPassId { get; set; }

    public string? WalletPassUrl { get; set; }
}