using System;
using UnityEngine;

[Serializable]
public struct DialogueLine
{
    [TextArea(3, 10)] // 여러 줄의 텍스트를 입력할 수 있게 해주는 속성
    public string line;
    public Sprite portrait; // NPC의 초상화
}

[CreateAssetMenu(fileName = "NewNPCData", menuName = "ScriptableObjects/NPC Data", order = 2)]
public class NPCData : ScriptableObject
{    
    public string npcName;
    public Sprite defaultportrait; // 기본 초상화 
    public DialogueLine[] dialogueLines;
    public GameObject npcPrefab; 
}