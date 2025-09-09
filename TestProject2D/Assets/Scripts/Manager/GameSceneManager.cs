
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject spawnPoint; // 캐릭터가 생성될 위치
    public CharacterData[] characterPrefabs; // 캐릭터 프리팹
    public CinemachineCamera cinemachineCamera;
    private string selectedCharacterName; // 선택된 캐릭터 이름

    void Start()
    {
        selectedCharacterName = PlayerPrefs.GetString("SelectedCharacterName", "이름 없음");

        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            GameObject player = Instantiate(characterPrefabs[selectedCharacterIndex].characterPrefab,
                                spawnPoint.transform.position, Quaternion.identity);

            PlayerInteraction interaction = player.GetComponent<PlayerInteraction>();

            if (interaction != null)
            { 
                cinemachineCamera.Target.TrackingTarget = player.transform;
                interaction.playerName = selectedCharacterName;
                interaction.interactionButton.gameObject.SetActive(true);
                interaction.dialoguePanel.gameObject.SetActive(true);
                interaction.dialogueText.gameObject.SetActive(true);
                interaction.dialoguePortrait.gameObject.SetActive(true);
            }

            // 플레이어 컨트롤러나 다른 스크립트에 캐릭터 데이터 전달
            // player.GetComponent<PlayerController>().SetCharacterData(characterPrefabs[selectedCharacterIndex]);
        }
        else
        {
            Debug.LogError("Invalid character index: " + selectedCharacterIndex);
        }
    }

    public void BackTitleButtonClick()
    {
        SceneManager.LoadScene("TitleScene"); // 게임 씬으로 이동
    }
}