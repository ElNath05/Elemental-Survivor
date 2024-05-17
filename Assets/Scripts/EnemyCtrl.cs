using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� �ǽð� ��ǥ�� �޾Ƴ�
        Vector3 pos = transform.position;   //������Ʈ�� ��ǥ�� �޾Ƴ�
        Vector3 moveDir = (playerPos - pos).normalized;  //������Ʈ�κ��� �÷��̾������ ���Ժ��͸� ����

        transform.Translate(moveDir*eSpeed*Time.deltaTime); //������Ʈ�� �÷��̾ ���󰡵��� �̵���Ŵ

        if(moveDir.x > 0)
        {

        }
        else if(moveDir.x < 0)
        {

        }
    }
}
