using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public void LoadCanTowerGameScene()
    {
        SceneManager.LoadScene("CanTowerGameScene"); 
    }

    
    public void LoadLockKeyGameScene()
    {
        SceneManager.LoadScene("LockKeyGameScene"); 
    }

   
    public void QuitApplication()
    {
        Debug.Log("Uygulama kapatılıyor.");
        Application.Quit();
    
    }
}