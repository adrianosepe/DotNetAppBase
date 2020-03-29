namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Data
{
	public class RecordValue : IPresentableValue
	{
		public string DefaultDisplay { get; set; }

		public decimal Value { get; set; }

		public object Tag { get; set; }
	}
}