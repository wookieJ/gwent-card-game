using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public Sprite[] smallFronts;
    public Sprite[] bigFronts;
    public string[] names;
    public int[] powers;
    public int[] groups;
    public int[] isSpecial;
    /* 
     * isSpecial table of values:
     * [1] - gold card
     * [2] - spy card
     * [3] - manekin card
     * [4] - destroy card
     * [5] - weather card
     */

    public Sprite getSmallFront(int index)
    {
        return smallFronts[index];
    }

    public Sprite getBigFront(int index)
    {
        return bigFronts[index];
    }

    public string getName(int index)
    {
        return names[index];
    }

    public int getPower(int index)
    {
        return powers[index];
    }

    public int getGroup(int index)
    {
        return groups[index];
    }

    public int getIsSpecial(int index)
    {
        return isSpecial[index];
    }
}
