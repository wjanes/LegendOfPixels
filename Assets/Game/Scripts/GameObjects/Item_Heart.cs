using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heart : Collectable
{
    public override void onCollect()
    {
        base.onCollect();
        SaveGameData.current.health.change(+1);
        Destroy(gameObject);
    }
}
