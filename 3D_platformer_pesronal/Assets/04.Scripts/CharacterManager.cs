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

    public Player _player; // ���� �����͸� �����ϴ� ����(�ʵ�)

    public Player Player // ������Ƽ (Property)
    {
        get { return _player; } // ���� ������ �� ����
        set { _player = value; } // ���� ������ �� ����
    }
    
    void Update()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // �ν��Ͻ��� null�� �ƴϸ� 
        else
        {
            // ���� ���� �� ���� �ִ� �Ŷ� ���ٸ�, ���� ���� �ı��϶�
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
