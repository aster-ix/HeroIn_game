using UnityEngine;

/// <summary>
/// Все статы игрока через витамины.
/// </summary>
[System.Serializable]
public class PlayerStats
{
   

    [Tooltip("Витамин D — HP")]
    public float vitaminD = 100f;

    [Tooltip("Витамин C — броня (пока что для простоты от 0 до 100)")]
    [Range(0f, 100f)]
    public float vitaminC = 0f;

    [Tooltip("Витамин A — размер партиклов/снарядов")]
    public float vitaminA = 0.0001f;

    [Tooltip("Витамин B — скорость")]
    public float vitaminB = 5f;

    [Tooltip("Витамин K — шанс крита")]
    [Range(0f, 100f)]
    public float vitaminK = 5f;

    [Tooltip("Витамин E — пассивное восстановление HP в сек")]
    public float vitaminE = 0f;

    [Tooltip("Витамин PP — урон")]
    public float vitaminPP = 10f;

    [Tooltip("Множитель крит-урона (если менять захотим)")]
    public float critMultiplier = 2f;

   


    // бросает дайс на крит
    // isCrit чтобы явно можно было показывать через переменную в игре
 
    public float RollDamage(out bool isCrit)
    {
        isCrit = Random.Range(0f, 100f) < vitaminK;
        return isCrit ? vitaminPP * critMultiplier : vitaminPP;
    }

    // дамаг с учетом брони
    public float ApplyArmor(float incomingDamage)
    {
        float reduction = vitaminC / 100f;
        return incomingDamage * (1f - reduction);
    }
}