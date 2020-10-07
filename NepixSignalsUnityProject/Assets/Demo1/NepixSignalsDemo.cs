using NepixSignals;
using NepixSignals.Api;
using UnityEngine;

namespace Demo1
{
    public class NepixSignalsDemo : MonoBehaviour
    {
        private Player _player;
        private ISignalCallback _playerDamageCallback;
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
                .On(OnPlayerJump) // Set handler
                .Countdown(3) // How many times handler (OnPlayerJump) will be called.
                .Priority(1000) // Priority of handler (OnPlayerJump) in queue.
                .When(() => _player.health >= 50); // Condition in which handler will be called.

            // Sometime very handy to listen signal only once.
            _playerDamageCallback = hub1.Get<Sgnls.PlayerSignals.Damage>().Once(damage =>
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
            // Add 10 coins every time player jumps
            _player.coins += 10;
        }
    }
}

