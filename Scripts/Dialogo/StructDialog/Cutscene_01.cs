using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueC01
{
    public string name;
    [TextArea(5, 10)]
    public string text;
}

[CreateAssetMenu(fileName = "Cutscene_01", menuName = "ScriptableObject/TalkScript", order = 1)]
public class Cutscene_01 : ScriptableObject
{
    public List<DialogueC01> talkScript;
}
