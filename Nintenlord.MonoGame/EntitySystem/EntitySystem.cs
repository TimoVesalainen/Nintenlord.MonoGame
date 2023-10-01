using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintenlord.MonoGame.EntitySystem
{
    public sealed class EntitySystem
    {
        readonly Dictionary<Type, IComponentProvider> providers;
        readonly Dictionary<Guid, Entity> entities = new();

        public EntitySystem(IEnumerable<IComponentProvider> providers)
        {
            this.providers = providers.ToDictionary(p => p.ComponentType, p => p);
        }

        public Entity CreateEntity()
        {
            var entity = new Entity(this);
            entities[entity.Id] = entity;
            return entity;
        }

        public void DeleteEntity(Entity entity)
        {
            if (entity.System != this)
            {
                throw new ArgumentException("Entity is not of this system", nameof(entity));
            }

            if (entities.Remove(entity.Id))
            {
                foreach (var (type, index) in entity.GetEntries())
                {
                    providers[type].Delete(index);
                    foreach (var item in entities.Values)
                    {
                        if (item != entity)
                        {
                            item.DecrementifLarger(type, index);
                        }
                    }
                }
            }
        }

        private bool TryGetComponentProvider<TComponent>(out ComponentProvider<TComponent> provider)
            where TComponent : struct
        {
            if (providers.TryGetValue(typeof(TComponent), out var rawProvider) && rawProvider is ComponentProvider<TComponent> provider2)
            {
                provider = provider2;
                return true;
            }
            provider = null;
            return false;
        }

        public ComponentProvider<TComponent> GetComponentProvider<TComponent>()
            where TComponent : struct
        {
            if (TryGetComponentProvider<TComponent>(out var componentProvider))
            {
                return componentProvider;
            }
            else
            {
                throw new InvalidOperationException($"No component of type {nameof(TComponent)} registered in the system");
            }
        }

        public void GetComponents<TComponent>(ComponentCallback<TComponent> action)
            where TComponent : struct
        {
            var provider = GetComponentProvider<TComponent>();

            var count = provider.Count;

            for (int i = 0; i < count; i++)
            {
                action(ref provider.Get(i));
            }
        }

        public void GetEntities<TComponent>(ComponentCallbackEntity<TComponent> action)
            where TComponent : struct
        {
            var provider = GetComponentProvider<TComponent>();

            foreach (var entity in entities.Values)
            {
                if (entity.TryGetIndex<TComponent>(out var index))
                {
                    action(entity, ref provider.Get(index));
                }
            }
        }

        public void GetEntities<TComponent1, TComponent2>(ComponentCallbackEntity<TComponent1, TComponent2> action)
            where TComponent1 : struct
            where TComponent2 : struct
        {
            var provider1 = GetComponentProvider<TComponent1>();
            var provider2 = GetComponentProvider<TComponent2>();

            foreach (var entity in entities.Values)
            {
                if (entity.TryGetIndex<TComponent1>(out var index1) && entity.TryGetIndex<TComponent2>(out var index2))
                {
                    action(entity, ref provider1.Get(index1), ref provider2.Get(index2));
                }
            }
        }

        public void GetEntities<TComponent1, TComponent2, TComponent3>(ComponentCallbackEntity<TComponent1, TComponent2, TComponent3> action)
            where TComponent1 : struct
            where TComponent2 : struct
            where TComponent3 : struct
        {
            var provider1 = GetComponentProvider<TComponent1>();
            var provider2 = GetComponentProvider<TComponent2>();
            var provider3 = GetComponentProvider<TComponent3>();

            foreach (var entity in entities.Values)
            {
                if (entity.TryGetIndex<TComponent1>(out var index1)
                    && entity.TryGetIndex<TComponent2>(out var index2)
                    && entity.TryGetIndex<TComponent3>(out var index3))
                {
                    action(entity, ref provider1.Get(index1), ref provider2.Get(index2), ref provider3.Get(index3));
                }
            }
        }
    }
}
