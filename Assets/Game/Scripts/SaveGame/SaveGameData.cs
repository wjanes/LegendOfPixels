using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameData
{
    //Aktueller Spielstand
    public static SaveGameData current = new SaveGameData();
    
    //Speicher f�r einsammelbare Objekte
    public Inventory inventory = new Inventory();

}
