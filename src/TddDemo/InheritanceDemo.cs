using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class InheritanceDemo
    {
        class A
        {
            public virtual string Method()
            {
                return "Deze methode zit op class A";
            }
        }

        class B : A
        {
            public override string Method()
            {
                return "Maar deze methode zit op class B";
            }

            public string RoepMethodAanOpBase()
            {
                return base.Method();
            }
        }

        [Fact] 
        public void VoorbeeldVanVirtualMethod()
        {
            var item = new B();
            var result = item.Method();

            Assert.Equal("Maar deze methode zit op class B", result);
        }

        [Fact]
        public void VoorbeeldVanVirtualMethodMetBaseClass()
        {
            A item = new B();
            var result = item.Method();

            Assert.Equal("Maar deze methode zit op class B", result);
        }

        [Fact]
        public void VoorbeeldVanVirtualMethodMetBaseCallVanuitDerivedClass()
        {
            var item = new B();
            var result = item.RoepMethodAanOpBase();

            Assert.Equal("Deze methode zit op class A", result);
        }

        class C : A
        {
            public string Method()
            {
                return "Deze hide de method van A omdat het geen override is!";
            }
        }

        [Fact]
        public void VoorbeeldVanMethodHidingInDerivedClass()
        {
            var item = new C();
            var result = item.Method();

            Assert.Equal("Deze hide de method van A omdat het geen override is!", result);
        }

        [Fact]
        public void VoorbeeldVanMethodHidingInDerivedClassMaarAanroepOpBase()
        {
            A item = new C();
            var result = item.Method();

            Assert.Equal("Deze methode zit op class A", result);
        }

        class D : A
        {
            public sealed override string Method()
            {
                return "Deze zit in D en mag verder niet meer overridden worden!";
            }
        }

        [Fact]
        public void HoeWerkenConstructorsMetDerivedClasses()
        {
            var b = new Base(21);
            Assert.Equal(21, b.PropertyFromBase);

            var d = new Derived();
            Assert.Equal(1234, d.PropertyFromDerived);
            Assert.Equal(21, d.PropertyFromBase);

            //  Dit kan natuurlijk niet, want de constructors
            //  van de base zijn niet beschikbaar op derived
            // new Derived(21)
        }

        class Base
        {
            public int PropertyFromBase { get; }
            public Base(int value)
            {
                PropertyFromBase = value;
            }
        }

        class Derived : Base
        {
            public Derived() : base(21)
            {
                PropertyFromDerived = 1234;
            }

            public int PropertyFromDerived { get; }
        }

        [Fact]
        public void HoeGaIkOmMetTweeInterfacesMetDezelfdeMethodeSignatuur()
        {
            var item = new MultipleInterfaces();
            item.A();
            item.A(10);

            item.B();
            int result = ((I2)item).B();
            Assert.Equal(10, result);
        }

        interface I1
        {
            void A();
            void B();
        }

        interface I2
        {
            void A(int input);
            int B();
        }

        class MultipleInterfaces : I1, I2
        {
            public void A()
            {
            }

            public void A(int input)
            {
            }

            public void B()
            {
            }

            int I2.B()
            {
                return 10;
            }
        }
    }
}
