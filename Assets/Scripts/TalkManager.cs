using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();

    }

    // Update is called once per frame
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?:2", "ó�� ���� ���̳�?:0", "�߰�...:1" });
        talkData.Add(2000, new string[] { "����:1", "�ƴٸ�Ƽ���� �������� Ư���Ѱ� ����..:0", "��! ����Į������ ����������!!:2" });
        talkData.Add(100, new string[] { "����� �������ڴ�." });

        // Quest Talk.
        talkData.Add(10 + 1000, new string[] { "� ��.:0", "�� ������ ���� ������ �Ҵٴµ�:1", "������ ȣ�� �ʿ� �絵�� �˷��ٲ���.:2" });

        talkData.Add(11 + 2000, new string[] { "� ��.:0", "�� ȣ���� ������ ������ �°ž�?:1", "�׷� �� �� �ϳ� ���ָ� �����ٵ�...:2", "�� �� ��ó�� ������ ������ ã����:3" });

        talkData.Add(20 + 1000, new string[] { "�絵�� ����?:1", "���� �긮�� �ٴϸ� ������!:3", "���߿� �絵���� �Ѹ��� �ؾ߰ھ�.:3",  });
        talkData.Add(20 + 2000, new string[] { "ã���� �� �� ���� ��.:1"});
        talkData.Add(20 + 5000, new string[] { "������ �ֿ���." });

        talkData.Add(21 + 2000, new string[] { "��!, ã���༭ ����.:2" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
                //����Ʈ �� ó�� ��縶�� ���� �� �⺻ ��縦 �����´�
                return GetTalk(id - id % 100, talkIndex);
            else
                //�ش� ����Ʈ ���� �� ��簡 ���� ��
                // ����Ʈ �� ó�� ��� �����´�
                return GetTalk(id - id % 10, talkIndex);
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
