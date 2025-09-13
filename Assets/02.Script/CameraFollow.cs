using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // �� ������ �Ÿ�
    Vector3 diff;

    // ����ٴϴ� ���
    public GameObject target;

    // ����ٴϴ� ��� ���� �ӵ�(���������� ����)
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // �� ������ �Ÿ� ���ϱ�
        diff = target.transform.position - transform.position;
    }

    // LateUpdate�� Update �Լ��� �������� �� ������ ȣ��
    // ������ Update �Լ��� ó���� ��� ���� ��
    // �� �̵� ó���� ���� �Ŀ� �����ϴ� ī�޶��� ��ġ�� �����Ѵ�.
    private void LateUpdate()
    {
        // ī�޶� �������� Lerp�� ���󰡱�
        transform.position = Vector3.Lerp(transform.position, target.transform.position - diff, Time.deltaTime * followSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
