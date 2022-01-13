using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// J : https://chameleonstudio.tistory.com/56 참고
public class DataController : MonoBehaviour
{
    
    static GameObject _container;
    static GameObject Container
    {
        get 
        {
            return _container;
        }
    }

    // J : 싱글톤으로 구현
    // J : DataContorller를 인스턴스화->다른 파일에서 스크립트를 찾지 않고 바로 접근 가능
    // J : static field, 객체 생성과 상관없이 클래스에서 선언된 순간 메모리에 할당되고 프로그램 끝날 때까지 유지
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);  // J : scene을 이동해도 game object 유지
            }
            return _instance;
        }
    }

    public string SettingDataFileName = "Setting.json";
    public string EndingDataFileName = "Ending.json";

    public SettingData _settingData;
    public SettingData settingData
    {
        get 
        {
            if (_settingData == null) 
            {
                LoadSettingData();
                SaveSettingData();
            }
            return _settingData;
        }
    }

    public EndingData _endingData;
    public EndingData endingData
    {
        get
        {
            if (_endingData == null)
            {
                LoadEndingData();
                SaveEndingData();
            }
            return _endingData;
        }
    }

    public void LoadSettingData()
    {
        string filePath = Application.persistentDataPath + "/" + SettingDataFileName;
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            Debug.Log("설정 데이터 불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath);
            _settingData = JsonUtility.FromJson<SettingData>(FromJsonData);
        }
        else 
        {
            Debug.Log("새로운 설정 데이터 파일 생성");
            _settingData = new SettingData();
        }
    }

    public void LoadEndingData()
    {
        string filePath = Application.persistentDataPath + "/" + EndingDataFileName;
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            Debug.Log("엔딩 데이터 불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath);
            _endingData = JsonUtility.FromJson<EndingData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 엔딩 데이터 파일 생성");
            _endingData = new EndingData();
        }
    }

    public void SaveSettingData()
    {
        string ToJsonData = JsonUtility.ToJson(settingData);
        string filePath = Application.persistentDataPath + "/" + SettingDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("설정 데이터 저장 완료");
    }

    public void SaveEndingData()
    {
        string ToJsonData = JsonUtility.ToJson(endingData);
        string filePath = Application.persistentDataPath + "/" + EndingDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("엔딩 데이터 저장 완료");
    }

    // J : 게임의 모든 데이터 삭제
    public void DeleteAllData()
    {
        // J : 설정 데이터 파일 삭제 -> 바로 소리가 켜지는 문제로 인해 설정 데이터는 삭제 X
        //File.Delete(Application.persistentDataPath + "/" + SettingDataFileName);
        //_settingData = null;

        // J : 엔딩 데이터 파일 삭제
        File.Delete(Application.persistentDataPath + "/" + EndingDataFileName);
        _endingData = null;
    }

    private void OnApplicationQuit()    // J : 프로그램 종료 시 데이터 저장
    {
        SaveSettingData();
        SaveEndingData();
    }
}
