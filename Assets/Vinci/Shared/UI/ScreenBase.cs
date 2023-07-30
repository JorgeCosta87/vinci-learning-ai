using System;
using UnityEngine;

namespace Vinci.Shared.UI
{
    public abstract class ScreenBase : MonoBehaviour
    {
        public bool isActive = false;

        public SequenceManager[] squences;
        private int counterSequences = 0;

        public virtual void Awake() 
        {
            squences = GetComponentsInChildren<SequenceManager>();
        }

        public virtual void EnterScreen()
        {
            foreach (var sequence in squences)
            {
                sequence.completedEnterSequence += OnCompleteEnterSequence;
                sequence.PlayEnterSequence();
            }
        }

        public virtual void ExitScreen()
        {
            foreach (var sequence in squences)
            {
                sequence.completedExitSequence += OnCompleteExitSequence;
                sequence.PlayExitSequence();
            }
        }

        public virtual void SetScreenState(bool state)
        {
            isActive = state;
           
            if(state)
            {
                this.gameObject.SetActive(state);
                EnterScreen();
            }
            else
            {
                ExitScreen();
            }
        }

        public void OnCompleteEnterSequence()
        {
            Debug.Log("complete neter sequences");
        }

        public void OnCompleteExitSequence()
        {
            counterSequences++;
            if (counterSequences == squences.Length)
            {
                this.gameObject.SetActive(false);
                counterSequences = 0;
            }
        }

        private void OnEnable()
        {
        
        }

        private void OnDisable()
        {
        }
    }
}