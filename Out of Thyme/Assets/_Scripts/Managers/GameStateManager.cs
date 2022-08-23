namespace TigerFrogGames
{

    public enum GameState
    {
        Gameplay,
        Paused
    }

    public class GameStateManager 
    {
        private static GameStateManager _instance;
        public static GameStateManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        
        private GameStateManager()
        {
            
        }

        #region Variables

        public GameState CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(GameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;


        #endregion

        #region Methods

        public void SetState( GameState newGameState )
        {
            if (CurrentGameState == newGameState)
            {
                return;
            }

            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }


        /*
         * 
         * Use the following methods in scripts that want to subscribe to game state. 
         * 
         * Awake/OnDestroy  Use to subscribe and unsubscribe to the event handler
         * 
         * GameStateManager_OnGameStateChanged(GameState newGameState) How do you want the code to work when in a specic game state. 
         * 
        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay);
        }
        */

        #endregion
    }
}