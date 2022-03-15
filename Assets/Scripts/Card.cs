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
    [SerializeField]
    GameObject graphicObj;
    [SerializeField]
    Sprite spr;
    [SerializeField]
    SpriteRenderer sr;
    Transform targetUI;
    Vector3 targetP, initialP;
    Quaternion targetR,initialR;
    bool lerping = false,isUI = false;
    float starttime, smoothness = 5;
    



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetUI = GameObject.Find("TargetCards").transform;
        initialP = transform.position;
        initialR = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            if((transform.position - targetP).magnitude < 0.1f && 
                ((transform.rotation.eulerAngles - targetR.eulerAngles).magnitude < 0.1))
            {
                lerping = false;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetP, (Time.time - starttime) / smoothness);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetR, (Time.time - starttime) / smoothness);
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Flip();
            //Vector3(0.626297474, 1.10657406, -1.18616629);
            //Vector3(1.778, 2.80699992, 0.796000004);
        }
        if (Input.GetMouseButtonDown(2))
        {
            print(isUI);
            if (!isUI)
            {
                LerpToPositionAndRotation(targetUI.position, targetUI.rotation);
                
            }
            else
            {
                LerpToPositionAndRotation(initialP,initialR);
            }

            isUI = !isUI;
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

    public void RandomizePosition()
    {
        graphicObj.transform.Rotate(0, Random.Range(-5, 5), 0);
    }

    public void LerpToPositionAndRotation(Vector3 p, Quaternion r)
    {
        targetP = p;
        targetR = r;
        lerping = true;
        starttime = Time.time;

    }

}
