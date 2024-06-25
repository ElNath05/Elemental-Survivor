using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; //탐색범위
    public LayerMask targetLayer;    //목표대상의 레이어
    public LayerMask targetLayer2;
    public RaycastHit2D[] targets;  //범위에 들어온 적들을 담는 배열
    public Transform nearTarget;    //가까운 적의 트랜스폼
    public Transform randTarget;    //범위 내의 무작위의 적의 트랜스폼
    void FixedUpdate()
    {
        //(레이캐스트의 시작위치, 범위, 캐스팅방향, 캐스팅 길이, 적의 레이어) 원형캐스트는 방향이 필요없기때문에 Vectro2.zero를 사용
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);  //원형의 레이범위 내에 들어온 오브젝트들을 리스트에 넣음
        //targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer2);
        nearTarget = GetNearest();
        randTarget = GetRandomTarget();
    }

    Transform GetRandomTarget() //레이범위에 들어온 타겟 중 랜덤한 하나의 타겟의 위치를 가져옴
    {
        if (targets.Length == 0)    //타겟이 없으면 탈출
            return null;

        Transform result = null;
        int randNum = Random.Range(0, targets.Length);  //레이에 들어온 타겟 중 하나의 인덱스 값을 가져와

        result = targets[randNum].transform;    //그 인덱스값에 해당하는 타겟의 트랜스폼을 받아옴
        return result;
    }

    Transform GetNearest()
    {
        Transform result = null;
        float dist = 100;   //최소거리 비교용으로 사용할 변수

        //레이에 맞은 타겟중에서 플레이어와의 거리가 가장 가까운 오브젝트의 transform을 result로 저장
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDist = Vector3.Distance(myPos, targetPos); //Vector3.Distance로 플레이어와 타겟사이의 거리를 구함

            if(curDist < dist)
            {
                dist = curDist;
                result = target.transform;
            }
        }
        return result;
    }
}
