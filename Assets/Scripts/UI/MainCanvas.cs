using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public InputField inputField;
    public Button cheatButton;

    public Canvas pause, gameOver, tutorial;

    private void Start()
    {
        //GetComponent<Canvas>().worldCamera = CameraManager.GetRenderCamera().GetComponent<Camera>();
        //pause.worldCamera = CameraManager.GetRenderCamera().GetComponent<Camera>();
        //gameOver.worldCamera = CameraManager.GetRenderCamera().GetComponent<Camera>();
        //tutorial.worldCamera = CameraManager.GetRenderCamera().GetComponent<Camera>();

        if (GameController.cheat != null)
        {
            cheatButton.interactable = true;
        }

        else
        {
            cheatButton.interactable = false;
        }
    }

    public void PauseGame()
    {
        inputField.DeactivateInputField();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        inputField.ActivateInputField();
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void DisableCheatButton()
    {
        cheatButton.interactable = false;
    }
}
