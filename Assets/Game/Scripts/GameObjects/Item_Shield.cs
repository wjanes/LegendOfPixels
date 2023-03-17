using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : Collectable
{
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
