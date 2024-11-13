using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanTower : MonoBehaviour
{
    public GameObject KutuPrefab;
    public float yatayBosluk = 0.2f;
    public float dikeyBosluk = 0.21f;
    public int satirSayisi = 5;
    public Material[] renkler;
    public Transform targetImageTransform;
    public UIManager uiManager;

    public List<List<int>> sayilar = new List<List<int>>();
    private List<List<int>> orijinalSayilar = new List<List<int>>();
    private Dictionary<Vector2Int, GameObject> kutuDict = new Dictionary<Vector2Int, GameObject>();
    public HashSet<Vector2Int> eksikKonumlar = new HashSet<Vector2Int>();

    private void Start()
    {
        RastgeleSayilarOlustur();
        orijinalSayilar = new List<List<int>>(sayilar); // Orijinal sayılar listesi
        EksikSayilariBelirle();
        KutuPiramitOlustur();
    }

    private void RastgeleSayilarOlustur()
    {
        sayilar.Clear();
        for (int satir = satirSayisi - 1; satir >= 0; satir--)
        {
            List<int> satirSayilari = new List<int>();
            if (satir == satirSayisi - 1)
            {
                for (int i = 0; i <= satir; i++)
                {
                    satirSayilari.Add(Random.Range(10, 100));
                }
            }
            else
            {
                for (int i = 0; i <= satir; i++)
                {
                    int altSol = sayilar[satirSayisi - satir - 2][i];
                    int altSag = sayilar[satirSayisi - satir - 2][i + 1];
                    satirSayilari.Add(altSol + altSag);
                }
            }
            sayilar.Add(satirSayilari);
        }
    }

    private void KutuPiramitOlustur()
    {
        for (int satir = 0; satir < sayilar.Count; satir++)
        {
            for (int sutun = 0; sutun < sayilar[satir].Count; sutun++)
            {
                Vector3 pozisyon = YeniPozisyonHesapla(satir, sutun);
                GameObject kutu = Instantiate(KutuPrefab, pozisyon, Quaternion.identity, targetImageTransform);
                kutuDict[new Vector2Int(satir, sutun)] = kutu; // Konum-kutu eşleşmesini saklıyoruz
                KutuUIElementleriniAyarlama(kutu, satir, sutun);
            }
        }
    }

    private void KutuUIElementleriniAyarlama(GameObject kutu, int satir, int sutun)
    {
        TMP_Text sayiText = kutu.GetComponentInChildren<TMP_Text>();
        if (sayiText != null)
        {
            if (eksikKonumlar.Contains(new Vector2Int(satir, sutun)))
            {
                sayiText.text = "?";
            }
            else
            {
                sayiText.text = sayilar[satir][sutun].ToString();
            }
            sayiText.color = Color.black;
            sayiText.fontStyle = FontStyles.Bold;
            sayiText.fontSize = 60;
        }

        Transform center = kutu.transform.Find("TenekeKutu/Center");
        if (center != null && renkler.Length > 0)
        {
            int renkIndeksi = Random.Range(0, renkler.Length);
            center.GetComponent<Renderer>().material = renkler[renkIndeksi];
        }

        Button button = kutu.GetComponentInChildren<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => KutuSecildi(satir, sutun));
        }
    }

    private Vector3 YeniPozisyonHesapla(int satir, int sutun)
    {
        float xPozisyon = sutun * yatayBosluk - (sayilar[satir].Count - 1) * yatayBosluk / 2;
        float yPozisyon = satir * dikeyBosluk;
        return new Vector3(xPozisyon, yPozisyon, 0);
    }

    private void EksikSayilariBelirle()
    {
        int eksikSayilar = 4;
        eksikKonumlar.Clear();

        while (eksikKonumlar.Count < eksikSayilar)
        {
            int satir = Random.Range(0, sayilar.Count - 1);
            int sutun = Random.Range(0, sayilar[satir].Count);
            Vector2Int konum = new Vector2Int(satir, sutun);
            eksikKonumlar.Add(konum);
        }
    }

    public void KutuSecildi(int satir, int sutun)
    {
        if (eksikKonumlar.Contains(new Vector2Int(satir, sutun)))
        {
            uiManager.KutuSec(satir, sutun);
        }
        else
        {
            Debug.Log("Bu kutu zaten dolu veya tahmin edilmiş.");
        }
    }

    public void GuncelleSayi(int satir, int sutun, int yeniDeger)
    {
        Vector2Int konum = new Vector2Int(satir, sutun);
        if (eksikKonumlar.Contains(konum) && kutuDict.ContainsKey(konum))
        {
            sayilar[satir][sutun] = yeniDeger;
            TMP_Text sayiText = kutuDict[konum].GetComponentInChildren<TMP_Text>();
            sayiText.text = yeniDeger.ToString();

            eksikKonumlar.Remove(konum); // Bu konum artık eksik değil
        }
    }

    public int OrijinalSayiyiAl(int satir, int sutun)
    {
        return orijinalSayilar[satir][sutun];
    }
}
