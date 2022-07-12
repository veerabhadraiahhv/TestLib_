using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GCWorkings.ResourceMgmt
{
    public enum ResourceState
    {
        FREE, BUSY
    }
    public class Resource
    {
        public static readonly Resource Instance = new Resource();
        private Resource() { }
        public ResourceState State = ResourceState.FREE;

    }
    public class A//:IDisposable
    {

        public static System.Threading.AutoResetEvent _handle = new System.Threading.AutoResetEvent(false);
        private bool disposedValue;

        public A()

        {
            lock (A._handle)
            {
                if (Resource.Instance.State == ResourceState.FREE)
                {
                    Console.WriteLine($"Resource Owned By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                }
                else
                {
                    Console.WriteLine($"Resource Awaited By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    //wait
                    _handle.WaitOne();
                    Console.WriteLine($"Resource Owned By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                }

                Resource.Instance.State = ResourceState.BUSY;
            }
        }

        public void UseResource()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Resource Used By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }

        }

        ~A()
        {
            //Resource.Instance.State = ResourceState.FREE;
            //_handle.Set();

            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;

            if (disposing)
            {
                Resource.Instance.State = ResourceState.FREE;
                _handle.Set();
            }

            disposedValue = true;
        }
        
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    class _Program
    {
        static void Main()
        {
            new System.Threading.Thread(Client).Start();
            new System.Threading.Thread(Client).Start();
        }
        static void Client()
        {
            A obj = null;
            obj = new A();
            obj.UseResource();
            obj = null;
            GC.Collect();

        }
    }
}
