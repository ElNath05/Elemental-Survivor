using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float e_SpawnTime;
    private float b_SpawnTime;
    private float m_SpawnTime;
    private float bossSpawnTime;

    private bool bossSpawn;

    private float minDistB = 7.5f;  //BasicŸ�� ���� ������ �ּ� �Ÿ�
    private float maxDistB = 10f;   //BasicŸ�� ���� ������ �ִ� �Ÿ�
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPlaying) //������ �����ϸ� ������Ʈ�Լ� ���� �ð��� �Ȱ����� �Ѵ�
            return;
        e_SpawnTime += Time.deltaTime; //�� ���� �ð��� ��
        if(e_SpawnTime > 0.5f && GameManager.Instance.min <= 1.5)   //�����ð��� �Ǹ� Ÿ�̸Ӹ� �ʱ�ȭ �ϰ� �� ��ȯ�Լ� ����
        {
            e_SpawnTime = 0;
            SpawnBasic();
        }

        if (e_SpawnTime > 0.35f && GameManager.Instance.min >= 1.5 && GameManager.Instance.min < 3)   //�����ð��� �Ǹ� Ÿ�̸Ӹ� �ʱ�ȭ �ϰ� �� ��ȯ�Լ� ����
        {
            e_SpawnTime = 0;
            SpawnBasic2();
        }

        if (e_SpawnTime > 0.25f && GameManager.Instance.min >= 3)   //�����ð��� �Ǹ� Ÿ�̸Ӹ� �ʱ�ȭ �ϰ� �� ��ȯ�Լ� ����
        {
            e_SpawnTime = 0;
            SpawnBasic3();
        }

        b_SpawnTime += Time.deltaTime;  //���� ��ȯ
        if (b_SpawnTime > 60f)
        {
            b_SpawnTime = 0;
            SpawnBat();
        }

        m_SpawnTime += Time.deltaTime;  //�ѷ��δ� �� ��ȯ
        if (m_SpawnTime > 150f)
        {
            m_SpawnTime = 0;
            SpawnRound();
        }

        if(GameManager.Instance.min > 10 && !bossSpawn) // ������ȯ
        {
            bossSpawn = true;
            SpawnBoss();
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

    void SpawnBasic2()   //�÷��̾ ���󰡴� �Ϲ� �� ����
    {
        GameObject enemy = GameManager.Instance.pool.Get(8);    // index0�� �ش��ϴ� ������Ʈ�� ������

        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ���� ��ġ���� ������
        Vector2 randDir = Random.insideUnitCircle.normalized;   //���������� ���͸� ����
        float randDist = Random.Range(minDistB, maxDistB);  //������Ʈ�� ������ �Ÿ��� �������� �޾Ƴ�

        //�÷��̾� �������� Ư�� �Ÿ������� ������ ��ġ�� �� ����
        Vector3 randPos = playerPos + new Vector3(randDir.x, randDir.y, 0) * randDist;
        enemy.transform.position = randPos;
    }

    void SpawnBasic3()   //�÷��̾ ���󰡴� �Ϲ� �� ����
    {
        GameObject enemy = GameManager.Instance.pool.Get(9);    // index0�� �ش��ϴ� ������Ʈ�� ������

        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ���� ��ġ���� ������
        Vector2 randDir = Random.insideUnitCircle.normalized;   //���������� ���͸� ����
        float randDist = Random.Range(minDistB, maxDistB);  //������Ʈ�� ������ �Ÿ��� �������� �޾Ƴ�

        //�÷��̾� �������� Ư�� �Ÿ������� ������ ��ġ�� �� ����
        Vector3 randPos = playerPos + new Vector3(randDir.x, randDir.y, 0) * randDist;
        enemy.transform.position = randPos;
    }

    void SpawnBoss()   //�÷��̾ ���󰡴� �Ϲ� �� ����
    {
        GameObject enemy = GameManager.Instance.pool.Get(11);    // index0�� �ش��ϴ� ������Ʈ�� ������

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
