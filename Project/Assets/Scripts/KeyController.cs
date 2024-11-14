using UnityEngine;

public class KeyController : MonoBehaviour
{
    // Tıklama sırasında çağrılacak işlev
    void OnMouseDown()
    {
        // İşlev burada çağrılır
        ActivateKey();
    }

    // İşlevin tanımı
    void ActivateKey()
    {
        Debug.Log("Anahtara tıklandı!");
        // Burada istediğiniz işlemi gerçekleştirin, örneğin:
        // Anahtarı topla, bir efekt göster veya bir animasyon oynat
    }
}
