using UnityEngine;

public class AnimationEventDeligate : MonoBehaviour
{

    Sword sword;

    public delegate void Callback();

    public static event Callback whenTimelineEventReached;

    public void onTimeLineEvent()
    {
        if (whenTimelineEventReached != null) {
            whenTimelineEventReached();
        }
        //sword = FindObjectOfType<Sword>();
        //sword.onTimeLineEvent();
    }
}
