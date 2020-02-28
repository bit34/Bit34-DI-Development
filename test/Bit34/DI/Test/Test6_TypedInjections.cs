// Copyright (c) 2018 Oğuz Sandıkçı
// This code is licensed under MIT license (see LICENSE.txt for details)

using System;
using Xunit;
using Bit34.DI;
using Bit34.DI.Error;
using Bit34.DI.Test.Payloads;


namespace Bit34.DI.Test
{
    public class Test6_TypedInjections
    {
        [Fact]
        public void Test_TypedInjection()
        {
            Injector injector = new Injector();

            //  Add first binding
            injector.AddBinding<SimpleClassA>().ToType<SimpleClassA>();

            //  Create injection target
            ClassThatUses_SimpleClassA target = new ClassThatUses_SimpleClassA();

            //  Check before injection
            Assert.Null(target.value1);
            Assert.Null(target.value2);
            
            //  Inject
            injector.InjectInto(target);

            //  Check error
            Assert.Equal(0, injector.ErrorCount);

            //  Check after injection
            Assert.NotNull(target.value1);
            Assert.NotNull(target.value2);
        }

        [Fact]
        public void Test_TypedInjectionToNestedMembers()
        {
            Injector injector = new Injector();

            //  Add first binding
            injector.AddBinding<SimpleClassA>().ToType<SimpleClassA>();

            //  Create injection target
            ExtendedClassThatUses_SimpleClassA target = new ExtendedClassThatUses_SimpleClassA();

            //  Check before injection
            Assert.Null(target.value1);
            Assert.Null(target.value2);
            
            //  Inject
            injector.InjectInto(target);

            //  Check error
            Assert.Equal(0, injector.ErrorCount);

            //  Check after injection
            Assert.NotNull(target.value1);
            Assert.NotNull(target.value2);
        }

        [Fact]
        public void Test_TypedInjectionToAssignableType()
        {
            Injector injector = new Injector();

            //  Add first binding
            injector.AddBinding<ISimpleInterfaceA>().ToType<SimpleClassA>();

            //  Create injection target
            ClassThatUses_SimpleInterfaceA target = new ClassThatUses_SimpleInterfaceA();

            //  Check before injection
            Assert.Null(target.value);
            
            //  Inject
            injector.InjectInto(target);

            //  Check error
            Assert.Equal(0, injector.ErrorCount);

            //  Check after injection
            Assert.NotNull(target.value);
        }

        [Fact]
        public void Test_Error_TypedInjectionToWrongType()
        {
            Injector injector = new Injector();

            //  Add first binding
            injector.AddBinding<SimpleClassB>().ToType<SimpleClassB>();

            //  Create injection target
            ClassThatUses_SimpleClassA target = new ClassThatUses_SimpleClassA();

            //  Check before injection
            Assert.Null(target.value1);
            Assert.Null(target.value2);
            
            //  Inject
            injector.InjectInto(target);

            //  Check error
            Assert.Equal(2, injector.ErrorCount);
            Assert.Equal(InjectionErrorType.CanNotFindBindingForType,injector.GetError(0).Error);
            Assert.Equal(InjectionErrorType.CanNotFindBindingForType,injector.GetError(1).Error);

            //  Check after injection
            Assert.Null(target.value1);
            Assert.Null(target.value2);
        }
    }
}
