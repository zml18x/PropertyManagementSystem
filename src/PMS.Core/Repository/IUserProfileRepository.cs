﻿using PMS.Core.Entities;

namespace PMS.Core.Repository
{
    public interface IUserProfileRepository
    {
        public Task<UserProfile> GetAsync(Guid id);
        public Task CreateAsync(UserProfile userProfile);
        public Task UpdateAsync(UserProfile userProfile);
        public Task DeleteAsync(UserProfile userProfile);
    }
}