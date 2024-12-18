using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    // Asans�r�n Animator'�n� referans al�yoruz
    public Animator elevatorAnimator;
    public string animationTrigger = "open";  // Animator trigger parametre ismi

    // Kap� animasyonunu tetiklemek i�in fonksiyon
    public void OpenElevatorDoors()
    {
        // Animator'dan trigger'� tetikle
        if (elevatorAnimator != null)
        {
            elevatorAnimator.SetTrigger(animationTrigger);  // Trigger'� �al��t�r
            Debug.Log("Asans�r kap�lar� a��l�yor!");
        }
        else
        {
            Debug.LogError("Animator referans� eksik!");
        }
    }
}
