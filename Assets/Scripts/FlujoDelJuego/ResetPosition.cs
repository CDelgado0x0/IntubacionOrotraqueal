using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Variables para el conteo de tiempo en el aire
    private float airTime = 0f;
    private bool isCountingAirTime = false;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] collisionClips;

    [Header("Settings")]
    [SerializeField] private float minVolume = 1.2f;
    [SerializeField] private float maxVolume = 2.0f;
    [SerializeField] private float maxAirTime = 2.0f;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isCountingAirTime)
        {
            airTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCountingAirTime)
        {
            if(airTime > maxAirTime) airTime = maxAirTime;
            // Calcula el volumen según el tiempo en el aire
            float t = Mathf.Clamp01(airTime / maxAirTime);
            float volume = Mathf.Lerp(minVolume, maxVolume, t);

            // Selecciona un sonido aleatorio y lo reproduce
            if (collisionClips != null && collisionClips.Length > 0 && audioSource != null)
            {
                int index = Random.Range(0, collisionClips.Length);
                audioSource.PlayOneShot(collisionClips[index], volume);
            }

            // Resetea el conteo
            airTime = 0f;
            isCountingAirTime = false;
        }

        if (collision.gameObject.CompareTag("Suelo"))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;

            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void OnReleaseGrab()
    {
        airTime = 0f;
        isCountingAirTime = true;
    }
}
