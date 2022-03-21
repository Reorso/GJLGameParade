using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;


public class CardEvent : Card
{
    [SerializeField] List<State> textToDisplay;

    public int current = 0;
    bool done = false;
    public int answerPicked = -1;
    public bool characterSelected = false;
    public TextBox tb;
    public CharacterCard ch;
    [SerializeField]public int eventType = 0;
    [SerializeField]public int bonus = 0;
    public string initial = "Which friend will help in this event?";
    public bool initialDone = false;


    public override void Start()
    {
        base.Start();
        tb = GameObject.Find("GameManager").GetComponent<GameManager>().t;
        //display.gameObject.SetActive(false);
        //display.gameObject.SetActive(false);
        print("start");
    }

    public override void Update()
    {
        base.Update();
    }



    public override void BackInPlace()
    {
        if(current == 1 && !initialDone)
        {
            tb.text.text = initial;
            if (!characterSelected)
            {
                gm.StartCharacterSelectionMode(GetComponent<CardEvent>());
                
            }
            else
            {
                if (!ch.facingUp)
                {
                    ch.Flip();
                }
                gameObject.GetComponent<CharacterEvent>().GetChSkill();
            }
            initialDone = true;
            
        }
        else
        {
            if (!done)
            {
                if (answerPicked >= 0)
                {
                    bonus += textToDisplay[current - 1].answers[answerPicked].bonus;
                    current = textToDisplay[current - 1].answers[answerPicked].jumpTo;
                    answerPicked = -1;
                }
                else if (textToDisplay[current - 1].jumpTo >= 1)
                {
                    current = textToDisplay[current - 1].jumpTo;
                }
            }

            if (current < textToDisplay.Count)
            {
                //display.gameObject.SetActive(true);
                if (textToDisplay[current].isQuestion)
                {
                    tb.text.text = textToDisplay[current].text;
                    int i = 0;
                    foreach (Answer a in textToDisplay[current].answers)
                    {
                        tb.SetCardEvent(this);
                        tb.ans[i].transform.parent.gameObject.SetActive(true);
                        tb.ans[i].text = a.text;

                        i++;
                    }
                    enableMovement = false;
                    current++;
                }
                else
                {
                    tb.text.text = textToDisplay[current].text;
                    current++;
                }

            }
            else
            {
                if (!done)
                {
                    string last, stat = " ";

                    if (bonus > 1)
                    {
                        last = "You are good at making decisions. <br>";
                    }
                    else if(bonus < -1)
                    {
                        last = "You are... ok at making decisions. <br>";
                    }
                    else
                    {last = "You didn't pick the right decisions. <br>";
                    }


                    int l = Random.Range(1, 7);
                    if (l > 4)
                    {
                        last += "You are very lucky. <br>";
                    }
                    else if (l > 2)
                    {
                        last += "You are mildly lucky. <br>";
                    }
                    else
                    {
                        last += "You are unlucky. <br>";
                    }

                    bonus += l;
                    
                    int skill = 0;

                    switch (eventType)
                    {
                        case 0:
                            skill = ch.agility;
                            stat = "AGI";
                            break;
                        case 1:
                            skill = ch.strenght;
                            stat = "STR";
                            break;
                        case 2:
                            skill= ch.wisdom;
                            stat = "WIS";
                            break;
                        case 3:
                            skill = ch.intelligence;
                            stat = "INT";
                            break;
                        case 4:
                            skill = ch.agility;
                            stat = "AGI";
                            break;
                        default:
                            break;
                    }

                    switch (skill)
                    {
                        case 1:
                            last += ch.name + " couldn't handle it. <br>";
                            break;
                        case 2:
                            last += ch.name + " handeled it someway. <br>";
                            break;
                        default:
                            last += ch.name + " handeled it really well. <br>";
                            break;

                    }

                    bonus += skill;

                    last += PrintEnd(stat);

                    tb.text.text = last;

                    done = true;
                }
                else
                {
                    base.BackInPlace();
                    tb.gameObject.SetActive(false);
                }
                
                

            }
        }

    }
    public override void OnMouseOver()
    {
        base.OnMouseOver();
    }
    public override void ShowToCamera()
    {
        base.ShowToCamera();
        tb.text.text = textToDisplay[current].text;
        current++;
        tb.gameObject.SetActive(true);
    }

    public virtual string PrintEnd(string stat)
    {
        string last = "";
        int l;
        if (bonus > 8)
        {
            l = Random.Range(1, 4);
            last += "It was a huge success! " + ch.name + " gained " + l + " " + stat;
            ch.AddBonus(l, stat);
        }
        else if (bonus > 6)
        {
            last += "It was a success! You can continue your journey...";
        }
        else if (bonus > 3)
        {
            l = Random.Range(1, 3);
            last += "Overall, it was not really good..." + ch.name + "lost" + l + " hp. ";
            if (!ch.ChangeHp(l * -1))
            {
                last += ch.name + "died. ";
            }
        }
        else
        {
            l = Random.Range(1, 3);
            int k = Random.Range(1, 4);
            last += "It was a disaster... " + ch.name + " lost " + l + " hp and " + k + " " + stat;
            ch.AddBonus(k * -1, stat);
            if (!ch.ChangeHp(l * -1))
            {
                last += "<br>" + ch.name + " died. ";
            }
        }
        last += " <br> ";
        return last;
    }
}
