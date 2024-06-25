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
        item = GetComponentsInChildren<Item>(true);  //�������ڽĵ� �� �� Ȱ��ȭ �� �ڽĵ��� ������Ʈ���� �ҷ���
    }

    // Update is called once per frame
    public void ShowLevelUp()   //������ â�� ���̰��ϴ� �Լ�
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Pause();
    }

    public void HideLevelUp()   //������ â�� ����� �Լ�
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
        //��� ������(����,���) ��Ȱ��ȭ
        foreach (Item item in item)
        {
            item.gameObject.SetActive(false);
        }

        //���� ������ ������ 4�� Ȱ��ȭ
        int[] rand = new int[4];
        while (true)
        {
            //������ ����Ʈ�� ������ 4���� �����´�
            rand[0] = Random.Range(0, item.Length);
            rand[1] = Random.Range(0, item.Length);
            rand[2] = Random.Range(0, item.Length);
            rand[3] = Random.Range(0, item.Length);

            //������ �������� ���� �ߺ��� ���� �����ϵ��� �Ѵ�
            if (rand[0] != rand[1] && rand[0] != rand[2] && rand[0] != rand[3] && rand[1] != rand[2] && rand[1] != rand[3] && rand[2] != rand[3])
            {
                break;
            }
        }
        for(int i = 0; i < rand.Length; i++)
        {
            Item randItem = item[rand[i]];

            //���� ������ �������� �����̸� �Һ���������� �ٲ۴�
            if(randItem.level == randItem.data.damages.Length)
            {
                item[6].gameObject.SetActive(true);
            }
            //�׷��� ������ �״�� Ȱ��ȭ �Ѵ�
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }
    }
}
