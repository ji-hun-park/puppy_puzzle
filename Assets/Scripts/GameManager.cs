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
}
