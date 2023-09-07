using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : Collectable
{
    public void Start() {
        if (SaveGameData.current.savePoint == gameObject.name) {
            gameObject.SetActive(false);
            FindObjectOfType<Hero>().transform.position = transform.position + new Vector3(0.5f, -0.5f, 0);
        }
    }
    public override void onCollect() {
            base.onCollect();

            SaveGameData.current.savePoint = gameObject.name;
            SaveGameData.current.save();
            gameObject.SetActive(false);

    }
}
