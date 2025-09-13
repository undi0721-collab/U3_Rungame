using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    /* ��ֹ��� ��ġ�Ǿ� �ִ� ���������� �迭�� ���� �� ��
     * �� ������������ �������� ���� ���ҽ��� Ŀ���� �˴ϴ�.
     * �׷��� �� �������� �����ϴ� ������ �����Ͽ� ���ҽ��� ���̴� ����Դϴ�.
     */
    public GameObject prefab; // �� �ʸ��� ��ġ�� ������ ������ ��ֹ�(���ӿ�����Ʈ ���� ��)

    private void Awake()
    {
        // �ν����� â�� �־���� �������� ���� �ϴ� �ڵ�
        GameObject go = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);

        // �������� ���� �� �� ��ü �����ǵ��� ��Ʈ ����
        go.transform.SetParent(transform, false);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        // ��ġ��ġ�� �� ���� ���� ���ϰ� �뷫���� ��ġ�� ���� �� �ְ� �Լ� ���

        // ����� �Ʒ��κ� ����� ���� ���� �����ϴ� ��
        Vector3 offset = new Vector3(0, 0.5f, 0);

        // ���Ǿ� �� ���� �� ��ġ ���
        Gizmos.color = new Color(0, 1, 0, 0.5f); // �� �����Ӱ�!
        Gizmos.DrawSphere(transform.position + offset, 0.5f);

        // �����տ� �־����ִ��� ����üũ
        if(prefab != null)
        {
            Gizmos.DrawIcon(transform.position + offset, prefab.name, true);
        }
    }
}
