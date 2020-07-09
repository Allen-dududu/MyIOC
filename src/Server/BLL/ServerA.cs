using MyContainer;
using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.BLL
{
    public class ServerA : IServer
    {
        private IRepositoryA repositoryA;
        [Constructor]
        public ServerA(IRepositoryA repositoryA)
        {
            this.repositoryA = repositoryA;
        }
        public void GetTest()
        {
           Console.WriteLine("ServerA"+ repositoryA.GetData());
        }
    }
}
