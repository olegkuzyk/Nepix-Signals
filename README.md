# Nepix Signals
Lightweight and powerful signal (event) lib for Unity.
---
Based on [Signals by Yanko Oliveira](https://github.com/yankooliveira/signals).
Inspired by [UniRx](https://github.com/neuecc/UniRx) chain-calls and [signal-js](https://www.npmjs.com/package/signal-js) with its on/once/off/ methods.

### Usage:
First, lets create a hub, that will hold all signals:

```c#
SignalHub hub = new SignalHub();
```

<details><summary>Pro Tip :sunglasses:</summary>
  
Use one hub per scene or per level, than when you restart scene or level you haven't to care about remove listeners from *old* objects.

</details>


Than create some signal types:
```c#
public class PlayerJump : ASignal {}
public class PlayerDamage : ASignal<float> {}
public class PlayerDied : ASignal {}
```
<details><summary>Pro Tip :sunglasses:</summary>

Use static class to hold all signals in one place. Than, when you need to get one, you just type the static class' name and *Code Completion* shows you all the signals you have.

```c#
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
}
```
</details>

Lastly, add listeners:
```c#
hub.Get<PlayerJump>().On(OnPlayerJump);
hub.Get<PlayerDamage>().On(OnPlayerDamage);
hub.Get<PlayerDied>().On(OnPlayerDied);
```
<details><summary>Pro Tip :sunglasses:</summary>

You can adjust a signal callback with additional method calls

```c#
hub.Get<Sgnls.PlayerSignals.Jump>()
  .On(OnPlayerJump) // Set handler method
  .Countdown(3) // How many times handler (OnPlayerJump) will be called.
  .Priority(1000) // Priority of handler (OnPlayerJump) in queue.
  .When(() => _player.health >= 50); // Condition in which handler will be called.
```

</details>
