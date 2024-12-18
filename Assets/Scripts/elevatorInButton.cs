using UnityEngine;

public class ElevatorInButton : MonoBehaviour
{
    // Asans�r�n GameObject'i
    public GameObject elevatorObject;  // Asans�r�n GameObject'ini buraya atayaca��z
    // Oyuncu veya VR kamera GameObject'i
    public GameObject player;  // Oyuncu veya VR Kamera GameObject'i

    public void OnButtonPressed()
    {
        // ElevatorObject'in �zerindeki ElevatorMove component'ini al
        ElevatorMove elevatorMove = elevatorObject.GetComponent<ElevatorMove>();

        if (elevatorMove != null)
        {
            elevatorMove.StartMove();  // Asans�r�n hareket etmesini ba�lat
            Debug.Log("Butona bas�ld�, asans�r hareket etmeye ba�lad�.");
        }
        else
        {
            Debug.LogError("ElevatorMove script'i bulunamad�!");
        }
    }
}
