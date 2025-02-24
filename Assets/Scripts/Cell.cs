using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public enum Movable
    {
        Move,
        Static
    }
    public enum CellColor
    {
        Light,
        Red,
        Blue,
        Orange,
        Brown,
        Dark,
        Green,
        Yellow,
        Violet,
        Pink
    }
    public Movable movable;
    public CellColor cellColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        spriteRenderer.sprite = GameManager.Instance.colorSprites[(int)cellColor];
    }

    public void CleanCell()
    {
        cellColor = 0;
    }
}
