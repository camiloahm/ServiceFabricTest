using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commonpersistence;
using userservice.Model;

namespace userservice.Persistence
{
    class UserService : IUserService
    {
        private readonly Lazy<SqlDataAccess> _sqlDataAccess;

        public SqlDataAccess SqlDataAccessvalue => _sqlDataAccess.Value;


        public UserService()
        {
            _sqlDataAccess = new Lazy<SqlDataAccess>();
        }

        public bool CreateUser(User user)
        {
            const string sql = " INSERT INTO [dbo].[user] " +
                               "            ([id] " +
                               "            ,[username]) " +
                               "      VALUES " +
                               "            (@id " +
                               "            ,@username) ";

            SqlDataAccessvalue.Execute(sql, new
            {
                id = user.Id,
                username = user.UserName
            });
            return true;
        }
    }
}