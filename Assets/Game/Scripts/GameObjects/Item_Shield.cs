using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : Collectable
{
    public void Start() {
        if (SaveGameData.current.inventory.shield) {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Wird ausgelöst wenn das Schild Item eingesammelt wird
    /// </summary>
    public override void onCollect()
    {
        base.onCollect();
        SaveGameData.current.inventory.shield = true;
        Destroy(gameObject);
    }

}
