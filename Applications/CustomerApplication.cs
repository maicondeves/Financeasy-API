using System;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;
using Financeasy.Api.Utils.Validations;

namespace Financeasy.Api.Applications
{
    public class CustomerApplication
    {
        [Inject]
        private CustomerRepository _repository { get; set; }

        [Inject]
        private ProjectApplication _projectApplication { get; set; }

        public OperationResult Insert(CustomerPostModel customerModel)
        {
            //Fazer as validações aqui.
            if (string.IsNullOrWhiteSpace(customerModel.Name))
                return new OperationResult(false, "Nome inválido.");

            if (customerModel.Name.Length < 2 || customerModel.Name.Length > 30)
                return new OperationResult(false, "Nome deve conter no mínimo 2 caracteres e no máximo 30.");
            
            if (!string.IsNullOrWhiteSpace(customerModel.Email) && !Validation.CheckEmail(customerModel.Email))
                return new OperationResult(false, "Email inválido.");
            
            var customer = customerModel.ToEntity();
            return InsertAndSave(customer);
        }

        public OperationResult InsertAndSave(Customer customer)
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

        public OperationResult Update(CustomerPutModel customerModel, long userId)
        {
            var currentCustomer = FindById(customerModel.Id, userId);
            if (currentCustomer == null)
                return new OperationResult(false, "Cliente não encontrado.");

            //Fazer as validações aqui.
            if (string.IsNullOrWhiteSpace(customerModel.Name))
                return new OperationResult(false, "Nome inválido.");

            if (customerModel.Name.Length < 2 || customerModel.Name.Length > 30)
                return new OperationResult(false, "Nome deve conter no mínimo 2 caracteres e no máximo 30.");

            if (!string.IsNullOrWhiteSpace(customerModel.Email) && !Validation.CheckEmail(customerModel.Email))
                return new OperationResult(false, "Email inválido.");
            
            var customer = customerModel.ToEntity(currentCustomer);
            return UpdateAndSave(customer);
        }

        public OperationResult UpdateAndSave(Customer customer)
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

        public OperationResult Delete(long id, long userId)
        {
            var customer = FindById(id, userId);
            if (customer == null)
                return new OperationResult(false, "Cliente não encontrado.");

            return DeleteAndSave(customer);
        }

        public OperationResult DeleteAndSave(Customer customer)
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

        public Customer FindById(long id, long userId) =>
            GetAll(userId).Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<CustomerListModel> GetList(long userId)
        {
            var customers = GetAll(userId).ToList();
            var customersModel = customers.Select(x => new CustomerListModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return customersModel;
        }

        public IQueryable<Customer> GetAll(long userId) => _repository.GetAll().Where(x => x.UserId == userId);

        public bool SearchForRegisters(long id, long userId) => 
            !_projectApplication.GetAll(userId).Any(x => x.UserId == userId && x.CustomerId == id);
    }
}