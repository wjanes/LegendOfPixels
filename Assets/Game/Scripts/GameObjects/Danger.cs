using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Realisiert eine Gefahrenquelle die den Spieler bei Berührung verletzt
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Danger : TouchableBlocker
{
    private float lastHit = 0f;
    public bool topLeftAnchor = false;


    /// <summary>
    /// Wenn die Gefahrenquelle berührt wird...
    /// </summary>
    public override void onTouch()
    {
        base.onTouch();

        if (Time.time - lastHit > 1f)
        {
            SaveGameData.current.health.change(-1);
            lastHit = Time.time;

            if (SaveGameData.current.health.current > 0)
            {
                Hero hero = FindObjectOfType<Hero>();
                hero.pushAwayFrom(this, topLeftAnchor);
                hero.flicker(3);
            }
        }
    }
}
