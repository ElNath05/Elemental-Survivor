using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public int id;  //���� id
    public int poolId;  //Ǯ �Ŵ����� �� �ִ� id
    public float damage;    //���� ������
    public int level;   //���� ����   
    public float wSpeed;    //���� �ӵ�

    float wTimer;   //���� �����ֱ� Ÿ�̸�
    float spikeTimer;
    float rayTimer;
    float eballTimer;
    PlayerCtrl playerCtrl;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = GameManager.Instance.player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPlaying) //������ �����ϸ� ������Ʈ�Լ� ���� �ð��� �Ȱ����� �Ѵ�
            return;
        switch (id)
        {
            case 0: //���ⱸü
                transform.Rotate(Vector3.back * wSpeed * Time.deltaTime);   //���⸦ �ð�������� ȸ��
                eballTimer += Time.deltaTime;
                if(eballTimer > 5)
                {
                    SetWeapon();
                }
                break;
            case 1: //ȭ����
                wTimer += Time.deltaTime;

                if(wTimer > wSpeed) //Ÿ�̸Ӱ� �����ð��� ������ �߻��Լ� ����, *���ǵ带 ���ؽð����� ���*
                {
                    wTimer = 0;

                    FireBall();
                }
                break;
            case 2: //������
                Transform child = transform.GetChild(0);
                if (level >=3)
                {
                    child.transform.localScale = new Vector3(2.4f, 3, 0);
                }
                if (level >= 5)
                {
                    child.transform.localScale = new Vector3(2.8f, 3.5f, 0);
                }
                break;
            case 3: //���ⷹ����
                rayTimer += Time.deltaTime;
                if(rayTimer > 4 - (level * 0.25))
                {                   
                    ElectRay();
                    rayTimer = 0;
                }
                if(playerCtrl.sprite.flipX && transform.localScale.x >0)
                {
                    transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,0);
                }
                else if(!playerCtrl.sprite.flipX && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
                }
                break;
            case 4:
                spikeTimer += Time.deltaTime;
                if(spikeTimer > 2-(level*0.25))
                {
                    IceSpike();
                    spikeTimer = 0;
                }
                break;
        }

        //if(Input.GetKeyDown(KeyCode.P)) //������ Ȯ�ο� �Լ�
        //{
        //    LevelUp(10, 5);
        //}
    }
    
    public void LevelUp(float damage, int level)    
    {
        this.damage = damage;
        this.level += level;

        if(id == 0)
        {
            SetWeapon();
        }
    }
    public void Init(ItemData data)
    {
        name = "Weapon " + data.itemId; //������Ʈ �̸��� ����
        transform.parent = GameManager.Instance.player.transform;   //������Ʈ�� �θ� �÷��̾�� ����
        transform.localPosition = Vector3.zero; //localposition�� �������� ����

        id = data.itemId;   //������ ���̵�, ������ ���� �����Ϳ� ����Ȱ��� ������
        damage = data.baseDamage;
        level = data.baseCount;

        for(int i = 0; i < GameManager.Instance.pool.prefebs.Length; i++)   //������ ǮID�� ������
        {
            if(data.projectile == GameManager.Instance.pool.prefebs[i])
            {
                poolId = i;
                break;
            }
        }
        switch (id)
        {
            case 0:
                wSpeed = 150;   //������ ȸ���ӵ��� ����
                SetWeapon();
                break;
            case 1:
                wSpeed = 2; //������ ���� �ð��� ����
                break;
            case 2:
                FireGround();
                break;
            case 3:
                ElectRay();
                break;
            case 4:
                break;
        }
    }

    void SetWeapon()
    {
        for(int i = 0; i < level; i++)
        {
            Transform weapon; 
            if(i < transform.childCount)    //�̹� ������ ���Ⱑ �ڽ����� ������
            {
                weapon = transform.GetChild(i);     //������ ���⸦ ��Ȱ��
            }
            else
            {
                weapon = GameManager.Instance.pool.Get(poolId).transform;   //���� �������� Ǯ���� ������
            }
            
            weapon.parent = transform; //�������� �θ� ���� �����ʷ� ����
            //weapon.gameObject.SetActive(true);
            weapon.localPosition = Vector3.zero;    //������ ���� ��ġ �ʱ�ȭ
            weapon.localRotation = Quaternion.identity; //������ ���� �����̼� �ʱ�ȭ

            //������ ���ⰳ����ŭ ������ ����� �������� ��ġ
            Vector3 rotVec = Vector3.forward * 360 * i / level; 
            weapon.Rotate(rotVec);
            weapon.Translate(weapon.up*1.5f, Space.World);

            weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1�� ���� ����
        }
    }

    void FireGround()
    {
        Transform weapon;
        if (0 < transform.childCount)    //�̹� ������ ���Ⱑ �ڽ����� ������
        {
            weapon = transform.GetChild(0);     //������ ���⸦ ��Ȱ��
        }
        else
        {
            weapon = GameManager.Instance.pool.Get(poolId).transform;   //���� �������� Ǯ���� ������
        }
        weapon.parent = transform; //�������� �θ� �÷��̾�� ����

        weapon.localPosition = Vector3.zero;    //������ ���� ��ġ �ʱ�ȭ
        
        weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1�� ���� ����
    }
    void FireBall()
    {
        if (!playerCtrl.scanner.randTarget) //����� Ÿ���� ������ �Լ� Ż��
            return;

        Vector3 targetPos = playerCtrl.scanner.randTarget.position;
        Vector3 dir = targetPos-transform.position; //���� �ִ� ������ ���
        dir = dir.normalized;   //�������� ����� ���͸� ����ȭ

        Transform fBall = GameManager.Instance.pool.Get(poolId).transform;  //Ǯ���� ���� �������� ������
        fBall.position = transform.position;    //���� ��ġ ����
        float angle = Mathf.Atan2(dir.x, dir.y)*Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle+180);
        fBall.rotation = rot;    //���⸦ vector.up���� �������� �ٶ󺸴� ������ ���ϰ� �����̼��� ����
        fBall.GetComponent<Weapon>().Init(damage, level, dir);
    }

    void ElectRay()
    {
        Transform weapon;
        if (0 < transform.childCount)    //�̹� ������ ���Ⱑ �ڽ����� ������
        {
            weapon = transform.GetChild(0);     //������ ���⸦ ��Ȱ��
        }
        else
        {
            weapon = GameManager.Instance.pool.Get(poolId).transform;   //���� �������� Ǯ���� ������
        }
        weapon.parent = transform; //�������� �θ� �÷��̾�� ����
        weapon.gameObject.SetActive(true);
        weapon.localPosition = new Vector3(6.04f, 0.35f,0);    //������ ���� ��ġ �ʱ�ȭ
        
        weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1�� ���� ����
    }

    void IceSpike()
    {
        if (!playerCtrl.scanner.randTarget) //����� Ÿ���� ������ �Լ� Ż��
            return;

        Vector3 targetPos = playerCtrl.scanner.randTarget.position; //�������� ��ġ�� �޾ƿ�

        Transform fBall = GameManager.Instance.pool.Get(poolId).transform;  //Ǯ���� ���� �������� ������
        fBall.position = targetPos;    //���� ��ġ ����
        fBall.GetComponent<Weapon>().Init(damage, -1, Vector3.zero);
    }
}
