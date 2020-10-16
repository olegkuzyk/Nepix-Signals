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

---

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

---

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


### Basic API:
#### ASignal

```c#
ISignalCallback On(T handler, bool single);
``` 
Adds a listener to this Signal. 

__handler__ Method to be called when signal is fired.

__single__ Must contains only one instance of handler

---

```c#
ISignalCallback On(bool isOn, T handler);
```

Adds a listener to this Signal

__isOn__ Whether ON or OFF the handler

__handler__ Method to be called when signal is fired

---

```c#
ISignalCallback Once(T handler);
```

Adds a listener to this Signal. This will fire once and than will be removed.

__handler__ Method to be called when signal is fired

---

```c#
ISignalCallback Off(T handler);
```

Removes a listener from this Signal. If was added multiple times the same handler that all of its instances will be removed. If you need to remove specific callback than do it via ISignalCallback.Off()

__handler__ Method to be unregistered from signal

---

```c#
void OffAll();
```

Remove all listeners.

---

```c#
void Sort();
```

Sort callbacks by priority.

---

```c#
bool Has(T handler);
```

__handler__ Has handler in any callback.

---

#### ISignalCallback

```c#
int priority;
```

Priority of callback.

---

```c#
int countdown;
```

How many times left to call before remove.
Negative value means infinite calls amount.

---

```c#
bool IsAddedToSignal();
```

Check if callback added to a signal.

---

```c#
void On();
```

On again the callback to signal, in case it was off. __Warning:__ exception could be fired if signal already has this callback.

---

```c#
void Once();
```

Once again the callback to signal, in case it was off. __Warning:__ exception could be fired if signal already has this callback.

---

```c#
void Off();
```

Off from signal.

---

```c#
ISignalCallback When(Func<bool> predicate);
```

The handler will be called only when predicate fulfilled.

__predicate__ When predicate.

---

```c#
ISignalCallback Priority(int value);
```

Set priority of callback.

__value__ Priority.

---

```c#
ISignalCallback Countdown(int value);
```

Set countdown for callback.
How many times it should be called.
Negative value means infinite calls amount.

__value__ Countdown amount.

