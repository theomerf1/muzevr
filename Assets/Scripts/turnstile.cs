using UnityEngine;
using System.Collections;

public class Turnstile : MonoBehaviour
{
    public Transform rotatingPart; // Turnikenin d�nen k�sm�
    public GameObject targetObject; // Pozisyon ve rotasyon atanacak hedef GameObject
    public float rotationAngle = 90f; // D�nd�r�lecek a��
    public float rotationSpeed = 10f; // D�n�� h�z�
    private bool isActivated = false;

    private Quaternion initialRotation; // Ba�lang�� rotasyonu
    private Quaternion targetRotation;  // Hedef rotasyonu

    public void ActivateTurnstile()
    {
        if (!isActivated && rotatingPart != null && targetObject != null)
        {
            isActivated = true;

            // 1. �zerinde oldu�u nesnenin pozisyonunu targetObject'in pozisyonuna ata
            transform.position = targetObject.transform.position;

            // 2. Rotasyonu ayarla (x = 0, y = -90, z = 0)
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);

            // 3. Turnikenin d�n��� i�in ba�lang�� ve hedef rotasyonlar� belirle
            initialRotation = rotatingPart.rotation;
            targetRotation = initialRotation * Quaternion.Euler(0, rotationAngle, 0);

            StartCoroutine(RotateTurnstile());
        }
    }

    private IEnumerator RotateTurnstile()
    {
        float elapsedTime = 0f; // Ge�en s�re

        while (elapsedTime < (rotationAngle / rotationSpeed))
        {
            // Rotasyonu Lerp ile yumu�at�lm�� bir �ekilde d�nd�r
            rotatingPart.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / (rotationAngle / rotationSpeed));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // D�nd�rme i�lemi tamamland���nda hedef rotasyona tam olarak ayarla
        rotatingPart.rotation = targetRotation;

        Debug.Log("Turnike a��ld�!");
    }
}
