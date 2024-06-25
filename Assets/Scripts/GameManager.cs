using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public PoolManager pool;
    public PlayerCtrl playerCtrl;

    private float sec;  //게임 시간 초를 세는 변수
    public float min;  //게임 시간 분을 세는 변수

    public float exp;
    public int level;

    public bool isPlaying;

    public Text time;
    public Slider expGage;
    public Slider hpGage;
    public LevelUp uiLevelUp;
    public GameObject gameOver;
    public GameObject gameClear;
    void Awake()
    { if (Instance == null)
        {
            Instance = this;
        }
        else
        {

        }

        playerCtrl = player.GetComponent<PlayerCtrl>();
    }
    // Start is called before the first frame update
    public void GaneStart()
    {

        uiLevelUp.Select(0);
        isPlaying = true;
    }
    public void GaneStart2()
    {

        uiLevelUp.Select(4);
        isPlaying = true;
    }
    public void GaneStart3()
    {

        uiLevelUp.Select(3);
        isPlaying = true;
    }
    public void GaneStart4()
    {

        uiLevelUp.Select(1);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) //게임이 정지하면 업데이트함수 내의 시간이 안가도록 한다
            return;

        sec += Time.deltaTime; //게임의 초를 계산
        if(sec > 59.5)  //1분이 되면 분을 담당하는 변수에 +1
        {
            sec = 0;
            min += 1;
        }

        //UI에 나오는 숫자가 각각 두자릿 수가 유지되도록 하는 코드
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

        if(min > 20)
        {
            GameClear();
        }

        float maxExp = 2 * (level + 2); //현재 레벨에 따라 다음 경험치 요구량을 설정
        if (exp > maxExp)  //경험치가 최대경험치를 넘으면 레벨업하고 경험치를 초기화
        {
            level++;
            exp = 0;
            uiLevelUp.ShowLevelUp();
        }

        expGage.value = exp / maxExp;   //현재 경험치에 따른 경험치바량 설정
        hpGage.value = playerCtrl.pHp/playerCtrl.maxHp;
    }

    public void Pause() //게임을 일시정지 시키는 함수
    {
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void Resume()    //정지된 게임을 다시 재생시키는 함수
    {
        isPlaying=true;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Pause();
        gameOver.SetActive(true);
    }
    public void GameClear()
    {
        Pause();
        gameClear.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
