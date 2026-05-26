using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class LoadScene : MonoBehaviour
    {
        public void OnClick_ReloadSceneButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
