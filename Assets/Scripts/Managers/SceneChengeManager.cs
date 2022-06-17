using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChengeManager : MonoBehaviour
{
    /// <summary>ゲームマネージャークラスのスタティックインスタンス</summary>
    static public SceneChengeManager Instance;

    /// <summary>タイトルシーン</summary>
    int _titleScene = 0;
    /// <summary>リザルトシーン</summary>
    int _resultScene = 2;
    /// <summary>全クリシーン</summary>
    int _clearScene;
    /// <summary>ゲームシーン</summary>
    int _gameScene = 1;


    /// <summary>現在のシーン</summary>
    int _nowScene;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        int count = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        Debug.Log(count);
        //_titleScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0);
        //_resultScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(2);
        //_gameScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1);
        _nowScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
    }

    /// <summary>
    /// タイトルシーンをロード
    /// </summary>
    public void TitleScene()
    {
        StartCoroutine(SceneChange(_titleScene));
    }

    /// <summary>
    /// ゲームシーンをロード
    /// </summary>
    public void GameScene()
    {
        StartCoroutine(SceneChange(_gameScene, GameManager.Instance.GameStart));
        //GameManager.Instance.GameStart();
    }

    /// <summary>
    /// リザルトシーンをロード
    /// </summary>
    public void ResultScene()
    {
        StartCoroutine(SceneChange(_resultScene));
    }

    /// <summary>
    /// 現在のシーンをロード
    /// </summary>
    public void ReLoad()
    {
        if (_nowScene == _gameScene)
        {
            Debug.Log(1);
            GameScene();
            return;
        }
        StartCoroutine(SceneChange(_nowScene));
    }

    /// <summary>
    /// シーンをロード
    /// </summary>
    IEnumerator SceneChange(int value)
    {
        SceneManager.LoadScene(value);
        yield return null;
        _nowScene = SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// シーンをロード
    /// </summary>
    IEnumerator SceneChange(int value, System.Action method)
    {
        SceneManager.LoadScene(value);
        yield return null;
        _nowScene = SceneManager.GetActiveScene().buildIndex;
        method.Invoke();
    }

}
