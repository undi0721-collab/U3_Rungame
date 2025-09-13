using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    // 각 스테이지 팁의 크기, z 축을 기준으로 30 단위로 스테이지가 이어짐.
    const int stageTip_Size = 30;

    // 현재 생성된 스테이지의 마지막 인덱스. 처음에는 30으로 설정되어 있음.
    int currentTipIndex = 30;

    // 캐릭터 위치를 받아오기 위한 Transform 변수
    public Transform character; 
    // 다양한 스테이지 팁 프리팹 배열. 스테이지가 반복되거나 무작위로 배열될 수 있음.
    public GameObject[] stageTips;
    // 스테이지 생성이 시작되는 인덱스
    public int startTipIndex;
    // 미리 생성 할 스테이지 팁의 개수 설정 (캐릭터가 이동 할 때 스테이지가 끊김 없이 이어지도록 함). 
    public int preInstantiate;
    // 생성된 스테이지 오브젝트들을 관리하기 위한 리스트.
    public List<GameObject> generateStageList = new List<GameObject>(); // 생성된 스테이지 보유 리스트

    void Start()
    {
        // 시작 인덱스를 설정. 스테이지는 startTipIndex로 시작하지만, 현재 인덱스는 -1을 해서 그 이전으로 설정.
        currentTipIndex = startTipIndex - 1;
        // 미리 설정한 preInstantiate 값만큼 스테이지 팁을 생성함.
        UpdateStage(preInstantiate);
    }

    void Update()
    {
        // 캐릭터의 현재 z 위치를 기준으로, 스테이지의 인덱스를 계산. 스테이지는 stageTip_Size 단위로 구분됨.
        int charaPositionIndex = (int)(character.position.z / stageTip_Size);

        // 캐릭터가 현재 생성된 스테이지 범위를 벗어나면 추가로 스테이지를 생성.
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            // 캐릭터의 인덱스와 미리 생성할 개수(preInstantiate)를 더해서 새로운 스테이지를 생성.
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    // 스테이지 팁을 생성하고 관리하는 함수. 인덱스가 toTipIndex까지의 스테이지 팁을 생성함.
    void UpdateStage(int toTipIndex)
    {
        // 현재 생성된 스테이지의 마지막 인덱스보다 작은 인덱스가 들어오면 함수를 종료함.
        if (toTipIndex <= currentTipIndex)
            return;

        // 현재 인덱스 이후부터 지정된 인덱스까지 스테이지 팁을 생성함.
        for(int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            // GenerateStage 함수로 새로운 스테이지 오브젝트를 생성.
            GameObject stageObject = GenerateStage(i);

            // 생성된 스테이지 오브젝트를 리스트에 추가하여 추적.
            generateStageList.Add(stageObject);
        }

        // 미리 생성한 개수보다 스테이지가 많아지면 가장 오래된 스테이지를 삭제함.
        while (generateStageList.Count > preInstantiate + 2)
        {
            DestroyOldestStage();
        }

        // 현재 생성된 마지막 스테이지의 인덱스를 갱신함.
        currentTipIndex = toTipIndex;
    }

    // 특정 인덱스의 스테이지 팁을 생성하는 함수. 랜덤하게 하나의 스테이지 팁을 선택해서 배치.
    GameObject GenerateStage(int tipIndex)
    {
        // stageTips 배열에서 무작위로 스테이지 팁을 선택 (배열의 길이만큼 범위 지정).
        int nextStageTip = Random.Range(0, stageTips.Length);

        // 선택된 스테이지 팁을 현재 인덱스에 맞는 위치에 생성함.
        // 스테이지는 x=0, y=0 위치에 고정되며 z는 인덱스에 따라 변화.
        // Quaternion.identity는 회전 없이 기본 방향으로 스테이지가 배치됨.
        GameObject stageObject = (GameObject)Instantiate(stageTips[nextStageTip], new Vector3(0, 0, tipIndex * stageTip_Size), Quaternion.identity);

        // 생성된 스테이지 오브젝트를 반환.
        return stageObject;
    }

    // 가장 오래된 스테이지를 삭제하는 함수.
    void DestroyOldestStage()
    {
        // 리스트의 첫 번째(가장 오래된) 스테이지를 가져옴.
        GameObject oldStage = generateStageList[0];

        // 리스트에서 가장 오래된 스테이지를 제거.
        generateStageList.RemoveAt(0);

        // 실제로 스테이지 오브젝트를 삭제하여 메모리와 리소스를 확보.
        Destroy(oldStage);
    }
}
