using UnityEngine;

public class ElevatorMove : MonoBehaviour
{
    public Transform elevator; // Asans�r objesi
    public Transform targetObject; // Hedef pozisyon olarak atanacak obje
    public Transform player; // Oyuncu veya VR kamera
    public float moveSpeed = 2f; // Asans�r hareket h�z�
    public Animator doorAnimator; // Kap� animat�r�
    public string doorOpenTrigger = "Open"; // Animat�rdeki tetikleyici

    private bool isMoving = false;

    void Update()
    {
        if (isMoving && targetObject != null)
        {
            // Hedef pozisyonun y�ksekli�ine do�ru hareket et
            Vector3 targetPosition = new Vector3(
                elevator.position.x,
                targetObject.position.y,
                elevator.position.z
            );

            // Asans�r� hareket ettir
            elevator.position = Vector3.MoveTowards(
                elevator.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Hedefe ula�t���nda i�lemi tamamla
            if (Vector3.Distance(elevator.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                TriggerDoorOpen();
                DetachPlayer();
            }
        }
    }

    public void StartMove()
    {
        if (!isMoving)
        {
            isMoving = true;

            // Oyuncuyu asans�r�n child'� yap
            if (player != null)
            {
                player.SetParent(elevator);
            }
            else
            {
                Debug.LogWarning("Oyuncu atanmad�!");
            }
        }
    }

    void TriggerDoorOpen()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(doorOpenTrigger); // Kap�y� a�an animasyon tetikleyici
        }
        else
        {
            Debug.LogWarning("Kap� animat�r� atanmad�!");
        }
    }

    void DetachPlayer()
    {
        // Oyuncuyu asans�rden ay�r
        if (player != null)
        {
            player.SetParent(null);
        }
    }
}
