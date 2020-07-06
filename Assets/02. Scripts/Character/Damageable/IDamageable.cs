namespace Isekai.Characters
{
    public interface IDamageable
    {
        void Damage(int damage);
        void OnDestroyed();
        bool IsAlive();
    }
}
