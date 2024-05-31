using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    private int savType;    //���̾��Ű â���� ������ �� Ÿ�԰��� �����ϴº���
    private float savSpeed; //���̾��Ű â���� ������ �� �ӵ��� �����ϴº���
    private float savHp;    //�� ü���� �����ϴ� ����

    private float dispawnTime;  //�������� �ѷ��δ� ������ ������ �ð��� ��� Ÿ�̸�

    private Vector3 playerPos;  //�÷��̾� ��ǥ ����� ����
    private Vector3 pos;    //������Ʈ ��ü�� ��ǥ
    private Vector3 moveDir;    //������Ʈ�� �̵��� ����
    private bool getPos = false;    //�÷��̾��� ��ġ�� �����Դ��� Ȯ���� ����

    public float eHp = 1; //���� HP

    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    WaitForFixedUpdate wait;
    private void Awake()
    {
        savSpeed = eSpeed;  //�̸� ������ ������ ���� ����, ���� �� ���
        savType = enemyType;    //�̸� ������ ������ ���� ����, ���� �� ���
        savHp = eHp;

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        wait = new WaitForFixedUpdate();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

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
                dispawnTime += Time.deltaTime;
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
                if(dispawnTime > 20f)
                {
                    Reset();
                    gameObject.SetActive(false);
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

    //IEnumerator KnockBack()
    //{
    //    yield return wait;
    //    Vector3 playerPos = GameManager.Instance.player.transform.position;
    //    Vector3 dirVec = transform.position - playerPos;
    //    rb.AddForce(dirVec.normalized*3,ForceMode2D.Impulse);
    //}

    private void Reset()    //������Ʈ �������� �ʱ�ȭ �ϴ� �Լ�
    {
        eSpeed = savSpeed;
        enemyType = savType;
        playerPos = new Vector3(0, 0, 0);
        pos = new Vector3(0, 0, 0);
        moveDir = new Vector3(0, 0, 0);
        getPos = false;
        eHp = savHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            eHp -= collision.GetComponent<Weapon>().damage;
            //StartCoroutine(KnockBack());
            if(eHp > 0)
            {
                animator.SetTrigger("Hit");
            }
            else
            {
                if (enemyType == 1)
                {
                    animator.SetBool("Dead",true);
                }
                Reset();
                GameManager.Instance.exp++;
                gameObject.SetActive(false);
            }
        }
    }

    void Die()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area") && enemyType == 3) //Ÿ��3 ���� ī�޶��� ���� ������ �Ѿ�� ���� �� ��Ȱ��ȭ
        {
            Reset();
            gameObject.SetActive(false) ;
        }
    }
}
