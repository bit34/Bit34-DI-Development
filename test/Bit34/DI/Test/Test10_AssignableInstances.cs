using System.Collections.Generic;
using Xunit;
using Com.Bit34Games.DI.Test.Payloads;

namespace Com.Bit34Games.DI.Test
{
    public class Test10_AssignableInstances
    {
        [Fact]
        public void Test_GetAssignableInstances()
        {
            Injector injector = new Injector(true);

            //  Add first binding
            injector.AddBinding<SimpleClassA>().ToType<SimpleClassA>();
            injector.AddBinding<SimpleClassAA>().ToType<SimpleClassAA>();
            injector.AddBinding<SimpleClassB>().ToType<SimpleClassB>();
            injector.AddBinding<SimpleClassC>().ToValue(new SimpleClassC());

            //  Validate binding
            Assert.Equal(4,injector.BindingCount);

            //  Check error
            Assert.Equal(0,injector.ErrorCount);

            //  Check instance types and count
            IEnumerator<ISimpleInterfaceAA> instances = injector.GetAssignableInstances<ISimpleInterfaceAA>();
            int instanceCounter = 0;
            while(instances.MoveNext())
            {
                Assert.IsAssignableFrom<ISimpleInterfaceAA>(instances.Current);
                instanceCounter++;
            }
            Assert.Equal(2, instanceCounter);
            
            //  Check error
            Assert.Equal(0,injector.ErrorCount);
        }

    }
}
