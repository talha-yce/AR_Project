// KeySpawner.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints;  // 6 spawn noktası atanacak
    public TextMeshProUGUI sonucText;
    
    private int currentStep = 0;
    private LockManager lockManager;

    void Start()
    {
        lockManager = FindObjectOfType<LockManager>();
        SpawnKey();
    }

    public void SpawnKey()
    {
        if (currentStep >= 4) return; // 4 key'den sonra spawn etme

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject key = Instantiate(keyPrefab, spawnPoint);
        
        int randomAngle = GenerateRandomAngle();
        key.GetComponentInChildren<RotaryControl>().SetTargetAngle(randomAngle);

        currentStep++;
    }

    private int GenerateRandomAngle()
    {
        int[] angles = { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900 };
        return angles[Random.Range(0, angles.Length)];
    }

}