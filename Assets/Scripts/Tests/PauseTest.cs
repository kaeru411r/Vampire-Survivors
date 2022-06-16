using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest : MonoBehaviour
{
    public void Pause()
    {
        GameManager.Instance.Pause();
    }

    public void Resume()
    {
        GameManager.Instance.Resume();
    }
}
