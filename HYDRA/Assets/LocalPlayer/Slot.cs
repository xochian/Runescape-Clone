using UnityEngine;
using System.Collections;

public class Slot
{
    public Item_Categories cat;
    public int itm;
    public int quant;

    public Slot()
    {
        cat = Item_Categories.Nothing;
        itm = 0;
        quant = 0;
    }
    public Slot(Item_Categories category, int item, int quantity)
    {
        cat = category;
        itm = item;
        quant = quantity;
    }

}