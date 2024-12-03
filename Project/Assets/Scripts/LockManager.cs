using UnityEngine;

public class LockManager : MonoBehaviour
{
    [SerializeField] private int tolerance = 10; // Toleransı azalttık
    [SerializeField] private Animator lockAnimator;

    private int correctAngle;
    private int currentAngle;
    private GameManager gameManager;
    private RotaryControl rotaryControl;

    private readonly int[] validAngles = { 0, 36, 72, 108, 144, 180, 216, 252, 288, 324 };

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (lockAnimator == null)
        {
            Debug.LogError("Animator bileşeni 'LockManager' GameObject'ine atanmadı. Lütfen Animator bileşenini atayın.");
        }

        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager bulunamadı. Lütfen sahnede bir GameManager olduğundan emin olun.");
        }

        rotaryControl = FindObjectOfType<RotaryControl>();
        if (rotaryControl == null)
        {
            Debug.LogError("RotaryControl bulunamadı. Lütfen sahnede bir RotaryControl olduğundan emin olun.");
        }
    }

    public void CheckCombination()
    {
        int closestValidAngle = FindClosestValidAngle(currentAngle);
        bool isCorrect = Mathf.Abs(closestValidAngle - correctAngle) <= tolerance;

        if (isCorrect)
        {
            HandleCorrectCombination();
        }
        else
        {
            HandleWrongCombination();
        }
    }

    private int FindClosestValidAngle(int angle)
    {
        int closestAngle = validAngles[0];
        int minDifference = Mathf.Abs(angle - validAngles[0]);

        foreach (int validAngle in validAngles)
        {
            int difference = Mathf.Abs(angle - validAngle);
            if (difference < minDifference)
            {
                minDifference = difference;
                closestAngle = validAngle;
            }
        }

        return closestAngle;
    }

    private void HandleCorrectCombination()
    {
        Debug.Log("Doğru konum!");
        PlayVibrateAnimation();
        gameManager.IncrementStep();
    }

    private void HandleWrongCombination()
    {
        Debug.Log("Yanlış konum!");
        gameManager.SetGameState(GameManager.GameState.WaitingForInput);
    }

    public void PlayOpenAnimation() => PlayAnimation("Open");
    public void PlayVibrateAnimation() => PlayAnimation("Vibrate");

    private void PlayAnimation(string triggerName)
    {
        if (triggerName != null)
        {
            lockAnimator.SetTrigger(triggerName);
        }
    }

    public void SetCorrectAngle(int angle)
    {
        correctAngle = FindClosestValidAngle(NormalizeAngle(angle));
    }

    public void SetCurrentAngle(int angle)
    {
        currentAngle = angle;
    }

    private int NormalizeAngle(int angle)
    {
        angle = angle % 360;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    public int GetCorrectAngle() => correctAngle;

    public void ResetLock()
    {
        currentAngle = 0;
        if (rotaryControl != null)
        {
            rotaryControl.ResetRotation();
        }
    }

    public bool IsCorrectPosition()
    {
        int closestValidAngle = FindClosestValidAngle(currentAngle);
        return Mathf.Abs(closestValidAngle - correctAngle) <= tolerance;
    }
}
