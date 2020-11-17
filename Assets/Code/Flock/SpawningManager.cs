using UnityEngine;

namespace AIAssignment3
{
    public class SpawningManager : MonoBehaviour
    {
        //Const
        const float ClickAndHoldSpawnInterval = 0.1f;

        //Variables
        [SerializeField] Flock Flock_Wolf;
        [SerializeField] Flock Flock_Sheep;

        //Status
        SpawningMode mode;

        //Reference
        Camera camera;
        UIManager ui;

        //Cache
        float spawnTimer;

        #region Monobehavior
        void Start()
        {
            //Reference
            camera = Camera.main;
            ui = UIManager.instance;
        }

        void Update()
        {
            TickSpawnTimer();
            SpawningInputUpdate();
        }
        #endregion

        #region Public
        //Public methods for switching between different spawning modes
        public void SetSpawnMode_Sheep() => SetSpawnMode(SpawningMode.Sheep);
        public void SetSpawnMode_Wolf() => SetSpawnMode(SpawningMode.Wolf);
        #endregion

        #region Private
        void SpawningInputUpdate()
        {
            //When player pressed the spawn button and when the spawn cooldown timer is ready...
            if (PlayerClicksSpawn() && IsSpawnTimerReady())
            {
                //Spawn a lifeform
                Spawnlifeform();
            }
            //Checks if the player pressed the key to exit spawning mode.
            else if (PlayerExitsSpawningMode())
            {
                //Switch off spawning mode
                mode = SpawningMode.None;
                ui.ExitSpawningMode();
                SetSpawnTimerToReady();
            }
        }

        //For changing spawning mode
        void SetSpawnMode(SpawningMode mode)
        {
            this.mode = mode;
            ui.EnterSpawningMode(mode);
        }

        Vector3 MouseWorldPosition()
        {
            //Convert mouse's screen position to world position
            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 1f;
            return pos;
        }

        void Spawnlifeform()
        {
            //Spawn a lifeform inside the game based off the current mode we're in
            switch (mode)
            {
                case SpawningMode.Sheep:
                    Flock_Sheep.Spawn(MouseWorldPosition());
                    break;
                case SpawningMode.Wolf:
                    Flock_Wolf.Spawn(MouseWorldPosition());
                    break;
            }
            ResetSpawnTimer();
        }

        void TickSpawnTimer()
        {
            //Tick down timer
            if (spawnTimer > 0f)
            {
                spawnTimer -= Time.deltaTime;
            }
        }

        //Expression bodies for creating self documenting code
        void ResetSpawnTimer() => spawnTimer = ClickAndHoldSpawnInterval;
        void SetSpawnTimerToReady() => spawnTimer = 0f;

        bool PlayerExitsSpawningMode() => Input.GetKeyDown(KeyCode.Escape);

        bool PlayerClicksSpawn() => Input.GetMouseButton(0);
        bool IsSpawnTimerReady() => spawnTimer <= 0f;
        #endregion
    }
}