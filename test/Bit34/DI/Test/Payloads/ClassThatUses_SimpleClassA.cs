namespace Bit34.DI.Test.Payloads
{
    class ClassThatUses_SimpleClassA
    {
        [Inject]
        public SimpleClassA value1;
        public SimpleClassA value2 { get{ return _value2; } }
        [Inject]
        private SimpleClassA _value2;

        public ClassThatUses_SimpleClassA(){value1 = null; _value2=null;}
    }
}
