using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class script_switchSprites : MonoBehaviour
{
    Sprite oldSprite;
    public Sprite newSprite;

    Image uiImage;

    void Start()
    {
        uiImage = GetComponent<Image>();
        oldSprite = uiImage.sprite;
    }

    public void NewSprite()
    {
        uiImage.sprite = newSprite;
    }

    public void OldSprite()
    {
        uiImage.sprite = oldSprite;
    }
}
