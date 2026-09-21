using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Wallet.Api.Services;

public class WalletWalletService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WalletWalletService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<Models.WalletWalletResponse> CreatePassAsync(
        Models.EmployeeCard card)
    {
        var apiKey = _configuration["WalletWallet:ApiKey"];

        var requestBody = new
        {
            barcodeValue = card.EmployeeNumber,
            barcodeFormat = "QR",

            logoText = card.Company,

            primaryFields = new[]
            {
                new
                {
                    label = "NAME",
                    value = $"{card.EmployeeSurname} {card.EmployeeLastName}"
                },
                new
                {
                    label = "EMPLOYEE",
                    value = card.EmployeeNumber
                }
            },

            colorPreset = "dark"
        };

        var json = JsonSerializer.Serialize(requestBody);

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.walletwallet.dev/api/passes");

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        request.Content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request);
var responseBody = await response.Content.ReadAsStringAsync();

if (!response.IsSuccessStatusCode)
{
    throw new Exception(
        $"WalletWallet API error: {response.StatusCode} - {responseBody}");
}

var walletPass =
    JsonSerializer.Deserialize<Models.WalletWalletResponse>(responseBody);

return walletPass!;
    }
}