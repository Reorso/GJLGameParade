using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class Card : MonoBehaviour
{
    //is the card facing up?
    bool facingUp = false;
    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Flip();
            //Vector3(0.626297474, 1.10657406, -1.18616629);
            //Vector3(1.778, 2.80699992, 0.796000004);
        }
    }

    public void Flip()
    {
        facingUp = !facingUp;
        anim.SetBool("NeedFlipping", facingUp);
    }

    public void Hover()
    {

    }

}
