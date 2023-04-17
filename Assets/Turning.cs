using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myAnimator.SetTrigger("TurnRight");
        }

    }
}
