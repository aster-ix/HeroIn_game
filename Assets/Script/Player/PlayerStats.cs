using UnityEngine;

/// <summary>
/// Все статы игрока через витамины.
/// </summary>
[System.Serializable]
public class PlayerStats
{
    [Tooltip("Витамин D — макс. HP")]
    public float vitaminD = 100f;

    [Tooltip("Витамин C — броня (0–100%)")]
    [Range(0f, 100f)]
    public float vitaminC = 0f;

    [Tooltip("Витамин A — размер снарядов")]
    public float vitaminA = 0.25f;

    [Tooltip("Витамин B — скорость движения")]
    public float vitaminB = 5f;

    [Tooltip("Витамин K — шанс крита, %")]
    [Range(0f, 100f)]
    public float vitaminK = 5f;

    [Tooltip("Витамин E — пассивный реген HP/с")]
    public float vitaminE = 0f;

    [Tooltip("Витамин PP — базовый урон")]
    public float vitaminPP = 10f;

    [Tooltip("Множитель крит-урона")]
    public float critMultiplier = 2f;

    public float RollDamage(out bool isCrit)
    {
        isCrit = Random.Range(0f, 100f) < vitaminK;
        return isCrit ? vitaminPP * critMultiplier : vitaminPP;
    }

    public float ApplyArmor(float incomingDamage)
    {
        float reduction = vitaminC / 100f;
        return incomingDamage * (1f - reduction);
    }
}
