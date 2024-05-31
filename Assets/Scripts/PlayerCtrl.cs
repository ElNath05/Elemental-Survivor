using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float pSpeed;

    [HideInInspector] public Vector3 moveDir;

    Animator animator;
    SpriteRenderer sprite;
    public Scanner scanner;
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
        float h = Input.GetAxis("Horizontal");  //수평방향 입력값을 받아옴
        float v = Input.GetAxis("Vertical");    //수직방향 입력값을 받아옴
        moveDir = ((Vector3.up * v) + (Vector3.right * h)).normalized; //수평, 수직 이동방향을 알아냄

        //벡터 크기가 1.0을 넘지 않도록 조절 (코드 참고 https://velog.io/@nagi0101/Unity-%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%EC%9D%98-%EB%8C%80%EA%B0%81%EC%84%A0-%EC%9D%B4%EB%8F%99%EC%8B%9C-%EC%86%8D%EB%8F%84-%EC%B2%98%EB%A6%AC )
        float moveSpeed = Mathf.Min((Vector3.up * v + Vector3.right * h).magnitude, 1.0f) * pSpeed;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime); //플레이어를 입력방향으로 이동

        Vector3 myPos = transform.position; //플레이어의 현재 위치좌표를 받아옴, (현재 사용하지않고있음, 이거 왜 적었더라)

        if(!sprite.flipX && h < 0)  //-x방향으로 입력중이면서 스프라이트가 좌우 반전되어있지 않다면 반전시킴 (왼쪽을 보도록함)
        {
            sprite.flipX = true;
        }
        else if(sprite.flipX && h > 0) //+x방향으로 입력중이면서 스프라이트가 좌우 반전되어있다면 반전시킴 (오른쪽을 보도록함)
        {
            sprite.flipX = false;
        }

        //방향입력값의 절대값들의 합을 애니메이션 변수에 대입 (움직임 입력이 있으면 달리기 애니메이션 재생)
        animator.SetFloat("Run", Mathf.Abs(h) + Mathf.Abs(v));  
    }
}
