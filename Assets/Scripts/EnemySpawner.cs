using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float e_SpawnTime;
    private float b_SpawnTime;
    private float m_SpawnTime;

    private float minDistB = 7.5f;  //Basic타입 적이 생성될 최소 거리
    private float maxDistB = 10f;   //Basic타입 적이 생성될 최대 거리
    // Update is called once per frame
    void Update()
    {
        e_SpawnTime += Time.deltaTime; //적 생성 시간을 잼
        if(e_SpawnTime > 1.5f)   //생성시간이 되면 타이머를 초기화 하고 적 소환함수 실행
        {
            e_SpawnTime = 0;
            SpawnBasic();
        }

        b_SpawnTime += Time.deltaTime;
        if (b_SpawnTime > 180f || Input.GetKeyDown(KeyCode.R))
        {
            b_SpawnTime = 0;
            SpawnBat();
        }

        m_SpawnTime = Time.time;
        if (m_SpawnTime > 300f || Input.GetKeyDown(KeyCode.Space))
        {
            m_SpawnTime = 0;
            SpawnRound();
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
        GameObject enemy1 = GameManager.Instance.pool.Get(2);
        enemy1.transform.position = randPos;

        //이후 생성될 적들이 첫 번째로 생성된 적 기준으로 어느정도 모아져서 생성되도록 함
        for (int i = 0; i < 6; i++) {
            GameObject enemy2 =GameManager.Instance.pool.Get(2);

            Vector2 randDir2 = Random.insideUnitCircle.normalized;  //반복문을 돌 때마다 랜덤방향을 받아냄
            //첫 번째로 생성된 적 기준 주변으로 최소한의 거리에 새로 생성된 적의 위치를 지정
            Vector3 randPos2 = randPos + new Vector3(randDir2.x, randDir2.y, 0) * 0.6f;
            enemy2.transform.position = randPos2;
        }
    }

    void SpawnRound()   //플레이어를 감싸는 적 생성
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어의 현재 위치값을 가져옴
        float radius = 10f; //플레이어까지의 거리
        float angleStep = 360f/20;  //360도를 20등분으로 나누어 스폰할 몬스터의 간격 설정
        for(int i = 0; i < 20; i++)
        {
            GameObject enemy = GameManager.Instance.pool.Get(1);

            float angle = angleStep * i;    //i값 만큼 각도 크기 계산
            
            float angleRad = angle*Mathf.Deg2Rad;   //각도를 라디안으로 변환

            float posX = playerPos.x + Mathf.Cos(angleRad)*radius;  //플레이어 위치 기준으로 x좌표 계산
            float posY = playerPos.y + Mathf.Sin(angleRad)*radius;  //플레이어 위치 기준으로 y좌표 계산

            Vector3 spawnPos = new Vector3(posX, posY, 0);  //스폰좌표 지정
            enemy.transform.position = spawnPos;    //적 위치 설정
        }
    }
}
