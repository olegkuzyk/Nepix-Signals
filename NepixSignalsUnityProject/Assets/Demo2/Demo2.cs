using NepixSignals;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace Demo2
{
    public class Demo2 : MonoBehaviour
    {
        public Button button1;
        public Button button2;
        
        public class OnClick : ASignal<Button> { }

        private void Start()
        {
            SignalHub hub = new SignalHub();

            button1.onClick.AddListener(() => hub.Get<OnClick>().Dispatch(button1));
            button2.onClick.AddListener(() => hub.Get<OnClick>().Dispatch(button2));

            hub.Get<OnClick>()
                .On(btn =>
                {
                    Debug.Log($"On Click Handler 1. Dispatcher {btn.name}");
                });

            hub.Get<OnClick>()
                .On(btn => Debug.Log($"On Click Handler 2. But should go first. Dispatcher {btn.name}"))
                .Priority(100);

            var count = 3;
            hub.Get<OnClick>()
                .On(btn =>
                {
                    count--;
                    Debug.Log($"On Click Handler 3. Left {count}. Dispatcher {btn.name}");
                })
                .Countdown(3);

            hub.Get<OnClick>()
                .On(btn =>
                {
                    Debug.Log($"On Click Handler 4. Dispatch when count == 0 {count}. Dispatcher {btn.name}");
                })
                .When(() => count == 0);
        }
    }
}