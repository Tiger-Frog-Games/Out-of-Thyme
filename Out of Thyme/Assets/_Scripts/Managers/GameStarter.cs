using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace TigerFrogGames
{
    public class GameStarter : MonoBehaviour
    {
        #region Variables

        private static GameStarter _instance;
        public static GameStarter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStarter();
                }
                return _instance;
            }
        }

        [SerializeField] private EventChannelGameStart OnGameStart;
        
        [SerializeField] private string[] GameplayScenes;
        
        [SerializeField] private StartGameData startGameData;
        
        private List<AsyncOperation> gameplayScenesLoading = new List<AsyncOperation>();
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        #endregion

        #region Methods

        public void loadGameScenes()
        {
            GameStateManager.Instance.SetState(GameState.Paused);
            StartCoroutine(loadGameplayScenes());
        }

        private IEnumerator loadGameplayScenes()
        {
            //Load the scenes 

            //need to load 1 scene first to get ride of the last one
            SceneManager.LoadScene(GameplayScenes[0], LoadSceneMode.Single);
            
            //AsyncOperation firstScene = SceneManager.LoadSceneAsync(GameplayScenes[0], LoadSceneMode.Single);
            //gameplayScenesLoading.Add(firstScene);
            //firstScene.allowSceneActivation = false;
            
            for (int i = 1; i < GameplayScenes.Length; i++)
            {
                //Add them to the Managers to wait for if its not dont loading
                
                if (!SceneManager.GetSceneByPath(GameplayScenes[i]).IsValid())
                {
                    AsyncOperation loadedScene = SceneManager.LoadSceneAsync(GameplayScenes[i], LoadSceneMode.Additive);
                    gameplayScenesLoading.Add(loadedScene);
                    loadedScene.allowSceneActivation = false;
                }
            }

            //wait till they are all loaded
            float timeoutMax = 200;
            float currentTimeOut = 0;
            
            while ( !checkIfAllGameplayScenesAreLoaded() )
            {
                
                yield return new WaitForSeconds(.1f);

                currentTimeOut += 1f;
                if (currentTimeOut > timeoutMax)
                {
                    //should probally bring you back to the main menu
                    Debug.LogWarning("Warning Scene manager time out - gameplay scenes");
                    yield break;
                }
            }

            foreach (AsyncOperation scene in gameplayScenesLoading)
            {
                scene.allowSceneActivation = true;
            }
            
            yield return new WaitForSeconds(.1f);

            preGameSetUp();
            
            yield return new WaitForSeconds(.1f);
            
            startGame();
        }

        private void preGameSetUp()
        {
            
        }

        private void startGame()
        {
            
            OnGameStart.RaiseEvent(startGameData);
            GameStateManager.Instance.SetState(GameState.Gameplay);
        }
        
        public void changeNumberOfPlayer(int i)
        {
            startGameData.changeNumberOfPlayers(i);
        }
        
        private bool checkIfAllGameplayScenesAreLoaded()
        {
            foreach (AsyncOperation scene in gameplayScenesLoading)
            {
                
                if (scene.isDone)
                {
                    return false;
                }
            }
            return true;
        }
        
        
        #endregion
    }
}




