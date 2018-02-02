using UnityEngine;
using System.Collections;

public class Anvil : MonoBehaviour
{
    public bool SmithSpears(Inventory i, int input, Macallen12 scotch)
    {
        int control = 0;

        while (i.getQuant(Item_Categories.Bars, (int)scotch) >= 1 && i.getQuant(Item_Categories.Logs, (int)scotch) >= 2)
        {
            if (control < input)
            {
         
                i.remove(Item_Categories.Bars, (int)scotch);

                i.remove(Item_Categories.Logs, (int)scotch);
                i.remove(Item_Categories.Logs, (int)scotch);

                i.add(Item_Categories.Spears, (int)scotch, 1);

                control++;
            }
            else
            {
                break;
            }

        }

        return true;
    }

    public bool SmithHatchets(Inventory i, int input, Macallen12 scotch)
    {
        int control = 0;

        while (i.getQuant(Item_Categories.Bars, (int)scotch) >= 1 && i.getQuant(Item_Categories.Logs, (int)scotch) >= 1)
        {//Changes to 1:1
            if (control < input)
            {
                i.remove(Item_Categories.Bars, (int)scotch);

                i.remove(Item_Categories.Logs, (int)scotch);

                i.add(Item_Categories.Hatchets, (int)scotch, 1);
                control++;
            }
            else
            {
                break;
            }
        }

        return true;
    }

    public bool SmithHammers(Inventory i, int input, Macallen12 scotch)
    {//Stays same 2:1
        int control = 0;

        while (i.getQuant(Item_Categories.Bars, (int)scotch) >= 2 && i.getQuant(Item_Categories.Logs, (int)scotch) >= 1)
        {
            if (control < input)
            {
                i.remove(Item_Categories.Bars, (int)scotch);
                i.remove(Item_Categories.Bars, (int)scotch);

                i.remove(Item_Categories.Logs, (int)scotch);

                i.add(Item_Categories.Hammers, (int)scotch, 1);
                control++;
            }
            else
            {
                break;
            }
        }

        return true;
    }

    public bool SmithClubs(Inventory i, int input, Macallen12 scotch)
    {//Changes to 2:2
        int control = 0;
        while (i.getQuant(Item_Categories.Bars, (int)scotch) >= 2 && i.getQuant(Item_Categories.Logs, (int)scotch) >= 2)
        {
            if (control < input)
            {
                i.remove(Item_Categories.Bars, (int)scotch);
                i.remove(Item_Categories.Bars, (int)scotch);

                i.remove(Item_Categories.Logs, (int)scotch);
                i.remove(Item_Categories.Logs, (int)scotch);

                i.add(Item_Categories.Clubs, (int)scotch, 1);
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

    