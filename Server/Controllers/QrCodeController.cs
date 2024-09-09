using Microsoft.AspNetCore.Mvc;
using QRCoder;
using SHARED.Helpers;
using System.Drawing;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly string _generalApi = $"{Constants.GeneralApi}";

        [HttpGet("Generate/{id}")]
        public IActionResult GenerateQRCode(int id)
        {
            try
            {
                string url = $"{_generalApi}/Product/single/{id}";
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20)) // 20 is pixel size per module
                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    string base64String = Convert.ToBase64String(byteImage);
                    return Ok(base64String);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to generate QR code: {ex.Message}");
            }
        }

        [HttpGet("locationGenerate/{locationid}")]
        public IActionResult GeneratelocationQRCode(string locationid)
        {
            try
            {
                string url = $"{_generalApi}/Location/single/{locationid}";
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20)) // 20 is pixel size per module
                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    string base64String = Convert.ToBase64String(byteImage);
                    return Ok(base64String);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to generate QR code: {ex.Message}");
            }
        }
    }
}
