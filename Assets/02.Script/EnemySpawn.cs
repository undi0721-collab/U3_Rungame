using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    /* 장애물이 배치되어 있는 스테이지를 배열로 관리 할 때
     * 각 스테이지마다 프리팹이 들어가서 리소스가 커지게 됩니다.
     * 그래서 한 프리팹을 참조하는 식으로 변경하여 리소스를 줄이는 방법입니다.
     */
    public GameObject prefab; // 각 맵마다 배치된 곳에서 생성될 장애물(게임오브젝트 담을 곳)

    private void Awake()
    {
        // 인스펙터 창에 넣어놓은 프리팹을 생성 하는 코드
        GameObject go = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);

        // 스테이지 삭제 될 때 전체 삭제되도록 세트 설정
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
        // 배치위치를 한 눈에 보기 편하고 대략적인 위치를 정할 수 있게 함수 사용

        // 기즈모 아랫부분 지면과 같은 높이 설정하는 값
        Vector3 offset = new Vector3(0, 0.5f, 0);

        // 스피어 색 설정 및 위치 잡기
        Gizmos.color = new Color(0, 1, 0, 0.5f); // 색 자유롭게!
        Gizmos.DrawSphere(transform.position + offset, 0.5f);

        // 프리팹에 넣어져있는지 예외체크
        if(prefab != null)
        {
            Gizmos.DrawIcon(transform.position + offset, prefab.name, true);
        }
    }
}
