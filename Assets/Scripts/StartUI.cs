using UnityEngine;

public class StartUI : MonoBehaviour
{
    public void OnClickStartButton()
    {
        GameManager.Instance.gameState = GameManager.GameState.GamePlaying;
    }
}
