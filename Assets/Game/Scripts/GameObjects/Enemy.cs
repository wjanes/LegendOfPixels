using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basisskript f�r Feindverhalten
/// </summary>
public class Enemy : MonoBehaviour
{
    public GameObject explotionPrototype;
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

        GameObject explosion = Instantiate(explotionPrototype, transform.parent);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}