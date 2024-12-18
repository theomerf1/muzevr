using UnityEngine;

public class TurnstileSensor : MonoBehaviour
{
    public Turnstile turnstileScript;
    public Turnstile turnstileScript2;// Turnike d�n���n� kontrol eden script
    public string validTicketTag = "Ticket"; // Do�ru biletin tag'i

    private void OnTriggerEnter(Collider other)
    {
        // E�er nesne do�ru tag'e sahipse (bilet okutuluyorsa)
        if (other.CompareTag(validTicketTag))
        {
            Debug.Log("Bilet okundu, turnike a��l�yor!");

            // Turnikeyi d�nd�r
            if (turnstileScript != null)
            {
                turnstileScript.ActivateTurnstile();
                turnstileScript2.ActivateTurnstile();
            }

            // Opsiyonel: Bileti yok et veya kullan�lm�� hale getir
            Destroy(other.gameObject);
        }
    }
}
