using System;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;

namespace Financeasy.Api.Applications
{
    public class UserApplication
    {
        [Inject]
        private UserRepository _repository { get; set; }

        public OperationResult Insert(UserPostModel userModel)
        {
            if (_repository.GetAll().Where(x => x.Email.Trim() == userModel.Email).ToList().Count > 0)
                return new OperationResult(false, "Este email já está sendo utilizado por outro usuário.");

            var user = userModel.ToEntity();
            return Insert(user);
        }

        public OperationResult Insert(User user)
        {
            try
            {
                _repository.Insert(user);
                _repository.Save();
                return new OperationResult(true, "Usuário adicionado com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(true, e.Message);
            }
        }

        public OperationResult Update(UserPutModel userModel)
        {
            var currentUser = FindById(userModel.Id);

            if (currentUser == null)
                return new OperationResult(false, "Usuário não encontrado.");

            if (_repository.GetAll().Where(x => x.Email.Trim() == userModel.Email).ToList().Count > 0)
                return new OperationResult(false, "Este email já está sendo utilizado por outro usuário.");
            
            var user = userModel.ToEntity(currentUser);
            return Update(user);
        }

        public OperationResult Update(User user)
        {
            try
            {
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
    }
}