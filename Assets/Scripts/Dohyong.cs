using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dohyong : MonoBehaviour
{
    public int cc;
    public enum Shape
    {
        nemo,
        ah,
        uh,
        ii,
        uu,
        l,
        nien,
        mium
    }
    public Shape shape;
    public Cell[] cells;

    void Start()
    {
        cc = Random.Range(1, 10);
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].cellColor = (Cell.CellColor)cc;
        }
    }
    
    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GameEnd)
        {
            Destroy(gameObject);
        }
    }
}
