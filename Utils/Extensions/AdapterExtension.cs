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
                RegisterDate = DateTime.Now,
                UpdateDate = null
            };
        }

        /// <summary>
        /// UserPostModel to User
        /// </summary>
        public static User ToEntity(this UserPostModel model)
        {
            return new User()
            {
                Id = 0,
                Name = model.Name.Trim(),
                Email = model.Email.Trim(),
                Password = Cryptography.BlowfishHash(model.Password.Trim()),
                Attempts = 0,
                Status = UserStatus.Active,
                RegisterDate = DateTime.Now,
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
                Name = model.Name?? currentUser.Name,
                Email = model.Email?? currentUser.Email,
                Password = Cryptography.BlowfishHash(model.Password)?? currentUser.Password,
                Attempts = 0,
                Status = UserStatus.Active,
                RegisterDate = currentUser.RegisterDate,
                UpdateDate = DateTime.Now
            };
        }

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
                UserId = 1,
                RegisterDate = DateTime.Now,
                UpdateDate = null
            };
        }

        /// <summary>
        /// CategoryPostModel to Category
        /// </summary>
        public static Category ToEntity(this CategoryPutModel model)
        {
            return new Category()
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                UserId = 1,
                RegisterDate = DateTime.Now,
                UpdateDate = null
            };
        }

    }
}