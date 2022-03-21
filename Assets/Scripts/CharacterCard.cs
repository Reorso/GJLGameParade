using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCard : Card
{
    public string name;
    public int agility = 0, strenght = 0, intelligence = 0, wisdom = 0, hp = 5;
    [SerializeField]TextMeshPro AGI, STR, INT, WIS, HP, NAME;
    bool needSelection = false;
    bool characterSelected = false;
    bool hovering = false;
    public CardEvent ce;
    public bool dead = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //GetComponent<Card>().Flip();
        UpdateValues();
        HP.text = "HP: " + hp.ToString();
        NAME.text = name;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    public override void OnMouseOver()
    {
        if (needSelection)
        {
            hovering = true;
            if (!base.facingUp)
            {
                Flip();
                
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                
                if (!characterSelected)
                {
                    gm.SelectCh(gameObject);
                    characterSelected = true;
                    needSelection = false;
                }
            }
        }
        else
        {
            
            base.OnMouseOver();
        }
    }
    private void OnMouseExit()
    {
        if (hovering)
        {
            hovering = false;
            
            BackInPlace();

        }
    }

    public void AddBonus(int value, string bonus)
    {
        switch (bonus)
        {
            case "AGI":
                agility += value;
                break;
            case "STR":
                strenght += value;
                break;
            case "INT":
                intelligence += value;
                break;
            case "WIS":
                wisdom += value;
                break;
            default:
                break;
        }
        UpdateValues();
    }

    void UpdateValues()
    {
        AGI.text = "AGI: "+agility.ToString();
        STR.text = "STR: "+strenght.ToString();
        INT.text = "INT: "+intelligence.ToString();
        WIS.text = "WIS: "+wisdom.ToString();
    }
    public override void BackInPlace()
    {
        base.BackInPlace();
    }
    public void SetCharacterSelection(CardEvent c, bool v)
    {
        needSelection = v;
        characterSelected = !v;
        ce = c;
        
    }

    public bool ChangeHp(int c)
    {
        hp += c;
        HP.text = "HP: " + hp.ToString();
        if (hp <= 0)
        {
            hp = 0;
            dead = true;
            gm.deathCount++;
            if(gm.deathCount >= 6)
            {
                gm.Lose();
            }
        }
        return hp > 0;
    }
}
