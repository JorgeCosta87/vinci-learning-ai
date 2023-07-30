using UnityEngine;
using Vinci.Shared.UI;
using System;

[System.Serializable]
public class SequenceManager : MonoBehaviour 
{
    [SerializeField]
    bool _playOnEnable = true;

    [SerializeField]
    BaseSequence[] sequences;

    public Action completedEnterSequence;
    public Action completedExitSequence;


    void Awake()
    {
        Init();
    }

    void Init()
    {
        sequences = GetComponentsInChildren<BaseSequence>();
    }

    public void PlayEnterSequence()
    {
        foreach (var sequence in sequences)
        {
            sequence.completedEnterSequence += OnCompleteEnterSequence;
            sequence.PlayEnterSequence();
        }
    }

    public void PlayExitSequence()
    {
        foreach (var sequence in sequences)
        {
            sequence.completedExitSequence += OnCompleteExitSequence;
            sequence.PlayExitSequence();
        }
    }

    private void OnEnable() {
        if(_playOnEnable)
        {
            PlayEnterSequence();
        }
 
    }

    private void OnDisable() 
    {
        foreach (var sequence in sequences)
        {
            sequence.completedEnterSequence -= OnCompleteEnterSequence;
            sequence.completedExitSequence -= OnCompleteExitSequence;
        }    
    }

    void OnCompleteEnterSequence()
    {
        completedEnterSequence?.Invoke();
    }

    void OnCompleteExitSequence()
    {
        completedExitSequence?.Invoke();
        //this.gameObject.SetActive(false);
    }
}