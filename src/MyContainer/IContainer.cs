using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyContainer
{
    public interface IContainer
    {
         void Register<TFrom, TTo>(string shortName = null, object[] parameters = null, LifeTime lifeTime = LifeTime.Transient) where TTo : TFrom;
         TFrom Resolve<TFrom>(string shortName = null);
         IContainer CreateChildContainer();
    }
}
