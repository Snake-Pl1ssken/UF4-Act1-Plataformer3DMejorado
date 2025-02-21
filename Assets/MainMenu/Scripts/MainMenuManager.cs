using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasMainMenu;
    [SerializeField] CanvasGroup canvasSettings;

    private void Start()
    {
        SetCanvasGroup(canvasMainMenu, 1f, true);
        SetCanvasGroup(canvasSettings, 0f, false);
    }

    public void SwitchScene()
    {
        Coin.ResetCoins();
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingsGame()
    {
        SetCanvasGroup(canvasMainMenu, 0f, false);
        SetCanvasGroup(canvasSettings, 1f, true);
    }

    public void BackMainMenu()
    {
        SetCanvasGroup(canvasMainMenu, 1f, true);
        SetCanvasGroup(canvasSettings, 0f, false);
    }

    private void SetCanvasGroup(CanvasGroup canvasGroup, float alpha, bool interactable)
    {
        canvasGroup.alpha = alpha;
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;
    }
}
