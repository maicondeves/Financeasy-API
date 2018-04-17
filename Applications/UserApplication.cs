using System;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;
using Financeasy.Api.Utils.Validations;

namespace Financeasy.Api.Applications
{
    public class UserApplication
    {
        [Inject]
        private UserRepository _repository { get; set; }

        public OperationResult Insert(User user)
        {
            try
            {
                user.RegisterDate = DateTime.Now;
                _repository.Insert(user);
                _repository.Save();
                return new OperationResult(true, "Usuário inserido com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }
        
        public OperationResult Update(User user)
        {
            try
            {
                user.UpdateDate = DateTime.Now;
                _repository.Update(user);
                _repository.Save();
                return new OperationResult(true, "Usuário atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Delete(User user)
        {
            try
            {
                _repository.Delete(user);
                _repository.Save();
                return new OperationResult(true, "Usuário excluído com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public User FindById(long id) => _repository.FindById(id);

        public User FindByEmail(string email) => _repository.GetAll().Where(x => x.Email.Trim() == email.Trim()).ToList().FirstOrDefault();

        public IEnumerable<User> GetAll() => _repository.GetAll().ToList();

        public OperationResult EditProfile(UserEditProfileModel userModel)
        {
            var currentUser = FindById(userModel.Id);

            if (currentUser == null)
                return new OperationResult(false, "Usuário não encontrado.");

            if (string.IsNullOrWhiteSpace(userModel.Email) || !Validation.CheckEmail(userModel.Email))
                return new OperationResult(false, "Email inválido.");

            if (string.IsNullOrWhiteSpace(userModel.Name))
                return new OperationResult(false, "Nome inválido.");

            if (userModel.Name.Length < 2 || userModel.Name.Length > 30)
                return new OperationResult(false, "Nome deve conter no mínimo 2 caracteres e no máximo 30.");

            if (string.IsNullOrWhiteSpace(userModel.Password) || string.IsNullOrWhiteSpace(userModel.PasswordConfirm))
                return new OperationResult(false, "Confirme a senha para continuar.");

            if (!string.IsNullOrWhiteSpace(userModel.Password) && !string.IsNullOrWhiteSpace(userModel.PasswordConfirm))
                if (userModel.Password != userModel.PasswordConfirm)
                    return new OperationResult(false, "As senhas informadas não são iguais.");

            if (userModel.Email != currentUser.Email)
                if (_repository.GetAll().Where(x => x.Email.Trim() == userModel.Email).ToList().Count > 0)
                    return new OperationResult(false, "Este email já está sendo utilizado por outro usuário.");

            var user = userModel.ToEntity(currentUser);
            return Update(user);
        }

        public OperationResult Register(UserRegisterModel userModel)
        {
            if (string.IsNullOrWhiteSpace(userModel.Email) || !Validation.CheckEmail(userModel.Email))
                return new OperationResult(false, "Email inválido.");

            if (string.IsNullOrWhiteSpace(userModel.Name))
                return new OperationResult(false, "Nome inválido.");

            if (userModel.Name.Length < 2 || userModel.Name.Length > 30)
                return new OperationResult(false, "Nome deve conter no mínimo 2 caracteres e no máximo 30.");

            if (string.IsNullOrWhiteSpace(userModel.Password) || string.IsNullOrWhiteSpace(userModel.PasswordConfirm))
                return new OperationResult(false, "Confirme a senha para continuar.");

            if (userModel.Password != userModel.PasswordConfirm)
                return new OperationResult(false, "As senhas informadas não são iguais.");

            if (_repository.GetAll().Where(x => x.Email.Trim() == userModel.Email).ToList().Count > 0)
                return new OperationResult(false, "Este email já está sendo utilizado por outro usuário.");



            var user = userModel.ToEntity();
            return Insert(user);
        }

        public OperationResult Authenticate(UserAuthenticateModel userModel)
        {
            var operationResult = new OperationResult()
            {
                Success = true,
                Message = "Usuário autenticado com sucesso."
            };

            if (string.IsNullOrWhiteSpace(userModel.Email) || !Validation.CheckEmail(userModel.Email))
            {
                operationResult.Success = false;
                operationResult.Message = "Email inválido.";
            }

            if (string.IsNullOrWhiteSpace(userModel.Password))
            {
                operationResult.Success = false;
                operationResult.Message = "Preencha a senha para continuar.";
            }
            
            var user = FindByEmail(userModel.Email);

            if (user == null)
            {
                operationResult.Success = false;
                operationResult.Message = "Usuário e/ou senha inválidos";

                return operationResult;
            }

            if (!Cryptography.Check(userModel.Password, user.Password))
            {
                operationResult.Success = false;
                operationResult.Message = "Usuário e/ou senha inválidos.";
            }

            //if (!operationResult.Success)
            //{
            //    if (user.Attempts < 5)
            //        AddAttempts(user);
            //    else
            //        BlockUser(user);
            //}
            
            return operationResult;
        }
        

        //private void BlockUser(User user)
        //{
        //    user.Status = UserStatus.Blocked;
        //    Update(user);
        //}

        //private void AddAttempts(User user)
        //{
        //    user.Attempts = (short) (user.Attempts + 1);
        //    Update(user);
        //}
    }
}