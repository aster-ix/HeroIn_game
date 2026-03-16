using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public float currentXP = 0f;

    public float baseXPRequired = 100f;
    public float xpScaling = 1.3f;

    public int upgradeChoicesCount = 3;

    public PlayerController player;

    public UnityEvent<int> OnLevelUp;
    public UnityEvent<List<UpgradeOption>> OnUpgradeChoices;
    public UnityEvent<float, float> OnXPChanged;

    private float xpRequired;

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
        var choices = GenerateUpgradeChoices(upgradeChoicesCount);
        OnUpgradeChoices?.Invoke(choices);
    }

    public void SelectUpgrade(UpgradeOption upgrade)
    {
        player.ApplyStatUpgrade(upgrade.statType, upgrade.value);
        Time.timeScale = 1f;
        Debug.Log($"{upgrade.displayName} +{upgrade.value}");
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
                description = "+HP",
                value       = 25f, // здесь значения надо продумать
                // icon        = "💛" // можно иконки поделать 
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminC,
                displayName = "Витамин C",
                description = "+% защиты",
                value       = 10f,
                
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminA,
                displayName = "Витамин A",
                description = "+ к размеру снарядов",
                value       = 0.3f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminB,
                displayName = "Витамин B",
                description = "+ к скорости",
                value       = 1f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminK,
                displayName = "Витамин K",
                description = "+% шанс крита",
                value       = 10f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminE,
                displayName = "Витамин E",
                description = "+ регенерации HP/с",
                value       = 2f,
            },
            new UpgradeOption
            {
                statType    = StatType.VitaminPP,
                displayName = "Витамин PP",
                description = "+ к урону",
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