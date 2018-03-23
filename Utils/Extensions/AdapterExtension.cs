using System;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core.DI;
using Financeasy.Api.Domain.Entities;
using Financeasy.Api.Domain.Enums;
using Financeasy.Api.Domain.Models;

namespace Financeasy.Api.Utils.Extensions
{
    public static class AdapterExtension
    {
        /// <summary>
        /// UserPostModel to User
        /// </summary>
        public static User ToEntity(this UserPostModel model)
        {
            return new User()
            {
                Id = 0,
                Name = model.Name,
                Email = model.Email,
                Password = Cryptography.BlowfishHash(model.Password),
                Attempts = 0,
                Status = UserStatus.NotConfirmed,
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