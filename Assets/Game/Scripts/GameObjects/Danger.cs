using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : TouchableBlocker
{
    private float lastHit = 0f;
    public override void onTouch()
    {
        base.onTouch();

        if (Time.time - lastHit > 1f)
        {
            SaveGameData.current.health.change(-1);
            lastHit = Time.time;

            Hero hero = FindObjectOfType<Hero>();
            hero.pushAwayFrom(this);
            hero.flicker(3);
        }
    }
}
