using System.Collections.Generic;

namespace Nintenlord.MonoGame.Utility
{
    public interface IControl<TControl>
    {
        IEnumerable<TControl> GetCurrent();
    }
}