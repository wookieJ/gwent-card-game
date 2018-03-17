using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour {
    BoxCollider [] colliders;
    BoxCollider deckCollider;
    BoxCollider swordCollider;

    void Awake()
    {
        colliders = GetComponents<BoxCollider>();
        
        deckCollider = colliders[(int)Colliders.DECK];
        swordCollider = colliders[(int)Colliders.SWORD];
    }

    public Bounds getDeckColliderBounds()
    {
        return deckCollider.bounds;
    }

    public Bounds getSwordColliderBounds()
    {
        return swordCollider.bounds;
    }

    private enum Colliders { DECK, SWORD };
}
