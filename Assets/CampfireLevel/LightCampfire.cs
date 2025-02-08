using System.Collections;
using UnityEngine;

public class LightCampfire : MonoBehaviour
{
    public Light fireLight;
    public CampfireController campfire;
    public AudioClip bonfireAudio;
    public AudioClip sizzleAudio;

    private AudioSource bonfireAudioSource;
    private bool hasPlayedSizzle = false; // Ensures the sizzle sound plays only once

    public float maxIntensity = 5f;
    public float minIntensity = 0.5f;
    private float flickerSpeed = 0.1f;
    private float baseIntensity;

    void Start()
    {
        if (fireLight == null)
            fireLight = GetComponent<Light>();

        // Create an AudioSource for bonfire sound
        bonfireAudioSource = gameObject.AddComponent<AudioSource>();
        bonfireAudioSource.clip = bonfireAudio;
        bonfireAudioSource.loop = true;
        bonfireAudioSource.playOnAwake = false;
        bonfireAudioSource.volume = 0.5f; // Adjust volume if needed
        bonfireAudioSource.Play(); // Start playing bonfire sound

        baseIntensity = maxIntensity; // Set base intensity to max
        StartCoroutine(FlickerEffect());
    }

    void Update()
    {
        if (campfire == null) return;

        AdjustLightIntensity();
    }

    void AdjustLightIntensity()
    {
        switch (campfire.currentState)
        {
            case CampfireController.FireState.High:
                baseIntensity = maxIntensity;
                fireLight.enabled = true;
                if (!bonfireAudioSource.isPlaying)
                {
                    bonfireAudioSource.Play();
                }
                hasPlayedSizzle = false; // Reset when fire is reignited
                break;

            case CampfireController.FireState.Mid:
                baseIntensity = Mathf.Lerp(baseIntensity, maxIntensity * 0.6f, Time.deltaTime);
                fireLight.enabled = true;
                if (!bonfireAudioSource.isPlaying)
                {
                    bonfireAudioSource.Play();
                }
                break;

            case CampfireController.FireState.Low:
                baseIntensity = Mathf.Lerp(baseIntensity, maxIntensity * 0.3f, Time.deltaTime);
                fireLight.enabled = true;
                if (!bonfireAudioSource.isPlaying)
                {
                    bonfireAudioSource.Play();
                }
                break;

            case CampfireController.FireState.Dead:
                fireLight.enabled = false;
                bonfireAudioSource.Stop(); // Stop bonfire sound
                if (!hasPlayedSizzle)
                {
                    PlaySizzleSound();
                    hasPlayedSizzle = true; // Prevents multiple plays
                }
                return;
        }
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            if (campfire == null || campfire.currentState == CampfireController.FireState.Dead)
            {
                fireLight.enabled = false;
                yield break;
            }

            float flickerAmount = Random.Range(-0.3f, 0.3f); // Vary flicker range
            fireLight.intensity = Mathf.Clamp(baseIntensity + flickerAmount, minIntensity, maxIntensity);

            yield return new WaitForSeconds(flickerSpeed);
        }
    }

    void PlaySizzleSound()
    {
        if (sizzleAudio == null) return;

        GameObject sizzleObject = new GameObject("SizzleAudio");
        AudioSource sizzleAudioSource = sizzleObject.AddComponent<AudioSource>();

        sizzleAudioSource.clip = sizzleAudio;
        sizzleAudioSource.Play();
        Destroy(sizzleObject, sizzleAudio.length); // Destroy after sound finishes
    }
}
