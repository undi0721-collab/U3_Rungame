using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    // �� �������� ���� ũ��, z ���� �������� 30 ������ ���������� �̾���.
    const int stageTip_Size = 30;

    // ���� ������ ���������� ������ �ε���. ó������ 30���� �����Ǿ� ����.
    int currentTipIndex = 30;

    // ĳ���� ��ġ�� �޾ƿ��� ���� Transform ����
    public Transform character; 
    // �پ��� �������� �� ������ �迭. ���������� �ݺ��ǰų� �������� �迭�� �� ����.
    public GameObject[] stageTips;
    // �������� ������ ���۵Ǵ� �ε���
    public int startTipIndex;
    // �̸� ���� �� �������� ���� ���� ���� (ĳ���Ͱ� �̵� �� �� ���������� ���� ���� �̾������� ��). 
    public int preInstantiate;
    // ������ �������� ������Ʈ���� �����ϱ� ���� ����Ʈ.
    public List<GameObject> generateStageList = new List<GameObject>(); // ������ �������� ���� ����Ʈ

    void Start()
    {
        // ���� �ε����� ����. ���������� startTipIndex�� ����������, ���� �ε����� -1�� �ؼ� �� �������� ����.
        currentTipIndex = startTipIndex - 1;
        // �̸� ������ preInstantiate ����ŭ �������� ���� ������.
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        // ĳ������ ���� z ��ġ�� ��������, ���������� �ε����� ���. ���������� stageTip_Size ������ ���е�.
        int charaPositionIndex = (int)(character.position.z / stageTip_Size);

        // ĳ���Ͱ� ���� ������ �������� ������ ����� �߰��� ���������� ����.
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            // ĳ������ �ε����� �̸� ������ ����(preInstantiate)�� ���ؼ� ���ο� ���������� ����.
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    // �������� ���� �����ϰ� �����ϴ� �Լ�. �ε����� toTipIndex������ �������� ���� ������.
    void UpdateStage(int toTipIndex)
    {
        // ���� ������ ���������� ������ �ε������� ���� �ε����� ������ �Լ��� ������.
        if (toTipIndex <= currentTipIndex)
            return;

        // ���� �ε��� ���ĺ��� ������ �ε������� �������� ���� ������.
        for(int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            // GenerateStage �Լ��� ���ο� �������� ������Ʈ�� ����.
            GameObject stageObject = GenerateStage(i);

            // ������ �������� ������Ʈ�� ����Ʈ�� �߰��Ͽ� ����.
            generateStageList.Add(stageObject);
        }

        // �̸� ������ �������� ���������� �������� ���� ������ ���������� ������.
        while (generateStageList.Count > preInstantiate + 2)
        {
            DestroyOldestStage();
        }

        // ���� ������ ������ ���������� �ε����� ������.
        currentTipIndex = toTipIndex;
    }

    // Ư�� �ε����� �������� ���� �����ϴ� �Լ�. �����ϰ� �ϳ��� �������� ���� �����ؼ� ��ġ.
    GameObject GenerateStage(int tipIndex)
    {
        // stageTips �迭���� �������� �������� ���� ���� (�迭�� ���̸�ŭ ���� ����).
        int nextStageTip = Random.Range(0, stageTips.Length);

        // ���õ� �������� ���� ���� �ε����� �´� ��ġ�� ������.
        // ���������� x=0, y=0 ��ġ�� �����Ǹ� z�� �ε����� ���� ��ȭ.
        // Quaternion.identity�� ȸ�� ���� �⺻ �������� ���������� ��ġ��.
        GameObject stageObject = (GameObject)Instantiate(stageTips[nextStageTip], new Vector3(0, 0, tipIndex * stageTip_Size), Quaternion.identity);

        // ������ �������� ������Ʈ�� ��ȯ.
        return stageObject;
    }

    // ���� ������ ���������� �����ϴ� �Լ�.
    void DestroyOldestStage()
    {
        // ����Ʈ�� ù ��°(���� ������) ���������� ������.
        GameObject oldStage = generateStageList[0];

        // ����Ʈ���� ���� ������ ���������� ����.
        generateStageList.RemoveAt(0);

        // ������ �������� ������Ʈ�� �����Ͽ� �޸𸮿� ���ҽ��� Ȯ��.
        Destroy(oldStage);
    }
}
