using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UILoader : MonoBehaviour
{
    //Make singleton
    public static UILoader instance;
    void Awake()
    {
        MakeSingleton();
        LoadUI();
    }
    void MakeSingleton()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void LoadUI()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++) loadedScenes[i] = SceneManager.GetSceneAt(i);
        if (loadedScenes.Contains(SceneManager.GetSceneByName("UIScene"))) print("UIScene is loaded");
        else SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

}
