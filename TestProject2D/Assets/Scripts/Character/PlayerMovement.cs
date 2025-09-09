using System;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float speed; // 플레이어의 이동 속도
}


public class PlayerMovement : MonoBehaviour
{
    public PlayerStat stat; // 플레이어의 상태를 담는 PlayerStat 클래스 인스턴스
    Animator animator; // 애니메이터 컴포넌트

    private void Start()
    {
        // 애니메이터 컴포넌트를 가져옴
        animator = GetComponent<Animator>();
    }

    private Vector2 last = Vector2.down;

    void SetAnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            // magnitude : 벡터의 길이
            // x,y,z 에 대한 각각의 제곱의 합의 루트 값
            if (direction.magnitude > 0)
            {
                animator.SetBool("IsMove", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);

                last = direction.normalized; // 방향을 정규화하여 저장
            }
            else
            {
                animator.SetBool("IsMove", false);
                animator.SetFloat("horizontal", last.x);
                animator.SetFloat("vertical", last.y);
            }
        }
    }
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector2(h, v);
        SetAnimateMovement(dir);

        transform.position += dir * stat.speed * Time.deltaTime;
    }
}
