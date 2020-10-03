using NepixSignals;
using NepixSignals.Api;
using UnityEngine;

namespace Demo1
{
    public class NepixSignalsDemo : MonoBehaviour
    {
        private Player _player;
        private void Start()
        {
            // Create some hubs.
            // I've created here 3 hubs.
            // But most of time you need only one hub 
            // per scene session or even game session.
            // Nonetheless in some situation you might need
            // several hubs in you game or scene session.
            var hub1 = new SignalHub();
            var hub2 = new SignalHub();
            var hub3 = new SignalHub();
            
            // Listen signals via hub1
            hub1.Get<Sgnls.PlayerSignals.Jump>()
                .On(OnPlayerJump)
                .Countdown(3) // How many times handler (OnPlayerJump) will be called.
                .Priority(1000) // Priority of handler (OnPlayerJump) in queue.
                .When(() => _player.health >= 50); // Condition in which handler will be called.

            ISignalCallback playerDamageCallback = hub1.Get<Sgnls.PlayerSignals.Damage>().On(damage =>
            {
                Debug.Log($"Update player's health bar: health {_player.health} / 100");
            });

            _player = new Player(hub1);
            
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.J))
            {
                _player.Jump();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                _player.Damage(10);
            }
            
        }

        private void OnPlayerJump()
        {
            Debug.Log("We ");
        }
    }
}

