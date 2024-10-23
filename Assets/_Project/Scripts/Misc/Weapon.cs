public class Weapon
{
    public Weapon(int projectileCount, int damage)
    {
        _projectileCount = projectileCount;
        _bullets = projectileCount;
        _damage = damage;
    }

    private int _projectileCount;
    private int _damage;
    private int _bullets;

    public int ProjectileCount => _projectileCount;
    public int Damage => _damage;
    public int Bullets => _bullets;

    /// <summary>
    /// Returns true if a bullet was used
    /// </summary>
    /// <returns></returns>
    public bool UseBullet()
    {
        _bullets--;

        return _bullets > -1;
    }

    public void Reload()
    {
        _bullets = _projectileCount;
    }
}
