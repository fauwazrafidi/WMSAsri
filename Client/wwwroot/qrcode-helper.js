function generateQRCode(data) {
    var qrcodeContainer = document.getElementById("qrcode");
    qrcodeContainer.innerHTML = ""; // Clear any existing QR code
    new QRCode(qrcodeContainer, data);
}
