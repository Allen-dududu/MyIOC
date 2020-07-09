using System;
using System.Collections.Generic;
using System.Text;

namespace MyContainer
{
    public class ContainerRegistModel
    {
        public Type type { get; set; }
        public LifeTime lifeTime { get; set; }
    }
    public enum LifeTime
    {
        Transient,
        Singleton,
        Scope
    }
}
