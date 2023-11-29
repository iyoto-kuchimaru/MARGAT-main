using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class FreezeDestinyObjects : MonoBehaviour
{
    public TMP_Text DestinyButtonText;
    private bool isDestinyLocked = false;
    private const string LOCKED_TEXT = "Unlock Destiny";
    private const string UNLOCKED_TEXT = "Lock Destiny";

    private Dictionary<Transform, ParentData> originalParents = new Dictionary<Transform, ParentData>();

    [System.Serializable]
    public class ParentData
    {
        public Transform parent;
        public Vector3 localPosition;
        public Quaternion localRotation;
    }

    public void ToggleDestinyLock()
    {
        isDestinyLocked = !isDestinyLocked;
        DestinyButtonText.text = isDestinyLocked ? LOCKED_TEXT : UNLOCKED_TEXT;
        if (isDestinyLocked)
        {
            LockChildrenRecursive(transform);
        }
        else
        {
            UnlockAllChildren();
        }
    }

    public void LockChildrenRecursive(Transform currentTransform)
    {
        foreach (Transform child in currentTransform)
        {
            LockPositionAndRotation(child);
            LockChildrenRecursive(child);
        }
    }

    public void LockPositionAndRotation(Transform targetTransform)
    {
        ParentData data = new ParentData
        {
            parent = targetTransform.parent,
            localPosition = targetTransform.localPosition,
            localRotation = targetTransform.localRotation
        };

        originalParents[targetTransform] = data;
        
        // No need to set position and rotation since the global position will be automatically preserved upon detaching from the parent
        targetTransform.parent = null;
    }

    public void UnlockAllChildren()
    {
        foreach (var kvp in originalParents)
        {
            UnlockPositionAndRotation(kvp.Key);
        }
        originalParents.Clear();
    }

    public void UnlockPositionAndRotation(Transform targetTransform)
    {
        if (originalParents.ContainsKey(targetTransform))
        {
            var data = originalParents[targetTransform];
            targetTransform.parent = data.parent;
            targetTransform.localPosition = data.localPosition;
            targetTransform.localRotation = data.localRotation;
        }
    }
}