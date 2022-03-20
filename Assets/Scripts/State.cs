using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class State
{
    public string text;
    public int jumpTo = -1;

    public bool isQuestion;
    public List<Answer> answers;
}
[System.Serializable]
public class Answer
{
    public string text;
    public int jumpTo = -1;
    public int bonus = 1;
}
