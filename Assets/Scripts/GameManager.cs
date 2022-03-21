using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CardEvent ce;
    [SerializeField] List<CharacterCard> cc;
    [SerializeField] public TextBox t;
    public int deathCount = 0;
    [SerializeField]GameObject deck;
    bool uiFree = true;
    [SerializeField]GameObject win, lose;
    [SerializeField] TextMeshProUGUI winscreen;

    [SerializeField] List<AudioClip> audios;
    [SerializeField] AudioSource aso;
    int a = 0;
    bool playing;
    // Start is called before the first frame update
    void Start()
    {
        //cc = new List<CharacterCard>();
        deck.SetActive(false);
        aso.clip = audios[a];
        StartCoroutine(StartMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIFree(bool v)
    {
        uiFree = v;
    }
    public bool IsUIFree()
    {
        return uiFree;
    }
    public void SelectCh(GameObject ch)
    {
        print("sto per sclerare");
        ce.ch = ch.GetComponent<CharacterCard>();
        ce.characterSelected = true;
        ce.gameObject.SetActive(true);

        foreach (CharacterCard cha in cc)
        {
            cha.SetCharacterSelection(ce,false);
        }
    }
    public void StartCharacterSelectionMode(CardEvent ce)
    {
        this.ce = ce;
        ce.gameObject.SetActive(false);
        foreach (CharacterCard cha in cc)
        {
            if (!cha.dead)
            {
                cha.SetCharacterSelection(ce, true);
            }
        }
    }

    public void Win()
    {
        deck.SetActive(false);
        winscreen.text += ""+deathCount ;
        win.SetActive(true);

    }

    public void Lose()
    {
        deck.SetActive(false);
        lose.SetActive(true);
    }

    public void StartGame()
    {
        deck.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public CharacterCard FindCharacterOfName(string chName)
    {
        foreach (CharacterCard cha in cc)
        {
            if (cha.name == chName)
            {
                return cha;
            }
        }
        print("CHARACTER ERROR");
        return new CharacterCard();
    }

    

    IEnumerator StartMusic()
    {
        AudioSource audio = aso;

        audio.PlayDelayed(3);
        yield return new WaitForSeconds(audio.clip.length);
        a++;
        audio.clip = audios[a];
        StartCoroutine(StartMusic());
    }
}
