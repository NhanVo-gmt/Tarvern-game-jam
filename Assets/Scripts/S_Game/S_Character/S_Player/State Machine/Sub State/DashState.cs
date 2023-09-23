using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AbilityState
{
    private float dashTime = .3f;
    float lastActiveTime;
    
    public DashState(Player player, Core core, StateMachine stateMachine, PlayerData data, int animId) : base(player, core, stateMachine, data, animId)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityZero();
        movement.SetGravityZero();

        player.inputManager.UseDashInput();

        lastActiveTime = Time.time;

        movement.SetVelocity(player.inputManager.movementInput * data.dashData.initialVelocity);

        combat.DisableCollider();
        
    }
    

    public override void Exit() 
    {
        movement.SetVelocityZero();
        movement.SetGravityNormal();

        combat.EnableCollider();
        
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();
        
        if (Time.time >= lastActiveTime + dashTime)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public bool CanDash()
    {
        return Time.time > lastActiveTime + data.dashData.cooldown;
    }
    
}

