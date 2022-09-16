using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour

{
    protected BoxCollider2D boxCollider;
    protected Collider2D[] colliders;
    protected ContactFilter2D contactFilter;
    protected int numFound = 0;

    public delegate void Callback(Collider2D collider);

    public Callback whenCollisionDetected;


    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        colliders = new Collider2D[10];
    }



    public bool IsColliding()
    {
        numFound = boxCollider.OverlapCollider(new ContactFilter2D(), colliders);
        return numFound > 0;
    }

    protected void Update()
    {
        if(whenCollisionDetected == null)
        {
            Debug.Log("CollisionDetector funktioniert nicht: nicht zugewiesen");
            enabled = false;
        }
        else  if (IsColliding())
        {
            for (int i = 0; i < numFound; i++)
            {
                whenCollisionDetected(colliders[i]);
            }

        }
    }
}
