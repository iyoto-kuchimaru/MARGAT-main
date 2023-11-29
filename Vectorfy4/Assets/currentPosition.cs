using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class currentPosition : MonoBehaviour
{
    public TMPro.TMP_Text currentText;
    float m_X = 0;
    float m_Y = 0;
    float m_Z = 0;

    void Start()
    {
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }

    public void PushButtonXpos()
    {
        m_X++;
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }

    public void PushButtonXneg()
    {
        m_X--;
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }
    public void PushButtonYpos()
    {
        m_Y++;
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }

    public void PushButtonYneg()
    {
        m_Y--;
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }
    public void PushButtonZpos()
    {
        m_Z++;
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }

    public void PushButtonZneg()
    {
        m_Z--;
        currentText.SetText("X:" + m_X.ToString() + ", Y:" + m_Y.ToString() + ", Z:" + m_Z.ToString());
    }
}
