using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subText;

    [SerializeField] private string titleMessage = "YOU DIED oh no :(";
    [SerializeField] private string subMessage = "better sunflower wins";

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }


    public void Show()
    {
        if (panel != null)
            panel.SetActive(true);

        if (titleText != null)
            titleText.text = titleMessage;

        if (subText != null)
        {
            subText.gameObject.SetActive(!string.IsNullOrEmpty(subMessage));
            subText.text = subMessage;
        }
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Quit()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
