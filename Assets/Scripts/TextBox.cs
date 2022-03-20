using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextBox : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<TextMeshProUGUI> ans;
    CardEvent ce;

    public void DisableAnswers()
    {
        foreach(var a in ans)
        {
            a.transform.parent.gameObject.SetActive(false);
        }
    }
    public void Answered(int i)
    {
        ce.answerPicked = i;
        ce.enableMovement = true;
        ce.BackInPlace();
    }
    public void SetCardEvent(CardEvent c)
    {
        ce = c;
    }
}
