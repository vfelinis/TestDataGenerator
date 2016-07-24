using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataGenerator.Data;

namespace TestDataGenerator.BL
{
    public interface IScriptGenerator
    {
        string CreateScript(int entityCount);
        UserEntity GenerateUser();
        string GetValueLine(UserEntity entity);
        string GetInsertLine();
    }
}
