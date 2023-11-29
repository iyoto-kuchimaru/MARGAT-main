using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q1Answer : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public Button HintButton;
    public Button submitButton;
    public GameObject correctPopup;
    public GameObject incorrectPopup;
    public GameObject HintPopup;

    private string correctAnswer = "6";

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

    public void Hint()
    {
        HintPopup.SetActive(true);
    }
}