using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : CardEvent
{
    [SerializeField]string chName;
    public override void Start()
    {
        base.Start();
        characterSelected = true;
        ch = base.gm.FindCharacterOfName(chName);
        base.initial = chName + "- \' I think i should be coming with you... i feel it. \'";
        
    }

    public void GetChSkill()
    {
        if (ch.agility >= ch.strenght && ch.agility >= ch.intelligence && ch.agility >= ch.wisdom)
        {
            base.eventType = 0;
        }
        else if (ch.strenght >= ch.intelligence && ch.strenght >= ch.wisdom)
        {
            base.eventType = 1;
        }
        else if (ch.wisdom >= ch.intelligence)
        {
            base.eventType = 3;
        }
        else
        {
            base.eventType = 2;
        }
    }
    public override string PrintEnd(string stat)
    {
        int bonus = base.bonus;
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
            last += "Overall, it was not really good... Unfortunately, " + ch.name + " died. ";
            if (!ch.ChangeHp(10 * -1))
            {
                
            }
        }
        else
        {
            l = Random.Range(1, 3);
            int k = Random.Range(1, 4);
            last += "It was a disaster... Unfortunately, " + ch.name + " died. ";
            if (!ch.ChangeHp(10 * -1))
            {
                
            }
        }
        last += " <br> ";
        return last;
    }
}
