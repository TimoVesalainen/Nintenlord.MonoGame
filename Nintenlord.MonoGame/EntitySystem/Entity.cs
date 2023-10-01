using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.EntitySystem
{
    public sealed class Entity
    {
        readonly Dictionary<Type, int> components = new();

        public Guid Id { get; }
        public EntitySystem System { get; }

        public Entity(EntitySystem system) : this(system, new Guid())
        {
        }

        public Entity(EntitySystem system, Guid id)
        {
            this.System = system ?? throw new ArgumentNullException(nameof(system));
            Id = id;
        }

        public ref TComponent CreateComponent<TComponent>()
            where TComponent : struct
        {
            var provider = System.GetComponentProvider<TComponent>();
            var index = provider.Create();
            components[typeof(TComponent)] = index;
            return ref provider.Get(index);
        }

        public bool TryGetIndex<TComponent>(out int index)
            where TComponent : struct
        {
            return components.TryGetValue(typeof(TComponent), out index);
        }

        public ref TComponent GetComponent<TComponent>()
            where TComponent : struct
        {
            var provider = System.GetComponentProvider<TComponent>();
            if (components.TryGetValue(typeof(TComponent), out var index))
            {
                return ref provider.Get(index);
            }
            else
            {
                throw new ArgumentException("This entity has no component of this type", nameof(TComponent));
            }
        }

        public void DecrementifLarger(Type componentType, int index)
        {
            if (components.TryGetValue(componentType, out var oldIndex) && oldIndex > index)
            {
                components[componentType] = oldIndex - 1;
            }
        }

        public IEnumerable<(Type type, int index)> GetEntries() => components.Select(kv => (kv.Key, kv.Value));
    }
}
