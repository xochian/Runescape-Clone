using UnityEngine;
using System.Collections;

public class Tree_Behavior : MonoBehaviour
{
    public Logs logType;
    int maxWood = 20;
    int currentWood = 20;
    float currentRegen = 0f;
    float regenTime = 0f;
    

    public bool Touched(GameObject player)
    {
        int playerLevel = player.GetComponent<PlayerLevels>().level;

        byte chance = (byte)Random.Range(1, 101);

        if (playerLevel < ((int)logType) * 15) 
        {
            return false;
        }
        else
        {
            if (chance > 40  + (int)logType*5)
            {
                currentWood--;
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
        if (currentWood < maxWood)
        {
            currentRegen += Time.deltaTime;

            if (currentRegen >= regenTime)
            {
                currentWood++;
                currentRegen -= regenTime;
            }
        }
    }

    void Start()
    {
        regenTime = 10 + ((int)logType) * 20;
    }
}
