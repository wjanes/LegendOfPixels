using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MainObject
{
    private ContactFilter2D triggerContactFilter;

    protected override void Awake()
    {
        base.Awake();
        triggerContactFilter = new ContactFilter2D();
        triggerContactFilter.useTriggers = true;
    }



    private void Update()
    {
        int found = boxCollider.OverlapCollider(triggerContactFilter, colliders);
        for(int i = 0; i < found; i++)
        {
            Collider2D collider = colliders[i];
            if (collider.isTrigger)
            {
                foreach(Collectable collectable in collider.GetComponents<Collectable>())
                {
                    collectable.onCollect();
                }
            }
        }
    }

}
