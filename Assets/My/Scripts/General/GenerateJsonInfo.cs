using System.Collections.Generic;
using UnityEngine;

public class GenerateJsonInfo : MonoBehaviour
{
    static GenerateJsonInfo instance;
    JsonInfo allInfo;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            string jsonText = Resources.Load<TextAsset>("InfoText").text;
            allInfo = JsonUtility.FromJson<JsonInfo>(jsonText);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string GetSceneInfo(string scene)
    {
        return allInfo.GetType().GetField(scene).GetValue(allInfo).ToString();
    }

    public List<string> GetSceneInfoList(string scene)
    {
        return (List<string>) allInfo.GetType().GetField(scene).GetValue(allInfo);
    }
}
