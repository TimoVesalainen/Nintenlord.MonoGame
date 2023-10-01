using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.MonoGame.EntitySystem
{
    public delegate void ComponentCallbackEntity<TComponent>(Entity entity, ref TComponent component)
            where TComponent : struct;

    public delegate void ComponentCallbackEntity<TComponent1, TComponent2>(Entity entity, ref TComponent1 component1, ref TComponent2 component2)
            where TComponent1 : struct
            where TComponent2 : struct;

    public delegate void ComponentCallbackEntity<TComponent1, TComponent2, TComponent3>(Entity entity, ref TComponent1 component1, ref TComponent2 component2, ref TComponent3 component3)
            where TComponent1 : struct
            where TComponent2 : struct
            where TComponent3 : struct;
}
