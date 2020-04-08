﻿namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations
{
	public enum EDataType
	{
		Custom = 0,

		DateTime = 1,
		Date = 2,
		Time = 3,
		Duration = 4,
		PhoneNumber = 5,
		Currency = 6,
		Text = 7,
		Html = 8,
		MultilineText = 9,
		EmailAddress = 10,
		Password = 11,
		Url = 12,
		ImageUrl = 13,
		CreditCard = 14,
		PostalCode = 15,
		Upload = 16,

		PrimaryKey = 20,
		ForeignKey = 21,
		Xml = 22
	}
}