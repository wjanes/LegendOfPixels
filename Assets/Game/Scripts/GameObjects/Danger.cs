using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : Collectable
{
    private float lastHit = 0f;
    public override void onCollect()
    {
        base.onCollect();

        if (Time.time - lastHit > 1f)
        {
            SaveGameData.current.health.change(-1);
            lastHit = Time.time;
        }

        Hero hero = FindObjectOfType<Hero>();
        hero.transform.position = hero.getFullTilePosition();

    }
}
