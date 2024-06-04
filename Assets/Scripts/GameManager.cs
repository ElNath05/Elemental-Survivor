using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public PoolManager pool;

    private float sec;  //게임 시간 초를 세는 변수
    private float min;  //게임 시간 분을 세는 변수

    public float exp;
    public int level;

    public Text time;
    public Slider expGage;
    private void Awake()
    { if (Instance == null)
        {
            Instance = this;
        }
        else
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime; 
        if(sec > 59.5)
        {
            sec = 0;
            min += 1;
        }
        if(min < 9.5)
        {
            if (sec < 9.5)
            {
                time.text = "0" + min.ToString("F0") + ":0" + sec.ToString("F0");
            }
            else
            {
                time.text = "0" + min.ToString("F0") + ":" + sec.ToString("F0");
            }
        }
        else
        {
            if (sec < 9.5)
            {
                time.text = min.ToString("F0") + ":0" + sec.ToString("F0");
            }
            else
            {
                time.text = min.ToString("F0") + ":" + sec.ToString("F0");
            }
        }


        float maxExp = 2 * (level + 2); //현재 레벨에 따라 다음 경험치 요구량을 설정
        if (exp > maxExp)  //경험치가 최대경험치를 넘으면 레벨업하고 경험치를 초기화
        {
            level++;
            exp = 0;
        }

        expGage.value = exp / maxExp;   //현재 경험치에 따른 경험치바량 설정
    }
}
