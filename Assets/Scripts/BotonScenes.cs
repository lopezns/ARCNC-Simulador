using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; 
#endif

public class SceneButtonLoader : MonoBehaviour
{
    [Header("Arrastra aquí la escena que quieres cargar")]
#if UNITY_EDITOR
    public SceneAsset sceneAsset;
#endif
    [HideInInspector] public string sceneName;

    private void OnValidate()
    { 
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
#endif
    }
        public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("No se ha asignado ninguna escena en " + gameObject.name);
            return;
        }

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("La escena '" + sceneName + "' no está en Build Settings.");
        }
    }
}
