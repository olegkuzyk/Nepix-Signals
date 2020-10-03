
#pragma warning disable 0649
namespace NepixSignals
{
    public partial class ABaseSignal<T>
    {
        private Callback Spawn(T handler)
        {
            Callback callback;
            if (_pool.Count > 0)
            {
                callback = _pool[0];
                callback.Reset(this, handler);
                _pool.RemoveAt(0);
            }
            else
            {
                callback = new Callback(this, handler);
            }

            return callback;
        }

        private void Despawn(Callback callback)
        {
            if (callback.IsAddedToSignal()) callback.Off();
            _pool.Add(callback);
        }
    }
}