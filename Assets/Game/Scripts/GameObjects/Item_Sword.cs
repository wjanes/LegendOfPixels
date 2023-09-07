using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Sword : Collectable
{
    public void Start() {
        if (SaveGameData.current.inventory.sword) {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Wird ausgelöst wenn das Schwert Item eingesammelt wird
    /// </summary>
    public override void onCollect()
    {
        base.onCollect();
        SaveGameData.current.inventory.sword = true;
        Destroy(gameObject);
    }
}
