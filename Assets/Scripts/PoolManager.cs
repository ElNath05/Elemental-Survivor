using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefebs;    //프리펩들을 담는 배열
    List<GameObject>[] pools;   //풀을 담당하는 리스트

    private void Awake()
    {
        pools = new List<GameObject>[prefebs.Length];   //프리펩 배열의 크기만큼 풀을 초기화
        for(int i = 0; i < pools.Length; i++)   //반복문을 돌려 풀 리스트를 초기화
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
        //선택한 풀에서 비활성화된 오브젝트에 접근
        foreach (GameObject item in pools[index])
        {
            // 비활성화된 오브젝트가 있다면 select에 할당후 활성화
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //비활성화된 오브젝트가 없으면 새로 생성하고 select에 할당
        if(!select)
        {
            //오브젝트를 새로 생성 후 풀 매니저 오브젝트의 자식으로 지정
            select = Instantiate(prefebs[index], transform);
            pools[index].Add(select);   //풀에 새로 생성한 오브젝트 저장
        }

        return select;
    }
}
