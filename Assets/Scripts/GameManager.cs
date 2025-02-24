using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GameStart,
        GamePlaying,
        GameEnd
    }
    // 싱글톤 패턴 적용
    public static GameManager Instance;
    
    public GameState gameState = GameState.GameStart;
    public UIManager uIManager;
    public GameBoard gameBoard;
    public GameObject summoner;
    public GameObject[] summoned;
    public Sprite[] colorSprites;
    public GameObject[] shapes;
    
    private void Awake()
    {
        // Instance 존재 유무에 따라 게임 매니저 파괴 여부 정함
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 기존에 존재 안하면 이걸로 대체하고 파괴하지 않기
        }
        else
        {
            Destroy(gameObject); // 기존에 존재하면 자신파괴
        }
    }

    void Start()
    {
        summoned = new GameObject[3];
    }

    private void Update()
    {
        if (gameState == GameState.GameStart)
        {
            uIManager.uIList[0].gameObject.SetActive(true);
            gameBoard.gameObject.SetActive(false);
            summoner.gameObject.SetActive(false);
        }
        else if (gameState == GameState.GamePlaying)
        {
            uIManager.uIList[0].gameObject.SetActive(false);
            gameBoard.gameObject.SetActive(true);
            summoner.gameObject.SetActive(true);
        }
        else if (gameState == GameState.GameEnd)
        {
            for (int i = 0; i < summoned.Length; i++)
            {
                summoned[i] = null;
            }
            gameBoard.gameObject.SetActive(false);
            summoner.gameObject.SetActive(false);
        }
    }

    public bool CanPlaceBlock(Vector3 checkPositon, int shape, int color)
    {
        // 블록이 게임판 내에서 유효한 위치에 있는지 확인하는 로직
        int num = 0;
        int col = 9, raw = 9;
        float dist = 1000000f;
        foreach (var curCell in gameBoard.cellArray)
        {
            if (Vector2.Distance(curCell.transform.position, checkPositon) < dist)
            {
                dist = Vector2.Distance(curCell.transform.position, checkPositon);
                col = num % 8;
                raw = num / 8;
            }

            num++;
        }
        
        int center = raw * 8 + col;
        
        if (dist >= 500f || raw == 9 || col == 9 || gameBoard.cellArray[center].cellColor != Cell.CellColor.Light)
        {
            return false;
        }
        
        switch (shape)
        {
            case 0: // 3x3 꽉찬 모양
                // 테두리 영역 밖 체크
                if (col - 1 < 0 || col + 1 >= 8 || raw + 1 >= 8 || raw - 1 < 0) return false;
                // 상화좌우 흰색 체크
                if (gameBoard.cellArray[center - 1].cellColor != Cell.CellColor.Light || gameBoard.cellArray[center + 1].cellColor != Cell.CellColor.Light || gameBoard.cellArray[center - 8].cellColor != Cell.CellColor.Light || gameBoard.cellArray[center + 8].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                // 꼭짓점 흰색 체크
                if (gameBoard.cellArray[center - 9].cellColor != Cell.CellColor.Light || gameBoard.cellArray[center + 9].cellColor != Cell.CellColor.Light || gameBoard.cellArray[center - 7].cellColor != Cell.CellColor.Light || gameBoard.cellArray[center + 7].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            default:
                return false;
        }
        
        return true;
    }
}
