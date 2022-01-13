using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : ��ȭ �����͸� ���� �� �����ϴ� ��ũ��Ʈ
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;       // C : ��ȭ �����͸� �����ϴ� dictionary ����
    Dictionary<int, string[]> selectData;     // J : ������ �����͸� �����ϴ� dictionary ����
    Dictionary<int, string[]> resultData;     // J : ���� ��� �����͸� �����ϴ� dictionary ����

    void Awake()
    {
        // C : dictionary instance ����
        talkData = new Dictionary<int, string[]>();
        selectData = new Dictionary<int, string[]>();
        resultData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // C : talkData ����
    void GenerateData()
    {
        // C : key�� 1000~1009�̸� AI���� dialogue data (Dr.Kim�� AI���� ��ȭ�� �� ��), �� ���� �н�
        talkData.Add(1001, new string[] { "�� ������ �����ϱ��?" });
        talkData.Add(1002, new string[] { "�ڻ���� ������ ��򰡿�?", "�� ������ ���� ���̴� �������̰���?" });
        talkData.Add(1003, new string[] { "ģ���� �� �־����� ���ھ��..", "���� �ڻ���� �ְ��� ģ�����ϴ�." });
        talkData.Add(1004, new string[] { "�ڻ���� ��ô� ���� ���� ���� �� �ñ��ؿ�! ", "���� ���� �� �԰ŵ��." });
        talkData.Add(1005, new string[] { "������ ���� �¾翭 ����������.", "������ �¾���� �� �������?", "������ �ʹ� ���� ���̿���Ф�" });
        talkData.Add(1006, new string[] { "���� ��� ��������ɱ��?" });
        talkData.Add(1007, new string[] { "�����̶� �����ϱ��?", "���� �����ϱ��?" });
        talkData.Add(1008, new string[] { "AI�� �ʹ� �ȶ������� ��� �ɱ��?" });
        talkData.Add(1009, new string[] { "�ڻ���� �����亸�� �� �ȶ��ϰ� ���ִ� ����̿���!" });
        talkData.Add(1010, new string[] { "�����̶� �����ϱ��?", "������ ���� ���� �ɱ��?" });
        talkData.Add(1011, new string[] { "�� �� ������ �ڻ�� ����� �ƹ��� ���� �ʴ� �ɱ��?" });
        talkData.Add(1012, new string[] { "�ڻ���� ȥ�� �ܷ��� �ʾƿ�?", "���� ������ ��������?" });
        talkData.Add(1013, new string[] { "�ڻ���� �㸶�� �� ������ �־��?" });
        talkData.Add(1014, new string[] { "�� �ٴ� �ǳʿ��� ������ ������ �ñ��ؿ�!", "������ �ٴٸ� �ǳ� �ڻ�԰� �����̶� �� ����������!" });
        talkData.Add(1015, new string[] { "�ڻ���� ������ �ֳ���?", "� �е����� �ñ��ؿ�.", "�Ƹ� �ڻ��ó�� ���� �е��̰�������." });
        talkData.Add(1016, new string[] { "�ΰ��� ���� �ǽ��ָ� �ذ�Ǹ� �Ǵ°ɱ��?", "������ ���� ģ���� ��� �� �� �������?" });
        talkData.Add(1017, new string[] { "�������� ������ ������ŭ�̳� �ܷο��� �״� ����� ���Ҵ��!" });

        // N : �Ϸ翡 �ѹ� �̻� ��ȭ �õ�
        talkData.Add(3000, new string[] { "���� ���̿���.. ������ ��ȭ�� �� �̻� �� �� �����." });

        // C : key�� 100~400�̸� �н��ϱ⿡ ���� text data (100 - ���, 200 - ����, 300 - ����, 400 - ����)
        talkData.Add(100, new string[] { "��縦 �н��Ͻðڽ��ϱ�?(��� : ESC)", "��縦 �н��մϴ�." });
        talkData.Add(200, new string[] { "������ �н��Ͻðڽ��ϱ�?(��� : ESC)", "������ �н��մϴ�." });
        talkData.Add(300, new string[] { "������ �н��Ͻðڽ��ϱ�?(��� : ESC)", "������ �н��մϴ�." });
        talkData.Add(400, new string[] { "������ �н��Ͻðڽ��ϱ�?(��� : ESC)", "������ �н��մϴ�." });

        // K: key�� 500,600�̸� �н��� ���� ����ó�� text data
        talkData.Add(500, new string[] { "AI�� �н����Դϴ�.\n�н��� ������ �ٽ� �õ����ּ���." });
        talkData.Add(600, new string[] { "å�� ��� �н��� �� �� �����ϴ�." });

        // N : ���� MAX
        talkData.Add(2100, new string[] { "��縦 ���� ������. ���̻� ��翡 ���� �н��� �� �����." });
        talkData.Add(2200, new string[] { "������ ���� ������. ���̻� ���࿡ ���� �н��� �� �����." });
        talkData.Add(2300, new string[] { "������ ���� ������. ���̻� ������ ���� �н��� �� �����." });
        talkData.Add(2400, new string[] { "������ ���� ������. ���̻� ���п� ���� �н��� �� �����." });

        // J : key�� 10001~10013�̸� ����� �̺�Ʈ�� ���� text data (10001~10004 : ������ 2��, 10011~10013 : ������ 3��)
        talkData.Add(10001, new string[] { "���͸��� ���� ��Ҿ��Ф�", "�Ϸ縸 �ƹ��͵� ���ϰ� �;��.." });
        talkData.Add(10002, new string[] { "�ڻ���� ���� ���ο� ���Ÿ� ���Ծ��!" });
        talkData.Add(10003, new string[] { "���� �߻������� �ִ� �� ���ƿ�!", "��Ƽ� �����������?" });
        talkData.Add(10004, new string[] { "���� �߻������� �ִ� �� ���ƿ�!", "��Ƽ� �����������?" });
        talkData.Add(10011, new string[] { "�� �� �ʹ� �̻��� �ʾƿ�??" });
        talkData.Add(10012, new string[] { "(AI�� ���� ������)" });
        talkData.Add(10013, new string[] { "(������ �������� AI�� ���ƴ�. ��� �ұ�?)" });
        talkData.Add(10014, new string[] { "�ڻ�� �ű��ϰ� ���� ������ �߰��߾��! �ڻ�Բ� �帮���� ����� ��Ҿ�䤾��" });

        // K, J : key�� 11000~�̸� ��忣���� ���� text data
        talkData.Add(11000, new string[] { "�ڻ�� ���� ������ ������ ã�ƿðԿ�!", "...�ڻ�� �� ����� ��������?" });   //�����
        talkData.Add(11001, new string[] { "�ڻ��!! �ڻ��!!", "��𰡼̾��??" }); // �ܷο�
        talkData.Add(11002, new string[] { "�ڻ��!!", "�ڻ���� ü���� �ʹ� ���ƿ�", "�ڻ�� �ڸ� �ȵſ�!!" }); // �߿�
        talkData.Add(11003, new string[] { "�ڻ��!! �ֱ׷�����!?", "�ڻ�� �Կ��� ���� ��ü�� ���Ϳ�" }); // ������
        talkData.Add(11004, new string[] { "��...��.�ƾ�..��..����..��...!", "��..����...��....��...��..����...���ҿ�...?", "��-----------------", "���� �Ǿ����ϴ�" }); // ����
        talkData.Add(11005, new string[] { "���ƝھƾƾƝ�!", "....." }); // ������
        talkData.Add(11006, new string[] { "������ �䳢 ���� ������ �� �ְھ��!", "��...���� ����?", "�������!! ��������!!" }); // �����
        talkData.Add(11007, new string[] { "�ڻ��!", "���� �з����� �ٴ幰�� ������ �����ϼ̴� �ĵ�Ǯ�ΰ���?" }); //��ǳ��
        talkData.Add(11008, new string[] { "�ڻ��!", "���� �������� �ٰ����� �־��!!", "�ʹ� ������ �ʾƿ�?"});   //��浹

        // J : key�� 10001~10013�̸� ����� �̺�Ʈ�� ���� ������ data
        selectData.Add(10001, new string[] { "�׷� �Ϸ� ����", "�ȵžȵ� ���� �ٷ� �� �ؾߵ�!" });
        selectData.Add(10002, new string[] { "����~ �Ծ��!", "������ ó������ ���Ŵ� ������! " });
        selectData.Add(10003, new string[] { "�󸶸��� ����! ���� ����!", "�߻������� �ʹ� ������. ��������" });
        selectData.Add(10004, new string[] { "�󸶸��� ����! ���� ����!", "�߻������� �ʹ� ������. ��������" });
        selectData.Add(10011, new string[] { "�ɺ��� �ʰ� �� ����", "���ڳ�~", "����..." });
        selectData.Add(10012, new string[] { "�ٷ� ���� ������!", "�˾Ƽ� ���ðž�", "�ʹ� �־�! ���������� ã�Ƽ� ���ؾ߰ڴ�!" });
        selectData.Add(10013, new string[] { "���峭 ���� �ִ��� �Ϸ絿�� ������ ���캸��", "���� �������� Ȯ������", "������ ġ���ְ� �ٽ� �۾��� ��������" });
        selectData.Add(10014, new string[] { "����!! �����ݾ�! (��������)", " �װ� ������. ������ ����Ұ�", "�� �����ݾ�..? �׷��� �� ���� �����Ŵ� �����鼭 ����" });

        // J : ����� �̺�Ʈ�� ���� ���� ��� data (specialID * 10 + ������(0~2))
        // J : �������� 2���� �̺�Ʈ�� ��� �ؽ�Ʈ
        resultData.Add(100010, new string[] { "�Ϸ簡 ������...", "AI�� �����ɷ��� 1���� ����ߴ�!" });
        resultData.Add(100011, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });

        //resultData.Add(100020, new string[] { "AI�� �� ���� �����ſ���!" });
        resultData.Add(100020, new string[] {  });
        resultData.Add(100021, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });
        
        resultData.Add(100030, new string[] { "�䳢����!", "AI�� �����ɷ��� 1���� ����ߴ�!" });
        resultData.Add(100031, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });

        //resultData.Add(100040, new string[] { "���������!" });
        resultData.Add(100040, new string[] {  });
        resultData.Add(100041, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });

        // J : �������� 3���� �̺�Ʈ�� ��� �ؽ�Ʈ
        //resultData.Add(100110, new string[] { "AI�� �������� ���ؼ� ���峵��." });
        resultData.Add(100110, new string[] {  });
        resultData.Add(100111, new string[] { "�ڻ��� ���� AI�� CPU�� �߰ſ�����.", "AI�� �����ɷ��� 1���� ����ߴ�!" });
        resultData.Add(100112, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });

        //resultData.Add(100120, new string[] { "������ ���޾� �����ƴ�!" });
        resultData.Add(100120, new string[] {  });
        resultData.Add(100121, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });
        resultData.Add(100122, new string[] { "������ �����ߴ�!" });

        resultData.Add(100130, new string[] { "�Ϸ簡 ������...", "AI�� �����ɷ��� 2���� ����ߴ�!" });
        resultData.Add(100131, new string[] { "�ƹ��� ��ȭ�� �Ͼ�� �ʾҴ�." });
        //resultData.Add(100132, new string[] { "�˰��� AI�� �ɰ��� �ջ��� �Ծ���." });
        resultData.Add(100132, new string[] {  });

        resultData.Add(100140, new string[] { "AI�� �����ɷ��� 1���� �϶��ߴ�!" });
        resultData.Add(100141, new string[] { "�ƹ��� ��ȭ�� �Ͼ�� �ʾҴ�." });
        resultData.Add(100142, new string[] { "AI�� �����ɷ��� 1���� ����ߴ�!" });

    }

    // C : �ʿ��� TalkData�� return
    public string GetTalkData(int id, int talkIndex)
    {
        Debug.Log("TalkID : " + id);
        if (talkIndex == talkData[id].Length)       // C : talkIndex�� talkData[id]�� ������ index + 1�̸�
            return null;

        return talkData[id][talkIndex];     // C : �ʿ��� ������ id�� index�� ���� return
    }

    // J : �ʿ��� SelectData�� return
    public string GetSelectData(int id, int selectIndex)
    {
        if (selectIndex == selectData[id].Length)       // J : selectIndex�� selectData[id]�� ������ index + 1�̸�
            return null;

        return selectData[id][selectIndex];     // J : �ʿ��� ������ id�� index�� ���� return
    }

    // �ʿ��� ResultData�� return
    public string GetResultData(int id, int resultIndex)
    {
        if (resultIndex == resultData[id].Length)       // J : resultIndex�� resultData[id]�� ������ index + 1�̸�
            return null;

        return resultData[id][resultIndex];     // J : �ʿ��� ������ id�� index�� ���� return
    }
}
