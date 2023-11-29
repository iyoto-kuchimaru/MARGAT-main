﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Coo : MonoBehaviour
{
    public GameObject breakEffect;

    public TMPro.TMP_Text currentText;
    public TMPro.TMP_Text DoText1;
    public GameObject newSphere1;
    public GameObject newSphere2;
    public GameObject newSphere3;
    public GameObject newSphere4;
    public GameObject newSphere5;
    public GameObject Pyramid;
    public GameObject Cylinder1;
    public GameObject Cylinder2;
    public GameObject Cylinder3;
    public GameObject Button1;
    public GameObject Button2;

    public TMPro.TMP_Text timerText;
    private bool timerStarted = true;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (timerStarted)
        {
            elapsedTime += Time.deltaTime;
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.gameObject.name == "targetSphere" || other.gameObject.name == "movableSphere")
        {
            //other.gameObject.SetActive(false);
            gameObject.SetActive(false);

            GenerateEffect();

            if (currentText.text == "X:2, Y:0, Z:1")
            {
                newSphere1.SetActive(true);
            }

            if (currentText.text == "X:6, Y:0, Z:1")
            {
                newSphere2.SetActive(true);
            }

            if (currentText.text == "X:6, Y:0, Z:5")
            {
                newSphere3.SetActive(true);
            }

            if (currentText.text == "X:2, Y:0, Z:5")
            {
                newSphere4.SetActive(true);
            }

            if (currentText.text == "X:4, Y:3, Z:3")
            {
                newSphere5.SetActive(true);
            }

            bool isObject1Active = newSphere1.activeSelf;
            bool isObject2Active = newSphere2.activeSelf;
            bool isObject3Active = newSphere3.activeSelf;
            bool isObject4Active = newSphere4.activeSelf;
            bool isObject5Active = newSphere5.activeSelf;

            if (isObject1Active && isObject2Active && isObject3Active && isObject4Active)
            {
                Cylinder1.SetActive(true);
                Cylinder2.SetActive(true);
                Cylinder3.SetActive(true);
                Button1.SetActive(true);
                Button2.SetActive(true);
            }

            if (isObject1Active && isObject2Active && isObject3Active && isObject4Active && isObject5Active)
            {
                DoText1.gameObject.SetActive(false);
                Pyramid.SetActive(true);
                timerStarted = false;
            }
        }
    }
    void GenerateEffect()
    {
        GameObject effect = Instantiate(breakEffect) as GameObject;
        effect.transform.position = gameObject.transform.position;
    }
}