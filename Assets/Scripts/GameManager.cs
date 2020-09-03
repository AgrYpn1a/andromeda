using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Andromeda
{

    public sealed class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private GameObject _pnGameOver;
        [SerializeField]
        private GameObject _pnStartGame;

        public int gameWorldRadius = 300;

        private void Start()
        {
            _pnGameOver.SetActive(false);
            _pnStartGame.SetActive(true);
            Time.timeScale = 0;
        }

        public void GameOver(float secs = 0f)
        {
            StartCoroutine(Delay(secs));
        }

        public void StartGame()
        {
            _pnGameOver.SetActive(false);
            _pnStartGame.SetActive(false);
            Time.timeScale = 1;
        }

        private IEnumerator Delay(float secs)
        {
            yield return new WaitForSeconds(secs);
            _pnGameOver.SetActive(true);
            Time.timeScale = 0;
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(this.gameObject);
        }
    }
}
