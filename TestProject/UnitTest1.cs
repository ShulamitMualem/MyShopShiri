using Entity;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task LogIn_ValidCredentials_ReturnsUser()
        {
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
           var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User>() { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            var result = await userRepository.LogIn( user.Password,user.UserName);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LogIn_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User>(); // �� ������ �������
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            // Act
            var result = await userRepository.LogIn("wrongPassword", "nonExistentUser");

            // Assert
            Assert.Null(result);  // ������ �� ����, �� �������� ������ null
        }

        [Fact]
        public async Task LogIn_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User { UserName = "ShiriToker", Password = "shirit782@gmail.com" };
            var mocContext = new Mock<MyShop328306782Context>();
            var users = new List<User> { user };
            mocContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new MyRepository(mocContext.Object);

            // Act
            var result = await userRepository.LogIn("wrongPassword", user.UserName); // ����� �� �����

            // Assert
            Assert.Null(result);  // �������� ����� ������ null ���� ������ �� �����
        }



    }
}