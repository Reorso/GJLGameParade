using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    //Card prefab
    [SerializeField]
    private GameObject card;

    [SerializeField]
    Transform discardPile;

    [SerializeField]
    List<GameObject> cards;

    //how many cards to spawn
    [SerializeField]
    private int n;

    int offset = 0;

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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawCard();
        }
    }

    void DrawCard()
    {
        if(cards.Count -1 - offset >= 0)
        {
            cards[cards.Count- 1 - offset].SetActive(false);
            offset++;
            GameObject temp = Instantiate(card,transform.position, Quaternion.identity, discardPile);
            temp.GetComponent<CardEvent>().display = GameObject.Find("GameManager").GetComponent<GameManager>().t;
            temp.transform.position = transform.position;
            temp.GetComponent<CardEvent>().SetInitialPAndR(discardPile.position + (Vector3.up * offset * 0.005f), Quaternion.identity);
            temp.GetComponent<CardEvent>().ShowToCamera();
            temp.GetComponent<CardEvent>().anim = temp.GetComponent<Animator>(); ;
            temp.GetComponent<CardEvent>().Flip();
            
        }
        else
        {
            //deck is empty
            print("deck is empty");
        }
    }
}
