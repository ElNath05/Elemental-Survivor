using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] item;
    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        item = GetComponentsInChildren<Item>(true);  //아이템자식들 중 비 활성화 된 자식들의 컴포넌트까지 불러옴
    }

    // Update is called once per frame
    public void ShowLevelUp()   //레벨업 창을 보이게하는 함수
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Pause();
    }

    public void HideLevelUp()   //레벨업 창을 숨기는 함수
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
    }

    public void Select(int index)
    {
        item[index].OnClick();
    }

    void Next()
    {
        //모든 아이템(무기,장비) 비활성화
        foreach (Item item in item)
        {
            item.gameObject.SetActive(false);
        }

        //랜덤 레벨업 아이템 4개 활성화
        int[] rand = new int[4];
        while (true)
        {
            //아이템 리스트중 랜덤한 4개를 가져온다
            rand[0] = Random.Range(0, item.Length);
            rand[1] = Random.Range(0, item.Length);
            rand[2] = Random.Range(0, item.Length);
            rand[3] = Random.Range(0, item.Length);

            //가져온 아이템이 서로 중복인 것을 방지하도록 한다
            if (rand[0] != rand[1] && rand[0] != rand[2] && rand[0] != rand[3] && rand[1] != rand[2] && rand[1] != rand[3] && rand[2] != rand[3])
            {
                break;
            }
        }
        for(int i = 0; i < rand.Length; i++)
        {
            Item randItem = item[rand[i]];

            //만약 가져온 아이템이 만렙이면 소비아이템으로 바꾼다
            if(randItem.level == randItem.data.damages.Length)
            {
                item[6].gameObject.SetActive(true);
            }
            //그렇지 않으면 그대로 활성화 한다
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }
    }
}
