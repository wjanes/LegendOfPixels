using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputController : MonoBehaviour
{
    public Hero hero;
    private static readonly int positive = 1;
    private static readonly int negative = -1;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            hero.change.x = positive;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hero.change.x = negative;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            hero.change.y = positive;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            hero.change.y = negative;
        }
        else if (Input.GetKeyUp(KeyCode.E)){
            hero.performAction();
        }
    }
}
