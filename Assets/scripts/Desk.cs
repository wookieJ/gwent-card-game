using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Flip the desk for another player
    /// </summary>
    public void flipDesk()
    {
        if(spriteRenderer.flipX == true)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        if (spriteRenderer.flipY == true)
            spriteRenderer.flipY = false;
        else
            spriteRenderer.flipY = true;
    }
}
