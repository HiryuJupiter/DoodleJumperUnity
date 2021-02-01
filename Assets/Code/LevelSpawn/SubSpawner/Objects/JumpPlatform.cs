using UnityEngine;
using System.Collections;

public class JumpPlatform : Interactable
{
    const int FloorLevelForMaxMovingChance = 600; //Beyond 600th platform, all platforms will move.
    const float MoveBounds = 2f;
    const float MoveSpeed = 1.5f;

    public PlatformTypes PlatformType;

    //Status
    bool moving;
    int movingDir;

    //Cache
    float startingX;

    #region Mono
    void Awake()
    {
        InteractableType = InteractableTypes.Platform;    
    }

    protected override void Update()
    {
        base.Update();
        MovePlatform();
    }
    #endregion

    #region Public
    public override void Respawned(Vector3 position)
    {
        base.Respawned(position);

        startingX = position.x;

        moving = CanPlatformMove();
        movingDir = GetRandomMovingDir();
    }

    public override void ReturnToPool()
    {
        //Fade out first...
        
        //...and then despawn
        base.ReturnToPool();
    }
    #endregion

    #region Moving
    bool CanPlatformMove()
    {
        int currentlySpawned = LevelSpawnManager.SpawnedFloors;
        return (currentlySpawned / (float)FloorLevelForMaxMovingChance) > Random.value;
    }

    int GetRandomMovingDir () => (Random.Range(0, 2) == 0) ? 1 : -1;

    void MovePlatform()
    {
        if (moving)
        {
            float x = Mathf.Sin(startingX + Time.time * MoveSpeed) * MoveBounds * movingDir;
            Vector3 pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }
    }
    #endregion
}