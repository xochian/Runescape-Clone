using UnityEngine;
using System.Collections;


public class Furnace : MonoBehaviour
{
   public bool SmeltBronze(Inventory i, int input)
    {
        int control = 0;

        while (i.getQuant(Item_Categories.Ores, (int)Ores.CopperOre) > 0 && i.getQuant(Item_Categories.Ores, (int)Ores.TinOre) > 0)
        {
            if (control < input)
            {
                i.remove(Item_Categories.Ores, (int)Ores.CopperOre);
                i.remove(Item_Categories.Ores, (int)Ores.TinOre);

                i.add(Item_Categories.Bars, (int)Bars.BronzeBar, 1);
                control++;
            }
            else
            {
                break;
            }
        }

        return true;
    }

    public bool SmeltIron(Inventory i, int input)
    {
        int control = 0;

        while (i.getQuant(Item_Categories.Ores, (int)Ores.IronOre) > 0)
        {
            if (control < input)
            {
                i.remove(Item_Categories.Ores, (int)Ores.IronOre);

                i.add(Item_Categories.Bars, (int)Bars.IronBar, 1);
                control++;
            }
            else
            {
                break;
            }

        }

        return true;
    }

    public bool SmeltMithril(Inventory i, int input)
    {
        int control = 0;

        while (i.getQuant(Item_Categories.Ores, (int)Ores.MithrilOre) > 0)
        {
            if (control < input)
            {
                i.remove(Item_Categories.Ores, (int)Ores.MithrilOre);

                i.add(Item_Categories.Bars, (int)Bars.MithrilBar, 1);
                control++;
            }
            else
            {
                break;
            }

        }

        return true;
    }
}
