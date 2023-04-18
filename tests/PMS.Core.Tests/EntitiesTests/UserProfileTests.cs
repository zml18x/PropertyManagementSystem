using PMS.Core.Entities;
using PMS.Core.Exceptions;

namespace PMS.Core.Tests.Entities
{
    public class UserProfileTests
    {
        [Fact]
        public void UserProfileConstructorSetsPropertiesCorrectly()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            var userProfile = new UserProfile(id, firstName, lastName, phoneNumber);


            Assert.Equal(id, userProfile.Id);
            Assert.Equal(firstName, userProfile.FirstName);
            Assert.Equal(lastName, userProfile.LastName);
            Assert.Equal(phoneNumber, userProfile.PhoneNumber);
            Assert.True(userProfile.IsActive);
            Assert.True(userProfile.CreatedAt > DateTime.MinValue);
            Assert.True(userProfile.LastUpdatedAt > DateTime.MinValue);
            Assert.True(userProfile.DeletedAt == null);
        }

        [Fact]
        public void UserProfileSetIdThrowsExceptionWhenIdIsInvalid()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            id = Guid.Empty;
            Assert.Throws<EmptyIdException>(() => new UserProfile(id, firstName, lastName, phoneNumber));
        }

        [Fact]
        public void UserProfileSetNamesThrowsExceptionWhenFirstNameOrLastNameIsInvalid()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            // CASE IF 'firstName' IS NULL
            firstName = null;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName,phoneNumber));

            // CASE IF 'firstName IS STRING EMPTY
            firstName = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName IS NULL
            firstName = "Test";
            lastName = null;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName IS STRING EMPTY
            lastName = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'firstName < 2
            firstName = "T";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName < 2
            firstName = "Test";
            lastName = "E";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));


            // CASE IF 'firstName > 100
            firstName = new string('a', 101);
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName > 100
            firstName = "Test";
            lastName = new string('a', 101);
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'firstName' CONTAINS SPACES
            firstName = " Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName' CONTAINS SPACES
            firstName = "Test";
            lastName = " Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'firstName' CONTAINS DIGITS
            firstName = "9Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName' CONTAINS DIGITS
            firstName = "Test";
            lastName = "1Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'firstName' CONTAINS SPECIAL CHARACTERS
            firstName = ".Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'lastName' CONTAINS SPECIAL CHARACTERS
            firstName = "Test";
            lastName = ",Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'firstName' CONTAINS SPECIAL CHARACTERS
            firstName = "#Test";
            lastName = "Example";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'firstName' CONTAINS SPECIAL CHARACTERS
            firstName = "Test";
            lastName = "Exa$mple";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));
        }

        [Fact]
        public void UserProfileSetPhoneNumberThrowsExceptionWhenPhoneNumberIsInvalid()
        {
            var id = Guid.NewGuid();
            var firstName = "Test";
            var lastName = "Example";
            var phoneNumber = "123456789";


            // CASE IF 'phoneNumber' IS NULL
            phoneNumber = null;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' IS STRING EMPTY
            phoneNumber = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' < 9
            phoneNumber = new string('1', 8);
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' > 9
            phoneNumber = new string('1', 10);
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS LETTERS
            phoneNumber = "12345678a";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS LETTERS
            phoneNumber = "P12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS WHITESPACE
            phoneNumber = " 12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS SPECIAL CHARACTERS
            phoneNumber = ".12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS SPECIAL CHARACTERS
            phoneNumber = "$12345678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS SPECIAL CHARACTERS
            phoneNumber = "123#45678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));

            // CASE IF 'phoneNumber' CONTAINS SPECIAL CHARACTERS
            phoneNumber = "123?45678";
            Assert.Throws<ArgumentException>(() => new UserProfile(id, firstName, lastName, phoneNumber));
        }
    }
}