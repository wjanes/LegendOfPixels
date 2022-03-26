using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionController : MonoBehaviour
{

    public Hero hero;

    // Update is called once per frame
    private void Update()
    {
        Vector3 heroPosition = hero.transform.position;
        heroPosition.z = transform.position.z;
        transform.position = heroPosition;
    }

}
