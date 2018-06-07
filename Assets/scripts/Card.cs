using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardName;
    public int power;
    public int index;
    public int group;
    private bool active = false;
    public int isSpecial;
    public bool weatherEffect = false;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D cardColider;

    private GameObject cardModelGameObject;
    private CardModel cardModel;
    
    void Awake()
    {
        cardModelGameObject = GameObject.Find("CardModel");
        cardModel = cardModelGameObject.GetComponent<CardModel>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cardColider = GetComponent<BoxCollider2D>();
        cardColider.size = new Vector2(1f, 1.45f);
    }

    /// <summary>
    /// Name of card
    /// </summary>
    /// <returns>name of card</returns>
    public string getCardName()
    {
        return this.cardName;
    }

    /// <summary>
    /// Set name of card
    /// </summary>
    /// <param name="cardName">New card's name</param>
    public void setCardName(string cardName)
    {
        this.cardName = cardName;
    }

    /// <summary>
    /// Power of card
    /// </summary>
    /// <returns>power of card</returns>
    public int getPower()
    {
        return this.power;
    }

    /// <summary>
    /// Set power of card
    /// </summary>
    /// <param name="power">New card's power</param>
    public void setPower(int power)
    {
        this.power = power;
    }

    /// <summary>
    /// Set index of card
    /// </summary>
    /// <param name="index">new card's index</param>
    public void setIndex(int index)
    {
        this.index = index;
        this.group = cardModel.groups[index];
    }

    /// <summary>
    /// Index of card
    /// </summary>
    /// <returns>Card's index</returns>
    public int getIndex()
    {
        return this.index;
    }

    /// <summary>
    /// Set new state of card
    /// </summary>
    /// <param name="state">New card's state (true or false)</param>
    public void setActive(bool state)
    {
        this.active = state;
    }

    /// <summary>
    /// Check if card is active
    /// </summary>
    /// <returns>True if card is active, false otherwise</returns>
    public bool isActive()
    {
        return this.active;
    }

    /// <summary>
    /// Get card's collision bounds
    /// </summary>
    /// <returns>Card's collision bounds</returns>
    public Bounds getBounds()
    {
        return this.cardColider.bounds;
    }

    /// <summary>
    /// Get card's name and it's power in string
    /// </summary>
    /// <returns>card's name and it's power in string</returns>
    public string toString()
    {
        return this.cardName + " card with power " + this.power;
    }

    /// <summary>
    /// Set new card's front image
    /// </summary>
    /// <param name="index">New card's front image</param>
    public void setFront(int index)
    {
        spriteRenderer.sprite = cardModel.getSmallFront(index);
    }

    /// <summary>
    /// Set new card's front image from big cards set
    /// </summary>
    /// <param name="index">index of new big front image</param>
    public void setBigFront(int index)
    {
        if (index == 0)
            spriteRenderer.sprite = null;
        else
            spriteRenderer.sprite = cardModel.getBigFront(index - 1);
    }

    /// <summary>
    /// Set card's group
    /// </summary>
    /// <param name="group">1 - sword, 2 - bow, 3 - trebuchet</param>
    public void setGroup(int group)
    {
        this.group = group;
    }

    /// <summary>
    /// Get card's group
    /// </summary>
    /// <returns>1 - sword, 2 - bow, 3 - trebuchet</returns>
    public int getGroup()
    {
        return this.group;
    }

    /// <summary>
    /// Get a cardModel object
    /// </summary>
    /// <returns>cardModel object</returns>
    public CardModel getCardModel()
    {
        return this.cardModel;
    }

    /// <summary>
    /// Get is Special status of card
    /// </summary>
    /// <returns>special group value of card ([0] - normal, [1] - gold, [2] - spy, [3] - manekin, [4] - destroy, [5] - weather)</returns>
    public int getIsSpecial()
    {
        return this.isSpecial;
    }

    /// <summary>
    /// Set a isSpecial attribute
    /// </summary>
    /// <param name="isSpecial">true if card is special type</param>
    public void setIsSpecial(int isSpecial)
    {
        this.isSpecial = isSpecial;
    }

    /// <summary>
    /// Flip card
    /// </summary>
    /// <param name="x">true if you want to flip in x axis</param>
    /// <param name="y">true if you want to flip in y axis</param>
    public void flip(bool x, bool y)
    {
        if (x == true)
        {
            if (spriteRenderer.flipX == true)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
        if (y == true)
        {
            if (spriteRenderer.flipY == true)
                spriteRenderer.flipY = false;
            else
                spriteRenderer.flipY = true;
        }
    }

    /// <summary>
    /// Mirror transformation around (0,0,0) point of Desk
    /// </summary>
    public void mirrorTransform()
    {
        transform.position = new Vector3(transform.position.x * -1 + 4.39f, transform.position.y * -1 + 1.435f, transform.position.z);
    }
}