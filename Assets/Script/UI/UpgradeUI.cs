using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


/// Иерархия Canvas:
///   UpgradePanel (этот GameObject)
///     ├─ Overlay (Image, тёмный полупрозрачный фон)
///     ├─ Window
///     │   ├─ TitleText (TMP)
///     │   ├─ LevelBadge (TMP) — "Уровень 4!"
///     │   └─ CardsContainer (HorizontalLayoutGroup)
///     │       ├─ Card_0  ← UpgradeCard prefab
///     │       ├─ Card_1
///     │       └─ Card_2
/// </summary>
public class UpgradeUI : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject panel;          
    [SerializeField] private TextMeshProUGUI levelBadge;  

    [Header("Карточки")]
    [SerializeField] private UpgradeCard[] cards;         // 3 карточки из сцены

    //LevelManager.OnUpgradeChoices
    public void ShowUpgradeChoices(List<UpgradeOption> choices)
    {
        // Скрываем лишние карточки
        foreach (var card in cards)
            card.Hide();

        // Заполняем активные
        for (int i = 0; i < choices.Count && i < cards.Length; i++)
        {
            int idx = i; // захват для лямбды
            cards[i].Setup(choices[i], () => SelectUpgrade(choices[idx]));
        }

        panel.SetActive(true);
    }

    // LevelManager.OnLevelUp
    public void OnLevelUp(int newLevel)
    {
        if (levelBadge != null)
            levelBadge.text = $"Уровень {newLevel}!";
    }

    private void SelectUpgrade(UpgradeOption option)
    {
        levelManager.SelectUpgrade(option);
        panel.SetActive(false);
    }

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false); // скрыта по умолчанию
    }
}