using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Card prefab
    [SerializeField]
    private GameObject card;

    //how many cards to spawn
    [SerializeField]
    private int n;

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < n; i++)
        //{
        //    GameObject temp = Instantiate(card, transform.position + (Vector3.up * 0.005f * i),Quaternion.identity, transform);
        //    //temp.transform.localPosition = (Vector3.up * 0.05f * i)
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetBool("Shuffle", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Animator>().SetBool("Shuffle", false);
        }
    }
}
