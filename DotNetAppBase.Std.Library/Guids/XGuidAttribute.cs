namespace DotNetAppBase.Std.Library.Guids
{
    public class XGuidAttribute : System.Attribute
    {
        private readonly System.Guid _value;

        public XGuidAttribute(string guid) => _value = System.Guid.Parse(guid);

        public System.Guid Value => _value;
    }
}