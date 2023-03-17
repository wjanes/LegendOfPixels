using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{

    public Sprite[] destructionFrames = new Sprite[0];
    public float duration = 0.5f;
    private bool isOnHitBySwordPlaying = false;

    /// <summary>
    /// Wenn der Busch von einer Waffe getroffen wird
    /// </summary>
    public void onHitbySword()
    {
        if (!isOnHitBySwordPlaying)
        {
            StartCoroutine(playOnHitBySword());
        }
    }

    /// <summary>
    /// Busch wird zerstört
    /// Sprites werden mittels einer Coroutine angezeigt
    /// </summary>
    /// <returns></returns>
    private IEnumerator playOnHitBySword()
    {
        isOnHitBySwordPlaying = true;
        RandomSpawn rs = GetComponent<RandomSpawn>();
        if (rs != null)
        {
            GameObject item = rs.spawn();
            if (item != null)
            {
                item.transform.position = transform.position;
            }
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < destructionFrames.Length; i++)
        {
            sr.sprite = destructionFrames[i];
            yield return new WaitForSeconds(duration / destructionFrames.Length);
        }
        Destroy(gameObject);
    }
}
