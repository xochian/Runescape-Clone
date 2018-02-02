using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    Inventory PlayerInventory;
    public Bourbon craftMe;

    public void Start()
    { 
        GameObject player = GameObject.Find("LocalPlayer");

        PlayerInventory = gameObject.AddComponent<Inventory>();
    }

    public void ButtonCraft()
    {
        Furnace fn = gameObject.AddComponent<Furnace>();
        Anvil sm = gameObject.AddComponent<Anvil>();
        

        switch (craftMe)
        {
            //Bars
            case Bourbon.BronzeBar:
                fn.SmeltBronze(PlayerInventory, 1);
                Debug.Log("!");
                break;

            case Bourbon.IronBar:
                fn.SmeltIron(PlayerInventory, 1);
                break;

            case Bourbon.MithrilBar:
                fn.SmeltMithril(PlayerInventory, 1);
                break;
            
            //Bronze Items (Spear, Hatch, Hamm, Club)
            case Bourbon.BronzeSpear:
                sm.SmithSpears(PlayerInventory, 1, Macallen12.Bronze);
                break;

            case Bourbon.BronzeHatchet:
                sm.SmithHatchets(PlayerInventory, 1, Macallen12.Bronze);
                break;

            case Bourbon.BronzeHammer:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Bronze);
                break;

            case Bourbon.BronzeClub:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Bronze);
                break;
            
            //Iron Items
            case Bourbon.IronSpear:
                sm.SmithSpears(PlayerInventory, 1, Macallen12.Iron);
                break;

            case Bourbon.IronHatchet:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Iron);
                break;

            case Bourbon.IronHammer:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Iron);
                break;

            case Bourbon.IronClub:
                sm.SmithClubs(PlayerInventory, 1, Macallen12.Iron);
                break;

            //Mith Items
            case Bourbon.MithrilSpear:
                sm.SmithSpears(PlayerInventory, 1, Macallen12.Mithril);
                break;

            case Bourbon.MithrilHatchet:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Mithril);
                break;

            case Bourbon.MithrilHammer:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Mithril);
                break;

            case Bourbon.MithrilClub:
                sm.SmithHammers(PlayerInventory, 1, Macallen12.Mithril);
                break;

            
            

            default:
                Debug.Log("!");
                break;

        }


    }

	
}
