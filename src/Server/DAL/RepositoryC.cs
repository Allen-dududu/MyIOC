using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAL
{
    public class RepositoryC : IRepositoryC
    {
        public string GetData()
        {
            return "这是来自数据库的文件";
        }
    }
}
