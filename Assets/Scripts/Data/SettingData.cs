using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// J : ���α׷��� ������ ����Ǿ�� �ϴ� ���Ӽ��� ������
[Serializable]  // J : ����ȭ�� Data
public class SettingData
{
    public float BackgroundSound = 1;  // J : �������
    public float EffectSound = 1;   // J : ȿ������
    public bool BackgroundMute = false;
    public bool EffectMute = false;
}