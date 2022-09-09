using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Collectable
{

    public override void onCollect()
    {
        base.onCollect();
        SaveGameData.current.inventory.shield = true;
        Destroy(gameObject);
    }

}
