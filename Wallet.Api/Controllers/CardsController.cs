using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet.Api.Data;
using Wallet.Api.Models;
using Wallet.Api.Services;

namespace Wallet.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly WalletDbContext _db;
    private readonly WalletWalletService _walletWalletService;

    public CardsController(
        WalletDbContext db,
        WalletWalletService walletWalletService)
    {
        _db = db;
        _walletWalletService = walletWalletService;
    }

    // GET /api/cards
    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var cards = await _db.EmployeeCards.ToListAsync();

        return Ok(cards);
    }

[HttpGet("{id}")]
public async Task<IActionResult> GetCard(Guid id)
{
    var card = await _db.EmployeeCards.FindAsync(id);

    if (card == null)
    {
        return NotFound();
    }

    return Ok(card);
}
  // POST /api/cards
[HttpPost]
public async Task<IActionResult> CreateCard(
    CreateEmployeeCardRequest request)
{
    try
    {
        var card = new EmployeeCard
        {
            EmployeeSurname = request.EmployeeSurname,
            EmployeeLastName = request.EmployeeLastName,
            EmployeeNumber = request.EmployeeNumber,
            Company = request.Company,
            EmployeeEmail = request.EmployeeEmail,
            PartnerCard = request.PartnerCard
        };

        // Eerst WalletWallet aanroepen
        var walletPass =
            await _walletWalletService.CreatePassAsync(card);

        // WalletWallet succesvol → gegevens opslaan
        card.WalletPassId = walletPass.SerialNumber;
        card.WalletPassUrl = walletPass.ShareUrl;

        _db.EmployeeCards.Add(card);

        await _db.SaveChangesAsync();

        return Ok(card);
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            message = "Could not create employee card.",
            error = ex.Message
        });
    }
}

//Delete /api/cards/{id}
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteCard(Guid id)
{
    var card = await _db.EmployeeCards.FindAsync(id);

    if (card == null)
    {
        return NotFound();
    }

    _db.EmployeeCards.Remove(card);

    await _db.SaveChangesAsync();

    return NoContent();
}

}