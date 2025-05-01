namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Interface for all behaviours being able to drop loot.
    /// For a concrete example check out the Loot.cs class.
    /// </summary>
    public interface ILootComponent : IComponent
    {
        void Drop();
    }
}