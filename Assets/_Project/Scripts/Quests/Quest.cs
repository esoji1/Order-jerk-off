using UnityEngine;

namespace _Project.Quests
{
    public abstract class Quest
    {
        private string _title;
        private string _description;
        private bool _isCompleted;

        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; }

        protected Quest(string title, string description)
        {
            _title = title;
            _description = description;
            _isCompleted = false;
        }

        public void CompleteQuest()
        {
            IsCompleted = true;
            Debug.Log("Quest Completed: " + Title);
        }
    }
}