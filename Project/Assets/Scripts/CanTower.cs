using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanTower : MonoBehaviour
{
    public GameObject KutuPrefab;
    public float yatayBosluk = 0.2f;
    public float dikeyBosluk = 0.21f;
    public int satirSayisi = 5;
    public Material[] renkler; // 3 farklı renk materyalini buraya ekleyin: kırmızı, yeşil, mavi

    public List<List<int>> sayilar = new List<List<int>>();
    public List<GameObject> kutuListesi = new List<GameObject>();

    private void Start()
    {
        RastgeleSayilarOlustur();
        KutuPiramitOlustur();
        EksikSayilariBelirle();
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
                    int toplam = altSol + altSag;
                    satirSayilari.Add(toplam);
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
                float xPozisyon = sutun * yatayBosluk - (sayilar[satir].Count - 1) * yatayBosluk / 2;
                float yPozisyon = satir * dikeyBosluk;
                Vector3 pozisyon = new Vector3(xPozisyon, yPozisyon, 0);

                GameObject kutu = Instantiate(KutuPrefab, pozisyon, Quaternion.identity, transform);
                kutuListesi.Add(kutu);

                TMP_Text sayiText = kutu.GetComponentInChildren<TMP_Text>();
                if (sayiText != null)
                {
                    sayiText.text = sayilar[satir][sutun].ToString();
                    sayiText.color = Color.black; // Yazı rengini siyah yap
                    sayiText.fontStyle = FontStyles.Bold; // Yazı stilini kalın yap
                }

                // Center nesnesine erişmek için güncelleme
                Transform center = kutu.transform.Find("TenekeKutu/Center");
                if (center != null && renkler.Length > 0)
                {
                    int renkIndeksi = Random.Range(0, renkler.Length);
                    Debug.Log($"Renk İndeksi: {renkIndeksi}"); // Renk indeksini kontrol et
                    center.GetComponent<Renderer>().material = renkler[renkIndeksi];
                }
            }
        }
    }

    private void EksikSayilariBelirle()
    {
        int eksikSayilar = 4;
        List<Vector2Int> eksikKonumlar = new List<Vector2Int>();

        for (int i = 0; i < eksikSayilar; i++)
        {
            int satir = Random.Range(0, sayilar.Count - 1);
            int sutun = Random.Range(0, sayilar[satir].Count);

            Vector2Int konum = new Vector2Int(satir, sutun);
            if (!eksikKonumlar.Contains(konum))
            {
                eksikKonumlar.Add(konum);

                GameObject kutu = kutuListesi[satir * (satir + 1) / 2 + sutun];
                TMP_Text sayiText = kutu.GetComponentInChildren<TMP_Text>();
                if (sayiText != null)
                {
                    sayiText.text = "?";
                    sayiText.color = Color.black; // Yazı rengini siyah yap
                    sayiText.fontStyle = FontStyles.Bold; // Yazı stilini kalın yap
                }
            }
        }
    }
}