using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveSphere : MonoBehaviour
{
    public GameObject sphere;
    public float moveDistance = 0.1f;


    public void MoveX()
    {
        MoveInDirection(Vector3.right * moveDistance);
    }

    public void MoveXReverse()
    {
        MoveInDirection(Vector3.left * moveDistance);
    }

    public void MoveY()
    {
        MoveInDirection(Vector3.up * moveDistance);
    }

    public void MoveYReverse()
    {
        MoveInDirection(Vector3.down * moveDistance);
    }

    public void MoveZ()
    {
        MoveInDirection(Vector3.forward * moveDistance);
    }

    public void MoveZReverse()
    {
        MoveInDirection(Vector3.back * moveDistance);
    }

    private void MoveInDirection(Vector3 direction)
    {
        sphere.transform.Translate(direction);
    }

}
