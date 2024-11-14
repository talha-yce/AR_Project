 using UnityEngine;

public class LockManager : MonoBehaviour
{
    public Transform lockDial; // Kilidin dönen kısmı
    public float correctAngle = 285f; // Doğru konuma karşılık gelen açı (Unity'de -75 dereceye denk)
    public float tolerance = 15f; // Kabul edilebilir hata payı

    public Animator lockAnimator; // Animator bileşenini public yaparak inspector'dan atama yapılabilir

    void Start()
    {
        if (lockAnimator == null)
        {
            Debug.LogError("Animator bileşeni 'LockManager' GameObject'ine atanmadı. Lütfen Animator bileşenini atayın.");
        }
    }

    public void CheckCombination()
    {
        float dialAngle = lockDial.localEulerAngles.x;

        // Eğer açı doğru aralıkta değilse kilidi otomatik olarak doğru konuma getir
        if (dialAngle < correctAngle - tolerance || dialAngle > correctAngle + tolerance)
        {
            Debug.Log("Yanlış konum! Doğru konuma getiriliyor...");
            PlayVibrateAnimation(); // Yanlış konumda vibrate animasyonunu oynat
        }
        else
        {
            Debug.Log("Kilit açıldı!");
            PlayOpenAnimation(); // Doğru konumda open animasyonunu oynat
        }
    }

    private void PlayOpenAnimation()
    {
        // "Open" animasyonunu oynat
        lockAnimator.SetTrigger("Open");
    }

    private void PlayVibrateAnimation()
    {
        // "Vibrate" animasyonunu oynat
        lockAnimator.SetTrigger("Vibrate");
    }
}