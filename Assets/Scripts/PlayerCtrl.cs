using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float pSpeed;

    [HideInInspector] public Vector3 moveDir;

    Animator animator;
    public SpriteRenderer sprite;
    public Scanner scanner;
    public float maxHp = 100;
    public float pHp = 100;
    private bool isHit;
    float hitTimer;

    public Slider hpGage;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        scanner = GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPlaying) //게임이 정지하면 업데이트함수 내의 시간이 안가도록 한다
            return;
        float h = Input.GetAxis("Horizontal");  //수평방향 입력값을 받아옴
        float v = Input.GetAxis("Vertical");    //수직방향 입력값을 받아옴
        moveDir = ((Vector3.up * v) + (Vector3.right * h)).normalized; //수평, 수직 이동방향을 알아냄

        //벡터 크기가 1.0을 넘지 않도록 조절
        float moveSpeed = Mathf.Min((Vector3.up * v + Vector3.right * h).magnitude, 1.0f) * pSpeed;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime); //플레이어를 입력방향으로 이동

        Vector3 myPos = transform.position; //플레이어의 현재 위치좌표를 받아옴, (현재 사용하지않고있음, 이거 왜 적었더라)

        if(!sprite.flipX && h < 0 && !Input.GetKey(KeyCode.Z))  //-x방향으로 입력중이면서 스프라이트가 좌우 반전되어있지 않다면 반전시킴 (왼쪽을 보도록함)
        {
            sprite.flipX = true;
        }
        else if(sprite.flipX && h > 0 && !Input.GetKey(KeyCode.Z)) //+x방향으로 입력중이면서 스프라이트가 좌우 반전되어있다면 반전시킴 (오른쪽을 보도록함)
        {
            sprite.flipX = false;
        }

        //방향입력값의 절대값들의 합을 애니메이션 변수에 대입 (움직임 입력이 있으면 달리기 애니메이션 재생)
        animator.SetFloat("Run", Mathf.Abs(h) + Mathf.Abs(v));  

        if(isHit)
        {
            hpGage.gameObject.SetActive(true);
            hitTimer += Time.deltaTime;
        }
        if (hitTimer > 1f)
        {
            isHit = false;
            hpGage.gameObject.SetActive(false);
            hitTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isHit)  //적에게 맞으면 체력감소
        {
            pHp -= 5;
            isHit = true;
            if(pHp <= 0)
            {
                animator.SetBool("Die", true);
            }
        }
    }
    public void GameOver()
    {
        GameManager.Instance.GameOver();
    } 
}
