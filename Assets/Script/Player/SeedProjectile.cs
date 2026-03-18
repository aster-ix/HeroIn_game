using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeedProjectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 0.6f; // короткое время — меньше клонов висит одновременно

    public float baseScale = 0.25f;

    public Color normalColor = Color.black;
    public Color critColor = Color.red;

    private float damage;
    private bool isCrit;
    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
    }

    public void Init(Vector2 dir, PlayerStats stats)
    {
        direction = dir.normalized;
        damage = stats.RollDamage(out isCrit);

        float scale = isCrit ? baseScale * 1.4f : baseScale;
        transform.localScale = new Vector3(scale, scale, 1f);

        if (sr != null)
            sr.color = isCrit ? critColor : normalColor;
    }

    private void Start()
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime); 
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyBaseScript>(out var enemy)) // нужны хп врагов
        {
            enemy.GetDamage(damage);
            if (isCrit) Debug.Log($"<color=red>КРИТ {damage}</color>");
            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger)
            Destroy(gameObject);
    }
}