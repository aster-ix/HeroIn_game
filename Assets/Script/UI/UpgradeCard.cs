using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
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

    public void Setup(UpgradeOption option, Action callback)
    {
        onSelected = callback;

        if (nameText != null) nameText.text = option.displayName;

        if (background != null) background.color = defaultBg;

        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);

    private void OnClick() => onSelected?.Invoke();

    private string FormatValue(UpgradeOption opt)
    {
        string v = opt.value % 1 == 0
            ? $"+{(int)opt.value}"
            : $"+{opt.value:F1}";

        if (opt.description.Contains("%")) v += "%";

        return v;
    }

    public void OnPointerEnter()
    {
        if (background != null) background.color = hoveredBg;
    }
    public void OnPointerExit()
    {
        if (background != null) background.color = defaultBg;
    }
}