using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// J : ���α׷��� ������ ����Ǿ�� �ϴ� ���Ӽ��� ������
[Serializable]  // J : ����ȭ�� Data
public class EndingData
{
    public int hungry, lonely, cold = 0;                                // J : human ���ȿ� ���� ��忣��
    public int poisonBerry, error, electric, pig, storm, space = 0;     // J : ����� �̺�Ʈ ����
    public int timeOut, two, AITown, people = 0;                        // J : ���ǿ���

    public int firstGame = 1;   // J : ù ���� ����

    public int currentEndingCode = 0;
}
