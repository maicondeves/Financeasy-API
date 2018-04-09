using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Financeasy.Api.Applications;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Enums;

namespace Financeasy.Api.Authentication
{
    public class AuthenticationProvider
    {
        [Inject]
        private UserApplication _userApplication { get; set; }

        public AuthenticationResult Authenticate(HttpRequestMessage request)
        {
            var authHeaderVal = request.Headers.Authorization;
            if (authHeaderVal != null)
                if (authHeaderVal.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                    return AuthenticateUser(authHeaderVal.Parameter);

            // Se não possui o cabeçalho de authorização retorna erro 401.
            return new AuthenticationResult()
            {
                IsAuthenticated = false,
                UserId = 0,
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Não autorizado."
            };
        }

        private AuthenticationResult AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string email = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                var user = _userApplication.FindByEmail(email);

                if (user == null)
                    return new AuthenticationResult()
                    {
                        IsAuthenticated = false,
                        UserId = 0,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Usuário e/ou senha inválidos."
                    };

                if (user.Status == UserStatus.Blocked)
                    return new AuthenticationResult()
                    {
                        IsAuthenticated = false,
                        UserId = 0,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Seu usuário foi bloqueado por motivos de segurança. Recupere a senha para desbloquear."
                    };

                else if (user.Status == UserStatus.Inactive)
                    return new AuthenticationResult()
                    {
                        IsAuthenticated = false,
                        UserId = 0,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Usuário inativo."
                    };

                else if (user.Status == UserStatus.NotConfirmed)
                    return new AuthenticationResult()
                    {
                        IsAuthenticated = false,
                        UserId = 0,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Confirmação de cadastro pendente. Verifique seu e-mail e finalize seu cadastro."
                    };

                //Valida se a senha da autenticação é compatível com a senha do usuário.
                if (!Cryptography.Check(password, user.Password))
                    return new AuthenticationResult()
                    {
                        IsAuthenticated = false,
                        UserId = 0,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Usuário e/ou senha inválidos."
                    };


                return new AuthenticationResult()
                {
                    IsAuthenticated = true,
                    UserId = user.Id,
                    StatusCode = HttpStatusCode.OK,
                    Message = ""
                };

            }
            catch (FormatException e)
            {
                return new AuthenticationResult()
                {
                    IsAuthenticated = false,
                    UserId = 0,
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = e.Message
                };
            }
        }
    }
}