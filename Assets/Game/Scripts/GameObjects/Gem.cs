using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{

    public void Start() {
        SaveGameData.current.recoverDestroy(gameObject);
    }
    /// <summary>
    /// Wird ausgelöst wenn ein Edelstein eingesammelt wird
    /// </summary>
    public override void onCollect()
    {
        base.onCollect();

        Debug.Log("onCollect triggered");
        SaveGameData.current.inventory.gems += 1;
        SaveGameData.current.recordDestroy(gameObject);

    }

}
