using UnityEngine;

public class KeyController : MonoBehaviour
{
    
    private GameManager gameManager;
    private AudioSource audioSource;
    private KeySpawner keySpawner;

    public void Initialize(KeySpawner spawner)
    {
        keySpawner = spawner;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager bulunamadı. Lütfen sahnede bir GameManager olduğundan emin olun.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnMouseDown()
    {
        if (gameManager != null && gameManager.CurrentGameState == GameManager.GameState.WaitingForKeyInteraction)
        {
            ActivateKey();
        }
    }

    private void ActivateKey()
    {
        Debug.Log("Anahtara tıklandı!");
        
        CollectKey();
        Destroy(gameObject, 1f);
    }

    private void CollectKey()
    {
        if (keySpawner != null)
        {
            keySpawner.KeyFound();
        }
        else
        {
            Debug.LogWarning("KeySpawner referansı bulunamadı!");
            gameManager?.KeyCollected();
        }
    }

    
}