using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �ڵ� �޸��� ���� ����
    const int minLine = -2;
    const int maxLine = 2;
    const float lineWidth = 1.0f;

    // �÷��̾� ��Ʈ�ѷ�
    CharacterController playerController;

    // ���� �߰� �ִϸ��̼�
    // Animator animator

    // ����, ������ ó�� ����
    const int defaultLife = 3;
    const float stunDur = 0.5f;

    int life = defaultLife;
    float recoverTime = 0.0f;

    Vector3 moveDir = Vector3.zero;

    public float gravity;
    public float speed_Z;   
    public float speed_J;

    int targetLine;
    public float speed_X;       // ���� ���� �ӵ�
    public float acceler_Z;    // ���� ���� �ӵ� 

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
            // ���� ����
            float acceleratedZ = moveDir.z + (acceler_Z * Time.deltaTime);
            moveDir.z = Mathf.Clamp(acceleratedZ, 0, speed_Z);
            //Debug.Log(moveDir.z);

            // �¿� ���� ���ϱ�
            //float ratioX = (targetLine * lineWidth - transform.position.x) / lineWidth;
            float ratioX = targetLine * lineWidth - transform.position.x;
            moveDir.x = ratioX * speed_X;
        }

        /*
        // ���� ��� ������
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

            // ����
            transform.Rotate(0, Input.GetAxis("Horizontal") * 0.5f, 0);

            //����
            if(Input.GetButton("Jump"))
            {
                moveDir.y = speed_Z;
                // animator.SetTrigger("jump");
            }
        }
        */

        // �߷� ���
        moveDir.y -= gravity * Time.deltaTime;

        // �̵�
        Vector3 globalDir = transform.TransformDirection(moveDir);
        playerController.Move(globalDir * Time.deltaTime);

        // �̵��ϸ鼭 ���� ��������� Y�� ����
        if(playerController.isGrounded)
        {
            moveDir.y = 0;
        }

        // �ӵ� 0 �̻��̸� animator ��ȯ
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
            // �ִϸ����� ���� Ʈ���� ���� �ϴ� ��
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isStun())
            return;
        //Debug.Log(hit.transform.name);

        // tag�� �ٸ���� �������� �ʽ��ϴ�.
        if(hit.gameObject.tag == "Enemy")
        {
            //  ���� ���̰� ���� ���·�
            life--;
            recoverTime = stunDur;

            Destroy(hit.gameObject);

            Debug.Log(life);
            Debug.Log(hit.gameObject.name);
            // ������ Ʈ���� �ִϸ��̼� ��ȣ �ִ°�

            // �ε��� ������Ʈ ����
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
