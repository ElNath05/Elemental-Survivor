using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public PoolManager pool;

    private float sec;  //���� �ð� �ʸ� ���� ����
    private float min;  //���� �ð� ���� ���� ����

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


        float maxExp = 2 * (level + 2); //���� ������ ���� ���� ����ġ �䱸���� ����
        if (exp > maxExp)  //����ġ�� �ִ����ġ�� ������ �������ϰ� ����ġ�� �ʱ�ȭ
        {
            level++;
            exp = 0;
        }

        expGage.value = exp / maxExp;   //���� ����ġ�� ���� ����ġ�ٷ� ����
    }
}
