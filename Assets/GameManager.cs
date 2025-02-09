using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] FloatVariable fireCount;
    [SerializeField] FloatVariable playerHealth;
    public CanvasGroup gameoverPanel;
    public CanvasGroup winPanel;

    public AudioSource winMusic;
    public AudioSource loseMusic;
    public AudioSource ambientMusic;

    private bool gameFinished;


    private void Start()
    {
        gameFinished = false;

    }
    // Update is called once per frame
    void Update()
    {
        // do nothing if we're in an end of game state
        if (gameFinished)
            return;

        // Check if all te fires are lit
        if (fireCount.value <= 0)
        {
            OnWin();
            fireCount.value = 0;
            gameFinished = true;
        }

        // See if the player died
        else if (playerHealth.value <= 0)
        {
            OnLose();
            fireCount.value = 0;
            gameFinished = true;
        }
    }

    private void OnApplicationQuit()
    {
        // reset the fire counter
        fireCount.value = 0;
    }

    private void OnWin()
    {
        StartCoroutine(FadeCanvasGroup(winPanel, 0f, 1f, 5f));
        winMusic.Play();
        ambientMusic.Stop();
    }

    private void OnLose()
    {
        StartCoroutine(FadeCanvasGroup(gameoverPanel, 0f, 1f, 5f));
        loseMusic.Play();
        ambientMusic.Stop();
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
}
