using System;

namespace Nintenlord.MonoGame.EntitySystem
{
    public interface IComponentProvider
    {
        Type ComponentType { get; }
        int Count { get; }
        int Create();
        void Delete(int index);
    }
}
