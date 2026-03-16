using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerAttack : MonoBehaviour
{
    public int burstCount = 3;

    public float burstDelay = 0.1f;

    public float attackCooldown = 0.8f;

    public GameObject seedPrefab;
    public Transform firePoint;

    public PlayerController player;

    private float cooldownTimer = 0f;
    private bool isBursting = false;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

     

        if (player == null)
            player = GetComponent<PlayerController>();

        if (firePoint == null)
            firePoint = transform;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && !isBursting)
        {
            Vector2 lockedDir = GetAimDirection();
            if (lockedDir != Vector2.zero)
            {
                StartCoroutine(Burst(lockedDir));
                cooldownTimer = attackCooldown;
            }
        }
    }

    private IEnumerator Burst(Vector2 direction)
    {
        isBursting = true;

        for (int i = 0; i < burstCount; i++)
        {
            Shoot(direction);
            yield return new WaitForSeconds(burstDelay);
        }

        isBursting = false;
    }

    private void Shoot(Vector2 direction)
    {
        if (seedPrefab == null || player == null || mainCamera == null) return;

        GameObject seed = Instantiate(seedPrefab, firePoint.position, Quaternion.identity);
        if (seed.TryGetComponent<SeedProjectile>(out var projectile))
            projectile.Init(direction, player.Stats);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private Vector2 GetAimDirection()
    {
        if (Mouse.current == null) return Vector2.zero;

        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(
            new Vector3(mouseScreen.x, mouseScreen.y, Mathf.Abs(mainCamera.transform.position.z))
        );
        mouseWorld.z = 0f;

        Vector2 dir = ((Vector2)mouseWorld - (Vector2)firePoint.position).normalized;
        return dir == Vector2.zero ? Vector2.right : dir;
    }

    public void SetCooldown(float newCooldown) => attackCooldown = Mathf.Max(0.1f, newCooldown);
}