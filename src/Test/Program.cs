using MyContainer;
using Server;
using Server.BLL;
using Server.DAL;
using Server.IDAL;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //构造函数注入
            {
                //IContainer icontainer = new Container();
                //icontainer.Register<IServer, ServerA>();
                //icontainer.Register<IRepositoryA, RepositoryA>();


                //var s = icontainer.Resolve<IServer>();
                //s.GetTest();
            }
            //属性注入
            {
                //IContainer icontainer = new Container();
                //icontainer.Register<IServer, ServerB>();
                //icontainer.Register<IRepositoryA, RepositoryA>();


                //var s = icontainer.Resolve<IServer>();
                //s.GetTest();
            }
            //单接口 多实现
            {
                //IContainer icontainer = new Container();
                //icontainer.Register<IRepositoryA, RepositoryA>();
                //icontainer.Register<IServer, ServerA>();
                //icontainer.Register<IServer, ServerB>(shortName: "ServerB");

                //var sA = icontainer.Resolve<IServer>();
                //var sB = icontainer.Resolve<IServer>(shortName: "ServerB");
                //sA.GetTest();
                //sB.GetTest();
            }
            //注入参数是值类型或string
            {
                IContainer icontainer = new Container();
                icontainer.Register<IRepositoryA, RepositoryA>();
                icontainer.Register<IServer, ServerC>(parameters: new object[] { 10085,"我是小傻逼"});

                var s = icontainer.Resolve<IServer>();
                s.GetTest();

            }
            //生命周期-瞬时，单例，作用域单例
            {
                //瞬时
                IContainer icontainer0 = new Container();
                icontainer0.Register<IRepositoryA, RepositoryA>(lifeTime: LifeTime.Transient);
                var A1 = icontainer0.Resolve<IRepositoryA>();
                var A2 = icontainer0.Resolve<IRepositoryA>();
                Console.WriteLine(object.ReferenceEquals(A1, A2));
                //单例
                IContainer icontainer1 = new Container();
                icontainer1.Register<IRepositoryA, RepositoryA>(lifeTime: LifeTime.Singleton);
                var B1 = icontainer1.Resolve<IRepositoryA>();
                var B2 = icontainer1.Resolve<IRepositoryA>();
                Console.WriteLine(object.ReferenceEquals(B1, B2));


                //作用域单例
                IContainer icontainer2 = new Container();
                icontainer2.Register<IRepositoryA, RepositoryA>(lifeTime: LifeTime.Scope);
                var icontainer21 = icontainer2.CreateChildContainer();
                var A11 = icontainer21.Resolve<IRepositoryA>();
                var A12 = icontainer21.Resolve<IRepositoryA>();

                var icontainer22 = icontainer2.CreateChildContainer();
                var A21 = icontainer22.Resolve<IRepositoryA>();
                var A22 = icontainer22.Resolve<IRepositoryA>();

                Console.WriteLine(object.ReferenceEquals(A11, A12));
                Console.WriteLine(object.ReferenceEquals(A11, A21));

            }
            {
                //IContainer icontainer = new Container();
                //icontainer.Register<IRepositoryA, RepositoryA>(lifeTime: LifeTime.Singleton);
                //var A11 = icontainer.Resolve<IRepositoryA>();
                //var A12 = icontainer.Resolve<IRepositoryA>();

                //Console.WriteLine(object.ReferenceEquals(A11, A12));

            }
        }
    }
}
