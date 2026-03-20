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
    [SerializeField] private TextMeshProUGUI currentText; // текущее значение стата

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


    public void Setup(UpgradeOption option, Action callback, PlayerStats stats)
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
            case StatType.VitaminC:
            case StatType.VitaminK:
                return $"+{opt.value:F0}%";
            case StatType.VitaminA:
                return $"+{opt.value:F2}x";
            default:
                return opt.value % 1 == 0 ? $"+{(int)opt.value}" : $"+{opt.value:F1}";
        }
    }


    private string FormatCurrent(StatType stat, PlayerStats s)
    {
        if (s == null) return "";
        switch (stat)
        {
            case StatType.VitaminD: return $"{s.vitaminD:F0} HP";
            case StatType.VitaminC: return $"{s.vitaminC:F0}% armor";
            case StatType.VitaminA: return $"{s.vitaminA:F2}x scale";
            case StatType.VitaminB: return $"{s.vitaminB:F1} speed";
            case StatType.VitaminK: return $"{s.vitaminK:F0}% crit chance";
            case StatType.VitaminE: return $"{s.vitaminE:F1} HP/s";
            case StatType.VitaminPP: return $"{s.vitaminPP:F0} damage";
            default: return "";
        }
    }

    public void OnPointerEnter() { if (background != null) background.color = hoveredBg; }
    public void OnPointerExit() { if (background != null) background.color = defaultBg; }
}
