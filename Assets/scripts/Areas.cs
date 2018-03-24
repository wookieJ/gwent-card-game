using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour {
    BoxCollider [] colliders;
    BoxCollider deckCollider;
    BoxCollider swordCollider;
    BoxCollider bowCollider;
    BoxCollider trebuchetCollider;

    void Awake()
    {
        colliders = GetComponents<BoxCollider>();
        
        deckCollider = colliders[(int)CardGroup.DECK];
        swordCollider = colliders[(int)CardGroup.SWORD];
        bowCollider = colliders[(int)CardGroup.BOW];
        trebuchetCollider = colliders[(int)CardGroup.TREBUCHET];
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
    /// Defined typed of card groups
    /// </summary>
    private enum CardGroup { DECK, SWORD, BOW, TREBUCHET };
}