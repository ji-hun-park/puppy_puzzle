using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Dohyong dohyong;
    private Vector3 _originalPosition;
    private bool _isDragging = false;

    void Start()
    {
        dohyong = GetComponent<Dohyong>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _originalPosition = transform.position;
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

        if (CanPlaceBlock()) // 게임판에 배치 가능 여부 검사
        {
            PlaceBlock();
        }
        else
        {
            transform.position = _originalPosition; // 원위치로 복귀
        }
    }

    private bool CanPlaceBlock()
    {
        // 블록이 게임판 내에서 유효한 위치에 있는지 확인하는 로직 추가
        return true;
    }

    private void PlaceBlock()
    {
        // 블록을 8x8 게임판에 맞게 정렬하고 고정하는 로직 추가
    }
}
