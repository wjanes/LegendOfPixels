using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogsInputController : MonoBehaviour
{

    private DialogsRenderer dr;

    protected void Awake()
    {
        dr = GetComponent<DialogsRenderer>();
    }

    protected void Update()
    {
        if (dr.GameOverDialog.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                SaveGameData.current = new SaveGameData();
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
