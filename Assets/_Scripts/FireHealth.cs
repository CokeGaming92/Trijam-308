using UnityEngine;

public class FireHealth : MonoBehaviour
{
    public FloatVariable playerHealth;
    public float startingHealth = 100f; // Starting health
    public float burnRate = 5f; // Health decrease per second
    public Transform objectToScale; // Object to scale
    public float minScalePercent = 0.1f; // Minimum scale percentage
    public Light fireLight; // Point light to dim
    public Light fireLight2; // 2nd Point light to dim
    public AudioSource fireAudio; // Audio source to scale volume
    public ParticleSystem redPuff;

    private Vector3 initialScale;
    private float initialVolume;

    void Start()
    {
        

        if (objectToScale != null)
        {
            initialScale = objectToScale.localScale;
        }

        if (fireAudio != null)
        {
            initialVolume = fireAudio.volume;
        }

        ResetHealth();
    }

    void Update()
    {

        playerHealth.value -= burnRate * Time.deltaTime;
        playerHealth.value = Mathf.Max(playerHealth.value, 0f); // Prevent health from going below 0

        UpdateVisuals();
    }

    public void ResetHealth()
    {
        playerHealth.value = startingHealth;

        redPuff.Play();

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        float healthPercent = playerHealth.value / startingHealth;
        float scaleMultiplier = Mathf.Lerp(minScalePercent, 1f, healthPercent);

        

        if (objectToScale != null)
        {
            objectToScale.localScale = new Vector3(
                initialScale.x * scaleMultiplier,
                initialScale.y * scaleMultiplier,
                initialScale.z * scaleMultiplier
            );
        }


        if (fireLight != null)
        {
            fireLight.intensity = Mathf.Lerp(0.05f, 1f, healthPercent);
        }

        if (fireLight2 != null)
        {
            fireLight2.intensity = Mathf.Lerp(0.05f, 1f, healthPercent);
        }

        if (fireAudio != null)
        {
            fireAudio.volume = Mathf.Lerp(0f, initialVolume, healthPercent);
        }
    }
}
