using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // This class is just for development purposes and is not to be included in the final build (maybe)
    //
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    //

    private Canvas canvas;
    public InputField inf;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            canvas.enabled = !canvas.enabled;
            inf.ActivateInputField();
        }
    }

    public void EditEnd(string command)
    {
        if (command.Contains("coins"))
        {
            command = command.Remove(0, 6);
            command = command.Trim(')');
            GameController.coins += int.Parse(command);
            inf.text = "";
        }
    }

}
