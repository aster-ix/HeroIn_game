using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    
    private Vector3 _dir;
    public float Damage;
    public float Speed;
    public float LifeTime;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _rb.gravityScale = 0f;
    }
    public void Init(Vector2 dir)
    {
        _dir = dir.normalized;
    }
    private void Start()
    {
        _rb.linearVelocity = _dir * Speed;
        Destroy(gameObject, LifeTime); 
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerHealth>(out var player))
        {
            player.TakeDamage(Damage);
            
            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger)
            Destroy(gameObject);
    }
}
