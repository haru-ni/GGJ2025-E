using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Utils
{
    public static class SaveDataUtil
    {
        /// <summary>
        /// データを保存
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public static void Save<T>(T data) where T : class
        {
            var json = JsonUtility.ToJson(data);
            var bytes = Encoding.UTF8.GetBytes(json);
            var path = Application.persistentDataPath + "/" + typeof(T).Name + ".json";
            try
            {
                File.WriteAllBytes(path, bytes);
            }
            catch (Exception e)
            {
                Debug.LogError($"データの保存に失敗しました\ndata:{data}\njson:{json}\nbytes:{bytes}\npath:{path}\nerror:{e}");
                throw;
            }
        }

        /// <summary>
        /// データを読み込み
        /// </summary>
        public static T Load<T>(T data) where T : class
        {
            var path = Application.persistentDataPath + "/" + typeof(T).Name + ".json";
            if (!File.Exists(path))
            {
                Debug.LogError($"指定されたパスにファイルがありませんでした：{path}");
                return null;
            }

            var bytes = File.ReadAllBytes(path);
            if (bytes.Length <= 0)
            {
                Debug.LogError($"読み込んだセーブデータの中身が空でした：{path}");
                return null;
            }

            var jsonString = Encoding.UTF8.GetString(bytes);
            var json = JsonUtility.FromJson<T>(jsonString);

            return json;
        }
    }
}