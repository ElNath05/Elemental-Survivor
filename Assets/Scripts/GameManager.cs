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

    private float sec;  //���� �ð� �ʸ� ���� ����
    public float min;  //���� �ð� ���� ���� ����

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
        if (!isPlaying) //������ �����ϸ� ������Ʈ�Լ� ���� �ð��� �Ȱ����� �Ѵ�
            return;

        sec += Time.deltaTime; //������ �ʸ� ���
        if(sec > 59.5)  //1���� �Ǹ� ���� ����ϴ� ������ +1
        {
            sec = 0;
            min += 1;
        }

        //UI�� ������ ���ڰ� ���� ���ڸ� ���� �����ǵ��� �ϴ� �ڵ�
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

        float maxExp = 2 * (level + 2); //���� ������ ���� ���� ����ġ �䱸���� ����
        if (exp > maxExp)  //����ġ�� �ִ����ġ�� ������ �������ϰ� ����ġ�� �ʱ�ȭ
        {
            level++;
            exp = 0;
            uiLevelUp.ShowLevelUp();
        }

        expGage.value = exp / maxExp;   //���� ����ġ�� ���� ����ġ�ٷ� ����
        hpGage.value = playerCtrl.pHp/playerCtrl.maxHp;
    }

    public void Pause() //������ �Ͻ����� ��Ű�� �Լ�
    {
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void Resume()    //������ ������ �ٽ� �����Ű�� �Լ�
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
