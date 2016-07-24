using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestDataGenerator.Data;

namespace TestDataGenerator.BL.Test
{
    [TestFixture]
    public class ScriptGeneratorTest
    {
        private ScriptGenerator _generator;

        [SetUp]
        public void Init()
        {
            IRepository repository = new RepositoryMock();
            _generator = new ScriptGenerator(repository);
        }

        [Test]
        public void GenerateUser_NameRequired()
        {
            UserEntity entity = _generator.GenerateUser();
            string name = entity.Name;

            Assert.That(string.IsNullOrEmpty(name), Is.False);
        }

        [Test]
        public void GenerateUser_SurnameRequired()
        {
            UserEntity entity = _generator.GenerateUser();
            string surname = entity.Surname;

            Assert.That(string.IsNullOrEmpty(surname), Is.False);
        }

        [Test]
        public void GenerateUser_LoginRequired()
        {
            UserEntity entity = _generator.GenerateUser();
            string login = entity.Login;

            Assert.That(string.IsNullOrEmpty(login), Is.False);
        }

        [Test]
        [Repeat(10000)]
        public void GenerateUser_PasswordRequired()
        {
            UserEntity entity = _generator.GenerateUser();
            string password = entity.Password;

            Assert.That(string.IsNullOrEmpty(password), Is.False);
        }

        [Test]
        public void GenerateUser_EmailRequired()
        {
            UserEntity entity = _generator.GenerateUser();
            string email = entity.Email;

            Assert.That(string.IsNullOrEmpty(email), Is.False);
        }

        [Test]
        [Repeat(10000)]
        public void GenerateUser_RegistrationDatePeriod()
        {
            UserEntity entity = _generator.GenerateUser();
            DateTime registrationDate = entity.RegistrationDate;

            Assert.That(registrationDate, Is.InRange(new DateTime(2010, 1, 1), new DateTime(2016, 2, 29)));
        }

        [Test]
        public void GetValueLine_ReturnString()
        {
            UserEntity user = new UserEntity()
            {
                Name = "Петр",
                Surname = "Петров",
                Patronymic = "Петрович",
                Email = "petr@gmail.com",
                Login = "petr",
                Password = "12345",
                RegistrationDate = new DateTime(2016, 1, 1)
            };
            const string EXPECTED_RESULT = @"VALUES ('Петр', 'Петров', 'Петрович', 'petr@gmail.com', 'petr', '12345', '20160101')";
            string result = _generator.GetValueLine(user);

            Assert.That(result, Is.EqualTo(EXPECTED_RESULT));
        }

        [Test]
        public void GetInsertLine_ReturnConstString()
        {
            const string EXPECTED_RESULT = @"INSERT INTO BlogUser (Name, Surname, Patronymic, Email, UserLogin, Password, RegistrationDate)";
            string result = _generator.GetInsertLine();

            Assert.That(result, Is.EqualTo(EXPECTED_RESULT));
        }

        [Test]
        public void MergeLines_Test()
        {
            const string INSERT_LINE = "INSERT LINE";
            IEnumerable<string> valueLines = new string[] { "VALUE LINE 1", "VALUE LINE 2" };
            string expected_result =
                         string.Format("INSERT LINE{0}VALUE LINE 1{0},VALUE LINE 2{0}", Environment.NewLine);

            string result = _generator.MergeLines(valueLines, INSERT_LINE);

            Assert.That(result, Is.EqualTo(expected_result));
        }
    }
}