using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefebs;    //��������� ��� �迭
    List<GameObject>[] pools;   //Ǯ�� ����ϴ� ����Ʈ

    private void Awake()
    {
        pools = new List<GameObject>[prefebs.Length];   //������ �迭�� ũ�⸸ŭ Ǯ�� �ʱ�ȭ
        for(int i = 0; i < pools.Length; i++)   //�ݺ����� ���� Ǯ ����Ʈ�� �ʱ�ȭ
        {
            pools[i] = new List<GameObject>();  
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        //������ Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ����
        foreach (GameObject item in pools[index])
        {
            // ��Ȱ��ȭ�� ������Ʈ�� �ִٸ� select�� �Ҵ��� Ȱ��ȭ
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //��Ȱ��ȭ�� ������Ʈ�� ������ ���� �����ϰ� select�� �Ҵ�
        if(!select)
        {
            //������Ʈ�� ���� ���� �� Ǯ �Ŵ��� ������Ʈ�� �ڽ����� ����
            select = Instantiate(prefebs[index], transform);
            pools[index].Add(select);   //Ǯ�� ���� ������ ������Ʈ ����
        }

        return select;
    }
}
