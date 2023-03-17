using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heart : Collectable
{

    /// <summary>
    /// Wird ausgelößt wenn das Herz Item eingesammelt wird
    /// </summary>
    public override void onCollect()
    {
        base.onCollect();
        SaveGameData.current.health.change(+1);
        Destroy(gameObject);
    }
}
