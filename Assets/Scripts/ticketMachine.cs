using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TicketMachine : MonoBehaviour
{
    public GameObject ticketPrefab; // �retilecek biletin prefab'�
    public Transform ticketSpawnPoint; // Biletin ��kaca�� yer
    public AudioSource printSound; // Bilet ��karma sesi

    public void GenerateTicket()
    {
        if (ticketPrefab != null && ticketSpawnPoint != null)
        {
            // Bileti olu�tur
            GameObject spawnedTicket = Instantiate(ticketPrefab, ticketSpawnPoint.position, Quaternion.Euler(90, 90, 0));

            // Tag'i ayarla
            spawnedTicket.tag = "Ticket";

            // �l�e�i ayarla
            spawnedTicket.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            // Rigidbody ekle ve kinematik olarak ba�lat
            Rigidbody rb = spawnedTicket.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedTicket.AddComponent<Rigidbody>();
            }
            rb.isKinematic = true; // Fizik etkilerini kapat

            // Collider ekle (e�er yoksa)
            if (spawnedTicket.GetComponent<Collider>() == null)
            {
                BoxCollider collider = spawnedTicket.AddComponent<BoxCollider>();
                collider.size = Vector3.one * 0.1f;
                collider.isTrigger = false;
            }

            // XRGrabInteractable ekle
            XRGrabInteractable grabInteractable = spawnedTicket.GetComponent<XRGrabInteractable>();
            if (grabInteractable == null)
            {
                grabInteractable = spawnedTicket.AddComponent<XRGrabInteractable>();
            }

            // Grab ba�lad���nda Rigidbody'yi kinematikten ��kar
            grabInteractable.selectEntered.AddListener((interactor) =>
            {
                rb.isKinematic = false; // Fizik etkilerini a�
            });

            // Ses �al
            if (printSound != null)
            {
                printSound.Play();
            }

            Debug.Log("Bilet �retildi, tag atand� ve XRGrabInteractable ile haz�r!");
        }
    }
}
