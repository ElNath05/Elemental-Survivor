using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float e_SpawnTime;

    private float minDistB = 7.5f;  //Basic타입 적이 생성될 최소 거리
    private float maxDistB = 10f;   //Basic타입 적이 생성될 최대 거리
    // Update is called once per frame
    void Update()
    {
        e_SpawnTime += Time.deltaTime; //적 생성 시간을 잼
        if(e_SpawnTime > 1f)   //생성시간이 되면 타이머를 초기화 하고 적 소환함수 실행
        {
            e_SpawnTime = 0;
            SpawnBasic();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBat();
        }
        
    }
    void SpawnBasic()   //플레이어를 따라가는 일반 적 생성
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);    // index0에 해당하는 오브젝트를 가져옴

        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어의 현재 위치값을 가져옴
        Vector2 randDir = Random.insideUnitCircle.normalized;   //랜덤방향의 벡터를 생성
        float randDist = Random.Range(minDistB, maxDistB);  //오브젝트를 생성할 거리를 랜덤으로 받아냄

        //플레이어 기준으로 특정 거리내에서 랜덤한 위치에 적 생성
        Vector3 randPos = playerPos + new Vector3(randDir.x, randDir.y, 0) * randDist;
        enemy.transform.position = randPos;
    }

    void SpawnBat() //뭉쳐서 플레이어가 있던 방향으로 날아가는 적 생성
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어의 현재 위치값을 가져옴
        Vector2 randDir = Random.insideUnitCircle.normalized;   //랜덤방향의 벡터를 생성
        float randDist = Random.Range(minDistB, maxDistB);  //오브젝트를 생성할 거리를 랜덤으로 받아냄

        //플레이어 기준으로 특정 거리내에서 랜덤한 위치에 적 생성
        Vector3 randPos = playerPos + new Vector3(randDir.x, randDir.y, 0) * randDist;
        for(int i = 0; i < 6; i++) {
            GameObject enemy =GameManager.Instance.pool.Get(2);
            enemy.transform.position = randPos;
        }
    }

    void SpawnRound()   //플레이어를 감싸는 적 생성
    {

    }
}
