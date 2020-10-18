using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;
    public Canvas tutorialCanvas;
    public Text tutorialText;
    private int obstacleCount;

    private void Start()
    {
        if (!GameController.tutorial)
        {
            EndTutorial();
            return;
        }

        obstacleCount = obstacleSpawner.obstacles.Length * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        InvokeRepeating("SlowTime", 0, 0.05f);

        if (other.CompareTag("Water")) tutorialText.text = "Type \"Earth\".";

        else if (other.CompareTag("Earth")) tutorialText.text = "Type \"Air\".";

        else if (other.CompareTag("Fire")) tutorialText.text = "Type \"Water\" or \"Earth\".";

        tutorialCanvas.gameObject.SetActive(true);
    }

    public void ObstacleDestroyed()
    {
        obstacleCount--;

        if (obstacleCount == 0)
        {
            GameController.tutorial = false;
            SaveManager.Instance.Save();
        }
        
        tutorialCanvas.gameObject.SetActive(false);
        InvokeRepeating("ResumeTime", 0, 0.05f);
    }


    void SlowTime()
    {
        if (Time.timeScale > 0.25f)
        {
            Time.timeScale -= 0.1f;
        }

        else
        {
            CancelInvoke();
        }
    }

    void ResumeTime()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale += 0.1f;
        }

        else
        {
            if (!GameController.tutorial)
            {
                EndTutorial();
            }
            CancelInvoke();
        }
    }
    
    public void EndTutorial()
    {
        Destroy(gameObject);
    }

}
