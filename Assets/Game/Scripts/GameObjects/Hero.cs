using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MainObject
{
    [System.Serializable]
    public class SpriteSet
    {
        public Sprite down;
        public Sprite up;
        public Sprite left;
        public Sprite right;

        public void apply(SpriteRenderer spriteRenderer, int lookAt)
        {
            spriteRenderer.flipX = false;
            if (lookAt == 0) { spriteRenderer.sprite = down; }
            else if (lookAt == 1) { spriteRenderer.sprite = left; }
            else if (lookAt == 2) { spriteRenderer.sprite = up; }
            else if (lookAt == 3) { spriteRenderer.sprite = right; }
        }

    }

    private ContactFilter2D triggerContactFilter;
    public RuntimeAnimatorController emptySkin;
    public RuntimeAnimatorController shieldSkin;
    public SpriteSet emptyActionSkin;
    public SpriteSet shieldActionSkin;


    protected override void Awake()
    {
        base.Awake();
        triggerContactFilter = new ContactFilter2D();
        triggerContactFilter.useTriggers = true;
    }



    private void Update()
    {
        int found = boxCollider.OverlapCollider(triggerContactFilter, colliders);
        for (int i = 0; i < found; i++)
        {
            Collider2D collider = colliders[i];
            if (collider.isTrigger)
            {
                foreach (Collectable collectable in collider.GetComponents<Collectable>())
                {
                    collectable.onCollect();
                }
            }
        }

        if (SaveGameData.current.inventory.shield)
        {
            animator.runtimeAnimatorController = shieldSkin;
        }
        else
        {
            animator.runtimeAnimatorController = emptySkin;
        }

        if(SaveGameData.current.health.current == 0) {
            animator.SetTrigger("die");
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            GetComponent<HeroInputController>().enabled = false;

            Time.timeScale = 0f; // Spiel pausieren 

        }

    }

    public void onDieAnimationComplete()
    {
        DialogsRenderer dr = FindAnyObjectByType< DialogsRenderer >();
        dr.GameOverDialog.SetActive(true);
    }

    public void performAction()
    {

        if (SaveGameData.current.inventory.sword == false) { return; }

        animator.enabled = false;
        AnimationEventDeligate.whenTimelineEventReached += resetSkin;

        if (SaveGameData.current.inventory.shield)
        {
            shieldActionSkin.apply(GetComponent<SpriteRenderer>(), Mathf.RoundToInt(animator.GetFloat("lookAt")));
        }
        else
        {
            emptyActionSkin.apply(GetComponent<SpriteRenderer>(), Mathf.RoundToInt(animator.GetFloat("lookAt")));
        }
        Sword sword = GetComponentInChildren<Sword>();
        sword.stroke();
    }

    private void resetSkin()
    {
        animator.enabled = true;
        AnimationEventDeligate.whenTimelineEventReached -= resetSkin;
    }

}
