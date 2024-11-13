using UnityEngine;
using UnityEngine.EventSystems;

public class RotaryControl : MonoBehaviour, IDragHandler
{
    public Transform lockDial; // Kilit prefabindeki döndürülebilir orta kısım
    private RectTransform rotaryRectTransform;
    private float currentAngle = 0;

    void Start()
    {
        rotaryRectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // UI elemanını döndürmek için açıyı hesapla
        Vector2 direction = eventData.position - (Vector2)rotaryRectTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Yeni açıyı döndürme işlemi için kullan
        float angleDelta = angle - currentAngle;
        currentAngle = angle;

        // Rotary UI'yi döndür
        rotaryRectTransform.rotation = Quaternion.Euler(0, 0, -currentAngle);

        // Kilit modelindeki orta kısmı döndür
        lockDial.Rotate(0, angleDelta, 0);
    }
}
