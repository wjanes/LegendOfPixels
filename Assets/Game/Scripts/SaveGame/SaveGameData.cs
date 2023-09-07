using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameData
{
    //Aktueller Spielstand
    public static SaveGameData current = loadOrNewGame();

    //Speicher f�r einsammelbare Objekte
    public Inventory inventory = new Inventory();

    //Aktueller Gesundheitszustand des Spielers
    public Health health = new Health();

    public string savePoint = "";

    public List<string> deletedObjects = new List<string>();

    /// <summary>
    /// Speichert den Spielstand
    /// </summary>
    public void save()
    {
        string data = JsonUtility.ToJson(this);
        string filepath = Path.Combine(Application.persistentDataPath, "savegame.json");
        File.WriteAllText(filepath, data);
        Debug.Log("Gespeichert \n Daten:" + data);
        DialogsRenderer dr = UnityEngine.Object.FindObjectOfType<DialogsRenderer>();
        if (dr != null)
        {
            dr.showSaveInfoDialog();
        }
    }

    /// <summary>
    /// Läd einen Spielstand aus dem JSON File oder erzeugt einen leeren
    /// Spielstand
    /// </summary>
    /// <returns>Den geladenen oder neuen Spielstand</returns>
    private static SaveGameData loadOrNewGame()
    {
        SaveGameData result = new SaveGameData();
        string filepath = Path.Combine(Application.persistentDataPath, "savegame.json");
        if (File.Exists(filepath)) {
            string data = File.ReadAllText(filepath);
            result = JsonUtility.FromJson<SaveGameData>(data);
            Debug.Log("Geladen \n Daten:" + data);
        }
        return result;
    }

    public void recordDestroy(GameObject go) {
        string id = go.scene.name + "/" + go.name;
        deletedObjects.Add(id);
        UnityEngine.Object.Destroy(go);
    }

    public void recoverDestroy(GameObject go) {
        string id = go.scene.name + "/" + go.name;
        if (deletedObjects.Contains(id)) {
            UnityEngine.Object.Destroy(go);
        }
    }


}
