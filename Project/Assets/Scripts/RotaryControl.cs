using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotaryControl : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform lockDial;
    
    private RectTransform rotaryRectTransform;
    private float currentAngle = 0;
    private int targetAngle;
    
    private GameManager gameManager;
    private LockManager lockManager;

    private void Start()
    {
        rotaryRectTransform = GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
        lockManager = FindObjectOfType<LockManager>();

        if (gameManager == null)
            Debug.LogError("GameManager instance not found!");
        if (lockManager == null)
            Debug.LogError("LockManager not found in the scene!");
    }

    public void SetTargetAngle(int angle)
    {
        targetAngle = angle;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"sılındır tıklamaya başlandı");
        if (!IsClickInsideCircle(eventData) || gameManager.CurrentGameState != GameManager.GameState.WaitingForInput)
            return;

        Vector2 direction = eventData.position - (Vector2)rotaryRectTransform.position;
        float newAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360;

        float angleDelta = Mathf.DeltaAngle(currentAngle, newAngle);
        currentAngle = newAngle;

        rotaryRectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
        lockDial.Rotate(angleDelta, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameManager.CurrentGameState != GameManager.GameState.WaitingForInput)
            return;

        currentAngle = NormalizeAngle(rotaryRectTransform.eulerAngles.z);
        lockManager.SetCurrentAngle((int)currentAngle);
        gameManager.SetGameState(GameManager.GameState.CheckingLock);
    }

    private bool IsClickInsideCircle(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rotaryRectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            return localPoint.magnitude <= rotaryRectTransform.rect.width / 2;
        }
        return false;
    }

    public float GetCurrentAngle() => currentAngle;

    private float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle < 0)
            angle += 360;
        return angle;
    }
    public void ResetRotation()
    {
        currentAngle = 0;
        rotaryRectTransform.rotation = Quaternion.identity;
        if (lockDial != null)
        {
            lockDial.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("lockDial is not assigned in RotaryControl!");
        }
    }
}