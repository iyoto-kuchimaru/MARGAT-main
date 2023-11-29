using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Vuforia;

public class Q1Button : MonoBehaviour
{
    public GameObject arObjectToPlace1;
    public GameObject arObjectToPlace2;
    public GameObject arObjectToPlace3;
    public GameObject arObjectToPlace4;
    public GameObject arObjectToPlace5;
    public GameObject cylinderQ2;
    public GameObject cylinderQ3;
    public GameObject cylinderQ4;
    public GameObject cylinderQ5;

    public void PlaceARObject1()
    {
        arObjectToPlace1.SetActive(true);
        arObjectToPlace2.SetActive(false);
        arObjectToPlace3.SetActive(false);
        arObjectToPlace4.SetActive(false);
        arObjectToPlace5.SetActive(false);
        cylinderQ2.SetActive(false);
        cylinderQ3.SetActive(false);
        cylinderQ4.SetActive(false);
        cylinderQ5.SetActive(false);

    }

    public void PlaceARObject2()
    {
        arObjectToPlace1.SetActive(false);
        arObjectToPlace2.SetActive(true);
        arObjectToPlace3.SetActive(false);
        arObjectToPlace4.SetActive(false);
        arObjectToPlace5.SetActive(false);
        cylinderQ2.SetActive(false);
        cylinderQ3.SetActive(false);
        cylinderQ4.SetActive(false);
        cylinderQ5.SetActive(false);
    }

    public void PlaceARObject3()
    {
        arObjectToPlace1.SetActive(false);
        arObjectToPlace2.SetActive(false);
        arObjectToPlace3.SetActive(true);
        arObjectToPlace4.SetActive(false);
        arObjectToPlace5.SetActive(false);
        cylinderQ2.SetActive(false);
        cylinderQ3.SetActive(false);
        cylinderQ4.SetActive(false);
        cylinderQ5.SetActive(false);
    }

    public void PlaceARObject4()
    {
        arObjectToPlace1.SetActive(false);
        arObjectToPlace2.SetActive(false);
        arObjectToPlace3.SetActive(false);
        arObjectToPlace4.SetActive(true);
        arObjectToPlace5.SetActive(false);
        cylinderQ2.SetActive(false);
        cylinderQ3.SetActive(false);
        cylinderQ4.SetActive(false);
        cylinderQ5.SetActive(false);
    }

    public void PlaceARObject5()
    {
        arObjectToPlace1.SetActive(false);
        arObjectToPlace2.SetActive(false);
        arObjectToPlace3.SetActive(false);
        arObjectToPlace4.SetActive(false);
        arObjectToPlace5.SetActive(true);
        cylinderQ2.SetActive(false);
        cylinderQ3.SetActive(false);
        cylinderQ4.SetActive(false);
        cylinderQ5.SetActive(false);
    }
}