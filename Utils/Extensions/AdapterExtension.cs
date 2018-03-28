using System;
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
        /// UserPutModel to User
        /// </summary>
        public static User ToEntity(this UserPutModel model, User currentUser)
        {
            return new User()
            {
                Id = currentUser.Id,
                Name = model.Name ?? currentUser.Name,
                Email = model.Email ?? currentUser.Email,
                Password = Cryptography.BlowfishHash(model.Password) ?? currentUser.Password,
                Attempts = 0,
                Status = UserStatus.Active,
                RegisterDate = currentUser.RegisterDate,
                UpdateDate = null
            };
        }

        /// <summary>
        /// User to UserProfileModel
        /// </summary>
        public static UserProfileModel ToModel(this User user)
        {
            return new UserProfileModel()
            {
                Email = user.Email,
                Name = user.Name,
                RegisterDate = user.RegisterDate,
                UpdateDate = user.UpdateDate
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
        /// CategoryPostModel to Category
        /// </summary>
        public static Category ToEntity(this CategoryPutModel model, Category currentCategory)
        {
            return new Category()
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                UserId = currentCategory.UserId,
                RegisterDate = currentCategory.RegisterDate,
                UpdateDate = null
            };
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
                RegisterDate = currentCustomer.RegisterDate,
                UpdateDate = null,
            };
        }
    }
}

}