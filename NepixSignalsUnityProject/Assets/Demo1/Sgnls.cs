
using NepixSignals;

#pragma warning disable 0649
namespace Demo1
{
    /// <summary>
    /// Static class that holds all types of signals.
    /// </summary>
    public static class Sgnls
    {
        /// <summary>
        /// Player's signals
        /// </summary>
        public static class PlayerSignals
        {
            /// <summary>
            /// Player's Jump signal
            /// </summary>
            public class Jump : ASignal {}
            
            /// <summary>
            /// Player's Damage signal
            /// float - how much damage player got
            /// </summary>
            public class Damage : ASignal<float> {}
            
            /// <summary>
            /// Player's Died signal
            /// </summary>
            public class Died : ASignal {}
        }
        
        public static class EnemySignals
        {
            /// <summary>
            /// An enemy attacked player with attack power
            /// float - attack power
            /// </summary>
            public class Attack : ASignal<float> {}
            
            /// <summary>
            /// An enemy drops a loot
            /// float - how much coins
            /// int - id of a item
            /// bool - is item rare
            /// </summary>
            public class DropLoot : ASignal<float, int, bool> {}
        }
    }
}