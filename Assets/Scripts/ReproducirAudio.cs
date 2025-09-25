using UnityEngine;

public class SonidoMovimiento : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoMovimiento; // Arrastra aquí tu .mp3
    private AudioSource audioSource;
    private Vector3 posicionAnterior;
    private Quaternion rotacionAnterior;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sonidoMovimiento;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        posicionAnterior = transform.position;
        rotacionAnterior = transform.rotation;
    }

    void Update()
    {
        bool seMueve = transform.position != posicionAnterior || transform.rotation != rotacionAnterior;

        if (seMueve && !audioSource.isPlaying)
            audioSource.Play();
        else if (!seMueve && audioSource.isPlaying)
            audioSource.Stop();

        posicionAnterior = transform.position;
        rotacionAnterior = transform.rotation;
    }
}
 