using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VectorOperationChange : MonoBehaviour
{

    public TMP_Dropdown scaleDropdown;

    private void Start()
    {
        scaleDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // When textmeshpro dropdown changed switch to one of these values sw 11-9-2023
    private void OnDropdownValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                GlobalVectorOperation.vectorOperation = "1";
                break;
            case 1:
                GlobalVectorOperation.vectorOperation = "2";
                break;
            case 2:
                GlobalVectorOperation.vectorOperation = "0";
                break;
            case 3:
                GlobalVectorOperation.vectorOperation = "-1";
                break;
            case 4:
                GlobalVectorOperation.vectorOperation = "-2";
                break;
        }
    }

    // Change the vector operation variable appropriately sw 11-9-2023
    public void setAddition()
    {
        Debug.Log(GlobalVectorOperation.vectorOperation);
        GlobalVectorOperation.vectorOperation = "+";
        Debug.Log(GlobalVectorOperation.vectorOperation);
    }

    public void setSubtraction()
    {
        GlobalVectorOperation.vectorOperation = "-";
    }

    public void setDotProduct()
    {
        GlobalVectorOperation.vectorOperation = ".";
    }

    public void setCrossProduct()
    {
        GlobalVectorOperation.vectorOperation = "x";
    }

    public void setClear()
    {
        GlobalVectorOperation.vectorOperation = "na";
    }

    
}
