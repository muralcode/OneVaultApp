# OneVaultApp

## Note

This is a MAUI application that authenticates to https://demo.duendesoftware.com/ and gets a token and then is capable of calling the Duende test API as described on the demo page (https://demo.duendesoftware.com/api/test).

### Problem Definition
1. Create a MAUI application with XAML pages for authentication, API calling, and QR code scanning.
2. Implement the MVVM pattern in the application.
3. Authenticate to https://demo.duendesoftware.com/ using an HTTP client and retrieve an access token.
4. Call the Duende test API (https://demo.duendesoftware.com/api/test) with the retrieved access token.
5. Add a XAML page that launches a camera, captures images, and processes QR codes.
6. Display the string result from the processed QR code on the screen.

It is recommended that the information in the other sections of this document are examined prior to attempting to compile and run the app. For the reasons discussed above, a developer cannot simply clone the repository and run the app.

### Software Prerequisites

In order to build the app you may use visual studio, the following software package is required:
- [Visual Studio](https://visualstudio.microsoft.com/)

### Procedure

1. Make sure that you have installed the above listed software on your machine.
2. Checkout the [master](https://github.com/muralcode/OneVaultApp) branch
3. Make sure that your local **develop** branch has all the latest changes.
