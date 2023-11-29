using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// RM 2023-09-07
// updates global parameter targetsDetected when target is found or lost
// targetsDetected allows other scripts to run (to show Result Vector)
public class DetectTarget : MonoBehaviour
{
    [SerializeField] string objDetected;
    public GlobalStringVariable targetsDetected;

    // RM 2023-09-08
    // Flags that Target has been Found/Detected
    public void FlagTargetFound(string objDetected)
    {
        if (targetsDetected.value == "Nothing Detected")
        {
            targetsDetected.value = objDetected;
        }
        else if (targetsDetected.value.Contains(objDetected) == false)
        {
            targetsDetected.value += objDetected;
        }
        Debug.Log(Time.time + "FlagTargetFound targetsDetected=" + targetsDetected.value);
    }

    // RM 2023-09-08
    // Flags that Target has been Lost (no longer detected)
    public void FlagTargetLost(string objDetected)
    {
        if (targetsDetected.value.Equals(objDetected) == true)
        {
            targetsDetected.value = "Nothing Detected";
        }
        else if (targetsDetected.value.Contains(objDetected) == true)
        {
            targetsDetected.value = targetsDetected.value.Replace(objDetected, "");
        }
        Debug.Log(Time.time + "FlagTargetLost targetsDetected=" + targetsDetected.value + " (after change)");
    }
}
