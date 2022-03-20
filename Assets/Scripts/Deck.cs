using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    [System.Serializable]
    public class CardType {[SerializeField]public GameObject card;[SerializeField]public int number;}
    //Card prefab
    [SerializeField]
    private List<CardType> card;
    [SerializeField]
    private List<GameObject> CharacterCards;
    
    [SerializeField]
    Transform discardPile;

    [SerializeField]
    List<GameObject> cards;
    List<GameObject> realDeck;

    //how many cards to spawn
    [SerializeField]
    private int n;

    int offset = 0;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        realDeck = new List<GameObject>();
        cards = new List<GameObject>();
        for (int i = 0; i<card.Count; i++)
        {
            for(int j =0;j< card[i].number; j++)
            {
                realDeck.Add(card[i].card);
                //realDeck.
            }
        }
        Shuffle();
        for(int i = 0; i < CharacterCards.Count; i++)
        {
            int index = realDeck.Count - 1 - (i * 2);
            realDeck.Insert(index,CharacterCards[i]);
        }
        for(int i = 0; i < realDeck.Count; i++)
        {

            GameObject temp = Instantiate(realDeck[i], transform.position + (Vector3.up * 0.005f * i),Quaternion.identity, transform);
            cards.Add(temp);
            if(i != realDeck.Count - 1)
            {
                temp.GetComponent<BoxCollider>().enabled = false;
            }
            
            //temp.transform.localPosition = (Vector3.up * 0.05f * i)
        }
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < realDeck.Count; i++)
        {
            int j = random.Next(i, realDeck.Count);
            GameObject temporary = realDeck[i];
            realDeck[i] = realDeck[j];
            realDeck[j] = temporary;
        }
    }


    // Update is called once per frame
    void Update()
    {

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
        if(cards.Count -1 - offset >= 0 && gm.IsUIFree())
        {
            //cards[cards.Count- 1 - offset].SetActive(false);
            if (cards.Count - 2 - offset >= 0)
            {
                cards[cards.Count - 2 - offset].GetComponent<BoxCollider>().enabled = true;
                cards[cards.Count - 2 - offset].GetComponent<CardEvent>().enableMovement = false;
            }
            //GameObject temp = Instantiate(cards[cards.Count - 1 - offset],transform.position, Quaternion.identity, discardPile);
            GameObject temp = cards[cards.Count - 1 - offset];
            temp.GetComponent<CardEvent>().tb = GameObject.Find("GameManager").GetComponent<GameManager>().t;
            temp.transform.position = transform.position;
            temp.GetComponent<CardEvent>().SetInitialPAndR(discardPile.position + (Vector3.up * offset * 0.005f), Quaternion.identity);
            temp.GetComponent<CardEvent>().ShowToCamera();
            temp.GetComponent<CardEvent>().enableMovement = true;
            temp.GetComponent<CardEvent>().anim = temp.GetComponent<Animator>(); 
            //temp.GetComponent<CardEvent>().Flip();
            offset++;
        }
        else
        {
            //deck is empty
            print("deck is empty or ui is occupied");
        }
    }
}
