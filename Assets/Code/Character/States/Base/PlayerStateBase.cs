using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class PlayerStateBase
{
    protected PlayerController player;
    protected PlayerFeedbacks feedback;

    protected InputManager input;
    protected GameSettings settings;
    protected PlayerStatus playerStatus;

    public PlayerStateBase(PlayerController player, PlayerFeedbacks feedback)
    {
        this.player = player;
        this.feedback = feedback;

        playerStatus = player.Status;
        input = InputManager.Instance;
        settings = GameSettings.Instance;
    }

    #region Public
    public virtual void StateEntry() { }
    public virtual void StateExit() { }
    public virtual void TickUpdate() { }
    public virtual void TickFixedUpdate() { }

    //Collisions (also exit conditions)
    //public virtual void PlayerTakesDamage() { }
    public virtual void HitsInteractable(Interactable other)
    {
    }
    #endregion

    #region Protected
    protected bool TryPolymorphToTargetClass<T>(Interactable input, out T output) where T : Interactable
    {
        if (input is T)
        {
            output = (T)input;
            return true;
        }

        output = null;

        Debug.LogError("Error : The collided object " + input.name + " cannot be converted to child class. ");
        OnCollisionWithInvalidInteractable();
        return false;
    }

    protected void OnCollisionWithInvalidInteractable() { }
    #endregion

}

//col.GetComponent<Interactable>().CollidedWithPlayer();

//bool TryConvertToChildClass(Interactable other, out LinearTranslator translator)
//{
//    if (other is LinearTranslator)
//    {
//        translator = (LinearTranslator)other;
//        return true;
//    }

//    translator = null;

//    Debug.LogError("Error : The collided object " + other.name + " cannot be converted to child class. ");
//    //Maybe exit and switch to jump state.
//    return false;
//}