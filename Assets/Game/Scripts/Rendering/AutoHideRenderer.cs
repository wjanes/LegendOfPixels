using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHideRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer r = GetComponent<Renderer>();

        if (r != null)
        {
            r.enabled = false;
        }
    }
}
