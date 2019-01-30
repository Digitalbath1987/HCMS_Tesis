using Healthcare_MS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Healthcare_MS
{
    public static class HelperHCMS
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HashingIterationsCount = 10101;

        public static bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }

        public static string CalcularDV(string rut)
        {
            int suma = 0;
            for (int x = rut.Length - 1; x >= 0; x--)
                suma += int.Parse(char.IsDigit(rut[x]) ? rut[x].ToString() : "0") * (((rut.Length - (x + 1)) % 6) + 2);
            int numericDigito = (11 - suma % 11);
            string digito = numericDigito == 11 ? "0" : numericDigito == 10 ? "K" : numericDigito.ToString();
            return digito;
        }


        public static byte[] CalcularHash(string password, byte[] sal, int iteraciones = HashingIterationsCount, int hashByteSize = HashByteSize)
        {
            using (Rfc2898DeriveBytes generadorHash = new Rfc2898DeriveBytes(password, sal))
            {
                generadorHash.IterationCount = iteraciones;
                return generadorHash.GetBytes(hashByteSize);
            }
        }

        public static byte[] GenerarSal(int saltByteSize = SaltByteSize)
        {
            using (RNGCryptoServiceProvider GeneradorSal = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltByteSize];
                GeneradorSal.GetBytes(salt);
                return salt;
            }
        }

        public static bool ComprobarPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            byte[] Hash = CalcularHash(password, passwordSalt);
            return ComprobarHashesIguales(Hash, passwordHash);
        }

        private static bool ComprobarHashesIguales(byte[] primerHash, byte[] segundoHash)
        {
            int longMinimaHash = primerHash.Length <= segundoHash.Length ? primerHash.Length : segundoHash.Length;
            var xor = primerHash.Length ^ segundoHash.Length;
            for (int i = 0; i < longMinimaHash; i++)
                xor |= primerHash[i] ^ segundoHash[i];
            return 0 == xor;
        }
    }
}