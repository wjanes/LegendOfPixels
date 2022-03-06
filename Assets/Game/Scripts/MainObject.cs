using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Main Object for all general Methods
public class MainObject : MonoBehaviour
{

    private static float pixelFrac = 1f / 16f;
    public Vector3 change = new Vector3();

    // Pixel Perfect runden :)
    public float roundToPixelgrid(float f)
    {
        return Mathf.Ceil(f / pixelFrac) * pixelFrac;
    }

    // Colliding Methode, gibt an ob das Objekt kollidiert ist oder nicht
    public bool IsColliding()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Collider2D[] colliders = new Collider2D[50];
        Physics2D.SyncTransforms();
        return boxCollider.OverlapCollider(new ContactFilter2D(), colliders) > 0;
    }


    void LateUpdate()
    {
        // Anwenden der in Change gesetzten Bewegung
        float step = roundToPixelgrid(1f * Time.deltaTime);

        Vector3 oldpos = transform.position;
        transform.position += change * step;

        if (IsColliding())
        {
            transform.position = oldpos;
        }

        change = Vector3.zero;

    }

}
