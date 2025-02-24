using UnityEngine;

public class Summoner : MonoBehaviour
{
    public enum PositionNum
    {
        p1,
        p2,
        p3
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PositionNum positionNum;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.summoned[(int)positionNum] == null && GameManager.Instance.gameState == GameManager.GameState.GamePlaying)
        {
            GameManager.Instance.summoned[(int)positionNum] = Instantiate(GameManager.Instance.shapes[Random.Range(0, GameManager.Instance.shapes.Length)], transform.position, Quaternion.identity);
            GameManager.Instance.summoned[(int)positionNum].GetComponent<DragManager>()._originalPosition = transform.position;
            GameManager.Instance.summoned[(int)positionNum].GetComponent<DragManager>().sumNum = (int)positionNum;
        }
    }
}
