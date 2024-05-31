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
        float h = Input.GetAxis("Horizontal");  //������� �Է°��� �޾ƿ�
        float v = Input.GetAxis("Vertical");    //�������� �Է°��� �޾ƿ�
        moveDir = ((Vector3.up * v) + (Vector3.right * h)).normalized; //����, ���� �̵������� �˾Ƴ�

        //���� ũ�Ⱑ 1.0�� ���� �ʵ��� ���� (�ڵ� ���� https://velog.io/@nagi0101/Unity-%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%EC%9D%98-%EB%8C%80%EA%B0%81%EC%84%A0-%EC%9D%B4%EB%8F%99%EC%8B%9C-%EC%86%8D%EB%8F%84-%EC%B2%98%EB%A6%AC )
        float moveSpeed = Mathf.Min((Vector3.up * v + Vector3.right * h).magnitude, 1.0f) * pSpeed;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime); //�÷��̾ �Է¹������� �̵�

        Vector3 myPos = transform.position; //�÷��̾��� ���� ��ġ��ǥ�� �޾ƿ�, (���� ��������ʰ�����, �̰� �� ��������)

        if(!sprite.flipX && h < 0)  //-x�������� �Է����̸鼭 ��������Ʈ�� �¿� �����Ǿ����� �ʴٸ� ������Ŵ (������ ��������)
        {
            sprite.flipX = true;
        }
        else if(sprite.flipX && h > 0) //+x�������� �Է����̸鼭 ��������Ʈ�� �¿� �����Ǿ��ִٸ� ������Ŵ (�������� ��������)
        {
            sprite.flipX = false;
        }

        //�����Է°��� ���밪���� ���� �ִϸ��̼� ������ ���� (������ �Է��� ������ �޸��� �ִϸ��̼� ���)
        animator.SetFloat("Run", Mathf.Abs(h) + Mathf.Abs(v));  
    }
}
