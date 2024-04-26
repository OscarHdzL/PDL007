using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilidades.CifradoMd5
{
    public class CifradoMd5
    {

        private string clave = "M(7==c&;V]xn#Q$mexV5Cy*t(h;X/B";

        public string cifrar(string cadena)
        {
            byte[] llave;
            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor();
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
            tripledes.Clear();

            string cadenaMd5 = Convert.ToBase64String(resultado, 0, resultado.Length);

            string encodeBase64 = Base64UrlEncoder.Encode(cadenaMd5);


            return encodeBase64;
        }
        public string descifrar(string cadena)
        {
            string dencodeBase64 = Base64UrlEncoder.Decode(cadena);

            byte[] llave;
            byte[] arreglo = Convert.FromBase64String(dencodeBase64);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateDecryptor();
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
            tripledes.Clear();
            string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado);
            return cadena_descifrada;
        }

    }
}
