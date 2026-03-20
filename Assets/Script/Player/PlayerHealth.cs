using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Управление HP игрока.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private float currentHp;

    public UnityEvent<float, float> OnHealthChanged;
    public UnityEvent OnDeath;

    private void Start()
    {
        if (player == null) player = GetComponent<PlayerController>();
        currentHp = player.Stats.vitaminD;
        OnHealthChanged?.Invoke(currentHp, player.Stats.vitaminD);
    }

    private void Update()
    {
        // Пассивный реген от витамина E
        if (player.Stats.vitaminE > 0 && currentHp < player.Stats.vitaminD)
            Heal(player.Stats.vitaminE * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        float finalDamage = player.Stats.ApplyArmor(amount);
        currentHp = Mathf.Max(0f, currentHp - finalDamage);
        OnHealthChanged?.Invoke(currentHp, player.Stats.vitaminD);

        if (currentHp <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        currentHp = Mathf.Min(player.Stats.vitaminD, currentHp + amount);
        OnHealthChanged?.Invoke(currentHp, player.Stats.vitaminD);
    }


    public void OnMaxHpUpgraded(float healAmount)
    {
        Heal(healAmount);
        OnHealthChanged?.Invoke(currentHp, player.Stats.vitaminD);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // TODO: экран смерти / рестарт
        Debug.Log("Player умер");
        
    }

    public float CurrentHp => currentHp;
    public float MaxHp => player.Stats.vitaminD;
}
