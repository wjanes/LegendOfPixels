using UnityEngine;

/// Hauptklasse mit Standard für Standardfunktionen
public class MainObject : MonoBehaviour
{

    private static float pixelFrac = 1f / 16f;
    public Vector3 change = new Vector3();
    protected BoxCollider2D boxCollider;
    protected Collider2D[] colliders;
    private Animator anim;


    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        colliders = new Collider2D[10];
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        // Animation set the stands (U D L R)
        anim.SetFloat("change_x", change.x);
        anim.SetFloat("change_y", change.y);

        if (change.y <= -1f)
        {
            anim.SetFloat("lookAt", 0f);
        }
        else if (change.x <= -1f)
        {
            anim.SetFloat("lookAt", 1f);
        }
        else if (change.y >= 1f)
        {
            anim.SetFloat("lookAt", 2f);
        }
        else if (change.x >= 1f)
        {
            anim.SetFloat("lookAt", 3f);
        }

        // Anwenden der in Change gesetzten Bewegung
        //float step = roundToPixelgrid(1f * Time.deltaTime);
        float step = 2f * Time.deltaTime;

        Vector3 oldpos = transform.position;
        transform.position += change * step;

        if (IsColliding())
        {
            transform.position = oldpos;
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
        return boxCollider.OverlapCollider(new ContactFilter2D(), colliders) > 0;
    }
    
}
