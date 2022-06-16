using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChengeManager : MonoBehaviour
{
    /// <summary>ゲームマネージャークラスのスタティックインスタンス</summary>
    static public SceneChengeManager Instance;

    /// <summary>タイトルシーン</summary>
    Scene _titleScene;
    /// <summary>リザルトシーン</summary>
    Scene _resultScene;
    /// <summary>全クリシーン</summary>
    Scene _clearScene;
    /// <summary>ゲームシーン</summary>
    Scene _gameScene;


    /// <summary>現在のシーン</summary>
    Scene _nowScene;

    private void Awake()
    {
        int count = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        Debug.Log(count);
        _titleScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0);
        _resultScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(1);
        _clearScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(2);
        _nowScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

    private void Start()
    {
    }

    /// <summary>
    /// タイトルシーンをロード
    /// </summary>
    public void TitleScene()
    {
        SceneChange(_titleScene.buildIndex);
    }

    /// <summary>
    /// ゲームシーンをロード
    /// </summary>
    public void GameScene()
    {
        SceneChange(_gameScene.buildIndex);
        GameManager.Instance.GameStart();
    }

    /// <summary>
    /// リザルトシーンをロード
    /// </summary>
    public void ResultScene()
    {
        SceneChange(_resultScene.buildIndex);
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
        SceneChange(_nowScene.buildIndex);
    }

    /// <summary>
    /// シーンをロード
    /// </summary>
    void SceneChange(int value)
    {
        SceneManager.LoadScene(value);
        _nowScene = SceneManager.GetActiveScene();
    }
}
