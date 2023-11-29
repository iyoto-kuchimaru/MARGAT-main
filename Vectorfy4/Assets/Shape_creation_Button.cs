using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape_creation_Button : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arObjectToPlace1;
    public GameObject arObjectToPlace2;
    public GameObject arObjectToPlace3;

    public void PlaceARObject1()
    {
        arObjectToPlace1.SetActive(true);
        arObjectToPlace2.SetActive(false);
        arObjectToPlace3.SetActive(false);
    }

    public void PlaceARObject2()
    {
        arObjectToPlace1.SetActive(false);
        arObjectToPlace2.SetActive(true);
        arObjectToPlace3.SetActive(false);
    }

    public void PlaceARObject3()
    {
        arObjectToPlace1.SetActive(false);
        arObjectToPlace2.SetActive(false);
        arObjectToPlace3.SetActive(true);
    }
}
