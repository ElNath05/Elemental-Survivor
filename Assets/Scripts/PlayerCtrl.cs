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
        if (!GameManager.Instance.isPlaying) //������ �����ϸ� ������Ʈ�Լ� ���� �ð��� �Ȱ����� �Ѵ�
            return;
        float h = Input.GetAxis("Horizontal");  //������� �Է°��� �޾ƿ�
        float v = Input.GetAxis("Vertical");    //�������� �Է°��� �޾ƿ�
        moveDir = ((Vector3.up * v) + (Vector3.right * h)).normalized; //����, ���� �̵������� �˾Ƴ�

        //���� ũ�Ⱑ 1.0�� ���� �ʵ��� ����
        float moveSpeed = Mathf.Min((Vector3.up * v + Vector3.right * h).magnitude, 1.0f) * pSpeed;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime); //�÷��̾ �Է¹������� �̵�

        Vector3 myPos = transform.position; //�÷��̾��� ���� ��ġ��ǥ�� �޾ƿ�, (���� ��������ʰ�����, �̰� �� ��������)

        if(!sprite.flipX && h < 0 && !Input.GetKey(KeyCode.Z))  //-x�������� �Է����̸鼭 ��������Ʈ�� �¿� �����Ǿ����� �ʴٸ� ������Ŵ (������ ��������)
        {
            sprite.flipX = true;
        }
        else if(sprite.flipX && h > 0 && !Input.GetKey(KeyCode.Z)) //+x�������� �Է����̸鼭 ��������Ʈ�� �¿� �����Ǿ��ִٸ� ������Ŵ (�������� ��������)
        {
            sprite.flipX = false;
        }

        //�����Է°��� ���밪���� ���� �ִϸ��̼� ������ ���� (������ �Է��� ������ �޸��� �ִϸ��̼� ���)
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
        if (collision.CompareTag("Enemy") && !isHit)  //������ ������ ü�°���
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
