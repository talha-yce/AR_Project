using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RawImage))]
public class CircleRaycast : MonoBehaviour, IPointerClickHandler
{
    private RawImage rawImage;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Tıklamanın merkezden olan konumunu hesapla
        RectTransform rectTransform = rawImage.rectTransform;
        Vector2 localPoint;
        
        // Ekran noktasını yerel koordinata çevir
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        
        // Yarıçapı hesapla
        float radius = rectTransform.rect.width / 2;

        // Tıklamanın merkezden olan mesafesini kontrol et
        if (localPoint.magnitude <= radius)
        {
            Debug.Log("Daire içi tıklama!");
            // Tıklama geçerli, ek bir işlem yapabilirsiniz
        }
        else
        {
            Debug.Log("Daire dışı tıklama!");
            // Tıklama geçersiz, işlenmeyecek
        }
    }
}
