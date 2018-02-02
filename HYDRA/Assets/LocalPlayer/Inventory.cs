using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    
    public int buinput = 3;
    public int uinput = 1;

    public Slot[] inventory;
    Dictionary<string, int> itemNum;

    public Inventory()
    {
        inventory = new Slot[30];

        for (int i = 0; i < 30; i++)
        {
            inventory[i] = new Slot();
        }

        itemNum = new Dictionary<string, int>();
    }

    public void ChangeQuant(Item_Categories iCat, int subType, int change)
    {
        string comp = iCat + "|" + subType;

        if (!itemNum.ContainsKey(comp))
        {
            itemNum.Add(comp, 0);
        }

        itemNum[comp] += change;
    }

    private int capacity = 30;
    int occupiedSlots = 0;

    public void add(Item_Categories category, int item, int quantity)
    {
        for (int i = 0; i < capacity && quantity > 0; i++)
        {
            if (inventory[i].quant == 0)
            {
                Slot s = new Slot(category, item, 1);
                inventory[i] = s;
                ChangeQuant(category, item, 1);
                occupiedSlots++;
                quantity--;
            }
        }

        if (occupiedSlots == capacity)
        {
            Debug.Log("Maxed Capacity");
        }
    }

    public int getQuant(Item_Categories iCat, int subType)
    {
        string comp = iCat + "|" + subType;

        if (!itemNum.ContainsKey(comp))
        {
            itemNum.Add(comp, 0);
        }
        return itemNum[comp];
    }

    public Slot remove(int index)
    {
        Slot tmp = inventory[index];
        inventory[index] = new Slot();

        if (tmp != null)
        {
            ChangeQuant(tmp.cat, tmp.itm, -1);
        }
        return tmp;
    }

    public Slot remove(Item_Categories e, int itype)
    {
        for (int i = 0; i < capacity; i++)
        {
            if (inventory[i].cat == e && inventory[i].itm == itype)
            {
                inventory[i] = new Slot();
                ChangeQuant(e, itype, -1);
                return null;
            }
        }

        return null;

    }
    

    void Start()
    {
        add(Item_Categories.Logs, (int)Logs.PineLogs, 2);
        add(Item_Categories.Ores, (int)Ores.TinOre, 6);
        add(Item_Categories.Ores, (int)Ores.CopperOre, 6);
        
        Furnace f = gameObject.AddComponent<Furnace>();
        f.SmeltBronze(this, buinput);

        Anvil a = gameObject.AddComponent<Anvil>();

        Debug.Log("!");
        a.SmithSpears(this, uinput, Macallen12.Bronze);

        int i = 1;

        foreach (Slot s in inventory)
        {
            Debug.Log(i + "|" + s.cat + "|" + s.itm + "|" + s.quant);
            i++;
        }

        
    }
}