using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public Sprite[] smallFronts;
    public Sprite[] bigFronts;
    public string[] names;
    public int[] powers;

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
}
