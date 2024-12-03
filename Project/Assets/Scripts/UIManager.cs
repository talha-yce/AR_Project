using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_InputField tahminInputField;
    public TextMeshProUGUI sonucText;
    public Button kontrolEtButton;
    public CanTower canTower;
      [SerializeField] private GameObject endGameMenu;
    

    private int seciliSatir = -1;
    private int seciliSutun = -1;

    private void Start()
    {
        kontrolEtButton.onClick.AddListener(KontrolEt);
    }

    private void Awake()
    {if (endGameMenu != null)
    {
        endGameMenu.SetActive(false); // Paneli başlangıçta gizle
    }}

    // Kullanıcının tahminini kontrol etme
    private void KontrolEt()
    {
        if (int.TryParse(tahminInputField.text, out int tahmin))
        {
            if (seciliSatir != -1 && seciliSutun != -1)
            {
                int dogruSayi = canTower.OrijinalSayiyiAl(seciliSatir, seciliSutun);

                if (canTower.eksikKonumlar.Contains(new Vector2Int(seciliSatir, seciliSutun))) // Eksik konum kontrolü
                {
                    if (tahmin == dogruSayi)
                    {
                        sonucText.text = "Doğru Tahmin!";
                        sonucText.color = Color.green;
                        canTower.GuncelleSayi(seciliSatir, seciliSutun, tahmin);
                    }
                    else
                    {
                        sonucText.text = $"Yanlış Tahmin. Doğru değer: {dogruSayi}";
                        sonucText.color = Color.red;
                    }
                }
                else
                {
                    sonucText.text = "Bu kutu zaten dolu.";
                    sonucText.color = Color.red;
                }
            }
            else
            {
                sonucText.text = "Lütfen önce bir kutu seçin.";
                sonucText.color = Color.red;
            }
        }
        else
        {
            sonucText.text = "Lütfen geçerli bir sayı girin.";
            sonucText.color = Color.red;
        }

        seciliSatir = -1;
        seciliSutun = -1;
        kontrolEtButton.interactable = false;

        // Eğer eksik kutu kalmamışsa (oyun bitmiş)
        if (canTower.eksikKonumlar.Count == 0)
        {
            EndGame();
        }
    }

    // Kutu seçildiğinde satır ve sütun numarasını belirleme
    public void KutuSec(int satir, int sutun)
    {
        seciliSatir = satir;
        seciliSutun = sutun;
        kontrolEtButton.interactable = true;
    }

     private void EndGame()
    {
        Debug.Log("EndGame triggered");
       endGameMenu.SetActive(true);
    }

   public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
