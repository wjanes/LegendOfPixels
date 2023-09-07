using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collectable : MonoBehaviour
{

    protected virtual void Awake() {
        if (!GetComponent<BoxCollider2D>().isTrigger) {
            Debug.LogError("Der BoxCollider2D von " + gameObject.name + "muss ein Trigger sein");
        }
    }

    public virtual void onCollect()
    {

    }

}
