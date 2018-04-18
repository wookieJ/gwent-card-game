using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDiamond : MonoBehaviour {

    public Sprite healthSprite;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    /// <summary>
    /// Setting visibility for health image
    /// </summary>
    /// <param name="visibility">true if want to see image, false otherwise</param>
    public void setVisibility(bool visibility)
    {
        this.spriteRenderer.gameObject.SetActive(visibility);
    }

    /// <summary>
    /// Moving health image to pointed position
    /// </summary>
    /// <param name="x">x coordinate of pointed position</param>
    /// <param name="y">y coordinate of pointed position</param>
    public void moveTo(float x, float y)
    {
        Vector3 vector = new Vector3(x, y, 0);
        this.transform.position = vector;
    }

    /// <summary>
    /// Enabling sprite to model
    /// </summary>
    public void enableSprite()
    {
        this.spriteRenderer.sprite = this.healthSprite;
    }

    /// <summary>
    /// Getting position of health diamond image
    /// </summary>
    /// <returns>position of health diamond image</returns>
    public Vector3 getPosition()
    {
        return this.transform.position;
    }

    /// <summary>
    /// Setting position of health diamond image
    /// </summary>
    /// <param name="vector"></param>
    public void setPosition(Vector3 vector)
    {
        this.transform.position = vector;
    }
}
