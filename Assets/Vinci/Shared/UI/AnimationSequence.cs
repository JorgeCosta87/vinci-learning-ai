using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Vinci.Shared.UI;

[System.Serializable]
public class AnimationInfo
{
   
    public DOTweenAnimation animation;
    public AudioClip audioclip;
}

[System.Serializable]
public class AnimationSequence : BaseSequence
{
    [SerializeField]
    List<AnimationInfo> enterAnimations;
    [SerializeField]
    List<AnimationInfo> animations;
    [SerializeField]
    List<AnimationInfo> exitAnimations;


    private int counterEnterAnimations = 0;
    private int counterExitAnimations = 0;


    public override void PlayEnterSequence()
    {
        StartEnterSequence?.Invoke();
        foreach(var animInfo in enterAnimations)
        {
            
            //Tween tween =  animInfo.animation.CreateTween(false, true);
           // tween.OnComplete(OnCompleteStartAnimations);

            counterEnterAnimations++;
        }
    }

    public void PlayAnimations()
    {
        foreach (var animInfo in animations)
        {
            //animation.CreateTween();

           // Tween tween = animInfo.animation.CreateTween(false, true);
            //tween.OnComplete(OnCompleteExitAnimations);
        }
    }

    public override void PlayExitSequence()
    {
        StartExitSequence?.Invoke();
        if (exitAnimations.Count == 0)
        {
            OnCompleteExitAnimations();
            return;
        }

        foreach (var animInfo in exitAnimations)
        {
            //animInfo.animation.CreateTween();

           // Tween tween = animInfo.animation.CreateTween(false, true);
           // tween.OnComplete(OnCompleteExitAnimations);
        }
    }

    public void OnCompleteStartAnimations()
    {
        if (counterEnterAnimations == enterAnimations.Count)
        {
            completedEnterSequence?.Invoke();

            PlayAnimations();

            counterEnterAnimations = 0;
        }
    }

    public void OnCompleteExitAnimations()
    {
        counterExitAnimations++;
        if (counterExitAnimations == exitAnimations.Count)
        {
            completedExitSequence?.Invoke();

            counterExitAnimations = 0;
        }
    }
}