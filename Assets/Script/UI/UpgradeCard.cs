using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI iconText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI currentText;

    [SerializeField] private Button selectButton;
    [SerializeField] private Image background;

    [SerializeField] private Color defaultBg = new Color(0.12f, 0.12f, 0.16f, 0.95f);
    [SerializeField] private Color hoveredBg = new Color(0.20f, 0.20f, 0.28f, 1.00f);

    private Action onSelected;

    private void Awake()
    {
        if (selectButton != null)
            selectButton.onClick.AddListener(OnClick);
    }


    public void Setup(UpgradeOption option, Action callback, PlayerStats stats = null)
    {
        onSelected = callback;

        if (iconText != null) iconText.text = string.IsNullOrEmpty(option.icon) ? "✦" : option.icon;
        if (nameText != null) nameText.text = option.displayName;
        if (descText != null) descText.text = option.description;
        if (valueText != null) valueText.text = FormatDelta(option);
        if (currentText != null) currentText.text = FormatCurrent(option.statType, stats);

        if (background != null) background.color = defaultBg;
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);

    private void OnClick() => onSelected?.Invoke();

    // "+25" / "+10%" / "+0.2x"
    private string FormatDelta(UpgradeOption opt)
    {
        switch (opt.statType)
        {
            case StatType.VitaminD: return $"+{opt.value:F0} HP";
            case StatType.VitaminC: return $"+{opt.value:F0}% armor";
            case StatType.VitaminA: return $"+{opt.value:F2}x scale";
            case StatType.VitaminB: return $"+{opt.value:F1} speed";
            case StatType.VitaminK: return $"+{opt.value:F0}% crit chance";
            case StatType.VitaminE: return $"+{opt.value:F1} HP/s";
            case StatType.VitaminPP: return $"+{opt.value:F0} damage";
            default: return $"+{opt.value}";
        }
    }

    // "сейчас: 100 HP" и т.п.
    private string FormatCurrent(StatType stat, PlayerStats s)
    {
        if (s == null) return "";
        switch (stat)
        {
            case StatType.VitaminD: return $"сейчас: {s.vitaminD:F0} HP";
            case StatType.VitaminC: return $"сейчас: {s.vitaminC:F0}% armor";
            case StatType.VitaminA: return $"сейчас: {s.vitaminA:F2}x scale";
            case StatType.VitaminB: return $"сейчас: {s.vitaminB:F1} speed";
            case StatType.VitaminK: return $"сейчас: {s.vitaminK:F0}% crit chance";
            case StatType.VitaminE: return $"сейчас: {s.vitaminE:F1} HP/s";
            case StatType.VitaminPP: return $"сейчас: {s.vitaminPP:F0} damage";
            default: return "";
        }
    }

    public void OnPointerEnter() { if (background != null) background.color = hoveredBg; }
    public void OnPointerExit() { if (background != null) background.color = defaultBg; }
}
