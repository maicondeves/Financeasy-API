using System;
using System.Collections.Generic;
using System.Linq;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Filters;
using Financeasy.Api.Domain.Models;
using Financeasy.Api.Persistence.Repositories;
using Financeasy.Api.Utils.Extensions;

namespace Financeasy.Api.Applications
{
    public class RevenueApplication
    {
        [Inject]
        private RevenueRepository _repository { get; set; }

        public OperationResult Insert(RevenuePostModel model)
        {
            //Validações aqui

            var revenue = model.ToEntity();
            return InsertAndSave(revenue);
        }

        public OperationResult InsertAndSave(Revenue revenue)
        {
            try
            {
                revenue.RegisterDate = DateTime.Now;
                _repository.Insert(revenue);
                _repository.Save();
                return new OperationResult(true, "Receita inserida com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult Update(RevenuePutModel model, long userId)
        {
            var currentRevenue = FindById(model.Id, userId);

            //Validações aqui

            var revenue = model.ToEntity(currentRevenue);
            return UpdateAndSave(revenue);
        }

        public OperationResult UpdateAndSave(Revenue revenue)
        {
            try
            {
                revenue.UpdateDate = DateTime.Now;
                _repository.Update(revenue);
                _repository.Save();
                return new OperationResult(true, "Receita atualizada com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public OperationResult DeleteAndSave(Revenue revenue)
        {
            try
            {
                _repository.Delete(revenue);
                _repository.Save();
                return new OperationResult(true, "Receita excluída com sucesso.");
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }

        public Revenue FindById(long id, long userId) =>
            GetAll(userId).Where(x => x.Id == id).FirstOrDefault();

        public IQueryable<Revenue> GetAll(long userId) =>
            _repository.GetAll().Where(x => x.UserId == userId);

        public List<Revenue> GetAllWithFilters(long userId, RevenueFilter filter) =>
            GetAll(userId).Where(x => x.ProjectId == filter.ProjectId && x.MonthPeriod == filter.MonthWork && x.YearPeriod == filter.YearWork).ToList();

        public List<RevenueCategoryModel> GetRevenuesPerCategory(long userId)
        {
            return _repository.GetRevenuesPerCategory(userId);
        }
    }
}