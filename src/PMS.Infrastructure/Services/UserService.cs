﻿using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Extensions;
using PMS.Infrastructure.Interfaces;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;



        public UserService(IUserRepository userRepository,IUserProfileRepository userProfileRepository)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
        }



        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetOrFailAsync(id);

            return new UserDto
            {
                UserId = user.Id,
                UserProfileId = user.UserProfileId,
                Role = user.Role,
                Email = user.Email
            };
        }

        public async Task RegisterAsync(string email, string password, string firstName, string lastName, string phoneNumber)
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
                throw new EmailAlreadyExistException($"User with email '{email}' already exist");

            ValidatePassword(password);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var userProfileId = Guid.NewGuid();
            var addressId = Guid.NewGuid();

            user = new Core.Entities.User(Guid.NewGuid(), userProfileId, email, passwordSalt, passwordHash);
            var userProfile = new Core.Entities.UserProfile(userProfileId, firstName, lastName, phoneNumber, addressId);

            await _userRepository.CreateAsync(user);
            await _userProfileRepository.CreateAsync(userProfile);
        }

        public Task LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be empty");

            if (password.Length < 8 || password.Length > 100)
                throw new ArgumentException("The password should be between 8 and 100 characters long");

            Regex regex = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\\s).{8,100}$");

            if (!regex.IsMatch(password))
                throw new ArgumentException("The password should contain at least 1 lowercase letter, 1 uppercase letter, 1 number, 1 special character. It should be 8-100 characters long.");
        }
    }
}