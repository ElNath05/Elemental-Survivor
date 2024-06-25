using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public int id;  //무기 id
    public int poolId;  //풀 매니저에 들어가 있는 id
    public float damage;    //무기 데미지
    public int level;   //무기 레벨   
    public float wSpeed;    //무기 속도

    float wTimer;   //무기 생성주기 타이머
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
        if (!GameManager.Instance.isPlaying) //게임이 정지하면 업데이트함수 내의 시간이 안가도록 한다
            return;
        switch (id)
        {
            case 0: //전기구체
                transform.Rotate(Vector3.back * wSpeed * Time.deltaTime);   //무기를 시계방향으로 회전
                eballTimer += Time.deltaTime;
                if(eballTimer > 5)
                {
                    SetWeapon();
                }
                break;
            case 1: //화염구
                wTimer += Time.deltaTime;

                if(wTimer > wSpeed) //타이머가 일정시간이 지나면 발사함수 실행, *스피드를 기준시간으로 사용*
                {
                    wTimer = 0;

                    FireBall();
                }
                break;
            case 2: //불장판
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
            case 3: //전기레이저
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

        //if(Input.GetKeyDown(KeyCode.P)) //레벨업 확인용 함수
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
        name = "Weapon " + data.itemId; //오브젝트 이름을 변경
        transform.parent = GameManager.Instance.player.transform;   //오브젝트의 부모를 플레이어로 지정
        transform.localPosition = Vector3.zero; //localposition을 원점으로 변경

        id = data.itemId;   //무기의 아이디, 데미지 등은 데이터에 저장된값을 가져옴
        damage = data.baseDamage;
        level = data.baseCount;

        for(int i = 0; i < GameManager.Instance.pool.prefebs.Length; i++)   //무기의 풀ID를 가져옴
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
                wSpeed = 150;   //무기의 회전속도를 지정
                SetWeapon();
                break;
            case 1:
                wSpeed = 2; //무기의 재사용 시간을 지정
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
            if(i < transform.childCount)    //이미 생성된 무기가 자식으로 있으면
            {
                weapon = transform.GetChild(i);     //생성된 무기를 재활용
            }
            else
            {
                weapon = GameManager.Instance.pool.Get(poolId).transform;   //무기 프리펩을 풀에서 가져옴
            }
            
            weapon.parent = transform; //프리펩의 부모를 웨폰 스포너로 변경
            //weapon.gameObject.SetActive(true);
            weapon.localPosition = Vector3.zero;    //생성한 무기 위치 초기화
            weapon.localRotation = Quaternion.identity; //생성한 무기 로테이션 초기화

            //생성한 무기개수만큼 간격을 띄워서 원형으로 배치
            Vector3 rotVec = Vector3.forward * 360 * i / level; 
            weapon.Rotate(rotVec);
            weapon.Translate(weapon.up*1.5f, Space.World);

            weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1은 무한 관통
        }
    }

    void FireGround()
    {
        Transform weapon;
        if (0 < transform.childCount)    //이미 생성된 무기가 자식으로 있으면
        {
            weapon = transform.GetChild(0);     //생성된 무기를 재활용
        }
        else
        {
            weapon = GameManager.Instance.pool.Get(poolId).transform;   //무기 프리펩을 풀에서 가져옴
        }
        weapon.parent = transform; //프리펩의 부모를 플레이어로 변경

        weapon.localPosition = Vector3.zero;    //생성한 무기 위치 초기화
        
        weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1은 무한 관통
    }
    void FireBall()
    {
        if (!playerCtrl.scanner.randTarget) //가까운 타겟이 없으면 함수 탈출
            return;

        Vector3 targetPos = playerCtrl.scanner.randTarget.position;
        Vector3 dir = targetPos-transform.position; //적이 있는 방향을 계산
        dir = dir.normalized;   //방향으로 사용할 벡터를 정규화

        Transform fBall = GameManager.Instance.pool.Get(poolId).transform;  //풀에서 무기 프리펩을 가져옴
        fBall.position = transform.position;    //무기 위치 조절
        float angle = Mathf.Atan2(dir.x, dir.y)*Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle+180);
        fBall.rotation = rot;    //무기를 vector.up축을 기준으로 바라보는 뱡향을 향하게 로테이션을 설정
        fBall.GetComponent<Weapon>().Init(damage, level, dir);
    }

    void ElectRay()
    {
        Transform weapon;
        if (0 < transform.childCount)    //이미 생성된 무기가 자식으로 있으면
        {
            weapon = transform.GetChild(0);     //생성된 무기를 재활용
        }
        else
        {
            weapon = GameManager.Instance.pool.Get(poolId).transform;   //무기 프리펩을 풀에서 가져옴
        }
        weapon.parent = transform; //프리펩의 부모를 플레이어로 변경
        weapon.gameObject.SetActive(true);
        weapon.localPosition = new Vector3(6.04f, 0.35f,0);    //생성한 무기 위치 초기화
        
        weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //-1은 무한 관통
    }

    void IceSpike()
    {
        if (!playerCtrl.scanner.randTarget) //가까운 타겟이 없으면 함수 탈출
            return;

        Vector3 targetPos = playerCtrl.scanner.randTarget.position; //랜덤적의 위치를 받아옴

        Transform fBall = GameManager.Instance.pool.Get(poolId).transform;  //풀에서 무기 프리펩을 가져옴
        fBall.position = targetPos;    //무기 위치 조절
        fBall.GetComponent<Weapon>().Init(damage, -1, Vector3.zero);
    }
}
