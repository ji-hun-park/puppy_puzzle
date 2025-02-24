using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void OnClickResume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnClickTitle()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(GameManager.Instance.summoned[i]);
            GameManager.Instance.summoned[i] = null;
        }

        Time.timeScale = 1;
        GameManager.Instance.gameState = GameManager.GameState.GameStart;
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
