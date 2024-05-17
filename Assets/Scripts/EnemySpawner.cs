using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float e_SpawnTime;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBat();
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

    }
}
