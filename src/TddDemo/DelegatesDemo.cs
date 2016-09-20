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
            NaamVanDeDelegate method1 = new NaamVanDeDelegate(DemoMethode);
            var method2 = new NaamVanDeDelegate(DemoMethode);
            //var method3 = DemoMethode;
            NaamVanDeDelegate method4 = DemoMethode;

            method1(4);
            method1.Invoke(4);
        }

        private static void DemoMethode(int i)
        {
        }

        [Fact]
        public void KunnenGenericsGecombineerdWordenMetDelegates()
        {
            NaamVanDeDelegate<bool> method = DemoMethode;
          
        }

        delegate void NaamVanDeDelegate<T>(T arg);
        delegate void NaamVanDeDelegate<T1, T2>(T1 arg1, T2 arg2);
        delegate void NaamVanDeDelegate<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg);

        delegate TResult NaamVanDeDelegateMetReturnType<TResult>();
        delegate TResult NaamVanDeDelegateMetReturnType<T, TResult>(T arg);
        delegate TResult NaamVanDeDelegateMetReturnType<T1, T2, TResult>(T1 arg1, T2 arg2);

        private static void DemoMethode(bool b)
        {
        }

        [Fact]
        public void WatNouAlsIkMoeiteHebMetKleineMethodesEnDommeNamen()
        {
            Func<int, bool> method1 = IsGetalDeelbaarDoorTwee;
            Func<int, bool> method2 = delegate (int i) { return i % 2 == 0; };

            Func<int, bool> method3 = (int i) => { return i % 2 == 0; };
            Func<int, bool> method4 = i => i % 2 == 0;
        }

        private static bool IsGetalDeelbaarDoorTwee(int i)
        {
            return i % 2 == 0;
        }

        [Fact]
        public void ContraVarianceMetDelegates()
        {
            Func<object, bool> method5 = i => true;
            Func<string, bool> method6 = method5;
            method5(12);
            method6("12");
        }

        int counter = 0;

        [Fact]
        public void HoeWerkenMulticastDelegates()
        {
           
            Action a = () => counter++;
            a += () => counter += 2;
            a += delegate () { counter += 10; };
            a += HoogDeCounterOp;

            a();
            Assert.Equal(43, counter);

            // Blijkbaar kan je zo een specifieke methode uitvoeren
            // uit het lijstje met gekoppelde methodes :)
            counter = 0;
            a.GetInvocationList()[1].DynamicInvoke();
            Assert.Equal(2, counter);
        }

        private void HoogDeCounterOp()
        {
            this.counter += 30;
        }
    }


}


