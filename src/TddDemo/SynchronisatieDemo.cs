using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    class SomeEventRaisingThing
    {
        public event Action Raise;


        public void RaiseEventOnSeperateThread()
        {
            Task.Run(async () => 
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                Raise?.Invoke();
            });
        }

    }
    public class SynchronisatieDemo
    {
        private AutoResetEvent isRaised;

        [Fact]
        public void HoeSynchroniseerIkIetsWatOpVerschillendeThreadsWordtUitgevoerd()
        {
            var thing = new SomeEventRaisingThing();
            thing.Raise += React;

            using (isRaised = new AutoResetEvent(false))
            {
                thing.RaiseEventOnSeperateThread();

                bool result = isRaised.WaitOne(TimeSpan.FromSeconds(10));
                Assert.True(result);
            }
        }

        private void React()
        {
            isRaised.Set();
        }
    }
}
