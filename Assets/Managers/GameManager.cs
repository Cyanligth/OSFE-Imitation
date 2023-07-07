using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static DataManager data;
    public static PlayerManager player;
    public static ResourceManager resource;
    public static PoolManager pool;
    public static UIManager ui;
    public static SceneManager scene;
    public static SoundManager sound;

    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return data; } }
    public static PlayerManager Player { get { return player; } }
    public static ResourceManager Resource { get { return resource; } }
    public static PoolManager Pool { get { return pool; } }
    public static UIManager UI { get { return ui; } }
    public static SceneManager Scene { get { return scene; } }
    public static SoundManager Sound { get { return sound; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {
        GameObject resourceObj = new GameObject() { name = "ResourceManager" };
        resourceObj.transform.SetParent(transform);
        resource = resourceObj.AddComponent<ResourceManager>();

        GameObject poolObj = new GameObject() { name = "PoolManager" };
        poolObj.transform.SetParent(transform);
        pool = poolObj.AddComponent<PoolManager>();

        GameObject uiObj = new GameObject() { name = "UIManager" };
        uiObj.transform.SetParent(transform);
        ui = uiObj.AddComponent<UIManager>();

        GameObject dataObj = new GameObject() { name = "DataManager" };
        dataObj.transform.SetParent(transform);
        data = dataObj.AddComponent<DataManager>();

        GameObject soundObj = new GameObject() { name = "SoundManager" };
        soundObj.transform.SetParent(transform);
        sound = soundObj.AddComponent<SoundManager>();

        GameObject playerObj = new GameObject() { name = "PlayerManager" };
        playerObj.transform.SetParent(transform);
        player = playerObj.AddComponent<PlayerManager>();

        GameObject sceneObj = new GameObject() { name = "SceneManager" };
        sceneObj.transform.SetParent(transform);
        scene = sceneObj.AddComponent<SceneManager>();

    }
}
