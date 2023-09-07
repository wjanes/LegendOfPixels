using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Verwaltet die Gesundheit der Spielfigur
[System.Serializable]
public class Health
{
    //Aktueller Gesundheitswert
    public int current = 5;
    // Maximaler Gesundheitswert
    public int max = 5;

    public void change(int delta)
    {
        current = Mathf.Clamp(current + delta,0,max);
    }



}
