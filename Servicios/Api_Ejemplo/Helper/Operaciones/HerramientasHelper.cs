using Modelos.Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Religiosos_Api.Helper.Operaciones
{
    /// <summary>
    /// Helper encargado de proporcionar herramientas  y rutinas auxiliares
    /// </summary>
    public static class HerramientasHelper
    {
        /// <summary>
        /// Método encargado de ejecutar  el calculo de edad actual del 
        /// usuario
        /// </summary>
        /// <param name="datosActuales"></param>
        /// <returns></returns>
        public static CURPStruct CalcularEdad(ref CURPStruct datosActuales)
        {
            if (datosActuales != null)
            {
                if(!string.IsNullOrEmpty(datosActuales.fechNac))
                {
                    var argFechas = datosActuales.fechNac.Split('/');
                    if (argFechas.Length > 2)
                    {
                        int.TryParse(argFechas[2], out int anioDefaul);
                        int.TryParse(argFechas[1], out int mesDefaul);
                        int.TryParse(argFechas[0], out int diaDefaul);
                        var fechaNacimiento = new DateTime(anioDefaul, mesDefaul, diaDefaul);

                        int edadActual = DateTime.Today.Year - fechaNacimiento.Year;

                        if (DateTime.Today < fechaNacimiento.AddYears(edadActual))
                             --edadActual;

                        datosActuales.edad = $"{edadActual}";
                    }
                }
            }

            return datosActuales;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = ")=U373}.nGWbPJVV+-wB=]mX43&rc+";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = ")=U373}.nGWbPJVV+-wB=]mX43&rc+";
            cipherText = cipherText.Replace(" ", "+");
            while (cipherText.Length % 4 != 0)
            {
                cipherText += "=";
            }
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
