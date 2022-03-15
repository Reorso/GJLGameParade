using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schyte : MonoBehaviour
{
    Vector3 offset = new Vector3(0.626297474f - 1.778f, 1.10657406f - 2.80699992f, -1.18616629f - 0.796000004f);
    Animator anim;
    Vector3 target;
    bool lerping = false;
    float startingTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera camera = Camera.main;
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                lerping = true;
                startingTime = Time.time;
                anim.SetBool("Charging", true);
                // Do something with the object that was hit by the raycast.
            }
        }
        //if (lerping)
        //{
        //    transform.parent.position = Vector3.Lerp(transform.parent.position, target + offset  , Time.deltaTime);
        //    if((transform.parent.position - (target + offs  )).magnitude < 0.1)
        //    {
        //        anim.SetBool("Hitting", true);
        //        lerping = false;
        //    }
        //}
    }
}
