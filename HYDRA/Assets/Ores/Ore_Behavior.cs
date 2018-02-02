using UnityEngine;
using System.Collections;

public class Ore_Behavior : MonoBehaviour
{
    public Ores oreType;
    int maxOre = 20;
    int currentOre = 20;
    float oreCurrentRegen = 0f;
    float oreRegenTime = 0f;


    public bool Touched(GameObject player)
    {
        int playerLevel = player.GetComponent <PlayerLevels>().level;

        byte chance = (byte)Random.Range(1, 101);
        
        if (playerLevel < ((int)oreType) * 15)
        {
            return false;
        }
        else
        {
            if (chance > 50 + (int)oreType * 5)
            {
                currentOre--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    void Update()
    {
        if (currentOre < maxOre)
        {
            oreCurrentRegen += Time.deltaTime;

            if (oreCurrentRegen >= oreRegenTime)
            {
                currentOre++;
                oreCurrentRegen -= oreRegenTime;
            }
        }
    }

    void Start ()
    {
        oreRegenTime = 15 + ((int)oreType * 20);
	}
	
	
}
