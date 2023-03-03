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
                TouchableBlocker tb = colliders[i].GetComponent<TouchableBlocker>();
                if (tb != null) {
                    tb.onTouch();
                }
            }
        }

        change = Vector3.zero;
    }

    public float roundToPixelgrid(float f)
    {
        return Mathf.Ceil(f / pixelFrac) * pixelFrac;
    }


    public bool IsColliding()
    {
        Physics2D.SyncTransforms();
        numFound = boxCollider.OverlapCollider(new ContactFilter2D(), colliders);
        return numFound  > 0;
    }

    public Vector3 getFullTilePosition()
    {
        Vector3 p = transform.position;
        p.x = Mathf.FloorToInt(p.x);
        p.y = Mathf.CeilToInt(p.y);

        p.x += 0.5f;
        p.y -= 0.5f;

        return p;
    }

    public void pushByTiles(float deltaX, float deltaY)
    {
        Vector3 tilepos = getFullTilePosition();
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
            StartCoroutine(animateMoveTowards(oldPos, tilepos));
        }

    }

    private IEnumerator animateMoveTowards(Vector3 fromPos, Vector3 toPos)
    {
        float duration = 0.1f;

        for (float t = 0f; t <= 1f; t = t + Time.deltaTime / duration)
        {
            transform.position = Vector3.Lerp(fromPos, toPos, t);
            yield return new WaitForEndOfFrame();
        }

    }

    public void pushAwayFrom(MonoBehaviour deflector)
    {

        Vector3 diff = transform.position - deflector.transform.position;
        pushByTiles(diff.x, diff.y);
    }

    public void flicker(int times)
    {
        StartCoroutine(animateFlicker(times));
    }

    private IEnumerator animateFlicker(int times)
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
