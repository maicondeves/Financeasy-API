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
    public class CustomerApplication
    {
        [Inject]
        private CustomerRepository _repository { get; set; }

        public OperationResult Insert(CustomerPostModel customerModel)
        {
            var customer = customerModel.ToEntity();
            return Insert(customer);
        }

        public OperationResult Insert(Customer customer)
        {
            try
            {
                customer.RegisterDate = DateTime.Now;
                _repository.Insert(customer);
                _repository.Save();
                return new OperationResult(true, "Cliente inserido com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Update(CustomerPutModel customerModel)
        {
            var currentCustomer = FindById(customerModel.Id);

            //Validações
            if (currentCustomer == null)
                return new OperationResult(false, "Cliente não encontrado.");

            var customer = customerModel.ToEntity(currentCustomer);
            return Update(customer);
        }

        public OperationResult Update(Customer customer)
        {
            try
            {
                customer.UpdateDate = DateTime.Now;
                _repository.Update(customer);
                _repository.Save();
                return new OperationResult(true, "Usuário atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Delete(Customer customer)
        {
            try
            {
                _repository.Delete(customer);
                _repository.Save();
                return new OperationResult(true, "Cliente excluído com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public Customer FindById(long id) => _repository.FindById(id);

        public IEnumerable<Customer> GetAll() => _repository.GetAll().ToList();
    }
}