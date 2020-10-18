using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text coins;
    public GameObject[] cheats;

    private void OnEnable()
    {
        coins.text = GameController.coins.ToString();

        ChangeButtonStatus();
    }

    protected void ChangeButtonStatus()
    {
        foreach (GameObject o in cheats)
        {
            if (GameController.cheat == null)
            {
                if (GetPrice(o) <= GameController.coins)
                {
                    EnableButton(o);
                }

                else
                {
                    DisableButton(o);
                }
            }

            else
            {
                DisableButton(o);
            }
        }
    }

    protected int GetPrice(GameObject g)
    {
        return int.Parse(g.transform.Find("Price").GetComponent<TextMeshProUGUI>().text);
    }

    protected void EnableButton(GameObject g)
    {
        g.GetComponent<Button>().interactable = true;
    }

    protected void DisableButton(GameObject g)
    {
        g.GetComponent<Button>().interactable = false;
    }

    public void BuyCheat(GameObject cheat)
    {
        GameController.cheat = cheat.name;
        GameController.coins -= GetPrice(cheat);
        coins.text = GameController.coins.ToString();
        SaveManager.Instance.Save();
        ChangeButtonStatus();
    }
}
