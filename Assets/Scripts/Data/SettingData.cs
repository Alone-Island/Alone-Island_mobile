using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// J : 프로그램이 끝나도 저장되어야 하는 게임설정 데이터
[Serializable]  // J : 직렬화된 Data
public class SettingData
{
    public float BackgroundSound = 1;  // J : 배경음악
    public float EffectSound = 1;   // J : 효과음악
    public bool BackgroundMute = false;
    public bool EffectMute = false;
}