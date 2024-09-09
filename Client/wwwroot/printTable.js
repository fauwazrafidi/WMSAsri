﻿function printElement(elementId) {
    var originalContent = document.getElementById(elementId).outerHTML;
    var printWindow = window.open('', '_blank');
    printWindow.document.open();
    printWindow.document.write(`
        <html>
            <head>
                <style>

                @page {
                    margin: 0.1cm; /* Adjust the margin as needed */
                }

                @media print {
                    body {
                        overflow: hidden; /* Prevents scrolling */
                    }

                th, td {
                    border: 1px solid black; /* Adds a black border to table cells */
                    padding: 2px; /* Adds padding to cells for better readability */
                    text-align: left; /* Aligns text to the left in the cells */
                    height: calc(100% / 14); /* Set the height of each cell to 50 pixels */
                }

                th {
                    overflow: visible; /* Ensures overflowed text is visible */
                }

                .custom-width {
                    width: 30%; /* Set the width of each data cell to a quarter of the table width */
                }

                .table-font {
                    font-size: 8pt;
                }

                .no-border-top {
                    border-top: none; /* Disables the top border */
                }

                .no-border-right {
                    border-right: none; /* Disables the right border */
                }

                .no-border-bottom {
                    border-bottom: none; /* Disables the bottom border */
                }

                .no-border-left {
                    border-left: none; /* Disables the left border */
                }

                .no-wraptext {
                    white-space: nowrap;
                }

                .qr-code {
                    position: absolute;
                    bottom: 1px; /* Adjust as needed */
                    right: 1px; /* Adjust as needed */
                }

                .position-relative {
                    position: relative; /* Makes this cell the reference point for absolute positioning */
                }

                .center-cell {
                    text-align: center;
                    vertical-align: middle;
                }

                .right-cell {
                    text-align: right;
                    vertical-align: middle;
                }

                .custom-height {
                    height: 75px;
                }

                .bottom-aligned-cell {
                    position: absolute;
                    bottom: 2%; /* Adjust to be 2% above the bottom of the label container */
                    left: 0;
                    right: 0;
                    width: 100%;
                    box-sizing: border-box; /* Ensure padding and border are included in the width */
                }

                .xs-text-cell {
                    font-size: 6pt;
                }

                .m-text-cell {
                    font-size: 12pt; /* Adjust the font size as needed */
                }

                .l-text-cell {
                    font-size: 30pt; /* Adjust the font size as needed */
                }

                .xl-text-cell {
                    font-size: 40pt; /* Adjust the font size as needed */
                }

                .text-right {
                    position: absolute;
                    top: 15%; /* Adjust as needed */
                    left: 400px; /* Adjust as needed */
                    transform: translateY(-50%);
                    white-space: nowrap;
                }

                .text-right-2 {
                    position: absolute;
                    top: 60%; /* Adjust as needed */
                    left: 400px; /* Adjust as needed */
                    transform: translateY(-50%);
                    white-space: nowrap;
                }

                th:first-child {
                    font-weight: bold;
                }

                td:first-child {
                    font-weight: bold;
                    width: 30%;
                }

                /* Adjust the width of the middle column */
                th:nth-child(2),
                td:nth-child(2) {
                    width: 5%; /* Set the width of the middle column */
                    font-weight: bold;
                }


                    .print-only-first-page {
                        page-break-after: always;
                    }
                }


                </style>
            </head>
            <body onload="window.print();window.close()">
                ${originalContent}
            </body>
        </html>
    `);
    printWindow.document.close();
}

function downloadFile(byteArray, filename) { // Accept byte array directly
    var blob = new Blob([byteArray], { type: 'application/octet-stream' });
    var link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = filename;
    link.click();
}