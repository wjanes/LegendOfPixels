using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // Liste möglicher Spielobjekte aus denen zufällig eines
    // ausgewählt und in der Szene dupliziert wird
    public GameObject[] possibleElements = new GameObject[0];

    public GameObject spawn()
    {
        GameObject template = possibleElements[Random.Range(0, possibleElements.Length)];
        if (template == null)
        {
            return null;
        }
        else
        {
            return Instantiate(template);
        }
    } 
}
