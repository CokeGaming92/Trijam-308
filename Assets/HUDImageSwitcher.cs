using UnityEngine;
using UnityEngine.UI;

public class HUDImageSwitcher : MonoBehaviour
{
    public Image targetImage; // UI Image component to update
    public Sprite[] images = new Sprite[6]; // Array to hold 5 images
    public FloatVariable firesLeft; // Public float variable to determine which image to use

    void Update()
    {
        int index = Mathf.Clamp(Mathf.RoundToInt(firesLeft.value), 0, images.Length - 1);

        switch (index)
        {
            case 0:
                targetImage.sprite = images[0];
                break;
            case 1:
                targetImage.sprite = images[1];
                break;
            case 2:
                targetImage.sprite = images[2];
                break;
            case 3:
                targetImage.sprite = images[3];
                break;
            case 4:
                targetImage.sprite = images[4];
                break;
            case 5:
                targetImage.sprite = images[5];
                break;

            default:
                break;
        }
    }
}
