using UnityEngine;

/// <summary>
/// Hauptklasse mit Standard für Standardfunktionen
/// </summary>
public class MainObject : MonoBehaviour
{

    private static float pixelFrac = 1f / 16f;
    public Vector3 change = new Vector3();
    private BoxCollider2D boxCollider;
    private Collider2D[] colliders;
    private Animator anim;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        colliders = new Collider2D[10];
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        anim.SetFloat("change_x", change.x);
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
    /// <summary>
    /// "Pixel Perfect" Runden
    /// </summary>
    /// <param name="f">Wert als FLoatpoint</param>
    /// <returns></returns>
    public float roundToPixelgrid(float f)
    {
        return Mathf.Ceil(f / pixelFrac) * pixelFrac;
    }


    /// <summary>
    /// Gibt an ob das Objekt kollidiert ist oder nicht
    /// </summary>
    /// <returns>Ja oder Nein</returns>
    public bool IsColliding()
    {
        Physics2D.SyncTransforms();
        return boxCollider.OverlapCollider(new ContactFilter2D(), colliders) > 0;
    }
}
