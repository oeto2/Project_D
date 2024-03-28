using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    protected static bool _isLoad = true;

    //고스트 객체 방지
    private static bool _isDestroy = false;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //오브젝트 생성 이름 지정
                string typeName = typeof(T).Name;
                GameObject obj = new GameObject(typeName);

                _instance = obj.AddComponent<T>();

                //isLoad가 true일때만 로드함
                if (_isLoad)
                    DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public virtual void Init()
    {
        Debug.Log(transform.name + "is Init");
    }

    private void OnDestroy()
    {
        _isDestroy = true;
    }
}
