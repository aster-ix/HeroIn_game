using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerController player;
    public PlayerHealth playerHealth;  

    [Header("XP / Level")]
    public int currentLevel = 1;
    public float currentXP = 0f;
    public float baseXPRequired = 100f;
    public float xpScaling = 1.3f;

    [Header("Апгрейды")]
    public int upgradeChoicesCount = 3;

    // HUDController.OnLevelUp  +  UpgradeUI.OnLevelUp
    public UnityEvent<int> OnLevelUp;
    // UpgradeUI.ShowUpgradeChoices
    public UnityEvent<List<UpgradeOption>> OnUpgradeChoices;
    // HUDController.OnXPChanged
    public UnityEvent<float, float> OnXPChanged;

    private float xpRequired;

    private void Awake()
    {
        if (player == null) player = GetComponent<PlayerController>();
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        xpRequired = baseXPRequired;
        OnXPChanged?.Invoke(currentXP, xpRequired);
    }

    public void AddXP(float amount)
    {
        currentXP += amount;
        OnXPChanged?.Invoke(currentXP, xpRequired);

        while (currentXP >= xpRequired)
        {
            currentXP -= xpRequired;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        xpRequired = Mathf.Round(baseXPRequired * Mathf.Pow(xpScaling, currentLevel - 1));

        OnLevelUp?.Invoke(currentLevel);
        OnXPChanged?.Invoke(currentXP, xpRequired);

        Time.timeScale = 0f;
        OnUpgradeChoices?.Invoke(GenerateUpgradeChoices(upgradeChoicesCount));
    }

    public void SelectUpgrade(UpgradeOption upgrade)
    {
        player.ApplyStatUpgrade(upgrade.statType, upgrade.value);

        // cgециальные побочные эффекты
        if (upgrade.statType == StatType.VitaminD && playerHealth != null)
            playerHealth.OnMaxHpUpgraded(upgrade.value); // лечим на величину апгрейда

        Time.timeScale = 1f;
        Debug.Log($"Апгрейд выбран: {upgrade.displayName} +{upgrade.value}");
    }

    private List<UpgradeOption> GenerateUpgradeChoices(int count)
    {
        var all = GetAllPossibleUpgrades();

        for (int i = all.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (all[i], all[j]) = (all[j], all[i]);
        }

        var choices = new List<UpgradeOption>();
        for (int i = 0; i < Mathf.Min(count, all.Count); i++)
            choices.Add(all[i]);
        return choices;
    }

    private List<UpgradeOption> GetAllPossibleUpgrades()
    {
        return new List<UpgradeOption>
        {
            new UpgradeOption
            {
                statType    = StatType.VitaminD,
                displayName = "Витамин D",
                description = "+HP (и мгновенное лечение)",
                icon        = "💛",
                value       = 25f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminC,
                displayName = "Витамин C",
                description = "+% защиты",
                icon        = "🟠",
                value       = 10f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminA,
                displayName = "Витамин A",
                description = "+ к размеру снарядов",
                icon        = "🟡",
                value       = 0.05f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminB,
                displayName = "Витамин B",
                description = "+ к скорости движения",
                icon        = "🔵",
                value       = 1f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminK,
                displayName = "Витамин K",
                description = "+% шанс крита",
                icon        = "🔴",
                value       = 10f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminE,
                displayName = "Витамин E",
                description = "+ регенерации HP/с",
                icon        = "💚",
                value       = 2f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminPP,
                displayName = "Витамин PP",
                description = "+ к урону",
                icon        = "⚔️",
                value       = 8f,
            },
        };
    }
}

[System.Serializable]
public class UpgradeOption
{
    public StatType statType;
    public string displayName;
    public string description;
    public float value;
    public string icon;
}
