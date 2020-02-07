using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Business;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Business.Enums;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Utilities;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model
{
	public abstract class WSEntityBase : EntityBase, IEntityWorkWithSituation
	{
		[Display(Name = EntityBaseDisplayPattern.DefaultDisplaySituation)]
		public abstract ELogicalDelete Situacao { get; set; }

		public new class Metadata : EntityBase.Metadata
		{
			[Display(Name = EntityBaseDisplayPattern.DefaultDisplaySituation)]
			public ELogicalDelete Situacao { get; set; }
		}

		public new class MetadataWithLookup : EntityBase.MetadataWithLookup
		{
			[Display(Name = EntityBaseDisplayPattern.DefaultDisplaySituation)]
			public ELogicalDelete Situacao { get; set; }
		}
	}
}