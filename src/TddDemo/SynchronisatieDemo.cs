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
        public event Action SomeEvent;


        public void RaiseEventOnOtherThread()
        {
            Task.Run(async () => 
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                SomeEvent?.Invoke();
            });
        }

    }
    public class SynchronisatieDemo
    {
        private AutoResetEvent eventIsRaised;

        [Fact]
        public void HoeSynchroniseerIkIetsWatOpVerschillendeThreadsWordtUitgevoerd()
        {
            // Arrange
            var thing = new SomeEventRaisingThing();
            thing.SomeEvent += ReactOnEvent;

            using (eventIsRaised = new AutoResetEvent(false))
            {
                // Act
                thing.RaiseEventOnOtherThread();

                bool result = eventIsRaised
                    .WaitOne(TimeSpan.FromSeconds(10));

                // Assert
                Assert.True(result);
            }
        }

        private void ReactOnEvent()
        {
            eventIsRaised.Set();
        }
    }
}
