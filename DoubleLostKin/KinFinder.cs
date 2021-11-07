using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
namespace DoubleLostKin
{
    class KinFinder:MonoBehaviour
    {
        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += FindKinS;
        }

        private void FindKinS(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            if(arg0.name=="GG_Workshop"&&arg1.name=="GG_Lost_Kin")
            {
                StartCoroutine(FindKin());
                Modding.Logger.Log("Find Kin");
            }
        }
        private IEnumerator FindKin()
        {
            yield return new WaitWhile(() => GameObject.Find("Lost Kin") == null);
            GameObject KinControl = new GameObject();
            KinControl.AddComponent<KinControl>();
            Modding.Logger.Log("Add Control");
        }
        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= FindKinS;
        }
    }
}
