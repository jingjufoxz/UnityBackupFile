using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Title
{
    // 타이틀 매니저 클래스
    // 타이틀 화면에서의 메뉴 관리 및 버튼 클릭 이벤트 처리
    // 캐릭터 선택, 게임 설명, 게임 종료 확인 등의 기능을 포함
    // 뒤로가기 버튼을 통해 메뉴를 닫는 기능도 포함
    // 게임 종료 기능도 포함
    // 유니티 에디터와 빌드된 게임에서의 종료 처리 방식이 다름
    public class TitleManager : MonoBehaviour
    {
        // 타이틀 시작 메뉴 버튼
        public GameObject characterSelectMenu; // 캐릭터 선택 메뉴 버튼
        public GameObject descriptionMenu;
        public GameObject exitCheckMenu; // 게임 종료 확인 버튼

        // 뒤로가기 버튼
        public GameObject cancelButton;



        public void CharacterSelectClick()
        {

            // 캐릭터 선택 메뉴 로직
            if (characterSelectMenu.activeSelf == false)
            {
                characterSelectMenu.SetActive(true);
            }
            else
            {
                characterSelectMenu.SetActive(false);
            }
            Debug.Log("캐릭터 선택 메뉴를 엽니다.");

        }


        public void DescriptionClick()
        {
            if (descriptionMenu.activeSelf == true)
            {
                descriptionMenu.SetActive(false);
            }
            else
            {
                descriptionMenu.SetActive(true);
            }
        }
        public void ExitMenuClick()
        {
            if (exitCheckMenu.activeSelf == true)
            {
                exitCheckMenu.SetActive(false);
            }
            else
            {
                exitCheckMenu.SetActive(true);
            }
        }

        public void CancelClick()
        {
            // 취소 버튼 클릭 시
            if (descriptionMenu.activeSelf == true)
            {
                descriptionMenu.SetActive(false);
            }
            else if (exitCheckMenu.activeSelf == true)
            {
                exitCheckMenu.SetActive(false);
            }
            else if (characterSelectMenu.activeSelf == true)
            {
                characterSelectMenu.SetActive(false);
            }
            else
            {
                // 다른 메뉴가 열려있지 않으면 아무 작업도 하지 않음
                Debug.Log("취소할 메뉴가 없습니다.");
            }

        }

        public void ExitGame()
        {
#if UNITY_EDITOR // 유니티 에디터 쪽에서의 작업
            UnityEditor.EditorApplication.isPlaying = false;
            // 누르면 바로 꺼지는 기능 (모바일, 빌드용)
#else
        Application.Quit(); // 현재 비활성화되는 코드가 바로 적용
#endif
        }
    }
}
