using CodeBase.Gameplay.Mechanics.Root;

namespace CodeBase.Gameplay.Mechanics
{
    public interface IItemModificator
    {
        void Initialize(DragableObject dragableObject);

        void Dispose();
    }
}
