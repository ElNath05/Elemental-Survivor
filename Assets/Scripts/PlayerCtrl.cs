using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float pSpeed;

    [HideInInspector] public Vector3 moveDir;

    Animator animator;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");  //������� �Է°��� �޾ƿ�
        float v = Input.GetAxis("Vertical");    //�������� �Է°��� �޾ƿ�
        moveDir = ((Vector3.up * v) + (Vector3.right * h)).normalized; //����, ���� �̵������� �˾Ƴ�
        float moveSpeed = Mathf.Min((Vector3.up * v + Vector3.right * h).magnitude, 1.0f) * pSpeed; //���� ũ�Ⱑ 1.0�� ���� �ʵ��� ����
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
