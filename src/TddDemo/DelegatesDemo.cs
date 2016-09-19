using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    delegate void NaamVanDeDelegate(int parameter);
    delegate int NaamVanDeDelegateMetReturnType();

    public class DelegatesDemo
    {
        [Fact]
        public void WatKanJeEigenlijkPreciesMetEenDelegate()
        {
            NaamVanDeDelegate method = new NaamVanDeDelegate(DemoMethode);
            method(4);
        }

        private static void DemoMethode(int i)
        {
        }
    }


}
