using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] private int enemyType;
    [SerializeField] private float eSpeed;

    private int savType;    //하이어라키 창에서 설정한 적 타입값을 저장하는변수
    private float savSpeed; //하이어라키 창에서 설정한 적 속도를 저장하는변수
    private float savHp;    //적 체력을 저장하는 변수

    private float dispawnTime;  //원형으로 둘러싸는 적들이 생성된 시간을 재는 타이머

    private Vector3 playerPos;  //플레이어 좌표 저장용 변수
    private Vector3 pos;    //오브젝트 본체의 좌표
    private Vector3 moveDir;    //오브젝트가 이동할 벡터
    private bool getPos = false;    //플레이어의 위치를 가져왔는지 확인할 변수

    public float eHp = 1; //적의 HP

    Animator animator;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    WaitForFixedUpdate wait;
    private void Awake()
    {
        savSpeed = eSpeed;  //미리 설정된 변수를 따로 저장, 리셋 시 사용
        savType = enemyType;    //미리 설정된 변수를 따로 저장, 리셋 시 사용
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
            case 1: //플레이어를 쫓아가는 적
                playerPos = GameManager.Instance.player.transform.position; //플레이어의 실시간 좌표를 받아냄
                pos = transform.position;   //오브젝트의 좌표를 받아냄
                moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함

                transform.Translate(moveDir * eSpeed * Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

                if (moveDir.x > 0 && sprite.flipX)   //오브젝트가 플레이어 방향을 바라보도록 flipX를 조절
                {
                    sprite.flipX = false;
                }
                else if (moveDir.x < 0 && !sprite.flipX)
                {
                    sprite.flipX = true;
                }
                break;
            case 2: //플레이어를 매우 느리게 따라오는 적 (플레이어를 둘러싸는 적)
                dispawnTime += Time.deltaTime;
                playerPos = GameManager.Instance.player.transform.position; //플레이어의 실시간 좌표를 받아냄
                pos = transform.position;   //오브젝트의 좌표를 받아냄
                moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함

                transform.Translate(moveDir * (eSpeed/10) * Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

                if (moveDir.x > 0 && sprite.flipX)   //오브젝트가 플레이어 방향을 바라보도록 flipX를 조절
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
            case 3: //생성된 시점의 플레이어 좌표로 빠르게 날아가는 적
                if (!getPos)    //플레이어의 좌표를 가져오지않았다면 실행
                {
                    getPos = true;  //플레이어의 좌표를 한번만(실시간X) 가져오도록 변수값을 변환
                    playerPos = GameManager.Instance.player.transform.position;
                    pos = transform.position;   //오브젝트의 좌표를 받아냄
                    moveDir = (playerPos - pos).normalized;  //오브젝트로부터 플레이어까지의 정규벡터를 구함
                    if (moveDir.x > 0 && sprite.flipX)   //오브젝트가 플레이어 방향을 바라보도록 flipX를 조절
                    {
                        sprite.flipX = false;
                    }
                    else if (moveDir.x < 0 && !sprite.flipX)
                    {
                        sprite.flipX = true;
                    }
                }

                transform.Translate(moveDir * (eSpeed*2) * Time.deltaTime); //오브젝트가 플레이어를 따라가도록 이동시킴

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

    private void Reset()    //오브젝트 변수들을 초기화 하는 함수
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
        if (collision.CompareTag("Area") && enemyType == 3) //타입3 적이 카메라의 일정 범위를 넘어가면 리셋 후 비활성화
        {
            Reset();
            gameObject.SetActive(false) ;
        }
    }
}
