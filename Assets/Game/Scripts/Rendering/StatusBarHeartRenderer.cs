using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarHeartRenderer : MonoBehaviour
{
    private Image image;
    public int value = 5;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (SaveGameData.current.health.current >= value)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.black;
        }
    }

}
