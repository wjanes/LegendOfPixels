using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Sword : Collectable
{
    public override void onCollect()
    {
        base.onCollect();
        SaveGameData.current.inventory.sword = true;
        Destroy(gameObject);
    }
}