using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // Use this statement to delete the save just for development. It will not be included in the final build.
        //PlayerPrefs.DeleteKey("save");
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        Load();
        print(Helper.Serialize<SaveState>(state));

    }

    public void Save()
    {
        state.ambientOcclusion = GameController.ambientOcclusion;
        state.vignette = GameController.vignette;
        state.bloom = GameController.bloom;
        state.coins = GameController.coins;
        state.tutorial = GameController.tutorial;
        state.cheat = GameController.cheat;
        state.music = GameController.music;
        state.sfx = GameController.sfx;

        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    public void Load()
    {
        // Check if PlayerPrefs already has a key
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            print("No save file found, creating a new one");
        }

        GameController.ambientOcclusion = state.ambientOcclusion;
        GameController.vignette = state.vignette;
        GameController.bloom = state.bloom;
        GameController.coins = state.coins;
        GameController.tutorial = state.tutorial;
        GameController.cheat = state.cheat;
        GameController.music = state.music;
        GameController.sfx = state.sfx;
    }

}
