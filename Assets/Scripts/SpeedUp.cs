using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedUp : MonoBehaviour
{
    public Button myButton;
    private string currentOption = "x1";

    // Start is called before the first frame update
    void Start()
    {
        // set the initial text of the button
        setButtonText(currentOption);

        // add an event listener to look out for button clicks
        myButton.onClick.AddListener(myButtonClick);
    }

    void myButtonClick()
    {
        switch (currentOption)
        {
            case "x1":
                Time.timeScale = 2;
                currentOption = "x2";
                setButtonText(currentOption);

                break;

            case "x2":
                Time.timeScale =3;
                currentOption = "x4";
                setButtonText(currentOption);
                break;

            case "x4":
                Time.timeScale = 1;
                currentOption = "x1";
                setButtonText(currentOption);
                break;
        }      
    }
    void setButtonText(string buttonText)
    {
        myButton.transform.GetChild(0).GetComponent<Text>().text = buttonText;
    }
}
