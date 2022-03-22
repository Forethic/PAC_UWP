using System;
using System.Threading;

namespace CharacterMap.Helpers
{
    public class ExceptionHandlingSynchronizationContext : SynchronizationContext
    {
        private readonly SynchronizationContext _syncContext;

        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;

        public static ExceptionHandlingSynchronizationContext Register()
        {
            SynchronizationContext current = Current;
            if (current == null)
            {
                throw new InvalidOperationException("Ensure a synchronization context exists before calling this method.");
            }

            ExceptionHandlingSynchronizationContext exceptionHandlingSynchronizationContext = current as ExceptionHandlingSynchronizationContext;
            if (exceptionHandlingSynchronizationContext == null)
            {
                exceptionHandlingSynchronizationContext = new ExceptionHandlingSynchronizationContext(current);
                SetSynchronizationContext(exceptionHandlingSynchronizationContext);
            }

            return exceptionHandlingSynchronizationContext;
        }

        private static void EnsureContext(SynchronizationContext context)
        {
            if (Current != context)
            {
                SetSynchronizationContext(context);
            }
        }

        public ExceptionHandlingSynchronizationContext(SynchronizationContext syncContext)
        {
            _syncContext = syncContext;
        }

        private SendOrPostCallback WrapCallback(SendOrPostCallback sendOrPostCallback)
        {
            return delegate (object state)
            {
                try
                {
                    sendOrPostCallback(state);
                }
                catch (Exception exception)
                {
                    if (!HandleException(exception))
                    {
                        throw;
                    }
                }
            };
        }

        private bool HandleException(Exception exception)
        {
            if (this.UnhandledException == null)
            {
                return false;
            }

            UnhandledExceptionEventArgs unhandledExceptionEventArgs = new UnhandledExceptionEventArgs
            {
                Exception = exception
            };
            UnhandledException(this, unhandledExceptionEventArgs);
            return unhandledExceptionEventArgs.Handled;
        }
    }
}