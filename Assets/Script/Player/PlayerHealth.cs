using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// управление hp
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public PlayerController player;

    [SerializeField] private float currentHp;

    public UnityEvent<float, float> OnHealthChanged; 
    public UnityEvent OnDeath;

    private void Start()
    {
        currentHp = player.Stats.vitaminD;
        OnHealthChanged?.Invoke(currentHp, player.Stats.vitaminD);
    }

    private void Update()
    {
        // Пассивный реген
        if (player.Stats.vitaminE > 0 && currentHp < player.Stats.vitaminD)
        {
            Heal(player.Stats.vitaminE * Time.deltaTime);
        }
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

    private void Die()
    {
        OnDeath?.Invoke();
        // TODO: показать экран смерти / рестарт
        Debug.Log("Player умер");
    }

    public float CurrentHp => currentHp;
    public float MaxHp => player.Stats.vitaminD;
}