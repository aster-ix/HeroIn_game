using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class UpgradeUI : MonoBehaviour
{

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI levelBadge;


    [SerializeField] private UpgradeCard[] cards;


    [SerializeField] private TextMeshProUGUI statsText;

    private void Awake()
    {
        if (levelManager == null)
            levelManager = FindFirstObjectByType<LevelManager>();

        if (levelManager == null)
            Debug.LogError("UpgradeUI");

        if (panel == null)
            Debug.LogError("UpgradeUI");
        else
            panel.SetActive(false);
    }


    public void ShowUpgradeChoices(List<UpgradeOption> choices)
    {
        if (panel == null || cards == null) return;

        PlayerStats stats = levelManager?.player?.Stats;

        foreach (var card in cards)
            card.Hide();

        for (int i = 0; i < choices.Count && i < cards.Length; i++)
        {
            int idx = i;
            cards[i].Setup(choices[i], () => SelectUpgrade(choices[idx]), stats);
        }

        RefreshStatsPanel(stats);
        panel.SetActive(true);
    }

    public void OnLevelUp(int newLevel)
    {
        if (levelBadge != null)
            levelBadge.text = $"Level {newLevel}";
    }

    private void SelectUpgrade(UpgradeOption option)
    {
        levelManager?.SelectUpgrade(option);
        if (panel != null) panel.SetActive(false);
    }

    private void RefreshStatsPanel(PlayerStats s)
    {
        if (statsText == null || s == null) return;

        statsText.text =
            $"[D]  HP:       {s.vitaminD:F0}\n" +
            $"[C]  armor:    {s.vitaminC:F0}%\n" +
            $"[PP]  damage:     {s.vitaminPP:F0}\n" +
            $"[B]  speed: {s.vitaminB:F1}\n" +
            $"[K]  crit chance:     {s.vitaminK:F0}%\n" +
            $"[E]  regeneration:    {s.vitaminE:F1} HP/s\n" +
            $"[A]  scale:   {s.vitaminA:F2}x";
    }
}
