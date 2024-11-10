using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CanTower canTower; // CanTower scriptine referans
    public TMP_InputField satirInputField;
    public TMP_InputField sutunInputField;
    public TMP_InputField tahminInputField;
    public Button kontrolEtButton;
    public TMP_Text sonucText; // Tahmin sonucunu gösterecek bir text alanı

    private void Start()
    {
        kontrolEtButton.onClick.AddListener(KontrolEt); // Kontrol Et butonuna tıklama işlemi atanıyor
    }

    private void KontrolEt()
    {
        int satir = int.Parse(satirInputField.text);
        int sutun = int.Parse(sutunInputField.text);
        int tahmin = int.Parse(tahminInputField.text);

        // Tahminin doğruluğunu kontrol et
        if (canTower != null && satir >= 0 && satir < canTower.sayilar.Count && sutun >= 0 && sutun < canTower.sayilar[satir].Count)
        {
            int dogruSayi = canTower.sayilar[satir][sutun];
            if (tahmin == dogruSayi)
            {
                sonucText.text = "Doğru Tahmin!";
                sonucText.color = Color.green;
            }
            else
            {
                sonucText.text = "Yanlış Tahmin. Tekrar deneyin.";
                sonucText.color = Color.red;
            }
        }
        else
        {
            sonucText.text = "Geçersiz satır veya sütun.";
            sonucText.color = Color.red;
        }
    }
}
