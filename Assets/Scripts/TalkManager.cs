using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C : 대화 데이터를 저장 및 관리하는 스크립트
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;       // C : 대화 데이터를 저장하는 dictionary 변수
    Dictionary<int, string[]> selectData;     // J : 선택지 데이터를 저장하는 dictionary 변수
    Dictionary<int, string[]> resultData;     // J : 선택 결과 데이터를 저장하는 dictionary 변수

    void Awake()
    {
        // C : dictionary instance 생성
        talkData = new Dictionary<int, string[]>();
        selectData = new Dictionary<int, string[]>();
        resultData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // C : talkData 생성
    void GenerateData()
    {
        // C : key가 1000~1009이면 AI와의 dialogue data (Dr.Kim이 AI에게 대화를 걸 때), 즉 공감 학습
        talkData.Add(1001, new string[] { "제 생일은 언제일까요?" });
        talkData.Add(1002, new string[] { "박사님의 고향은 어딘가요?", "제 고향은 저기 보이는 연구실이겠죠?" });
        talkData.Add(1003, new string[] { "친구가 더 있었으면 좋겠어요..", "물론 박사님이 최고의 친구랍니다." });
        talkData.Add(1004, new string[] { "박사님이 드시는 밥은 무슨 맛일 지 궁금해요! ", "저는 쌀을 못 먹거든요." });
        talkData.Add(1005, new string[] { "저에게 밥은 태양열 에너지에요.", "차가운 태양빛은 왜 없을까요?", "여름에 너무 덥단 말이에요ㅠㅠ" });
        talkData.Add(1006, new string[] { "저는 어떻게 만들어진걸까요?" });
        talkData.Add(1007, new string[] { "생명이란 무엇일까요?", "저도 생명일까요?" });
        talkData.Add(1008, new string[] { "AI가 너무 똑똑해지면 어떻게 될까요?" });
        talkData.Add(1009, new string[] { "박사님은 제페토보다 더 똑똑하고 멋있는 사람이에요!" });
        talkData.Add(1010, new string[] { "죽음이란 무엇일까요?", "죽으면 어디로 가는 걸까요?" });
        talkData.Add(1011, new string[] { "왜 이 섬에는 박사님 말고는 아무도 살지 않는 걸까요?" });
        talkData.Add(1012, new string[] { "박사님은 혼자 외롭지 않아요?", "제가 있으니 괜찮나요?" });
        talkData.Add(1013, new string[] { "박사님은 밤마다 왜 가만히 있어요?" });
        talkData.Add(1014, new string[] { "저 바다 건너에는 무엇이 있을지 궁금해요!", "언젠가 바다를 건너 박사님과 여행이란 걸 떠나볼래요!" });
        talkData.Add(1015, new string[] { "박사님은 가족이 있나요?", "어떤 분들일지 궁금해요.", "아마 박사님처럼 멋진 분들이겠지만요." });
        talkData.Add(1016, new string[] { "인간의 삶은 의식주만 해결되면 되는걸까요?", "마음을 나눌 친구가 없어도 살 수 있을까요?" });
        talkData.Add(1017, new string[] { "옛날에는 위험한 질병만큼이나 외로워서 죽는 사람도 많았대요!" });

        // N : 하루에 한번 이상 대화 시도
        talkData.Add(3000, new string[] { "충전 중이에요.. 오늘은 대화를 더 이상 할 수 없어요." });

        // C : key가 100~400이면 학습하기에 대한 text data (100 - 농사, 200 - 건축, 300 - 공예, 400 - 공학)
        talkData.Add(100, new string[] { "농사를 학습하시겠습니까?(취소 : ESC)", "농사를 학습합니다." });
        talkData.Add(200, new string[] { "건축을 학습하시겠습니까?(취소 : ESC)", "건축을 학습합니다." });
        talkData.Add(300, new string[] { "공예를 학습하시겠습니까?(취소 : ESC)", "공예를 학습합니다." });
        talkData.Add(400, new string[] { "공학을 학습하시겠습니까?(취소 : ESC)", "공학을 학습합니다." });

        // K: key가 500,600이면 학습에 대한 예외처리 text data
        talkData.Add(500, new string[] { "AI가 학습중입니다.\n학습이 끝나고 다시 시도해주세요." });
        talkData.Add(600, new string[] { "책이 없어서 학습을 할 수 없습니다." });

        // N : 레벨 MAX
        talkData.Add(2100, new string[] { "농사를 전부 배웠어요. 더이상 농사에 대해 학습할 수 없어요." });
        talkData.Add(2200, new string[] { "건축을 전부 배웠어요. 더이상 건축에 대해 학습할 수 없어요." });
        talkData.Add(2300, new string[] { "공예를 전부 배웠어요. 더이상 공예에 대해 학습할 수 없어요." });
        talkData.Add(2400, new string[] { "공학을 전부 배웠어요. 더이상 공학에 대해 학습할 수 없어요." });

        // J : key가 10001~10013이면 스페셜 이벤트에 대한 text data (10001~10004 : 선택지 2개, 10011~10013 : 선택지 3개)
        talkData.Add(10001, new string[] { "배터리가 많이 닳았어요ㅠㅠ", "하루만 아무것도 안하고 싶어요.." });
        talkData.Add(10002, new string[] { "박사님을 위해 새로운 열매를 따왔어요!" });
        talkData.Add(10003, new string[] { "저기 야생동물이 있는 것 같아요!", "잡아서 구워먹을까요?" });
        talkData.Add(10004, new string[] { "저기 야생동물이 있는 것 같아요!", "잡아서 구워먹을까요?" });
        talkData.Add(10011, new string[] { "이 꽃 너무 이쁘지 않아요??" });
        talkData.Add(10012, new string[] { "(AI가 물에 빠졌다)" });
        talkData.Add(10013, new string[] { "(나무가 쓰러져서 AI가 다쳤다. 어떻게 할까?)" });
        talkData.Add(10014, new string[] { "박사님 신기하게 생긴 생물을 발견했어요! 박사님께 드리려고 힘들게 잡았어요ㅎㅎ" });

        // K, J : key가 11000~이면 배드엔딩에 대한 text data
        talkData.Add(11000, new string[] { "박사님 제가 뭐든지 먹을걸 찾아올게요!", "...박사님 왜 대답이 없으세요?" });   //배고픔
        talkData.Add(11001, new string[] { "박사님!! 박사님!!", "어디가셨어요??" }); // 외로움
        talkData.Add(11002, new string[] { "박사님!!", "박사님의 체온이 너무 낮아요", "박사님 자면 안돼요!!" }); // 추움
        talkData.Add(11003, new string[] { "박사님!! 왜그러세요!?", "박사님 입에서 빨간 액체가 나와요" }); // 독열매
        talkData.Add(11004, new string[] { "바...바.아악..사..니이..님...!", "제..제에...가....왜...이..이이...러죠오...?", "삐-----------------", "포맷 되었습니다" }); // 에러
        talkData.Add(11005, new string[] { "으아앜아아아앜!", "....." }); // 감전사
        talkData.Add(11006, new string[] { "오늘은 토끼 고기로 포식할 수 있겠어요!", "어...저게 뭐죠?", "멧돼지다!! 도망가요!!" }); // 멧돼지
        talkData.Add(11007, new string[] { "박사님!", "저기 밀려오는 바닷물이 저번에 말씀하셨던 파도풀인가요?" }); //폭풍우
        talkData.Add(11008, new string[] { "박사님!", "별이 저희한테 다가오고 있어요!!", "너무 멋있지 않아요?"});   //운석충돌

        // J : key가 10001~10013이면 스페셜 이벤트에 대한 선택지 data
        selectData.Add(10001, new string[] { "그래 하루 쉬자", "안돼안돼 지금 바로 일 해야돼!" });
        selectData.Add(10002, new string[] { "고마워~ 먹어보자!", "고맙지만 처음보는 열매는 위험해! " });
        selectData.Add(10003, new string[] { "얼마만의 고기야! 당장 가자!", "야생동물은 너무 위험해. 가지말자" });
        selectData.Add(10004, new string[] { "얼마만의 고기야! 당장 가자!", "야생동물은 너무 위험해. 가지말자" });
        selectData.Add(10011, new string[] { "꽃보다 너가 더 예뻐", "예쁘네~", "별로..." });
        selectData.Add(10012, new string[] { "바로 손을 내밀자!", "알아서 나올거야", "너무 멀어! 나뭇가지를 찾아서 구해야겠다!" });
        selectData.Add(10013, new string[] { "고장난 곳이 있는지 하루동안 찬찬히 살펴보자", "대충 괜찮은지 확인하자", "나무를 치워주고 다시 작업을 시작하자" });
        selectData.Add(10014, new string[] { "으악!! 벌레잖아! (도망간다)", " 그건 벌레야. 고맙지만 사양할게", "윽 벌레잖아..? 그래도 날 위해 잡은거니 웃으면서 받자" });

        // J : 스페셜 이벤트에 대한 선택 결과 data (specialID * 10 + 선택지(0~2))
        // J : 선택지가 2개인 이벤트의 결과 텍스트
        resultData.Add(100010, new string[] { "하루가 지나고...", "AI의 공감능력이 1레벨 상승했다!" });
        resultData.Add(100011, new string[] { "AI의 공감능력이 1레벨 하락했다!" });

        //resultData.Add(100020, new string[] { "AI가 준 것은 독열매였다!" });
        resultData.Add(100020, new string[] {  });
        resultData.Add(100021, new string[] { "AI의 공감능력이 1레벨 하락했다!" });
        
        resultData.Add(100030, new string[] { "토끼였다!", "AI의 공감능력이 1레벨 상승했다!" });
        resultData.Add(100031, new string[] { "AI의 공감능력이 1레벨 하락했다!" });

        //resultData.Add(100040, new string[] { "멧돼지였다!" });
        resultData.Add(100040, new string[] {  });
        resultData.Add(100041, new string[] { "AI의 공감능력이 1레벨 하락했다!" });

        // J : 선택지가 3개인 이벤트의 결과 텍스트
        //resultData.Add(100110, new string[] { "AI가 이해하지 못해서 고장났다." });
        resultData.Add(100110, new string[] {  });
        resultData.Add(100111, new string[] { "박사의 말에 AI의 CPU가 뜨거워졌다.", "AI의 공감능력이 1레벨 상승했다!" });
        resultData.Add(100112, new string[] { "AI의 공감능력이 1레벨 하락했다!" });

        //resultData.Add(100120, new string[] { "나까지 덩달아 감전됐다!" });
        resultData.Add(100120, new string[] {  });
        resultData.Add(100121, new string[] { "AI의 공감능력이 1레벨 하락했다!" });
        resultData.Add(100122, new string[] { "구조에 성공했다!" });

        resultData.Add(100130, new string[] { "하루가 지나고...", "AI의 공감능력이 2레벨 상승했다!" });
        resultData.Add(100131, new string[] { "아무런 변화도 일어나지 않았다." });
        //resultData.Add(100132, new string[] { "알고보니 AI는 심각한 손상을 입었다." });
        resultData.Add(100132, new string[] {  });

        resultData.Add(100140, new string[] { "AI의 공감능력이 1레벨 하락했다!" });
        resultData.Add(100141, new string[] { "아무런 변화도 일어나지 않았다." });
        resultData.Add(100142, new string[] { "AI의 공감능력이 1레벨 상승했다!" });

    }

    // C : 필요한 TalkData를 return
    public string GetTalkData(int id, int talkIndex)
    {
        Debug.Log("TalkID : " + id);
        if (talkIndex == talkData[id].Length)       // C : talkIndex가 talkData[id]의 마지막 index + 1이면
            return null;

        return talkData[id][talkIndex];     // C : 필요한 문장을 id와 index를 통해 return
    }

    // J : 필요한 SelectData를 return
    public string GetSelectData(int id, int selectIndex)
    {
        if (selectIndex == selectData[id].Length)       // J : selectIndex가 selectData[id]의 마지막 index + 1이면
            return null;

        return selectData[id][selectIndex];     // J : 필요한 문장을 id와 index를 통해 return
    }

    // 필요한 ResultData를 return
    public string GetResultData(int id, int resultIndex)
    {
        if (resultIndex == resultData[id].Length)       // J : resultIndex가 resultData[id]의 마지막 index + 1이면
            return null;

        return resultData[id][resultIndex];     // J : 필요한 문장을 id와 index를 통해 return
    }
}
