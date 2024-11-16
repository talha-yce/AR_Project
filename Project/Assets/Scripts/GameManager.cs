using UnityEngine;
using TMPro; // TextMeshPro kullanımı için

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        MainMenu,
        SpawningKey,
        WaitingForKeyInteraction,
        DisplayingHint,
        WaitingForInput,
        CheckingLock,
        GameOver
    }

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private KeySpawner keySpawner;
    [SerializeField] private LockManager lockManager;
    [SerializeField] private RotaryControl rotaryControl;

    public GameState CurrentGameState { get; private set; }
    public int currentStep { get; private set; } = 0; 
    public const int MaxSteps = 4; 
    private float correctAngle;
    private string currentMathProblem;

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else if (Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    // KeySpawner'ı burada ayarlayalım
    if (keySpawner == null)
    {
        keySpawner = FindObjectOfType<KeySpawner>();
        if (keySpawner == null)
        {
            Debug.LogError("KeySpawner component not found in the scene!");
        }
    }
}

private void Start()
{
    StartCoroutine(DelayedStart());
}

private System.Collections.IEnumerator DelayedStart()
{
    yield return new WaitForSeconds(0.1f); // 0.1 saniye bekle
    StartGame();
    Debug.Log("Game started, current state: " + CurrentGameState);
}

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        Debug.Log("Game state changed to: " + newState);

        switch (newState)
        {
            case GameState.SpawningKey:
                SpawnNewKey();
                break;
            case GameState.WaitingForKeyInteraction:
                messageText.text = "Find and tap the key to get a hint!";
                break;
            case GameState.DisplayingHint:
                DisplayMathProblem();
                break;
            case GameState.WaitingForInput:
                EnableRotaryControl();
                break;
            case GameState.CheckingLock:
                CheckLockPosition();
                break;
            case GameState.GameOver:
                EndGame();
                break;
        }
    }

    private void SpawnNewKey()
{
    Debug.Log("Attempting to spawn new key...");
    if (keySpawner != null)
    {
        keySpawner.SpawnKey();
        Debug.Log("Key spawned successfully.");
        SetGameState(GameState.WaitingForKeyInteraction);
    }
    else
    {
        Debug.LogError("KeySpawner is not assigned in GameManager!");
    }
}

    private void DisplayMathProblem()
    {
        GenerateMathProblem();
        messageText.text = $"Solve: {currentMathProblem}\nRotate the lock to the correct angle.";
        SetGameState(GameState.WaitingForInput);
    }

    private void GenerateMathProblem()
{
    int[] possibleValues = { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900 };
    int resultIndex = Random.Range(0, possibleValues.Length);
    int targetValue = possibleValues[resultIndex];

    int num1, num2;
    bool isAddition = Random.value > 0.5f;

    if (isAddition)
    {
        num1 = Random.Range(0, targetValue + 1);
        num2 = targetValue - num1;
        currentMathProblem = $"{num1} + {num2}";
    }
    else
    {
        num1 = Random.Range(targetValue, 1000);
        num2 = num1 - targetValue;
        currentMathProblem = $"{num1} - {num2}";
    }

    // Kilit açısını 0-360 derece aralığına dönüştür
    correctAngle = (int)((float)targetValue / 1000 * 360);

    lockManager.SetCorrectAngle((int)correctAngle);

    Debug.Log($"Generated problem: {currentMathProblem}, Target value: {targetValue}, Lock angle: {correctAngle}");
}
    private void CheckLockPosition()
    {
        if (lockManager.IsCorrectPosition())
        {
            if (currentStep >= MaxSteps - 1)
            {
                lockManager.PlayOpenAnimation();
                SetGameState(GameState.GameOver);
            }
            else
            {
                lockManager.PlayVibrateAnimation();
                IncrementStep();
            }
        }
        else
        {
            messageText.text = "Wrong answer. Try again!";
            SetGameState(GameState.WaitingForInput);
        }
    }

    public void IncrementStep()
    {
        currentStep++;
        if (currentStep >= MaxSteps)
        {
            SetGameState(GameState.GameOver);
        }
        else
        {
            SetGameState(GameState.SpawningKey);
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame method called");
        currentStep = 0;
        SetGameState(GameState.SpawningKey);
    }

    private void EndGame()
    {
        messageText.text = "Congratulations! You've completed all steps!";
        // Implement any additional end game logic here
    }

    public void KeyCollected()
    {
        if (CurrentGameState == GameState.WaitingForKeyInteraction)
        {
            SetGameState(GameState.DisplayingHint);
        }
    }

    public void RotationCompleted()
    {
        if (CurrentGameState == GameState.WaitingForInput)
        {
            SetGameState(GameState.CheckingLock);
        }
    }

    private void EnableRotaryControl()
    {
        if (rotaryControl != null)
        {
            rotaryControl.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("RotaryControl is not assigned in GameManager");
        }
    }
}