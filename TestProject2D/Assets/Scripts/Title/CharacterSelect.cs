using System;
using System.Collections.Generic;
using Assets.Scripts.Title;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CharacterSelect : MonoBehaviour
{
    public List<CharacterData> characterList; // 캐릭터 프리팹 리스트
    public Image portraitImage; // 캐릭터 초상화 이미지 UI 요소
    public GameObject characterSpawnPoint; // 캐릭터가 생성될 위치
    public GameObject selectButtonMenu; // 선택 버튼 메뉴 오브젝트
    public Text characterName;
    public Button nextButton, prevButton, startButton;
    private GameObject currentCharacterModel; // 현재 선택된 캐릭터 모델
    private int selectedIndex = 0;
    private string selectedCharacterName;

    TitleManager titleManager;

    void Start()
    {
        if (characterList == null || characterList.Count == 0)
        {
            Debug.LogError("Character list is empty or not assigned.");
            return;
        }

        if (characterName == null || portraitImage == null || characterSpawnPoint == null ||
            nextButton == null || prevButton == null || startButton == null)
        {
            Debug.LogError("UI elements are not assigned properly.");
            return;
        }
        UpdateCharacterDisplay();

        nextButton.onClick.AddListener(SelectNext);
        prevButton.onClick.AddListener(SelectPrev);
        startButton.onClick.AddListener(StartGame);
    }

    public void UpdateCharacterDisplay()
    {

        // 이전 캐릭터 모델이 있다면 제거
        if (currentCharacterModel != null)
        {
            Destroy(currentCharacterModel);
        }

        // 현재 선택된 캐릭터 모델 생성
        CharacterData selectedCharacter = characterList[selectedIndex];

        // 선택된 캐릭터 이름 저장
        selectedCharacterName = selectedCharacter.characterName;

        // UI 업데이트 (초상화 이미지 설정)
        portraitImage.sprite = selectedCharacter.portrait;

        //UI 업데이트 (캐릭터 선택 시 이름 설정)
        characterName.text = selectedCharacter.name;

        // 캐릭터 모델 생성
        currentCharacterModel = Instantiate(selectedCharacter.characterPrefab,
                                 characterSpawnPoint.transform.position, Quaternion.identity);
        
       
    }


    private void SelectNext()
    {
        selectedIndex++;
        if (selectedIndex >= characterList.Count)
        {
            selectedIndex = 0; // 루프
        }
        UpdateCharacterDisplay();
    }

    private void SelectPrev()
    {
        selectedIndex--;
        if (selectedIndex < 0)
        {
            selectedIndex = characterList.Count - 1; // 루프
        }
        UpdateCharacterDisplay();
    }

    private void StartGame()
    {
        // 선택된 캐릭터 정보를 저장하고 게임 시작
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);
        PlayerPrefs.SetString("SelectedCharacterName", selectedCharacterName); // 선택된 캐릭터 이름도 저장
        SceneManager.LoadScene("GameScene"); // 게임 씬으로 이동
    }
}