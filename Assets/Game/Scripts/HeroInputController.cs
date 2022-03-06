using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputController : MonoBehaviour
{
    public Hero hero;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            hero.change.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hero.change.x = -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            hero.change.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            hero.change.y = -1;
        }
    }
}
