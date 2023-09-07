using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsRenderer : MonoBehaviour
{
    public GameObject GameOverDialog;
    public GameObject SaveInfoDialog;

    protected void Awake()
    {
       GameOverDialog.SetActive(false);
       SaveInfoDialog.SetActive(false);
    }

    public void showSaveInfoDialog() {
        StartCoroutine(showSaveInfoDialogAndHide());
    }

    private IEnumerator showSaveInfoDialogAndHide() {
        SaveInfoDialog.SetActive(true);
        yield return  new WaitForSeconds(1f); 
        SaveInfoDialog.SetActive(false);
    }
}
