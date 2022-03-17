using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using TMPro;

public class CardEvent : Card
{
    [SerializeField] List<string> textToDisplay;
    public TextMeshProUGUI display;
    int current = 0;

    public override void Start()
    {
        base.Start();
        display = GameObject.Find("GameManager").GetComponent<GameManager>().t;
        //display.gameObject.SetActive(false);
        //display.gameObject.SetActive(false);
    }

    public override void BackInPlace()
    {
        if(current < textToDisplay.Count)
        {
            //display.gameObject.SetActive(true);
            display.text = textToDisplay[current];
            current++;
        }
        else{
            base.BackInPlace();
            display.gameObject.SetActive(false);
        }

    }

    public override void ShowToCamera()
    {
        
        base.ShowToCamera();
        display.text = textToDisplay[current];
        current++;
        display.gameObject.SetActive(true);
        

    }
}
