using UnityEngine;

public class TargetEventHandler : MonoBehaviour
{
    // Kutu objesi için bir referans
    public GameObject kutuObjesi;

    // Hedef bulunduğunda çağrılacak metot
    public void OnTargetFound()
    {
        Debug.Log("Target Found!");
        // Kutu objesini görünür yap
        if (kutuObjesi != null)
        {
            kutuObjesi.SetActive(true);
        }
    }

    // Hedef kaybolduğunda çağrılacak metot
    public void OnTargetLost()
    {
        Debug.Log("Target Lost!");
        // Kutu objesini görünmez yap
        if (kutuObjesi != null)
        {
            kutuObjesi.SetActive(false);
        }
    }
}
