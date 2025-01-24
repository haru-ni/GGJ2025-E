using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                // シーン内に存在するオブジェクトを探す
                _instance = FindObjectOfType<T>();

                // 存在しない場合は新しいオブジェクトを作成
                if (_instance == null)
                {
                    var singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }

                // シーンを切り替えてもオブジェクトが破棄されないようにする
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }

        private void Awake()
        {
            // 既にインスタンスが存在する場合は、現在のオブジェクトを破棄
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this as T;
            }
        }
    }
}