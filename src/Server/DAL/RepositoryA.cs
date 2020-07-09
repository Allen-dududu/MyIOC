using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAL
{
    public class RepositoryA : IRepositoryA
    {
        public string GetData()
        {
            return "来自数据库A";
        }
    }
}
