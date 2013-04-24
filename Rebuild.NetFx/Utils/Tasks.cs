using Rebuild.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rebuild.Utils
{
    #region enum CancellationMode
    public enum CancellationMode
    {
        AmbientOrNew,
        AmbientAndNew,
        RequireAmbient,
        RequireNew
    }
    #endregion

    public static class Tasks
    {
        [ThreadStatic]
        private static CancellationToken _cancellation;

        public static CancellationToken Cancellation
        {
            get { return _cancellation; }
            internal set { _cancellation = value; }
        }

        private static void CreateToken(CancellationMode mode, out CancellationTokenSource source, out CancellationToken token)
        {
            source = default(CancellationTokenSource);
            token = default(CancellationToken);

            switch (mode)
            {
                case CancellationMode.RequireAmbient:
                    if (!_cancellation.CanBeCanceled)
                        throw new InvalidOperationException("Ambient cancellation token cannot be found.");

                    token = _cancellation;
                    break;

                case CancellationMode.RequireNew:
                    source = new CancellationTokenSource();
                    token = source.Token;
                    break;

                case CancellationMode.AmbientAndNew:
                    if (!_cancellation.CanBeCanceled)
                    {
                        throw new InvalidOperationException("Ambient cancellation token cannot be found.");
                    }
                    source = new CancellationTokenSource();
                    token = CancellationTokenSource.CreateLinkedTokenSource(source.Token, _cancellation).Token;
                    break;

                case CancellationMode.AmbientOrNew:
                    source = new CancellationTokenSource();
                    token = _cancellation.CanBeCanceled
                        ? CancellationTokenSource.CreateLinkedTokenSource(source.Token, _cancellation).Token
                        : source.Token;
                    break;
            }
        }

        public static Task StartWithCancellation(Action action, CancellationMode mode = CancellationMode.AmbientOrNew)
        {
            CancellationToken token;
            CancellationTokenSource source;
            CreateToken(mode, out source, out token);

            Action a = () =>
            {
                _cancellation = token;
                try
                {
                    action();
                }
                finally
                {
                    _cancellation = CancellationToken.None;
                }
            };

            return Task.Factory.StartNew(a, token).AddCancellationTokenSource(source);
        }

        public static Task<TResult> StartWithCancellation<TResult>(Func<TResult> function, CancellationMode mode = CancellationMode.AmbientOrNew)
        {
            CancellationToken token;
            CancellationTokenSource source;
            CreateToken(mode, out source, out token);

            Func<TResult> f = () =>
            {
                _cancellation = token;
                try
                {
                    return function();
                }
                finally
                {
                    _cancellation = CancellationToken.None;
                }
            };

            return Task.Factory.StartNew(f, token).AddCancellationTokenSource(source);
        }
    }
}
