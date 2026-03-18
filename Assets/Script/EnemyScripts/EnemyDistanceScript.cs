using UnityEngine;

public class EnemyDistanceScript : EnemyBaseScript
{
    public GameObject ProjectilePrefab;
    public Transform FirePoint;
    public float ProjectileSpeed = 1f;
    public float ProjectileLifeTime = 1f;
    
    private Vector3 _direction;
    protected override void Attack()
    {
        _direction = GetAimDirection();
        GameObject projectile = Instantiate(ProjectilePrefab, FirePoint.position, Quaternion.identity);
        if (projectile.TryGetComponent<EnemyProjectileScript>(out var projectileScript))
        {
            projectileScript.Damage = Damage;
            projectileScript.Speed = ProjectileSpeed;
            projectileScript.LifeTime = ProjectileLifeTime;
            projectileScript.Init(_direction);
        }
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        FirePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    private Vector2 GetAimDirection()
    {
        Vector2 dir = ((Vector2)_playerPos - (Vector2)FirePoint.position).normalized;
        return dir == Vector2.zero ? Vector2.right : dir;
    }
}
