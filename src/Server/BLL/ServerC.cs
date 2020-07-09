using MyContainer;
using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.BLL
{

    public class ServerC : IServer
    {
        private IRepositoryA repositoryA;
        private int x = 0;
        private string s = null;
        [Constructor]
        public ServerC(IRepositoryA repositoryA, [Parameter] int x, [Parameter] string s)
        {
            this.repositoryA = repositoryA;
            this.x = x;
            this.s = s;
        }
        public void GetTest()
        {
            Console.WriteLine("ServerA" + repositoryA.GetData() + "int 类型的值是：" + x+"string 是："+s);
        }
    }

}
