using System;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Loading
{
    public class LoadingStep
    {
        public LoadingStep(string description, Func<Task> actionAsync)
        {
            Description = description;
            ActionAsync = actionAsync;
        }

        public string Description { get; private set; }
        public Func<Task> ActionAsync {  get; private set; }
    }
}