using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public GameObject curButton;
    public GameObject nextButton;


    public void CallButton()
    {
        curButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);


    }


}
