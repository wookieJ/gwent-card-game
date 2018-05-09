using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour {
    BoxCollider [] colliders;
    BoxCollider deckCollider;
    BoxCollider swordCollider;
    BoxCollider bowCollider;
    BoxCollider trebuchetCollider;
    BoxCollider special1Collider;
    BoxCollider special2Collider;
    BoxCollider sword2Collider;

    void Awake()
    {
        colliders = GetComponents<BoxCollider>();
        
        deckCollider = colliders[(int)CardGroup.DECK];
        swordCollider = colliders[(int)CardGroup.SWORD];
        bowCollider = colliders[(int)CardGroup.BOW];
        trebuchetCollider = colliders[(int)CardGroup.TREBUCHET];
        special1Collider = colliders[(int)CardGroup.SPECIAL1];
        special2Collider = colliders[(int)CardGroup.SPECIAL2];
        sword2Collider = colliders[(int)CardGroup.SWORD2];
    }

    /// <summary>
    /// Get player deck's collision bounds
    /// </summary>
    /// <returns>Deck's collision bounds</returns>
    public Bounds getDeckColliderBounds()
    {
        return deckCollider.bounds;
    }

    /// <summary>
    /// Get sword group collision bounds
    /// </summary>
    /// <returns>Sword group collision bounds</returns>
    public Bounds getSwordColliderBounds()
    {
        return swordCollider.bounds;
    }

    /// <summary>
    /// Get bow group collision bounds
    /// </summary>
    /// <returns>Bow group collision bounds</returns>
    public Bounds getBowColliderBounds()
    {
        return bowCollider.bounds;
    }

    /// <summary>
    /// Get trebuchet group collision bounds
    /// </summary>
    /// <returns>Trebuchet group bounds</returns>
    public Bounds getTrebuchetColliderBounds()
    {
        return trebuchetCollider.bounds;
    }

    /// <summary>
    /// Get special 1 group collision bounds
    /// </summary>
    /// <returns>Special 1 group bounds</returns>
    public Bounds getSpecial1ColliderBounds()
    {
        return special1Collider.bounds;
    }

    /// <summary>
    /// Get special 2 group collision bounds
    /// </summary>
    /// <returns>Special 2 group bounds</returns>
    public Bounds getSpecial2ColliderBounds()
    {
        return special2Collider.bounds;
    }

    /// <summary>
    /// Get sword in player 2 group collision bounds
    /// </summary>
    /// <returns>Sword in player 2 group bounds</returns>
    public Bounds getSword2ColliderBounds()
    {
        return sword2Collider.bounds;
    }

    /// <summary>
    /// Get deck collider center vector
    /// </summary>
    /// <returns>deck collider center vector</returns>
    public Vector3 getDeckCenterVector()
    {
        return deckCollider.center;
    }

    /// <summary>
    /// Get sword group collider center vector
    /// </summary>
    /// <returns>sworr group collider center vector</returns>
    public Vector3 getSwordsCenterVector()
    {
        return swordCollider.center;
    }

    /// <summary>
    /// Get bow group collider center vector
    /// </summary>
    /// <returns>bow group collider center vector</returns>
    public Vector3 getBowsCenterVector()
    {
        return bowCollider.center;
    }

    /// <summary>
    /// Get trebuchet group collider center vector
    /// </summary>
    /// <returns>trebuchet group collider center vector</returns>
    public Vector3 getTrebuchetsCenterVector()
    {
        return trebuchetCollider.center;
    }

    /// <summary>
    /// Get special 1 box collider center vector
    /// </summary>
    /// <returns>special 1 box collider center vector</returns>
    public Vector3 getSpecial1CenterVector()
    {
        return special1Collider.center;
    }

    /// <summary>
    /// Get special 2 box collider center vector
    /// </summary>
    /// <returns>special 2 box collider center vector</returns>
    public Vector3 getSpecial2CenterVector()
    {
        return special2Collider.center;
    }

    /// <summary>
    /// Get sword 2 box collider center vector
    /// </summary>
    /// <returns>sword 2 box collider center vector</returns>
    public Vector3 getSword2CenterVector()
    {
        return sword2Collider.center;
    }

    /// <summary>
    /// Defined typed of card groups
    /// </summary>
    private enum CardGroup { DECK, SWORD, BOW, TREBUCHET, SPECIAL1, SPECIAL2, SWORD2};
}