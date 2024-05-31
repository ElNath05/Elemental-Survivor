using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float e_SpawnTime;
    private float b_SpawnTime;
    private float m_SpawnTime;

    private float minDistB = 7.5f;  //BasicŸ�� ���� ������ �ּ� �Ÿ�
    private float maxDistB = 10f;   //BasicŸ�� ���� ������ �ִ� �Ÿ�
    // Update is called once per frame
    void Update()
    {
        e_SpawnTime += Time.deltaTime; //�� ���� �ð��� ��
        if(e_SpawnTime > 1.5f)   //�����ð��� �Ǹ� Ÿ�̸Ӹ� �ʱ�ȭ �ϰ� �� ��ȯ�Լ� ����
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
    void SpawnBasic()   //�÷��̾ ���󰡴� �Ϲ� �� ����
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);    // index0�� �ش��ϴ� ������Ʈ�� ������

        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ���� ��ġ���� ������
        Vector2 randDir = Random.insideUnitCircle.normalized;   //���������� ���͸� ����
        float randDist = Random.Range(minDistB, maxDistB);  //������Ʈ�� ������ �Ÿ��� �������� �޾Ƴ�

        //�÷��̾� �������� Ư�� �Ÿ������� ������ ��ġ�� �� ����
        Vector3 randPos = playerPos + new Vector3(randDir.x, randDir.y, 0) * randDist;
        enemy.transform.position = randPos;
    }

    void SpawnBat() //���ļ� �÷��̾ �ִ� �������� ���ư��� �� ����
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ���� ��ġ���� ������
        Vector2 randDir = Random.insideUnitCircle.normalized;   //���������� ���͸� ����
        float randDist = Random.Range(minDistB, maxDistB);  //������Ʈ�� ������ �Ÿ��� �������� �޾Ƴ�

        //�÷��̾� �������� Ư�� �Ÿ������� ������ ��ġ�� �� ����
        Vector3 randPos = playerPos + new Vector3(randDir.x, randDir.y, 0) * randDist;
        GameObject enemy1 = GameManager.Instance.pool.Get(2);
        enemy1.transform.position = randPos;

        //���� ������ ������ ù ��°�� ������ �� �������� ������� ������� �����ǵ��� ��
        for (int i = 0; i < 6; i++) {
            GameObject enemy2 =GameManager.Instance.pool.Get(2);

            Vector2 randDir2 = Random.insideUnitCircle.normalized;  //�ݺ����� �� ������ ���������� �޾Ƴ�
            //ù ��°�� ������ �� ���� �ֺ����� �ּ����� �Ÿ��� ���� ������ ���� ��ġ�� ����
            Vector3 randPos2 = randPos + new Vector3(randDir2.x, randDir2.y, 0) * 0.6f;
            enemy2.transform.position = randPos2;
        }
    }

    void SpawnRound()   //�÷��̾ ���δ� �� ����
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ���� ��ġ���� ������
        float radius = 10f; //�÷��̾������ �Ÿ�
        float angleStep = 360f/20;  //360���� 20������� ������ ������ ������ ���� ����
        for(int i = 0; i < 20; i++)
        {
            GameObject enemy = GameManager.Instance.pool.Get(1);

            float angle = angleStep * i;    //i�� ��ŭ ���� ũ�� ���
            
            float angleRad = angle*Mathf.Deg2Rad;   //������ �������� ��ȯ

            float posX = playerPos.x + Mathf.Cos(angleRad)*radius;  //�÷��̾� ��ġ �������� x��ǥ ���
            float posY = playerPos.y + Mathf.Sin(angleRad)*radius;  //�÷��̾� ��ġ �������� y��ǥ ���

            Vector3 spawnPos = new Vector3(posX, posY, 0);  //������ǥ ����
            enemy.transform.position = spawnPos;    //�� ��ġ ����
        }
    }
}
