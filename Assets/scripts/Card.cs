using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardName;
    private int power;
    private int index;
    private bool active = false;
    private CardModel cardModel;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D cardColider;
    public GameObject cardModelGameObject;
    
    //TODO - another getters and setters
    public string getCardName()
    {
        return this.cardName;
    }

    public void setCardName(string cardName)
    {
        this.cardName = cardName;
    }

    public void setPower(int power)
    {
        this.power = power;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public void setActive(bool state)
    {
        this.active = state;
    }

    public bool isActive()
    {
        return this.active;
    }

    public Bounds getBounds()
    {
        return this.cardColider.bounds;
    }

    public string toString()
    {
        return this.cardName + " card with power " + this.power;
    }

    public void setFront(int index)
    {
        spriteRenderer.sprite = cardModel.getFront(index);
    }

    void Awake()
    {
        cardModel = cardModelGameObject.GetComponent<CardModel>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cardColider = GetComponent<BoxCollider2D>();
        cardColider.size = new Vector2(1f, 1.57f);
    }

    void OnMouseDown()
    {
        //this.active = true;
       /*
        * if (areas.getDeckColliderBounds().Contains(this.transform.position))
        {
            Vector3 up = new Vector3(0, 0.2f, 0);
            // TODO - zabezpieczyć aby wysuwało się tylko raz
            this.transform.position += up;
        }*/
    }
}
