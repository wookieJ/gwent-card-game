using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour {
    BoxCollider [] colliders;
    BoxCollider deckCollider;
    BoxCollider swordCollider;
    BoxCollider bowCollider;
    BoxCollider catapultCollider;

    void Awake()
    {
        colliders = GetComponents<BoxCollider>();
        
        deckCollider = colliders[(int)Colliders.DECK];
        swordCollider = colliders[(int)Colliders.SWORD];
        bowCollider = colliders[(int)Colliders.BOW];
        catapultCollider = colliders[(int)Colliders.CATAPULT];
    }

    public Bounds getDeckColliderBounds()
    {
        return deckCollider.bounds;
    }

    public Bounds getSwordColliderBounds()
    {
        return swordCollider.bounds;
    }

    public Bounds getBowColliderBounds()
    {
        return bowCollider.bounds;
    }

    public Bounds getCatapultColliderBounds()
    {
        return catapultCollider.bounds;
    }

    private enum Colliders { DECK, SWORD, BOW, CATAPULT };
}
