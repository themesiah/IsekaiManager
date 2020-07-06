namespace Isekai.Characters
{
    public interface IDamageable
    {
        void Damage(AttackData damage);
        void OnDestroyed();
        bool IsAlive();
    }
}
