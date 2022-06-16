using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusBarRenderer : MonoBehaviour
{

    public TextMeshProUGUI gemLabel;
    public Image weaponA;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gemLabel.text = SaveGameData.current.inventory.gems.ToString("D3");
        weaponA.gameObject.SetActive(SaveGameData.current.inventory.shield);
   
    }
}
