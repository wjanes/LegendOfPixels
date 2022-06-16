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

    public void Start()
    {
        setVisible(false);
    }

    public void stroke()
    {

        int lookAt = Mathf.RoundToInt(characterAnimator.GetFloat("lookAt"));

        if (lookAt == 0)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (lookAt == 1)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (lookAt == 2)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        }
        else if (lookAt == 3)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }

        setVisible(true);

        animator.SetTrigger("onStroke");
    }

    public void onTimeLineEvent()
    {
        setVisible(false);
    }

}
