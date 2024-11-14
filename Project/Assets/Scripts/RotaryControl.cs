using UnityEngine;
using UnityEngine.EventSystems;

public class RotaryControl : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Transform lockDial; 
    private KeySpawner keySpawner; 
    private RectTransform rotaryRectTransform;
    private float currentAngle = 0;
    private int targetAngle;
    
    private CircleRaycast circleRaycast; // CircleRaycast referansı

    void Start()
    {
        rotaryRectTransform = GetComponent<RectTransform>();
        keySpawner = FindObjectOfType<KeySpawner>();
        keySpawner.SpawnKey();
        circleRaycast = GetComponent<CircleRaycast>(); 
    }

    public void SetTargetAngle(int angle)
    {
        targetAngle = angle;
        UpdateTargetText(angle); // UI'da hedef açıyı güncelle
    }

    private void UpdateTargetText(int angle)
    {
        // Text UI elementini bul ve açıyı güncelle
        // Örneğin: `targetText.text = $"Rotate to {angle}";`
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Eğer tıklama daire içinde değilse, döndürme işlemini yapma
        if (!IsClickInsideCircle(eventData))
        {
            return;
        }

        // UI elemanını döndürmek için açıyı hesapla
        Vector2 direction = eventData.position - (Vector2)rotaryRectTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Yeni açıyı döndürme işlemi için kullan
        float angleDelta = angle - currentAngle;
        currentAngle = angle;

        // Rotary UI'yi döndür
        rotaryRectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);

        // Kilit modelindeki orta kısmı döndür
        lockDial.Rotate(angleDelta, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Döndürmeyi bıraktığınızda mevcut açıyı kaydedin
        currentAngle = rotaryRectTransform.eulerAngles.z;

        // Kullanıcı döndürmeyi bıraktığında kombinasyonu kontrol et
        FindObjectOfType<LockManager>().CheckCombination();
    }

    // Daire içinde tıklanıp tıklanmadığını kontrol eden fonksiyon
    private bool IsClickInsideCircle(PointerEventData eventData)
    {
        RectTransform rectTransform = rotaryRectTransform;
        Vector2 localPoint;
        
        // Ekran noktasını yerel koordinata çevir
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        
        // Yarıçapı hesapla
        float radius = rectTransform.rect.width / 2;

        // Tıklamanın merkezden olan mesafesini kontrol et
        return localPoint.magnitude <= radius;
    }
}