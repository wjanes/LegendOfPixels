using System;
using System.Collections;
using UnityEngine;

/// Hauptklasse mit Standard für Standardfunktionen
public class MainObject : MonoBehaviour
{

    private static float pixelFrac = 1f / 16f;
    public Vector3 change = new Vector3();
    protected BoxCollider2D boxCollider;
    protected Collider2D[] colliders;
    protected Animator animator;
    protected int numFound = 0;


    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        colliders = new Collider2D[10];
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        // Animation set the stands (U D L R)
        animator.SetFloat("change_x", change.x);
        animator.SetFloat("change_y", change.y);

        if (change.y <= -1f)
        {
            animator.SetFloat("lookAt", 0f);
        }
        else if (change.x <= -1f)
        {
            animator.SetFloat("lookAt", 1f);
        }
        else if (change.y >= 1f)
        {
            animator.SetFloat("lookAt", 2f);
        }
        else if (change.x >= 1f)
        {
            animator.SetFloat("lookAt", 3f);
        }

        // Anwenden der in Change gesetzten Bewegung
        //float step = roundToPixelgrid(1f * Time.deltaTime);
        float step = 2f * Time.deltaTime;

        Vector3 oldpos = transform.position;
        transform.position += change * step;

        if (IsColliding())
        {
            transform.position = oldpos;
            for(int i = 0; i < numFound; i++) {
                if (colliders[i].TryGetComponent<TouchableBlocker>(out var tb)) {
                    tb.onTouch();
                }
            }
        }

        change = Vector3.zero;
    }

    public float RoundToPixelgrid(float f)
    {
        return Mathf.Ceil(f / pixelFrac) * pixelFrac;
    }


    public bool IsColliding()
    {
        Physics2D.SyncTransforms();
        numFound = boxCollider.OverlapCollider(new ContactFilter2D(), colliders);
        return numFound  > 0;
    }

    public Vector3 GetFullTilePosition()
    {
        Vector3 p = transform.position;
        p.x = Mathf.FloorToInt(p.x);
        p.y = Mathf.CeilToInt(p.y);

        p.x += 0.5f;
        p.y -= 0.5f;

        return p;
    }

    public void PushByTiles(float deltaX, float deltaY)
    {
        Vector3 tilepos = GetFullTilePosition();
        Vector3 oldPos = tilepos;

        tilepos.x += deltaX;
        tilepos.y += deltaY;
        transform.position = tilepos;

        if (IsColliding())
        {
            transform.position = oldPos;
        }
        else
        {
            StartCoroutine(AnimateMoveTowards(oldPos, tilepos));
        }

    }

    private IEnumerator AnimateMoveTowards(Vector3 fromPos, Vector3 toPos)
    {
        float duration = 0.1f;

        for (float t = 0f; t <= 1f; t = t + Time.deltaTime / duration)
        {
            transform.position = Vector3.Lerp(fromPos, toPos, t);
            yield return new WaitForEndOfFrame();
        }

    }

    /// <summary>
    /// Drückt die Figur vom angegebenen Punkt wegwärts
    /// </summary>
    /// <param name="deflector">Objekt, von dem die Figur abprallt</param>
    /// <param name="topLeftAnchor">ist das Deflector Objekt links oben ausgerichtet</param>
    public void PushAwayFrom(MonoBehaviour deflector, bool topLeftAnchor)
    {

        Vector3 diff;

        if (topLeftAnchor) {
            diff = transform.position - ( deflector.transform.position ) + new Vector3(0.5f, -0.5f, 0f);
        } else {
            diff = transform.position - deflector.transform.position;
        }

        PushByTiles(diff.x, diff.y);
    }

    /// <summary>
    /// lässt die Figur blinken
    /// </summary>
    /// <param name="times">Wie oft soll figur blinken</param>
    public void Flicker(int times)
    {
        StartCoroutine(AnimateFlicker(times));
    }

    private IEnumerator AnimateFlicker(int times)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 0; i < times; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
