﻿using System;
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
    }
}
