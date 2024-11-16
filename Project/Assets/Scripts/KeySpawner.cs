using UnityEngine;
using TMPro;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Transform[] spawnPoints;  // 6 spawn noktası atanacak
    [SerializeField] private TextMeshProUGUI sonucText;
    
    private GameManager gameManager;

    private void Awake()
{
    gameManager = GameManager.Instance;
    if (gameManager == null)
    {
        Debug.LogError("GameManager instance not found in KeySpawner!");
    }
}

    public void SpawnKey()
    {
        Debug.Log("SpawnKey method called in KeySpawner");
        if (gameManager == null)
    {
        Debug.LogError("GameManager is null in KeySpawner!");
        return;
    }
        if (gameManager.CurrentGameState != GameManager.GameState.SpawningKey)
        {
            Debug.LogWarning("Attempting to spawn key in incorrect game state.");
            return;
        }

        if (spawnPoints.Length == 0)
    {
        Debug.LogError("No spawn points assigned to KeySpawner!");
        return;
    }

    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
    GameObject key = Instantiate(keyPrefab, spawnPoint);

    KeyController keyController = key.GetComponent<KeyController>();
    if (keyController != null)
    {
        keyController.Initialize(this);
    }
    else
    {
        Debug.LogError("KeyController component not found on spawned key!");
    }
        
        UpdateUI();

        gameManager.SetGameState(GameManager.GameState.WaitingForKeyInteraction);
    }

    private void UpdateUI()
    {
        if (sonucText != null)
        {
            sonucText.text = $"Anahtar {gameManager.currentStep + 1}/{GameManager.MaxSteps}";
        }
        else
        {
            Debug.LogWarning("sonucText is not assigned in KeySpawner!");
        }
    }

    // Bu metod, anahtar bulunduğunda çağrılacak
    public void KeyFound()
    {
        gameManager.KeyCollected();
    }
}