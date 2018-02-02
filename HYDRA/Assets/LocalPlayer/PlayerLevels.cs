using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLevels : MonoBehaviour
{

    decimal experience;
    public int level;
    Dictionary<Skills, decimal> conversion;

    public void AddExp(Skills skill, decimal xp)
    {
        conversion[skill] += xp;

    }

    public void getLevel(Skills skill, decimal xp)
    {
        level = (int)Mathf.Sqrt((float)xp);
    }

    void Start()
    {
        conversion = new Dictionary<Skills, decimal>();
        conversion.Add(Skills.Mining, 0m);
        conversion.Add(Skills.Woodcutting, 0m);
        conversion.Add(Skills.Smithing, 0m);
    }

}
