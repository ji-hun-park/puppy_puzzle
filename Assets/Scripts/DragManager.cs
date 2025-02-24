using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Dohyong dohyong;
    public Vector3 _originalPosition;
    private bool _isDragging = false;
    public int sumNum;

    void Start()
    {
        dohyong = GetComponent<Dohyong>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //_originalPosition = transform.position;
        _isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
            newPosition.z = transform.position.z; // 원래 Z값 유지
            transform.position = newPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        if (GameManager.Instance.CanPlaceBlock(transform.position, (int)dohyong.shape, dohyong.cc)) // 게임판에 배치 가능 여부 검사
        {
            PlaceBlock();
        }
        else
        {
            transform.position = _originalPosition; // 원위치로 복귀
        }
    }

    private void PlaceBlock()
    {
        // 블록을 8x8 게임판에 맞게 정렬하고 고정하는 로직 추가
        GameManager.Instance.summoned[sumNum] = null;
        Destroy(this);
    }
}
