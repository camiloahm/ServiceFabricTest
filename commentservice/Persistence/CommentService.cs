using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commentservice.Model;
using commonpersistence;


namespace commentservice.Persistence
{
    class CommentService:ICommentService
    {
        private readonly Lazy<SqlDataAccess> _sqlDataAccess;

        public SqlDataAccess SqlDataAccessvalue => _sqlDataAccess.Value;

        public CommentService()
        {
            _sqlDataAccess = new Lazy<SqlDataAccess>();
        }

        public bool SaveComment(Comment comment)
        {
            const string sql = " INSERT INTO [dbo].[comment] " +
                               "            ([id] " +
                               "            ,[message]" +
                               "            ,[username]) "+
                               "      VALUES " +
                               "            (@id " +
                               "            ,@message"+
                               "            ,@username) ";

            SqlDataAccessvalue.Execute(sql, new
            {
                id = comment.Id,
                message=comment.Message,
                username = comment.UserName
            });
            return true;
        }
    }


}
