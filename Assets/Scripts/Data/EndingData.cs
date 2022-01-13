using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// J : 프로그램이 끝나도 저장되어야 하는 게임설정 데이터
[Serializable]  // J : 직렬화된 Data
public class EndingData
{
    public int hungry, lonely, cold = 0;                                // J : human 스탯에 따른 배드엔딩
    public int poisonBerry, error, electric, pig, storm, space = 0;     // J : 스페셜 이벤트 엔딩
    public int timeOut, two, AITown, people = 0;                        // J : 해피엔딩

    public int firstGame = 1;   // J : 첫 게임 여부

    public int currentEndingCode = 0;
}
