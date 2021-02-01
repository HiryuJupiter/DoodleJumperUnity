using UnityEngine;

public class PlayerStatus
{
    const float TranslatorSwitchCooldown = 0.2f; //A cooldown to prevent switching back and forth between translators too quickly, a bug which occurs when player is triggering multiple translators.
    const float CooldownBetweenHittingTheSameObject = 0.25f;


    public Vector2 Velocity = Vector2.zero;

    //Collided
    public Interactable RecentlyHitPlatform;
    public Interactable RecentlyHitSpinner;

    //Timer
    public float RecentlyHitPlaformTimer { get; private set; }
    public float RecentlyHitSpinnerTimer { get; private set; }


    #region Recently hit objects
    public bool IsPlatformRecentlyHit(Interactable platform) => RecentlyHitPlatform == platform;
    public bool IsSpinnerRecentlyHit(Interactable spinner) => RecentlyHitSpinner == spinner;

    public void SetRecentlyHitPlatform(Interactable platform)
    {
        RecentlyHitPlatform = platform;
        RecentlyHitPlaformTimer = CooldownBetweenHittingTheSameObject;
    }

    public void SetRecentlyHitSpinner(Interactable spinner)
    {
        RecentlyHitSpinner = spinner;
        RecentlyHitSpinnerTimer = CooldownBetweenHittingTheSameObject;
    }

    public void TimerUpdate()
    {
        //Platform timer
        if (RecentlyHitPlaformTimer > 0f)
            RecentlyHitPlaformTimer -= Time.deltaTime;
        else
            RecentlyHitPlatform = null;

        //Spinner timer
        if (RecentlyHitSpinnerTimer > 0f)
            RecentlyHitSpinnerTimer -= Time.deltaTime;
        else
            RecentlyHitSpinner = null;
    }
    #endregion
}