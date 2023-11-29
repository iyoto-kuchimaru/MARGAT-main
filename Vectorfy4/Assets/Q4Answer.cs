using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q4Answer : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public Button HintButton1;
    public Button HintButton2;
    public Button submitButton;
    public GameObject correctPopup;
    public GameObject incorrectPopup;
    public GameObject HintPopup;
    public GameObject HintCylinder;

    private string correctAnswer = "0.71";

    public void CheckAnswer()
    {
        string userAnswer = inputField.text.Trim();

        if (userAnswer == correctAnswer)
        {
            correctPopup.SetActive(true);
        }
        else
        {
            incorrectPopup.SetActive(true);
        }
    }

    public void Hint1()
    {
        HintCylinder.SetActive(true);
    }

    public void Hint2()
    {
        HintPopup.SetActive(true);
    }
}