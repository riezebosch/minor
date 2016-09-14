using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace TddDemo
{
    public class PropertyDemo
    {
        private int MyProperty { get; set; }

        public int MyPublicField;

        private readonly int _myReadOnlyField;

        public int MyReadOnlyProperty { get; private set; }

        /// <summary>
        /// Deze mag gezet worden vanuit de constructor en sinds C# 6 ook direct
        /// </summary>
        public int MyPropertyMetAlleenEenGetter { get; } = 5;

        private int _field;
        public int MyPropertyMetAlleenEenSetter { set { _field = value; } }

        private int _fullPropertyBackingField;
        private ITestOutputHelper _output;

        public int FullProperty
        {
            get { return _fullPropertyBackingField; }
            set { _fullPropertyBackingField = value; }
        }

        public PropertyDemo(ITestOutputHelper output)
        {
            _output = output;
            MyPropertyMetAlleenEenGetter = 5;
        }

        public int ExpressionBodiedProperty => 5;

        public int ExpressionBodiedMethode() => 5;

        public void KanIkEenPropertyMetAlleenEenGetterVanWaardeVeranderen()
        {
            // Nee, dat mag dus niet
            //MyPropertyMetAlleenEenGetter = 5;
        }

    

        #region De java manier van "properties"
        public void SetFullProperty(int value)
        {
            _fullPropertyBackingField = value;
        }

        public int GetFullProperty()
        {
            return _fullPropertyBackingField;
        } 
        #endregion

        [Fact]
        public void WatZijnProperties()
        {
            var type = this.GetType();
            foreach (var member in type.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                _output.WriteLine(member.Name);
            }
        }
    }
}
