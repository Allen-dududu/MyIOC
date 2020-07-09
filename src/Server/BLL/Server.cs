using MyContainer;
using Server.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Server
{
    public class Server : IServer
    {
        private IRepositoryC repository = null;
        private int x = 0;
        [Constructor]
        public Server(IRepositoryC repository, [Parameter]int x)
        {
            this.repository = repository;
            this.x = x;
        }
        [Proporty]
        [ShortName(shortName:"MySql")]
        public IRepositoryA repositoryA {get;set;}
        public void GetTest()
        {
            var str = repository.GetData();
            var strA = repositoryA.GetData();
            Console.WriteLine(str);
            Console.WriteLine(strA);
            Console.WriteLine(this.x);


        }
    }
}
