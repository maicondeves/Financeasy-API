using System;
using System.Collections.Generic;
using Financeasy.Api.Authentication;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Enums;
using Financeasy.Api.Domain.Models;

namespace Financeasy.Api.Utils.Extensions
{
    public static class AdapterExtension
    {
        /// <summary>
        /// UserRegisterModel to User
        /// </summary>
        public static User ToEntity(this UserRegisterModel model)
        {
            return new User()
            {
                Id = 0,
                Name = model.Name.Trim(),
                Email = model.Email.Trim(),
                Password = Cryptography.BlowfishHash(model.Password.Trim()),
                Attempts = 0,
                Status = UserStatus.Active,
                RegisterDate = null,
                UpdateDate = null
            };
        }

        /// <summary>
        /// UserEditProfileModel to User 
        /// </summary>
        public static User ToEntity(this UserEditProfileModel model, User currentUser)
        {
            currentUser.Name = model.Name.Trim() == currentUser.Name.Trim() ? currentUser.Name.Trim() : model.Name.Trim();
            currentUser.Email = model.Email.Trim() == currentUser.Email.Trim() ? currentUser.Email.Trim() : model.Email.Trim();
            currentUser.Password = string.IsNullOrWhiteSpace(model.Password.Trim()) ? currentUser.Password.Trim() : Cryptography.BlowfishHash(model.Password.Trim());
            currentUser.Attempts = 0;
            currentUser.Status = UserStatus.Active;
            return currentUser;
        }

        /// <summary>
        /// User to UserViewProfileModel
        /// </summary>
        public static UserViewProfileModel ToModel(this User user)
        {
            return new UserViewProfileModel()
            {
                Email = user.Email,
                Name = user.Name,
                RegisterDate = user.RegisterDate,
                UpdateDate = user.UpdateDate.GetValueOrDefault()
            };
        }

        /// <summary>
        /// CategoryPostModel to Category
        /// </summary>
        public static Category ToEntity(this CategoryPostModel model)
        {
            return new Category()
            {
                Id = 0,
                Name = model.Name,
                Type = model.Type,
                UserId = model.UserId,
                RegisterDate = null,
                UpdateDate = null
            };
        }

        /// <summary>
        /// CategoryPutModel to Category
        /// </summary>
        public static Category ToEntity(this CategoryPutModel model, Category currentCategory)
        {
            currentCategory.Name = model.Name;
            currentCategory.Type = model.Type;
            return currentCategory;
        }

        /// <summary>
        /// CustomerPostModel to Customer
        /// </summary>
        public static Customer ToEntity(this CustomerPostModel model)
        {
            return new Customer()
            {
                Id = 0,
                Name = model.Name,
                RG = model.RG,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                Email = model.Email,
                HomePhone = model.HomePhone,
                CommercialPhone = model.CommercialPhone,
                CellPhone = model.CellPhone,
                CEP = model.CEP,
                StreetAddress = model.StreetAddress,
                Complement = model.Complement,
                District = model.District,
                City = model.City,
                State = model.State,
                RegisterDate = null,
                UpdateDate = null,
                UserId = model.UserId
            };
        }

        /// <summary>
        /// CustomerPutModel to Customer
        /// </summary>
        public static Customer ToEntity(this CustomerPutModel model, Customer currentCustomer)
        {
            currentCustomer.Name = model.Name;
            currentCustomer.RG = model.RG;
            currentCustomer.CPF = model.CPF;
            currentCustomer.CNPJ = model.CNPJ;
            currentCustomer.Email = model.Email;
            currentCustomer.HomePhone = model.HomePhone;
            currentCustomer.CommercialPhone = model.CommercialPhone;
            currentCustomer.CellPhone = model.CellPhone;
            currentCustomer.CEP = model.CEP;
            currentCustomer.StreetAddress = model.StreetAddress;
            currentCustomer.Complement = model.Complement;
            currentCustomer.District = model.District;
            currentCustomer.City = model.City;
            currentCustomer.State = model.State;
            return currentCustomer;
        }

        /// <summary>
        /// ProjectPostModel to Project
        /// </summary>
        public static Project ToEntity(this ProjectPostModel model)
        {
            return new Project()
            {
                Id = 0,
                Name = model.Name,
                Description = model.Description,
                Status = model.Status,
                StartDate = model.StartDate,
                ConclusionDate = model.ConclusionDate,
                CEP = model.CEP,
                StreetAddress = model.StreetAddress,
                Complement = model.Complement,
                District = model.District,
                City = model.City,
                State = model.State,
                CategoryId = model.CategoryId,
                CustomerId = model.CustomerId,
                Expenses = new List<Expense>(),
                Revenues = new List<Revenue>(),
                RegisterDate = DateTime.Now,
                UpdateDate = null,
                UserId = model.UserId
            };
        }

        /// <summary>
        /// ProjectPutModel to Project
        /// </summary>
        public static Project ToEntity(this ProjectPutModel model, Project currentProject)
        {
            currentProject.Name = model.Name;
            currentProject.Description = model.Description;
            currentProject.Status = model.Status;
            currentProject.StartDate = model.StartDate;
            currentProject.ConclusionDate = model.ConclusionDate;
            currentProject.CEP = model.CEP;
            currentProject.StreetAddress = model.StreetAddress;
            currentProject.Complement = model.Complement;
            currentProject.District = model.District;
            currentProject.City = model.City;
            currentProject.State = model.State;
            currentProject.CategoryId = model.CategoryId;
            currentProject.CustomerId = model.CustomerId;
            return currentProject;
        }

        /// <summary>
        /// RevenuePostModel to Revenue
        /// </summary>
        public static Revenue ToEntity(this RevenuePostModel model)
        {
            return new Revenue()
            {
                Id = 0,
                Description = model.Description,
                Status = model.Status,
                ReceivableAmount = model.ReceivableAmount,
                ReceivedAmount = model.ReceivedAmount,
                ReceivedDate = model.ReceivedDate,
                MonthPeriod = model.MonthPeriod,
                YearPeriod = model.YearPeriod,
                CategoryId = model.CategoryId,
                ProjectId = model.ProjectId,
                RegisterDate = DateTime.Now,
                UpdateDate = null,
                UserId = model.UserId
            };
        }

        /// <summary>
        /// RevenuePutModel to Revenue
        /// </summary>
        public static Revenue ToEntity(this RevenuePutModel model, Revenue currentRevenue)
        {
            currentRevenue.Description = model.Description;
            currentRevenue.Status = model.Status;
            currentRevenue.ReceivableAmount = model.ReceivableAmount;
            currentRevenue.ReceivedAmount = model.ReceivedAmount;
            currentRevenue.ReceivedDate = model.ReceivedDate;
            currentRevenue.MonthPeriod = model.MonthPeriod;
            currentRevenue.YearPeriod = model.YearPeriod;
            currentRevenue.CategoryId = model.CategoryId;
            return currentRevenue;
        }

        /// <summary>
        /// ExpensePostModel to Expense
        /// </summary>
        public static Expense ToEntity(this ExpensePostModel model)
        {
            return new Expense()
            {
                Id = 0,
                Description = model.Description,
                Status = model.Status,
                Amount = model.Amount,
                ExpirationDate = model.ExpirationDate,
                PaymentAmount = model.PaymentAmount,
                PaymentDate = model.PaymentDate,
                MonthPeriod = model.MonthPeriod,
                YearPeriod = model.YearPeriod,
                CategoryId = model.CategoryId,
                ProjectId = model.ProjectId,
                RegisterDate = DateTime.Now,
                UpdateDate = null,
                UserId = model.UserId
            };
        }

        /// <summary>
        /// ExpensePutModel to Expense
        /// </summary>
        public static Expense ToEntity(this ExpensePutModel model, Expense currentRevenue)
        {
            currentRevenue.Description = model.Description;
            currentRevenue.Status = model.Status;
            currentRevenue.Amount = model.Amount;
            currentRevenue.ExpirationDate = model.ExpirationDate;
            currentRevenue.PaymentAmount = model.PaymentAmount;
            currentRevenue.PaymentDate = model.PaymentDate;
            currentRevenue.MonthPeriod = model.MonthPeriod;
            currentRevenue.YearPeriod = model.YearPeriod;
            currentRevenue.CategoryId = model.CategoryId;
            return currentRevenue;
        }
    }
}