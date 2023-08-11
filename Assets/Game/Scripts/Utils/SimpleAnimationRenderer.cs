using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationRenderer : MonoBehaviour
{
    public Sprite[] frames = new Sprite[0];
    public float duration = 0.5f;
    public bool loop = true;
    public bool destroyObject = false;

    private void Start()
    {
        StartCoroutine(play());
    }

    private IEnumerator play()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
 
        do
        {
            for (int i = 0; i < frames.Length; i++)
            {
                sr.sprite = frames[i];
                yield return new WaitForSeconds(duration / frames.Length);
            }
        }
        while (enabled && loop == true);

    if (destroyObject == true ) {
        Destroy(gameObject);
    }

    }



    
}
