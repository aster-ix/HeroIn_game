using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUDController : MonoBehaviour
{
   
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Image hpFill;          
    [SerializeField] private TextMeshProUGUI hpText; 

    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI xpText;    
    [SerializeField] private TextMeshProUGUI levelText; 
    // цвета
    [SerializeField] private Color hpHighColor = new Color(0.22f, 0.80f, 0.28f); // зелёный
    [SerializeField] private Color hpMidColor = new Color(0.95f, 0.75f, 0.10f); // жёлтый
    [SerializeField] private Color hpLowColor = new Color(0.90f, 0.18f, 0.18f); // красный

    public void OnHealthChanged(float current, float max)
    {
        float t = max > 0f ? current / max : 0f;

        if (hpSlider != null)
            hpSlider.value = t;

        if (hpFill != null)
            hpFill.color = GradientColor(t);

        if (hpText != null)
            hpText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }

    public void OnXPChanged(float current, float required)
    {
        if (xpSlider != null)
            xpSlider.value = required > 0f ? current / required : 0f;

        if (xpText != null)
            xpText.text = $"{Mathf.FloorToInt(current)} / {Mathf.FloorToInt(required)} XP";
    }

    public void OnLevelUp(int newLevel)
    {
        if (levelText != null)
            levelText.text = $"Ур. {newLevel}";
    }

    private Color GradientColor(float t)
    {
        // 0..0.5 → red→yellow, 0.5..1 → yellow→green
        if (t < 0.5f)
            return Color.Lerp(hpLowColor, hpMidColor, t * 2f);
        return Color.Lerp(hpMidColor, hpHighColor, (t - 0.5f) * 2f);
    }
}