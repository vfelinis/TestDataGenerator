using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Data;

namespace TestDataGenerator.BL.Test
{
    public class RepositoryMock : IRepository
    {
        public string GetRandomEmailDomain()
        {
            return "test.ru";
        }

        public string GetRandomName()
        {
            return "Иван";
        }

        public string GetRandomPatronymic()
        {
            return "Иванович";
        }

        public string GetRandomSurname()
        {
            return "Иванов";
        }

        public string GetRandomUniqLogin()
        {
            return "ivan";
        }

        public void Init()
        {
            
        }
    }
}
