using UnityEngine;

public class FireHealth : MonoBehaviour
{
    public float health = 100f; // Starting health
    public float burnRate = 5f; // Health decrease per second
    public Transform objectToScale; // Object to scale
    public float minScalePercent = 0.1f; // Minimum scale percentage
    public Light fireLight; // Point light to dim
    public AudioSource fireAudio; // Audio source to scale volume

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
    }

    void Update()
    {
        health -= burnRate * Time.deltaTime;
        health = Mathf.Max(health, 0f); // Prevent health from going below 0

        UpdateVisuals();
    }

    public void ResetHealth()
    {
        health = 100f;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        float healthPercent = health / 100f;
        float scaleMultiplier = Mathf.Lerp(minScalePercent, 1f, healthPercent);

        if (objectToScale != null)
        {
            objectToScale.localScale = new Vector3(
                initialScale.x * scaleMultiplier,
                initialScale.y * scaleMultiplier,
                initialScale.z
            );
        }

        if (fireLight != null)
        {
            fireLight.intensity = Mathf.Lerp(0f, 1f, healthPercent);
        }

        if (fireAudio != null)
        {
            fireAudio.volume = Mathf.Lerp(0f, initialVolume, healthPercent);
        }
    }
}
