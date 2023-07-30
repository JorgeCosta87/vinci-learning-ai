using UnityEngine;
using System;

namespace Vinci.Shared.UI
{

    [System.Serializable]
    public abstract class BaseSequence : MonoBehaviour
    {
        public Action StartEnterSequence;
        public Action StartExitSequence;

        public Action completedEnterSequence;
        public Action completedExitSequence;

        public abstract void PlayEnterSequence();
        public abstract void PlayExitSequence();
    }
}