using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace MyContainer
{
    public class Container : IContainer
    {
        private Dictionary<string, ContainerRegistModel> ContainerDictionary = new Dictionary<string, ContainerRegistModel>();

        private Dictionary<string, object[]> ContainerValueDictionary = new Dictionary<string, object[]>();
        //单例集合
        private Dictionary<string, object> SingletonDictionary = new Dictionary<string, object>();
        //作用域单例集合
        private Dictionary<string, object> ScopeDictionary = new Dictionary<string, object>();

        public void Register<TFrom, TTo>(string shortName = null, object[] parameters = null,LifeTime lifeTime = LifeTime.Transient) where TTo : TFrom
        {
            this.ContainerDictionary.Add(typeof(TFrom).FullName + shortName, new ContainerRegistModel { type = typeof(TTo),lifeTime=lifeTime }) ;
            if(parameters!=null && parameters.Length != 0)
            {
                this.ContainerValueDictionary.Add(typeof(TFrom).FullName + shortName, parameters);

            }

        }
        public IContainer CreateChildContainer()
        {
            return new Container(this.ContainerDictionary,this.ContainerValueDictionary,new Dictionary<string, object>());
        }
        public  Container()
        {

        }
        private Container(Dictionary<string, ContainerRegistModel> containerDictionary, Dictionary<string, object[]> containerValueDictionary, Dictionary<string, object> scopeDictionary)
        {
            this.ContainerDictionary = containerDictionary;
            this.ContainerValueDictionary = containerValueDictionary;
            this.ScopeDictionary = scopeDictionary;
        }

        public TFrom Resolve<TFrom>(string shortName = null)
        {
            object obj = this.ResolveObject(typeof(TFrom),shortName);
            return (TFrom)obj;
        }

        private object ResolveObject(Type type, string shortName)
        {
            string key = type.FullName+shortName;
            var to = this.ContainerDictionary[key];
            switch (to.lifeTime)
            {
                case LifeTime.Transient:
                    break;
                case LifeTime.Singleton:
                    if(SingletonDictionary.TryGetValue(key,out var instance))
                    {
                        return instance;
                    }
                    break;
                case LifeTime.Scope:
                    if(ScopeDictionary.TryGetValue(key, out instance))
                    {
                        return instance;
                    }
                    break;
            }
            //构造函数
            ConstructorInfo constructorInfo = null;
            var toType = to.type;
            var constr = toType.GetConstructors().FirstOrDefault(i => i.IsDefined(typeof(ConstructorAttribute), true));
            if(constr == null)
            {
                constr = toType.GetConstructors().OrderByDescending(i => i.GetParameters().Length).First();
            }

            List<object> paraList = new List<object>();
            this.ContainerValueDictionary.TryGetValue(key,out var structPara);
            int index = 0;
            foreach(var p in constr.GetParameters())
            {
                var para = new object();
                if (p.IsDefined(typeof(ParameterAttribute)))
                {
                    para = structPara[index];
                    index++;
                }
                else
                {
                    var paraType = p.ParameterType;
                    var paraShortName = GetShortName(paraType);
                    para = this.ResolveObject(paraType, paraShortName);
                }
                paraList.Add(para);
            }

            object oinstance = Activator.CreateInstance(toType, paraList.ToArray());

            //属性注入
            const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var Prop in toType.GetProperties(InstanceBindFlags).Where(i => i.IsDefined(typeof(ProportyAttribute), true)))
            {
                var propShortName = GetShortName(Prop);
                var pinstance = ResolveObject(Prop.PropertyType, propShortName);
                Prop.SetValue(oinstance, pinstance);

            }
            switch (to.lifeTime)
            {
                case LifeTime.Transient:
                    break;
                case LifeTime.Singleton:
                    SingletonDictionary.Add(key, oinstance);
                    break;
                case LifeTime.Scope:
                    ScopeDictionary.Add(key, oinstance);
                    break;
            }


            return oinstance;
        }
        private string GetShortName(ICustomAttributeProvider type)
        {
            if (type.IsDefined(typeof(ShortNameAttribute), true))
            {
                var attr = (ShortNameAttribute)type.GetCustomAttributes(typeof(ShortNameAttribute), true)[0];
                return attr.shortName;
            }
            else
                return null;
        }
    }
}
