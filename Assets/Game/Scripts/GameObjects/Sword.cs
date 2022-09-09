using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public Animator animator;
    public Animator characterAnimator;

    private void setVisible(bool isVisible)
    {
        animator.gameObject.SetActive(isVisible);
    }

    public void Start() => setVisible(false);

    public void onEnable() {
        AnimationEventDeligate.whenTimelineEventReached += onTimeLineEvent; 
    }

    public void onDisable() {
        AnimationEventDeligate.whenTimelineEventReached -= onTimeLineEvent;
    }

    public void stroke()
    {

        int lookAt = Mathf.RoundToInt(characterAnimator.GetFloat("lookAt"));
        float scaleX = 1f;
        float rotateZ = 0f;

        transform.localScale = new Vector3(1f, 1f, 1f);

        if (lookAt == 0) { rotateZ = 90f; }
        else if (lookAt == 2) { rotateZ = -90f; }
        else if (lookAt == 3) { scaleX = -1f; }

        transform.localScale = new Vector3(scaleX, 1f, 1f);
        transform.localRotation = Quaternion.Euler(0f, 0f, rotateZ);

        setVisible(true);

        animator.SetTrigger("onStroke");
    }

    public void onTimeLineEvent()
    {
        setVisible(false);
    }

}
