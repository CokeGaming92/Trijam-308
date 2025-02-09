using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    private float targetIntensity;

    void Start()
    {
        if (fireLight != null)
        {
            targetIntensity = fireLight.intensity;
            InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
        }
    }

    void Flicker()
    {
        if (fireLight != null)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            fireLight.intensity = Mathf.Lerp(fireLight.intensity, targetIntensity, 0.5f);
        }
    }
}