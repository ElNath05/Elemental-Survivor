using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    private int savType;    //���̾��Ű â���� ������ �������� �����ϴº���
    private float savSpeed; //���̾��Ű â���� ������ �������� �����ϴº���

    private Vector3 playerPos;  //�÷��̾� ��ǥ ����� ����
    private Vector3 pos;    //������Ʈ ��ü�� ��ǥ
    private Vector3 moveDir;    //������Ʈ�� �̵��� ����
    private bool getPos = false;    //�÷��̾��� ��ġ�� �����Դ��� Ȯ���� ����

    [HideInInspector] public float eHp = 1;

    SpriteRenderer sprite;

    private void Awake()
    {
        savSpeed = eSpeed;  //�̸� ������ ������ ���� ����, ���� �� ���
        savType = enemyType;    //�̸� ������ ������ ���� ����, ���� �� ���
    }
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

        if(eHp < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Reset()    //������Ʈ �������� �ʱ�ȭ �ϴ� �Լ�
    {
        eSpeed = savSpeed;
        enemyType = savType;
        playerPos = new Vector3(0, 0, 0);
        pos = new Vector3(0, 0, 0);
        moveDir = new Vector3(0, 0, 0);
        getPos = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area") && enemyType == 3) //Ÿ��3 ���� ���� ������ �Ѿ�� ���� �� ��Ȱ��ȭ
        {
            Reset();
            gameObject.SetActive(false) ;
        }
    }
}
