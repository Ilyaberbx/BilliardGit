using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Service.Scene
{
    public class SceneLoader : ISceneLoaderService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner) 
            => _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null)
        => _coroutineRunner.StartCoroutine(LoadScene(name,onLoaded));

        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }


            AsyncOperation wait = SceneManager.LoadSceneAsync(name);

            while (!wait.isDone)
              yield return null;

            onLoaded?.Invoke();
        }
    }
}
