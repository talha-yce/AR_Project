using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints;
    public TextMeshProUGUI sonucText;

    private int currentStep = 0;

    public void SpawnKey()
    {
        if (currentStep >= 4) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject key = Instantiate(keyPrefab, spawnPoint);

        int randomAngle = GenerateRandomAngle();
        key.GetComponentInChildren<RotaryControl>().SetTargetAngle(randomAngle);

        // Anahtarın altındaki Canvas'ta bulunan butonu bul
        Button button = key.GetComponentInChildren<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => UpdateInstructionText(randomAngle));
        }

        currentStep++;
    }

    private int GenerateRandomAngle()
    {
        int[] angles = { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900 };
        return angles[Random.Range(0, angles.Length)];
    }

    public void UpdateInstructionText(int angle)
    {
        sonucText.text = "Anahtar Yönergesi: " + angle;
    }
}
