using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusBarRenderer : MonoBehaviour
{

    public TextMeshProUGUI gemLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gemLabel.text = SaveGameData.current.inventory.gems.ToString("D3");
   
    }
}
