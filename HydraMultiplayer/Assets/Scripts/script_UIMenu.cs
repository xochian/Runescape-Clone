using UnityEngine;
using System.Collections;
using System;

public class script_UIMenu : MonoBehaviour
{
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public float topY = 200;
    float bottomY;

    public float menuSpeed = 1.0f;

    float targetY, startY;
    public MenuItems currentItem = MenuItems.None;

    public GameObject inventoryUI, skillsUI, socialUI, settingsUI;

    public void ChangeItem(string newItemS)
    {
        MenuItems newItem = ParseEnum<MenuItems>(newItemS);

        if (newItem == currentItem)
        {
            if (newItem != MenuItems.None)
            {
                StartMove(false);
                currentItem = MenuItems.None; 
            }

            return;
        }


        if (currentItem == MenuItems.None)
        {
            StartMove(true);
        }
        else if(newItem == MenuItems.None)
        {
            StartMove(false);
        }

        currentItem = newItem;

        inventoryUI.active = currentItem == MenuItems.Item;
        skillsUI.active = currentItem == MenuItems.Skill;
        socialUI.active = currentItem == MenuItems.Social;
        settingsUI.active = currentItem == MenuItems.Settings;
    }



    void Start()
    {
        currentItem = MenuItems.None;
        bottomY = transform.position.y;
    }

    public void StartMove(bool rising)
    {
        if(rising)
        {
            targetY = topY;
            startY = bottomY;
        }
        else
        {
            targetY = bottomY;
            startY = topY;
        }

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        for (float f = 0; f <= 1.0f; f += Time.deltaTime*menuSpeed)
        {
            float curY = Mathf.Lerp(startY, targetY, f);
            transform.position = new Vector3(transform.position.x, curY, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    void PopUp(MenuItems item)
    {
        switch(item)
        {
            case MenuItems.Item:
                break;

            case MenuItems.Settings:
                break;

            case MenuItems.Skill:
                break;

            case MenuItems.Social:
                break;

            default:
                break;
        }
    }
}
