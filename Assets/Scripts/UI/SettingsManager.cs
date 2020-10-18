using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle music, sfx, ambientOcclusion, vignette, bloom;
    public MainMenuController mmc;

    // Start is called before the first frame update
    void Start()
    {
        if (ambientOcclusion != null)
        {
            ambientOcclusion.isOn = GameController.ambientOcclusion;
            vignette.isOn = GameController.vignette;
            bloom.isOn = GameController.bloom;

            ambientOcclusion.onValueChanged.AddListener(delegate { ToggleValueChanged(ambientOcclusion); });
            vignette.onValueChanged.AddListener(delegate { ToggleValueChanged(vignette); });
            bloom.onValueChanged.AddListener(delegate { ToggleValueChanged(bloom); });
        }

        music.isOn = GameController.music;
        sfx.isOn = GameController.sfx;

        music.onValueChanged.AddListener(delegate { ToggleValueChanged(music); });
        sfx.onValueChanged.AddListener(delegate { ToggleValueChanged(sfx); });
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.name == "Music")
        {
            ToggleMusic(toggle);
        }

        else if (toggle.name == "SFX")
        {
            ToggleSfx(toggle);
        }

        if (GetComponent<AudioSource>() && GameController.sfx)
        {
            GetComponent<AudioSource>().Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ToggleFootSteps();
        }

        else if (mmc)
        {
            mmc.PlayClickSound();
        }
    }

    public void ToggleAmbientOcclusion(Toggle toggle)
    {
        GameController.instance.ToggleAmbientOcclusion(toggle);
    }

    public void ToggleVignette(Toggle toggle)
    {
        GameController.instance.ToggleVignette(toggle);
    }

    public void ToggleBloom(Toggle toggle)
    {
        GameController.instance.ToggleBloom(toggle);
    }

    public void ToggleMusic(Toggle toggle)
    {
        GameController.instance.ToggleMusic(toggle);
    }

    public void ToggleSfx(Toggle toggle)
    {
        GameController.instance.ToggleSfx(toggle);
    }
}
