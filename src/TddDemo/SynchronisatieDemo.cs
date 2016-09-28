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
            Task.Run(() => 
            {
                Task.Delay(TimeSpan.FromSeconds(2));
                Raise?.Invoke();
            });
        }

    }
    public class SynchronisatieDemo
    {
        private bool isRaised;

        [Fact]
        public void HoeSynchroniseerIkIetsWatOpVerschillendeThreadsWordtUitgevoerd()
        {
            var thing = new SomeEventRaisingThing();
            thing.Raise += React;
            thing.RaiseEventOnSeperateThread();

            Thread.Sleep(2000);

            Assert.True(isRaised);
        }

        private void React()
        {
            isRaised = true;
        }
    }
}
