using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class Card : MonoBehaviour
{
    //is the card facing up?
    public bool facingUp = false;
    public Animator anim;
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
    [SerializeField]
    public bool enableFlip = true, enableMovement = true;
    public GameManager gm;



    // Start is called before the first frame update
    public virtual void Start()
    {
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        targetUI = GameObject.Find("TargetCards").transform;
        
        initialP = transform.position;
        initialR = transform.rotation;
        

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (lerping)
        {
            if((transform.position - targetP).magnitude < 0.1f && 
                ((transform.rotation.eulerAngles - targetR.eulerAngles).magnitude < 0.1))
            {
                lerping = false;
                RandomizePosition();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, targetP, (Time.time - starttime) / smoothness);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetR, (Time.time - starttime) / smoothness);
            }
        }
        else if (isUI)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (enableMovement)
                {
                    if (GetComponent<BoxCollider>().enabled)
                    {
                        BackInPlace();
                    }
                }
            }
        }
        
    }

    public virtual void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(2))
        {
            //Flip();
            //Vector3(0.626297474, 1.10657406, -1.18616629);
            //Vector3(1.778, 2.80699992, 0.796000004);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (enableMovement)
            {
                if (GetComponent<BoxCollider>().enabled) {

                    if (!isUI)
                    {
                        if (gm.IsUIFree())
                        {
                            ShowToCamera();
                        }

                    }
                }
            }
        }
    }

    public virtual void Flip()
    {
        if (enableFlip)
        {
            facingUp = !facingUp;
            anim.SetBool("NeedFlipping", facingUp);
        }

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

    public virtual void ShowToCamera()
    {
        if(targetUI== null)
        {
            targetUI = GameObject.Find("TargetCards").transform;
        }
        if (!facingUp)
        {
            Flip();
        }
        LerpToPositionAndRotation(targetUI.position, targetUI.rotation);
        isUI = true;
        gm.SetUIFree(false);
    }
   
    public virtual void BackInPlace()
    {
        LerpToPositionAndRotation(initialP, initialR);
        isUI = false;
        gm.SetUIFree(true);
    } 

    public void SetInitialPAndR(Vector3 ip, Quaternion rp)
    {
        initialP = ip;
        initialR = rp;
    }
}
