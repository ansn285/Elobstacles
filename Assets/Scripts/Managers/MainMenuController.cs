using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private AsyncOperation asyncLoad;
    public AudioSource crackSound;
    public AudioSource clickSound;

    // Start is called before the first frame update
    void Start()
    {
        asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;
    }

    public void LoadLevel(int level)
    {
        GameController.instance.AudioFadeOut();
        StartCoroutine(Load(level));
    }

    IEnumerator Load(int level)
    {
        yield return new WaitForSeconds(1.20f);
        asyncLoad.allowSceneActivation = true;
    }
    
    public void PlayCrackSound()
    {
        if (GameController.sfx)
        {
            crackSound.Play();
        }
    }
    
    public void PlayClickSound()
    {
        if (GameController.sfx)
        {
            clickSound.Play();
        }
    }
}
