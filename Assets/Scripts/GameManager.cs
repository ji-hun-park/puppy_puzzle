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
    
    public int score;
    public GameState gameState = GameState.GameStart;
    public UIManager uIManager;
    public GameBoard gameBoard;
    public GameObject summoner;
    public GameObject[] summoned;
    public Sprite[] colorSprites;
    public GameObject[] shapes;
    public IngameUI ingameUI;
    
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
        ingameUI = uIManager.uIList[1].GetComponent<IngameUI>();
        score = 0;
        ingameUI.scoreTMP.text = score.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uIManager.uIList[2].gameObject.SetActive(true);
        }
        
        if (gameState == GameState.GameStart)
        {
            uIManager.uIList[0].gameObject.SetActive(true);
            uIManager.uIList[1].gameObject.SetActive(false);
            uIManager.uIList[2].gameObject.SetActive(false);
            gameBoard.gameObject.SetActive(false);
            summoner.gameObject.SetActive(false);
            score = 0;
        }
        else if (gameState == GameState.GamePlaying)
        {
            uIManager.uIList[1].gameObject.SetActive(true);
            uIManager.uIList[0].gameObject.SetActive(false);
            gameBoard.gameObject.SetActive(true);
            summoner.gameObject.SetActive(true);
            ingameUI.scoreTMP.text = score.ToString();
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
        
        if (dist >= 800f || raw == 9 || col == 9 || gameBoard.cellArray[center].cellColor != Cell.CellColor.Light)
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
                // 전부 색칠하기
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+8].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-8].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+7].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-7].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+9].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-9].cellColor = (Cell.CellColor)color;
                score += 9;
                break;
            case 1: // ㅏ 모양
                if (col-1 < 0 || center + 7 >= 64 || center - 9 < 0) return false;
                if (gameBoard.cellArray[center + 7].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 9].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 1].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-9].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+7].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            case 2: // ㅓ 모양
                if (col+1 >= 8 || center + 9 >= 64 || center - 7 < 0) return false;
                if (gameBoard.cellArray[center - 7].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center + 9].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center + 1].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+9].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-7].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            case 3: // ㅣ 모양
                if (raw + 1 >= 8 || raw - 1 < 0 || raw - 2 < 0) return false;
                if (gameBoard.cellArray[center+8].cellColor != 0 || gameBoard.cellArray[center-8].cellColor != 0 || gameBoard.cellArray[center-16].cellColor != 0)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+8].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-8].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-16].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            case 4: // ㅡ 모양
                if (col + 1 >= 8 || col - 1 < 0 || col - 2 < 0) return false;
                if (gameBoard.cellArray[center+1].cellColor != 0 || gameBoard.cellArray[center-1].cellColor != 0 || gameBoard.cellArray[center-2].cellColor != 0)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-2].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            case 5: // L 모양
                if (col-1 < 0 || center - 17 < 0 || center - 9 < 0) return false;
                if (gameBoard.cellArray[center - 17].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 9].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 1].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-9].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-17].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            case 6: // ㄴ 모양
                if (col-1 < 0 || col + 1 >= 8 || center - 9 < 0) return false;
                if (gameBoard.cellArray[center + 1].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 9].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 1].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-9].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center+1].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            case 7: // 2X2 사각형
                if (raw-1 < 0 || col - 1 < 0 || center - 9 < 0) return false;
                if (gameBoard.cellArray[center - 9].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 8].cellColor != Cell.CellColor.Light ||
                    gameBoard.cellArray[center - 1].cellColor != Cell.CellColor.Light)
                {
                    return false;
                }
                gameBoard.cellArray[center].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-1].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-9].cellColor = (Cell.CellColor)color;
                gameBoard.cellArray[center-8].cellColor = (Cell.CellColor)color;
                score += 4;
                break;
            default:
                return false;
        }
        
        return true;
    }

    public void CheckLines()
    {
        for (int i = 0; i < 8; i++)
        {
            bool pull = true;
            for (int j = 0; j < 8; j++)
            {
                if (gameBoard.cellArray[i*8+j].cellColor == Cell.CellColor.Light) pull = false;
            }

            if (pull)
            {
                for (int j = 0; j < 8; j++)
                {
                    gameBoard.cellArray[i*8+j].cellColor = 0;
                }
                ScoreUp();
            }
        }
        
        for (int i = 0; i < 8; i++)
        {
            bool pull = true;
            for (int j = 0; j < 8; j++)
            {
                if (gameBoard.cellArray[j*8+i].cellColor == Cell.CellColor.Light) pull = false;
            }

            if (pull)
            {
                for (int j = 0; j < 8; j++)
                {
                    gameBoard.cellArray[j*8+i].cellColor = 0;
                }
                ScoreUp();
            }
        }
    }

    private void ScoreUp()
    {
        score += 30;
    }
}
