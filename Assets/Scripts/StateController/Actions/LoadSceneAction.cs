using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using StateControllerManagement;

[CreateAssetMenu(fileName = "LoadScene_{scene}_Action", menuName = "Actions/General/Load Scene")]
public class LoadSceneAction : Action 
{
    [SerializeField]
    SceneField scene;

    public override void Act<T>(T controller = null)
    {
        LoadScene();
    }

    public void Act()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}