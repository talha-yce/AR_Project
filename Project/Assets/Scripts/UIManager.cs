using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CanTower canTower;
    public TMP_InputField satirInputField;
    public TMP_InputField sutunInputField;
    public TMP_InputField tahminInputField;
    public Button kontrolEtButton;
    public TMP_Text sonucText;

    private void Start()
    {
        kontrolEtButton.onClick.AddListener(KontrolEt);
    }

    private void KontrolEt()
{
    if (int.TryParse(satirInputField.text, out int goruntuSatir) &&
        int.TryParse(sutunInputField.text, out int goruntuSutun) &&
        int.TryParse(tahminInputField.text, out int tahmin))
    {
        
        int satir = canTower.satirSayisi - goruntuSatir;
        int sutun = goruntuSutun - 1;

        if (canTower != null && 
            satir >= 0 && satir < canTower.satirSayisi &&
            sutun >= 0 && sutun < canTower.sayilar[satir].Count)
        {
            int dogruSayi = canTower.sayilar[satir][sutun];
            if (tahmin == dogruSayi)
            {
                sonucText.text = "Doğru Tahmin!";
                sonucText.color = Color.green;
                canTower.GuncelleSayi(satir, sutun, tahmin);
            }
            else
            {
                sonucText.text = "Yanlış Tahmin. Tekrar deneyin.";
                sonucText.color = Color.red;
            }
        }
        else
        {
            sonucText.text = $"Geçersiz satır veya sütun. Lütfen satır için 1-{canTower.satirSayisi}, " +
                             $"sütun için 1-{goruntuSatir} arası bir değer girin.";
            sonucText.color = Color.red;
        }
    }
    else
    {
        sonucText.text = "Lütfen geçerli sayılar girin.";
        sonucText.color = Color.red;
    }
}
}