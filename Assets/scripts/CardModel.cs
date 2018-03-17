using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public Sprite[] fronts;
    public string[] names;

    public Sprite getFront(int index)
    {
        return fronts[index];
    }
}
