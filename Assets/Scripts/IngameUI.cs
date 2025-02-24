using UnityEngine;
using TMPro;

public class IngameUI : MonoBehaviour
{
    public TMP_Text scoreTMP;

    public void OnClickPause()
    {
        Time.timeScale = 0;
        GameManager.Instance.uIManager.uIList[2].gameObject.SetActive(true);
    }
}
