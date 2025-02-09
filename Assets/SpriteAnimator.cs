using System.Collections;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] frames; // Drag your sprite frames here in the Inspector
    public float frameRate = 0.1f; // Time per frame
    private SpriteRenderer spriteRenderer;
    private int currentFrame;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateSprite());
    }

    IEnumerator AnimateSprite()
    {
        while (true)
        {
            spriteRenderer.sprite = frames[currentFrame];
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
