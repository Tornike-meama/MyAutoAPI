using MyAutoAPI1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAutoAPI1.Services
{
    public interface IStatementServices
    {
        public Statement GetStatementById(int id);
        public List<Statement> GetAllStatements();
        public Statement AddStatement(Statement data);
    }
}
