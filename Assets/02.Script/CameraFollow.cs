using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 두 사이의 거리
    Vector3 diff;

    // 따라다니는 대상
    public GameObject target;

    // 따라다니는 대상에 대한 속도(선형보간에 쓰임)
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // 둘 사이의 거리 구하기
        diff = target.transform.position - transform.position;
    }

    // LateUpdate는 Update 함수와 마찬가지 매 프레임 호출
    // 순서는 Update 함수의 처리가 모두 끝난 후
    // 즉 이동 처리가 끝난 후에 추적하는 카메라의 위치를 설정한다.
    private void LateUpdate()
    {
        // 카메라 포지션을 Lerp로 따라가기
        transform.position = Vector3.Lerp(transform.position, target.transform.position - diff, Time.deltaTime * followSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
