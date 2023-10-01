using Nintenlord.Collections.Lists;
using System;
using System.Collections.Generic;

namespace Nintenlord.MonoGame.EntitySystem
{
    public sealed class ComponentProvider<TComponent> : IComponentProvider
        where TComponent : struct
    {
        readonly RefList<TComponent> components = new();
        readonly TComponent start = new();

        public Type ComponentType => typeof(TComponent);

        public int Count => components.Count;

        public int Create()
        {
            components.Add(start);
            return components.Count - 1;
        }

        public void Delete(int index)
        {
            components.RemoveAt(index);
        }

        public ref TComponent Get(int index)
        {
            return ref components.GetReference(index);
        }
    }
}
