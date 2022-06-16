using UnityEngine;

public class AnimationEventDeligate : MonoBehaviour
{

    Sword sword;

    public void onTimeLineEvent()
    {
        sword = FindObjectOfType<Sword>();
        sword.onTimeLineEvent();
    }
}
