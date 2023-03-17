using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsRenderer : MonoBehaviour
{
    public GameObject GameOverDialog;

    protected void Awake()
    {
       GameOverDialog.SetActive(false);
    }
}
