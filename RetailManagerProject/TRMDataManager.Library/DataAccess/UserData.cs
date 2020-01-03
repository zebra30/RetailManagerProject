using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            var sqlDataAccess = new SQLDataAccess();

            //anonymous object
            var p = new { Id = Id };
            //dynamic type will work in assembly
            var list = sqlDataAccess.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "MyConnectionString");

            return list;
        }
    }
}
