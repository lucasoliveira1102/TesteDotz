using Dotz.Core.Infraestrutura.Seguranca;
using System;

namespace TesteDotz.Core
{
    public static class CryptoExtensions
    {
        public static string Criptografar(this string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return string.Empty;

            return MD5Crypto.Criptografar(valor);
        }

        public static string Descriptografar(this string valor)
        {
            throw new NotImplementedException("CriptografiaMD5");
        }
    }
}
