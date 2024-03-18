using BrDateTimeUtils;
using FileHelper;
using Signature.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Signature.Infraestructure.Services
{
    public class FileService : IFileService
    {
        private byte[] _base64SignatureArray;
        private int _signaturePositionY;
        private int _signaturePositionX;
        private int _base64SignatureWidth;
        private byte[] _base64UserPictureArray;
        private int _userPositionY;
        private int _userPositionX;
        private int _base64UserPictureWidth;

        public byte[] GenerateDigitalSignature(
            string identification,
            string name,
            string cellphone,
            string email,
            string documentNumber,
            string originalDocumentHash,
            string originalDocumentSignedHash,
            DateTime signatureDate,
            DateTime signatureGenerationDate,
            string signatureIp,
            string signatureLatitude,
            string signatureLongitude,
            string signatureCity,
            string signatureState,
            string signatureCountry,
            string signaturePostalCode,
            string signatureLocationLatitude,
            string signatureLocationLongitude,
            string signaturePictureBase64string,
            string base64SignatureString)
        {
            if (!string.IsNullOrEmpty(base64SignatureString))
            {
                _signaturePositionY = 490;
                _signaturePositionX = 140;
                _base64SignatureWidth = 300;
                _base64SignatureArray = Convert.FromBase64String(base64SignatureString);
                _base64UserPictureArray = Convert.FromBase64String(signaturePictureBase64string);
                _userPositionX = 240;
                _userPositionY = 340;
                _base64UserPictureWidth = 125;
            }

            var document = createDigitalSignature(new List<IEnumerable<KeyValuePair<string, TextInfoDto>>>
            {
                new List<KeyValuePair<string, TextInfoDto>>
                {
                    FileHelperService.GetValuePositions($"{signatureDate.Day.ToString("D2")}", 161, 130, 9),
                    FileHelperService.GetValuePositions($"{signatureDate.ToString(@"MMM", new CultureInfo("PT-br"))}", 161, 172, 9),
                    FileHelperService.GetValuePositions($"{signatureDate.Year}", 161, 213, 9),
                    FileHelperService.GetValuePositions($"{signatureDate.Hour.ToString("D2")}", 161, 253, 9),
                    FileHelperService.GetValuePositions($"{signatureDate.Minute.ToString("D2")}", 161, 278, 9),
                    FileHelperService.GetValuePositions($"{signatureDate.Second.ToString("D2")}", 161, 302, 9),
                    FileHelperService.GetValuePositions($"{documentNumber}", 175, 171, 9),
                    FileHelperService.GetValuePositions($"{name},", 221, 60, 17),
                    FileHelperService.GetValuePositions($"{identification}", 234, 60, 9),
                    FileHelperService.GetValuePositions($"{signatureCity}, {signatureState}, {signatureCountry} - {signaturePostalCode}", 243, 60, 6),
                    FileHelperService.GetValuePositions($"{signatureGenerationDate.ToString("yyyy-MM-ddTHH:mm:ss.fffK")}", 294, 130, 9),
                    FileHelperService.GetValuePositions($"{cellphone}", 270, 130, 9),
                    FileHelperService.GetValuePositions($"{email}", 282, 130, 9),
                    FileHelperService.GetValuePositions($"{signatureIp}", 334, 130, 9),
                    FileHelperService.GetValuePositions($"{signatureLocationLongitude}", 307, 130, 9),
                    FileHelperService.GetValuePositions($"{signatureLocationLatitude}", 321, 130, 9),
                    FileHelperService.GetValuePositions($"{originalDocumentHash}", 665, 84, 8),
                    FileHelperService.GetValuePositions($"{originalDocumentSignedHash}", 691, 84, 8),
                },
            });

            clearSignatureAndImageProps();

            return document;
        }

        private byte[] createDigitalSignature<T>(IEnumerable<T> paginas) where T : IEnumerable<KeyValuePair<string, TextInfoDto>>
        {
            var paginasList = paginas.ToList();
            var path = "Signature.Domain/Resources/DigitalSignature";
            var parentDir = Directory.GetParent(Environment.CurrentDirectory).FullName;
            var filePath = $"{parentDir}/{path}";
            var dir = parentDir.Length > 1 ? filePath : path;

            var pagina1 = FileHelperService.CreateDocumentPage(Path.Combine(dir, "1.jpg"), paginasList[0], true, true, _base64SignatureArray, _base64SignatureWidth, _signaturePositionX, _signaturePositionY, _base64UserPictureArray, _base64UserPictureWidth, _userPositionX, _userPositionY);

            return pagina1;
        }
        private void clearSignatureAndImageProps()
        {
            _base64SignatureArray = null;
            _base64UserPictureArray = null;
        }
        private string decimalToWords(decimal number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + decimalToWords(Math.Abs(number));

            string words = "";

            int intPortion = (int)number;
            decimal fraction = (number - intPortion) * 100;
            int decPortion = (int)fraction;

            words = numberToWords(intPortion) + " reais";
            if (decPortion > 0)
            {
                words += " e " + numberToWords(decPortion) + " centavos";
            }
            return words;
        }
        public string numberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + numberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += numberToWords(number / 1000000) + " milhão";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += numberToWords(number / 1000) + " mil ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += numberToWords(number / 100) + "centos";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " e ";

                var unitsMap = new[] { "zero", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "deessete", "dezoito", "dezenove" };
                var tensMap = new[] { "zero", "dez", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " e " + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
