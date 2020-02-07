using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Business.Imp
{
    public class StubEntity : IEntity
    {
        public int ID { get; set; }

        public string Display { get; set; }
    }
}