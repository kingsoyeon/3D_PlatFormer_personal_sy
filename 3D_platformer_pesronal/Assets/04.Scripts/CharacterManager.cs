using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            { _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>(); }
            return _instance;
        }
    }

    public Player _player; // 실제 데이터를 저장하는 변수(필드)

    public Player Player // 프로퍼티 (Property)
    {
        get { return _player; } // 값을 가져올 때 실행
        set { _player = value; } // 값을 설정할 때 실행
    }
    
    void Update()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // 인스턴스가 null이 아니면 
        else
        {
            // 지금 넣을 게 원래 있는 거랑 같다면, 현재 것을 파괴하라
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
