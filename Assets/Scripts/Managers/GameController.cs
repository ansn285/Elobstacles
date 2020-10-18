using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public PostProcessVolume ppv;

    public static bool ambientOcclusion, vignette, bloom;
    public static int coins;
    public static bool tutorial = true;
    public static string cheat;
    public static bool music = true, sfx = true;

    private AmbientOcclusion ao;
    private MotionBlur mb;
    private Vignette v;
    private Bloom b;
    public AudioClip mainMenu, gameplay;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        // Setting the each option for Graphics according to the saved settings
        ppv.profile.TryGetSettings(out ao);
        ppv.profile.TryGetSettings(out v);
        ppv.profile.TryGetSettings(out b);


        ao.enabled.value = ambientOcclusion;
        v.enabled.value = vignette;
        b.enabled.value = bloom;

        if (music)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0 && music)
        {
            GetComponent<AudioSource>().enabled = true;
            GetComponent<AudioSource>().clip = mainMenu;
            GetComponent<AudioSource>().Play();
        }
    }


    public void ToggleAmbientOcclusion(Toggle toggle)
    {
        ambientOcclusion = toggle.isOn;

        ppv.profile.TryGetSettings(out ao);
        ao.enabled.value = ambientOcclusion;

        SaveManager.Instance.Save();
    }
    
    public void ToggleVignette(Toggle toggle)
    {
        vignette = toggle.isOn;

        ppv.profile.TryGetSettings(out v);
        v.enabled.value = vignette;

        SaveManager.Instance.Save();
    }
    
    public void ToggleBloom(Toggle toggle)
    {
        bloom = toggle.isOn;

        ppv.profile.TryGetSettings(out b);
        b.enabled.value = bloom;

        SaveManager.Instance.Save();
    }

    public void ToggleMusic(Toggle toggle)
    {
        music = toggle.isOn;
        
        if (music)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                GetComponent<AudioSource>().clip = mainMenu;
            }

            else
            {
                GetComponent<AudioSource>().clip = gameplay;
            }
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        SaveManager.Instance.Save();
    }

    public void ToggleSfx(Toggle toggle)
    {
        sfx = toggle.isOn;
        SaveManager.Instance.Save();
    }

    public void AudioFadeOut()
    {
        if (music)
        {
            InvokeRepeating("VolumeDown", 0, 0.02f);
        }
    }

    protected void VolumeDown()
    {
        if (GetComponent<AudioSource>().volume > 0)
        {
            GetComponent<AudioSource>().volume -= Time.deltaTime;
        }

        else
        {
            CancelInvoke("VolumeDown");

            if (GetComponent<AudioSource>().clip.name == mainMenu.name)
            {
                GetComponent<AudioSource>().clip = gameplay;
            }

            else
            {
                GetComponent<AudioSource>().clip = mainMenu;
            }
            GetComponent<AudioSource>().Play();
            InvokeRepeating("VolumeUp", 0, 0.02f);
        }
    }

    protected void VolumeUp()
    {
        if (GetComponent<AudioSource>().volume < .6f)
        {
            GetComponent<AudioSource>().volume += Time.deltaTime;
        }

        else
        {
            CancelInvoke("VolumeUp");
        }
    }
}
