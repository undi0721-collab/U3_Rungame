using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 자동 달리기 차선 설정
    const int minLine = -2;
    const int maxLine = 2;
    const float lineWidth = 1.0f;

    // 플레이어 컨트롤러
    CharacterController playerController;

    // 추후 추가 애니메이션
    // Animator animator

    // 스턴, 데미지 처리 관련
    const int defaultLife = 3;
    const float stunDur = 0.5f;

    int life = defaultLife;
    float recoverTime = 0.0f;

    Vector3 moveDir = Vector3.zero;

    public float gravity;
    public float speed_Z;   
    public float speed_J;

    int targetLine;
    public float speed_X;       // 수평 방향 속도
    public float acceler_Z;    // 전진 방향 속도 

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        //Animator = GetConponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("left"))
        {
            Move_Left();
        }

        if(Input.GetKeyDown("right"))
        {
            Move_Right();
        }

        if(Input.GetKeyDown("space"))
        {
            Jump();
        }

        if(isStun())
        {
            moveDir.x = 0.0f;
            moveDir.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            // 가속 전진
            float acceleratedZ = moveDir.z + (acceler_Z * Time.deltaTime);
            moveDir.z = Mathf.Clamp(acceleratedZ, 0, speed_Z);
            //Debug.Log(moveDir.z);

            // 좌우 방향 구하기
            //float ratioX = (targetLine * lineWidth - transform.position.x) / lineWidth;
            float ratioX = targetLine * lineWidth - transform.position.x;
            moveDir.x = ratioX * speed_X;
        }

        /*
        // 땅에 닿아 있으면
        if(playerController.isGrounded)
        {
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                moveDir.z = Input.GetAxis("Vertical") * speed_Z;
            }
            else
            {
                moveDir.z = 0;
            }

            // 방향
            transform.Rotate(0, Input.GetAxis("Horizontal") * 0.5f, 0);

            //점프
            if(Input.GetButton("Jump"))
            {
                moveDir.y = speed_Z;
                // animator.SetTrigger("jump");
            }
        }
        */

        // 중력 계산
        moveDir.y -= gravity * Time.deltaTime;

        // 이동
        Vector3 globalDir = transform.TransformDirection(moveDir);
        playerController.Move(globalDir * Time.deltaTime);

        // 이동하면서 땅에 닿아있으면 Y는 고정
        if(playerController.isGrounded)
        {
            moveDir.y = 0;
        }

        // 속도 0 이상이면 animator 전환
        // animator.SetBool("run", moveDir.z > 0.0f);
    }
    public int Life()
    {
        return life;
    }

    public bool isStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    public void Move_Left()
    {
        if (isStun())
            return;

        if(playerController.isGrounded && targetLine > minLine)
        {
            EffectSound.LeftSoundPlay();
            targetLine--;
        }
    }

    public void Move_Right()
    {
        if (isStun())
            return;

        if(playerController.isGrounded && targetLine < maxLine)
        {
            EffectSound.RightSoundPlay();
            targetLine++;
        }
    }

    public void Jump()
    {
        if (isStun())
            return;

        if(playerController.isGrounded)
        {
            EffectSound.JumpSoundPlay();
            moveDir.y = speed_J;
            // 애니메이터 점프 트리거 설정 하는 곳
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isStun())
            return;
        //Debug.Log(hit.transform.name);

        // tag가 다른경우 구현되지 않습니다.
        if(hit.gameObject.tag == "Enemy")
        {
            //  생명 줄이고 기절 상태로
            life--;
            recoverTime = stunDur;

            Destroy(hit.gameObject);

            Debug.Log(life);
            Debug.Log(hit.gameObject.name);
            // 데미지 트리거 애니메이션 신호 주는곳

            // 부딪힌 오브젝트 삭제
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.tag == "Life")
        {
            life++;
            Destroy(hit.gameObject);
        }
        else if ( hit.gameObject.tag == "Life")
        {
            if (life < 3)
            {
                life++;
                if ( life < 3)
                {
                    life = 3;

                }
                Destroy(hit.gameObject);
            }
        }
    }
}
