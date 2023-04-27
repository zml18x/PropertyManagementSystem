using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Repository;

namespace PMS.Infrastructure.UnitTests.RepositoryTests
{
    public class UserProfileRepositoryTests
    {
        private UserProfileRepository _userProfileRepository;
        private PmsDbContext _context;

        Guid id = Guid.NewGuid();
        string firstName = "Test";
        string lastName = "Example";
        string phoneNumber = "012345678";


        private void SetUp()
        {
            var options = new DbContextOptionsBuilder<PmsDbContext>()
                .UseInMemoryDatabase(databaseName: "PMS_TEST_DB")
                .Options;

            _context = new PmsDbContext(options);
            _userProfileRepository = new UserProfileRepository(_context);

            _context.RemoveRange(_context.UserProfiles);
            _context.SaveChanges();
        }


        [Fact]
        public async Task CreateAsyncShouldAddUserProfile()
        {
            SetUp();
            var userProfile = new UserProfile(id,firstName,lastName,phoneNumber);
            await _userProfileRepository.CreateAsync(userProfile);

            var result = await _userProfileRepository.GetAsync(userProfile.Id);

            Assert.NotNull(result);
            Assert.Equal(userProfile, result);

            _context.Dispose();
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateUser()
        {
            SetUp();

            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);
            await _userProfileRepository.CreateAsync(userProfile);
            _context.Entry(userProfile).State = EntityState.Detached;

            var updatedUserProfile = new UserProfile(id, "UpdatedName", "UpdatedLastName", "001234567");
            await _userProfileRepository.UpdateAsync(updatedUserProfile);

            var result = await _userProfileRepository.GetAsync(userProfile.Id);

            Assert.NotNull(result);
            Assert.NotEqual(userProfile, result);
            Assert.Equal(userProfile.Id, result.Id);
            Assert.NotEqual(userProfile.LastUpdatedAt, result.LastUpdatedAt);
            Assert.True(userProfile.LastUpdatedAt < result.LastUpdatedAt);

            _context.Dispose();
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteUserAndThrowsUserNotFoundException()
        {
            SetUp();
            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);
            await _userProfileRepository.CreateAsync(userProfile);

            var usersCountBeforeDelete = _context.UserProfiles.Count();

            await _userProfileRepository.DeleteAsync(userProfile);

            var usersCountAfterDelete = _context.UserProfiles.Count();

            await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userProfileRepository.GetAsync(userProfile.Id));
            Assert.True(usersCountBeforeDelete > usersCountAfterDelete);

            _context.Dispose();
        }

        [Fact]
        public async Task GetAsyncByIdShouldReturnUserIfExist()
        {
            SetUp();
            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);
            await _userProfileRepository.CreateAsync(userProfile);

            var result = await _userProfileRepository.GetAsync(userProfile.Id);

            Assert.Equal(userProfile, result);

            _context.Dispose();
        }

        [Fact]
        public async Task GetAsyncShouldThrowUserNotFoundExceptionIfUserDoesNotExist()
        {
            SetUp();

            await Assert.ThrowsAsync<UserNotFoundException>(() => _userProfileRepository.GetAsync(Guid.NewGuid()));

            _context.Dispose();
        }
    }
}
