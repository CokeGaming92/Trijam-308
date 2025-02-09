using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public AudioClip clip;
    
    public float rotationSpeed = 50f;
    public float floatSpeed = 1f;
    public float floatHeight = 0.5f;


    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        RotatePickup();
        FloatPickup();
    }

    void RotatePickup()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void FloatPickup()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {


      
       Destroy(gameObject);
        PlaySound(clip);
 
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject pickupAudio = new GameObject("AudioPickup");
        AudioSource pickupAudioSource = pickupAudio.AddComponent<AudioSource>();
        pickupAudioSource.clip = clip;
        pickupAudioSource.Play();
        Destroy(pickupAudio, clip.length);
    }
}
