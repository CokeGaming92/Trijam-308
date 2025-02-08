using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public AudioClip playClip;
    public AudioClip fireCrackClip;
    public CanvasGroup fadePanel;
    public AudioSource bgMusic;

    void Start()
    {
        // Set initial fade panel state
        fadePanel.alpha = 0;
        fadePanel.gameObject.SetActive(false);
        
        // Add listeners for the buttons
        playButton.onClick.AddListener(OnPlayClick);
    }

    public void OnMouseOverPlay()
    {
        Debug.Log("Mouse is over Play button.");
        PlaySound(playClip);
    }

    public void OnMouseExitPlay()
    {
        Debug.Log("Mouse exited Play button.");
        PlaySound(playClip);
    }

    void OnPlayClick()
    {
        Debug.Log("Play button clicked.");
        StartCoroutine(FadeOutMusicAndStartSequence());
    }

    IEnumerator FadeOutMusicAndStartSequence()
    {
        StartCoroutine(FadeOutAudio(bgMusic, 1f));
        StartCoroutine(FadeInAndPlayFireCrack());
        yield return new WaitForSeconds(fireCrackClip.length);
        SceneManager.LoadSceneAsync("GameScene");
    }

    IEnumerator FadeInAndPlayFireCrack()
    {
        fadePanel.gameObject.SetActive(true);
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0f, 1f, 1f));
        PlaySound(fireCrackClip);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }

    IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }

    void PlaySound(AudioClip clip)
    {
        GameObject audioGO = new GameObject("ButtonAudio");
        AudioSource audioSource = audioGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioGO, clip.length);
    }
}
