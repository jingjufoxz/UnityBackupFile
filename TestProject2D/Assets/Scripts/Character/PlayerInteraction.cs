using System;
using System.Collections;
using System.Text;
using Assets.Scripts.Title;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{     
    public float interactionDistance = 2f;

    public Button interactionButton; // 상호작용 버튼
    public GameObject dialoguePanel; // 대화창
    public Text dialogueText; // 대화 텍스트
    public Image dialoguePortrait; // NPC 초상화
    public Text dialogueName;
    public Button dialogueNextButton;
    
    public string playerName = "";
    private NPCData currentNPC;
    private int currentDialogueIndex = 0;

    void Awake()
    {
        interactionButton = GameObject.Find("InteractionButton").GetComponent<Button>();
        dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        dialoguePortrait = GameObject.Find("DialoguePortrait").GetComponent<Image>();
        dialogueName = GameObject.Find("DialogueName").GetComponent<Text>();
        dialogueNextButton = GameObject.Find("DialogueNextButton").GetComponent<Button>();
        

    }

    void Start()
    {

        if (interactionButton != null)
        {
            interactionButton.onClick.AddListener(ShowDialogue);
            interactionButton.gameObject.SetActive(false); // 시작 시 버튼 비활성화
        }
        if (dialoguePanel != null)
        {
            dialogueNextButton.onClick.AddListener(NextDialogueLine);
            dialoguePanel.SetActive(false); // 시작 시 대화창 비활성화
        }
    }

    void Update()
    {
        // 주변 NPC 감지 (이전 코드와 동일)
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance);
        currentNPC = null;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<NPC>())
            {
                currentNPC = hitCollider.GetComponent<NPC>().npcData;
                break;
            }
        }

        // 상호작용 버튼 활성화/비활성화, 대화창이 켜져 있을 때는 버튼 비활성화
        interactionButton.gameObject.SetActive(currentNPC != null && !dialoguePanel.activeSelf);
    }

    void ShowDialogue()
    {
        if (currentNPC != null && currentNPC.dialogueLines.Length > 0)
        {
            dialoguePanel.SetActive(true);
            currentDialogueIndex = 0; // 대화 시작 시 첫 대사부터 시작
            StartCoroutine(DisplayCurrentDialogueLine(dialogueText.text));
            dialogueName.text = currentNPC.npcName;
        }
    }

    public IEnumerator DisplayCurrentDialogueLine(string text)
    {
        if (currentDialogueIndex < currentNPC.dialogueLines.Length)
        {
            DialogueLine currentLine = currentNPC.dialogueLines[currentDialogueIndex];
            string processLine = currentLine.line;

            processLine = currentLine.line.Replace("{PlayerName}", playerName);
            dialogueText.text = processLine;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < processLine.Length; i++)
            {
                sb.Append(processLine[i]); // 현재 문자 추가
                dialogueText.text = sb.ToString(); // UI 텍스트 업데이트
                yield return new WaitForSeconds(0.1f); // 0.1초 대기
            }

            if (currentLine.portrait != null)
            {
                dialoguePortrait.sprite = currentLine.portrait; // 초상화 설정
            }
            else if (currentNPC.defaultportrait != null)
            {
                dialoguePortrait.sprite = currentNPC.defaultportrait; // 기본 초상화 설정
            }
            else
            {
                dialoguePortrait.sprite = null; // 초상화가 없으면 비활성화
            }
        }

        else
        {
            dialoguePanel.SetActive(false); // 모든 대사를 표시한 후 대화창 닫기
        }
    }

    public void NextDialogueLine()
    {
        currentDialogueIndex++;
        StartCoroutine(DisplayCurrentDialogueLine(dialogueText.text)); // 다음 대사 표시
    }

    // 디버깅을 위한 Gizmo     
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}