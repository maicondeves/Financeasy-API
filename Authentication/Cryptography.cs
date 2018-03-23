using CryptSharp;

namespace Financeasy.Api.Authentication
{
    public class Cryptography
    {
        /// <summary>
        /// Método para gerar Hash da senha que será armazenado no banco de dados.
        /// </summary>
        public static string BlowfishHash(string password)
        {
            var options = new CrypterOptions();
            options.Add(CrypterOption.Rounds, 05);

            return Crypter.Blowfish.Crypt(password, options);
        }

        /// <summary>
        /// Método que compara as senhas no formato de Hash e verifica se são compatíveis.
        /// </summary>
        public static bool Check(string password, string hash)
        {
            return Crypter.CheckPassword(password, hash);
        }
    }
}