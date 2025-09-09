using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName; // 캐릭터 이름
    public GameObject characterPrefab; // 캐릭터 프리펩
    public Sprite portrait; // 캐릭터 이미지
}
