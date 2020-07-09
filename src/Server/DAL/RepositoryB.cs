using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAL
{
    public class RepositoryB : IRepositoryA
    {
        public string GetData()
        {
            return "来自mysql数据库";
        }
    }
}
