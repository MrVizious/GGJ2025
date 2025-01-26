using DesignPatterns;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneController : Singleton<sceneController>
{
    protected override bool keepOldestInstance => false;
    public void ChangeGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        ChangeGameScene("MainMenuScene");
    }

    public void GoToPlayerSelector()
    {
        ChangeGameScene("PlayerSelector");
    }

    public void GoToGameplayScene()
    {
        ChangeGameScene("GameplayScene");
    }

    public void GoToBurbujasScene()
    {
        ChangeGameScene("BurbujasScene");
    }

    public void GoToLooseScene()
    {
        ChangeGameScene("LooseScene");
    }

    public void GoToCreditScene()
    {
        ChangeGameScene("CreditScene");
    }

    public void GoToControlsScene()
    {
        ChangeGameScene("ControlsScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
