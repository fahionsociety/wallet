using System.Text.Json.Serialization;

namespace Wallet.Api.Models;

public class WalletWalletResponse
{
    [JsonPropertyName("serialNumber")]
    public string SerialNumber { get; set; } = "";

    [JsonPropertyName("shareUrl")]
    public string ShareUrl { get; set; } = "";
}