using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Enums;
using Financeasy.Api.Domain.Filters;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;

namespace Financeasy.Api.Applications
{
    public class ExpenseApplication
    {
        [Inject]
        private ExpenseRepository _repository { get; set; }

        public OperationResult Insert(ExpensePostModel model)
        {
            //Validações aqui

            var expense = model.ToEntity();
            return InsertAndSave(expense);
        }

        public OperationResult InsertAndSave(Expense expense)
        {
            try
            {
                expense.RegisterDate = DateTime.Now;
                _repository.Insert(expense);
                _repository.Save();
                return new OperationResult(true, "Despesa inserida com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Update(ExpensePutModel model, long userId)
        {
            var currentExpense = FindById(model.Id, userId);

            //Validações aqui

            var expense = model.ToEntity(currentExpense);
            return UpdateAndSave(expense);
        }

        public OperationResult UpdateAndSave(Expense expense)
        {
            try
            {
                expense.UpdateDate = DateTime.Now;
                _repository.Update(expense);
                _repository.Save();
                return new OperationResult(true, "Despesa atualizada com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult DeleteAndSave(Expense expense)
        {
            try
            {
                _repository.Delete(expense);
                _repository.Save();
                return new OperationResult(true, "Despesa excluída com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public Expense FindById(long id, long userId) =>
            GetAll(userId).Where(x => x.Id == id).FirstOrDefault();

        public IQueryable<Expense> GetAll(long userId) =>
            _repository.GetAll().Where(x => x.UserId == userId);

        public List<Expense> GetAllWithFilters(long userId, ExpenseFilter filter) => 
            GetAll(userId).Where(x => x.ProjectId == filter.ProjectId && x.MonthPeriod == filter.MonthWork && x.YearPeriod == filter.YearWork).ToList();

        public List<ExpenseCategoryModel> GetExpensesPerCategory(long userId)
        {
            return _repository.GetExpensesPerCategory(userId);
        }

        public List<Expense> GetExpensesCloseToExpiration(long userId)
        {
            var dataDe = DateTime.Now;
            var dataAte = DateTime.Now.AddDays(7);
            return GetAll(userId).Where(x => x.ExpirationDate >= dataDe && x.ExpirationDate <= dataAte && x.Status == ExpenseStatus.Aberto).ToList();
        }

        public decimal GetTotalExpenses(long userId)
        {
            return _repository.GetTotalExpenses(userId);
        }
    }
}