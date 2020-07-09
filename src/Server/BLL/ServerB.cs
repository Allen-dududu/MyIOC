using MyContainer;
using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.BLL
{
    public class ServerB : IServer
    {
        [Proporty]
        private IRepositoryA repositoryA { get; set; }

        public void GetTest()
        {
            Console.WriteLine("serverB"+ repositoryA.GetData());
        }
    }
}
