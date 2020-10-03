using NepixSignals;
using UnityEngine;

#pragma warning disable 0649
namespace Demo1
{
    /// <summary>
    /// Demo player class
    /// </summary>
    public class Player
    {
        private SignalHub _hub;

        private float _health;

        public float health => _health;
        
        public Player(SignalHub hub)
        {
            _health = 100;
            _hub = hub;
        }

        public void Jump()
        {
            // Do some math here...
            Debug.Log("Player jumps...");
            
            // Dispatch via Signal Hub that we made a jump.
            _hub.Get<Sgnls.PlayerSignals.Jump>().Dispatch();
        }

        public void Damage(float amount)
        {
            _health -= amount;
            _health = Mathf.Max(0, _health);
            Debug.Log($"Player got damage... health: {_health}");
            
            // Dispatch via Signal Hub that we got a damage.
            _hub.Get<Sgnls.PlayerSignals.Damage>().Dispatch(amount);

            if (_health <= 0)
            {
                Debug.Log($"Player has died...");
                // Dispatch via Signal Hub that player died
                _hub.Get<Sgnls.PlayerSignals.Died>().Dispatch();
            }
        }
    }
}