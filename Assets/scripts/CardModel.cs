using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public Sprite[] fronts;
    public string[] names;
    public int[] powers;

    public Sprite getFront(int index)
    {
        return fronts[index];
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
