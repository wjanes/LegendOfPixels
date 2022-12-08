using UnityEngine;

public class Sword : MonoBehaviour
{

    public Animator animator;
    public Animator characterAnimator;
    public CollisionDetector collisionDetector;

    private void setVisible(bool isVisible)
    {
        animator.gameObject.SetActive(isVisible);
    }

    public void Start()
    {
        setVisible(false);
        collisionDetector.whenCollisionDetected = onCollisionDetected;
    }

    private void onCollisionDetected(Collider2D collider)
    {
        Bush bush = collider.GetComponent<Bush>();
        if(bush != null)
        {
            bush.onHitbySword();
        }
    }

    public void OnEnable() {
        AnimationEventDeligate.whenTimelineEventReached += onTimeLineEvent; 
    }

    public void OnDisable() {
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
