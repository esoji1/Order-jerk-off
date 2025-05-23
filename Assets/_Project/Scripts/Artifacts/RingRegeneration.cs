using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Artifacts
{
    public class RingRegeneration : IArtifact
    {
        private Player.Player _player;
        private MonoBehaviour _monoBehaviour;

        private int _regenerationValue = 3;
        private Coroutine _coroutine;
        private float _cooldown = 5f;

        public RingRegeneration(Player.Player player, MonoBehaviour monoBehaviour)
        {
            _player = player;
            _monoBehaviour = monoBehaviour;
        }

        public void Activate()
        {
            if(_coroutine == null)
            {
                _coroutine = _monoBehaviour.StartCoroutine(Regeneration());
            }
        }

        public void Deactivate()
        {
            if (_coroutine != null)
            {
                _monoBehaviour.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        public TypeArtefact GetArtifactType() => TypeArtefact.RingRegeneration;

        private IEnumerator Regeneration()
        {
            while (true)
            {
                yield return new WaitForSeconds(_cooldown);

                if (_player.Health.HealthValue < _player.PlayerCharacteristics.Health)
                {
                    int healthNeeded = _player.PlayerCharacteristics.Health - _player.Health.HealthValue;
                    int healAmount = Mathf.Min(_regenerationValue, healthNeeded);

                    _player.Health.AddHealth(healAmount);
                    _player.HealthView.AddHealth(healAmount);
                }
            }
        }
    }
}
