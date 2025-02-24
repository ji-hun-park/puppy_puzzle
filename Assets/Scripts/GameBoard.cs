using System;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public Cell[] cellArray;

    private void Start()
    {
        cellArray = transform.GetComponentsInChildren<Cell>();
    }
}
