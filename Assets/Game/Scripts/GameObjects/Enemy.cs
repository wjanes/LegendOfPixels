using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basisskript für Feindverhalten
/// </summary>
public class Enemy : MonoBehaviour
{
    public void onHitBySword()
    {
        RandomSpawn rs = GetComponent<RandomSpawn>();
        if (rs != null)
        {
            GameObject item = rs.spawn();
            if (item != null)
            {
                item.transform.position = transform.position;
            }
        } 

        Destroy(gameObject);
    }
}