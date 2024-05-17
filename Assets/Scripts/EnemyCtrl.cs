using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    private Vector3 playerPos;  //�÷��̾� ��ǥ ����� ����
    private Vector3 pos;    //������Ʈ ��ü�� ��ǥ
    private Vector3 moveDir;    //������Ʈ�� �̵��� ����
    private bool getPos = false;    //�÷��̾��� ��ġ�� �����Դ��� Ȯ���� ����

    [HideInInspector] public float eHp = 1;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case 1: //�÷��̾ �Ѿư��� ��
                playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� �ǽð� ��ǥ�� �޾Ƴ�
                pos = transform.position;   //������Ʈ�� ��ǥ�� �޾Ƴ�
                moveDir = (playerPos - pos).normalized;  //������Ʈ�κ��� �÷��̾������ ���Ժ��͸� ����

                transform.Translate(moveDir * eSpeed * Time.deltaTime); //������Ʈ�� �÷��̾ ���󰡵��� �̵���Ŵ

                if (moveDir.x > 0 && sprite.flipX)   //������Ʈ�� �÷��̾� ������ �ٶ󺸵��� flipX�� ����
                {
                    sprite.flipX = false;
                }
                else if (moveDir.x < 0 && !sprite.flipX)
                {
                    sprite.flipX = true;
                }
                break;
            case 2: //�÷��̾ �ſ� ������ ������� �� (�÷��̾ �ѷ��δ� ��)
                playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� �ǽð� ��ǥ�� �޾Ƴ�
                pos = transform.position;   //������Ʈ�� ��ǥ�� �޾Ƴ�
                moveDir = (playerPos - pos).normalized;  //������Ʈ�κ��� �÷��̾������ ���Ժ��͸� ����

                transform.Translate(moveDir * (eSpeed/10) * Time.deltaTime); //������Ʈ�� �÷��̾ ���󰡵��� �̵���Ŵ

                if (moveDir.x > 0 && sprite.flipX)   //������Ʈ�� �÷��̾� ������ �ٶ󺸵��� flipX�� ����
                {
                    sprite.flipX = false;
                }
                else if (moveDir.x < 0 && !sprite.flipX)
                {
                    sprite.flipX = true;
                }
                break;
            case 3: //������ ������ �÷��̾� ��ǥ�� ������ ���ư��� ��
                if (!getPos)    //�÷��̾��� ��ǥ�� ���������ʾҴٸ� ����
                {
                    getPos = true;  //�÷��̾��� ��ǥ�� �ѹ���(�ǽð�X) ���������� �������� ��ȯ
                    playerPos = GameManager.Instance.player.transform.position;
                    pos = transform.position;   //������Ʈ�� ��ǥ�� �޾Ƴ�
                    moveDir = (playerPos - pos).normalized;  //������Ʈ�κ��� �÷��̾������ ���Ժ��͸� ����
                    if (moveDir.x > 0 && sprite.flipX)   //������Ʈ�� �÷��̾� ������ �ٶ󺸵��� flipX�� ����
                    {
                        sprite.flipX = false;
                    }
                    else if (moveDir.x < 0 && !sprite.flipX)
                    {
                        sprite.flipX = true;
                    }
                }

                transform.Translate(moveDir * (eSpeed*2) * Time.deltaTime); //������Ʈ�� �÷��̾ ���󰡵��� �̵���Ŵ

                break;
        }
    }
}
