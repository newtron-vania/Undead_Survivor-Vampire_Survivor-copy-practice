using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public List<int> SetRandomItem(int count, int weaponCount)
    {
        List<int> randomItem = new List<int>();
        int i = 0;
        while(i < count)
        {
            int rd = Random.Range(1, weaponCount);
            if (randomItem.Find(x => x == rd) == -1 || rd == 3)
                continue;
            randomItem.Add(rd);
            i++;
        }
        return randomItem;
    }
    
    
    
    
}
