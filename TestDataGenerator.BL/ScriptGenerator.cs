using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Data;

namespace TestDataGenerator.BL
{
    public class ScriptGenerator : IScriptGenerator
    {
        private readonly IRepository _repository;
        private Random _random = new Random();
        public ScriptGenerator(IRepository repository)
        {
            _repository = repository;
        }

        public UserEntity GenerateUser()
        {
            UserEntity entity = new UserEntity();

            entity.Login = _repository.GetRandomUniqLogin();
            entity.Name = _repository.GetRandomName();
            entity.Surname = _repository.GetRandomSurname();
            entity.Patronymic = _repository.GetRandomPatronymic();

            string randomEmailDomain = _repository.GetRandomEmailDomain();

            entity.Email = $"{entity.Login}@{randomEmailDomain}";
            entity.Password = _random.Next(1000, 10000).ToString();

            int year = _random.Next(2010, 2017);
            int month = _random.Next(1, 13);
            int day = _random.Next(1, 29);
            if(year == 2016 && month > 2)
            {
                month = 2;
            }
            entity.RegistrationDate = new DateTime(year, month, day);

            return entity;
        }

        public string CreateScript(int entityCount)
        {
            IEnumerable<UserEntity> users = Enumerable.Repeat(GenerateUser(), entityCount);
            IEnumerable<string> valueLines = users.Select(GetValueLine);
            string insertLine = GetInsertLine();

            string result = MergeLines(valueLines, insertLine);

            return result;
        }
        internal string MergeLines(IEnumerable<string> valueLines, string insertLine)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(insertLine);

            int i = 0;
            foreach(var valueLine in valueLines)
            {
                if(i > 0)
                {
                    builder.Append(",");
                }
                builder.AppendLine(valueLine);
                i++;
            }

            return builder.ToString();
        }

        public string GetInsertLine()
        {
            return @"INSERT INTO BlogUser (Name, Surname, Patronymic, Email, UserLogin, Password, RegistrationDate)";
        }

        public string GetValueLine(UserEntity entity)
        {
            string registrationDate = entity.RegistrationDate.ToString("yyyyMMdd");
            string result = string.Format("VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                            entity.Name, entity.Surname, entity.Patronymic, entity.Email,
                            entity.Login, entity.Password, registrationDate);
            return result;
        }
    }
}
