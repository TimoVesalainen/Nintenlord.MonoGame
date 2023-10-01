using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintenlord.MonoGame.EntitySystem
{
    public delegate void ComponentCallback<TComponent>(ref TComponent component)
            where TComponent : struct;
}
