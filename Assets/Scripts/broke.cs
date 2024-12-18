using System.Collections.Generic;
using UnityEngine;

public class Broke : MonoBehaviour
{
    [Header("K�r�lma Ayarlar�")]
    public AudioClip breakSound; // K�r�lma sesi
    public float breakForceThreshold = 10f; // K�r�lma i�in gereken minimum kuvvet

    private AudioSource audioSource;
    private bool isBroken = false; // Nesne zaten k�r�ld� m�?

    private void Start()
    {
        // Ses kayna�� ekle
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        if (breakSound != null)
        {
            audioSource.clip = breakSound;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken) return; // Nesne zaten k�r�ld�ysa i�lem yapma

        float collisionForce = collision.relativeVelocity.magnitude;

        if (collisionForce >= breakForceThreshold)
        {
            BreakObject();
        }
    }

    private void BreakObject()
    {
        isBroken = true;

        // Par�alar� olu�tur
        List<PartMesh> parts = SplitMesh();
        foreach (PartMesh part in parts)
        {
            part.MakeGameObject(this);
        }

        // K�r�lma sesini �al
        if (breakSound != null)
        {
            PlayBreakSound();
        }
    }

    private void PlayBreakSound()
    {
        // Ge�ici bir nesne olu�tur ve ses �al
        GameObject tempAudioObject = new GameObject("BreakSound");
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
        tempAudioSource.clip = breakSound;
        tempAudioSource.Play();

        // Ses tamamland�ktan sonra ge�ici nesneyi yok et
        Destroy(tempAudioObject, breakSound.length);
    }

    private List<PartMesh> SplitMesh()
    {
        Mesh originalMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = originalMesh.vertices;
        Vector3[] normals = originalMesh.normals;
        Vector2[] uv = originalMesh.uv;
        int[] triangles = originalMesh.triangles;

        // Rastgele par�alara ay�rmak i�in
        int numParts = Random.Range(5, 15); // 5 ile 15 aras�nda rastgele par�a say�s�
        List<PartMesh> parts = new List<PartMesh>();

        for (int i = 0; i < numParts; i++)
        {
            List<Vector3> partVertices = new List<Vector3>();
            List<Vector3> partNormals = new List<Vector3>();
            List<Vector2> partUV = new List<Vector2>();
            List<int> partTriangles = new List<int>();

            for (int j = 0; j < triangles.Length; j += 3)
            {
                // Rastgele ��gen se�imi
                if (Random.value > 0.5f)
                {
                    int index1 = triangles[j];
                    int index2 = triangles[j + 1];
                    int index3 = triangles[j + 2];

                    partTriangles.Add(partVertices.Count);
                    partTriangles.Add(partVertices.Count + 1);
                    partTriangles.Add(partVertices.Count + 2);

                    partVertices.Add(vertices[index1]);
                    partVertices.Add(vertices[index2]);
                    partVertices.Add(vertices[index3]);

                    partNormals.Add(normals[index1]);
                    partNormals.Add(normals[index2]);
                    partNormals.Add(normals[index3]);

                    partUV.Add(uv[index1]);
                    partUV.Add(uv[index2]);
                    partUV.Add(uv[index3]);
                }
            }

            if (partTriangles.Count > 0)
            {
                PartMesh partMesh = new PartMesh
                {
                    Vertices = partVertices.ToArray(),
                    Normals = partNormals.ToArray(),
                    UV = partUV.ToArray(),
                    Triangles = new int[1][] { partTriangles.ToArray() }
                };

                parts.Add(partMesh);
            }
        }

        return parts;
    }
}

public class PartMesh
{
    public Vector3[] Vertices;
    public Vector2[] UV;
    public Vector3[] Normals;
    public int[][] Triangles;
    public Bounds Bounds;
    public GameObject GameObject;

    public void MakeGameObject(Broke destroyer)
    {
        GameObject = new GameObject("PartMesh");
        GameObject.transform.position = destroyer.transform.position;
        GameObject.transform.rotation = destroyer.transform.rotation;

        Mesh mesh = new Mesh
        {
            vertices = Vertices,
            uv = UV,
            normals = Normals
        };

        for (int i = 0; i < Triangles.Length; i++)
        {
            mesh.SetTriangles(Triangles[i], i);
        }

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        var meshFilter = GameObject.AddComponent<MeshFilter>();
        var meshRenderer = GameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.materials = destroyer.GetComponent<MeshRenderer>().materials;

        Rigidbody rigidbody = GameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = true;

        // Par�alar�n belirli bir s�re sonra yok edilmesi
        Object.Destroy(GameObject, 10f); // 10 saniye sonra par�alar� yok et
    }
}
