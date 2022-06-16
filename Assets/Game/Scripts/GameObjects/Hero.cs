using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MainObject
{
    private ContactFilter2D triggerContactFilter;
    public RuntimeAnimatorController emptySkin;
    public RuntimeAnimatorController shieldSkin;


    protected override void Awake()
    {
        base.Awake();
        triggerContactFilter = new ContactFilter2D();
        triggerContactFilter.useTriggers = true;
    }



    private void Update()
    {
        int found = boxCollider.OverlapCollider(triggerContactFilter, colliders);
        for (int i = 0; i < found; i++)
        {
            Collider2D collider = colliders[i];
            if (collider.isTrigger)
            {
                foreach (Collectable collectable in collider.GetComponents<Collectable>())
                {
                    collectable.onCollect();
                }
            }
        }

        if (SaveGameData.current.inventory.shield)
        {
            animator.runtimeAnimatorController = shieldSkin;
        }
        else
        {
            animator.runtimeAnimatorController = emptySkin;
        }
    }

    public void performAction() {
        Sword sword = GetComponentInChildren<Sword>();
        sword.stroke();
    }

}
