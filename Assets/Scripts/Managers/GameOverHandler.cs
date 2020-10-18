using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    private Player player;
    public InputField inputField;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inputField.DeactivateInputField();
    }

    public void ChangeLevel(int level)
    {
        GameController.coins += player.GetCoins();
        SaveManager.Instance.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

}
