using UnityEngine;
using System.Collections;


public static class SceneEvents
{
    //Game start
    public static SceneEvent GameStart { get; set; } = new SceneEvent("Gamestart");


    //Gameplay - Int parameter events
    public static SceneEventInt CoinPickup { get; set; } = new SceneEventInt("Coinpickup");

    //Gameplay - normal Events
    public static SceneEvent Bounced { get; set; } = new SceneEvent("Bounced");
    public static SceneEvent TakesRocket { get; set; } = new SceneEvent("PlayerDead");
    public static SceneEvent TakesBaloon { get; set; } = new SceneEvent("PlayerDead");
    public static SceneEvent Dashing { get; set; } = new SceneEvent("Dashing");
    public static SceneEvent PlayerSlamming { get; set; } = new SceneEvent("PlayerDead");


    //Exiting
    public static SceneEventInt PlayerTakesDamage { get; set; } = new SceneEventInt("Coinpickup");
    public static SceneEvent PlayerDead { get; set; } = new SceneEvent("PlayerDead");
    public static SceneEvent GameOverBackToMain { get; set; } = new SceneEvent("GameOverBackToMain");

    public static bool GameWideEventsInitialized { get; set; }
    public static bool PerLevelEventsInitialized { get; set; }

    public static void UnSubscribePerLevelEvents()
    {
        //Game start
        GameStart.UnSubscribeAll();

        //Gameplay - Int parameter events
        CoinPickup.UnSubscribeAll();

        //Gameplay - normal Events
        Bounced.UnSubscribeAll();
        TakesRocket.UnSubscribeAll();
        TakesBaloon.UnSubscribeAll();
        Dashing.UnSubscribeAll();
        PlayerSlamming.UnSubscribeAll();

        //Exiting
        PlayerTakesDamage.UnSubscribeAll();
        PlayerDead.UnSubscribeAll();
        GameOverBackToMain.UnSubscribeAll();
    }
}
