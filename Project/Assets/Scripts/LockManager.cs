using UnityEngine;

public class LockManager : MonoBehaviour
{
    public float correctAngle = 90f; // Doğru açı örneğin 90 derece
    public float tolerance = 5f; // Kabul edilebilir hata payı
    public Transform lockDial; // Kilidin dönen kısmı

    void Update()
    {
        CheckCombination();
    }

    private void CheckCombination()
    {
        float dialAngle = lockDial.localEulerAngles.y;

        if (Mathf.Abs(dialAngle - correctAngle) < tolerance)
        {
            Debug.Log("Kilit açıldı!");
            // Kilit açılma işlemini buraya ekleyin (örneğin animasyon veya ses efekti)
        }
    }
}
