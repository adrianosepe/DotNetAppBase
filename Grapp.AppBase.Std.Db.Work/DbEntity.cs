using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Grapp.AppBase.Std.Exceptions.Bussines;
using Grapp.AppBase.Std.Library;
using Grapp.AppBase.Std.Library.ComponentModel.Model;

// ReSharper disable UnusedMember.Global

namespace Grapp.AppBase.Std.Db.Work
{
    public class DbEntity : Entity
    {
        private bool _dynamicScheme;

        public DbEntity() => _dynamicScheme = true;

        public DbEntity(DataRow data, bool dynamicScheme = false)
        {
            Data = data;

            _dynamicScheme = dynamicScheme;
        }

        [JsonIgnore]
        public DataRow Data { get; set; }

        [JsonIgnore]
        public bool IsDataNull => Data == null;

        [JsonIgnore]
        private DataTable Table => Data.Table;

        public static IEnumerable<T> Create<T>(DataTable dt) where T : DbEntity, new()
        {
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach(var row in dt.Rows.Cast<DataRow>())
                {
                    var entity = new T();
                    entity.Update(row);

                    yield return entity;
                }
            }
        }

        public static TModel Create<TModel>(DataRow row) where TModel : DbEntity, new()
        {
            var model = new TModel();
            model.Update(row);

            return model;
        }

        public static TModel New<TModel>() where TModel : DbEntity, new()
        {
            var model = new TModel();

            model.ConfigureAsNew();

            return model;
        }

        public T Read<T>(string fieldName, T defaultValue)
        {
            if(IsDataNull)
            {
                return defaultValue;
            }

            var value = Data[fieldName];

            return XHelper.Obj.IsNull(value) ? defaultValue : value.CastTo<T>();
        }

        public T Read<T>([Localizable(isLocalizable: false)] string fieldName)
        {
            if(IsDataNull)
            {
                throw XFlowException.Create(failReason: $"Os dados deste {nameof(DbEntity)} não foram informados.");
            }

            var value = InternalReadField<T>(fieldName);

            return value.CastTo<T>();
        }

        public void Update(DataRow data) => Data = data;

        public void Write<T>(string fieldName, T value)
        {
            CheckExistsColumn<T>(fieldName);

            Data[fieldName] = XHelper.Obj.IsNull(value) ? (object)DBNull.Value : value;

            OnPropertyChanged(fieldName);
        }

        protected T AutoRead<T>([CallerMemberName] string fieldName = null) => Read(fieldName, defaultValue: default(T));

        protected T AutoReadEnum<T>([CallerMemberName] string fieldName = null) => (T)Enum.ToObject(enumType: typeof(T), value: Read(fieldName, defaultValue: default(byte)));

        protected void AutoWrite<T>(T value, [CallerMemberName] string fieldName = null) => Write(fieldName, value);

        protected void AutoWriteEnum<T>(T value, [CallerMemberName] string fieldName = null) => Write(fieldName, value: Convert.ChangeType(value, conversionType: typeof(byte)));

        private void CheckExistsColumn<T>(string fieldName)
        {
            if(!_dynamicScheme)
            {
                return;
            }

            if(Data == null)
            {
                ConfigureAsNew();
            }
            else if(!_dynamicScheme || Table.Columns.Contains(fieldName))
            {
                return;
            }

            Table.Columns.Add(fieldName, type: typeof(T));
        }

        private void ConfigureAsNew()
        {
            _dynamicScheme = true;

            var table = new DataTable();
            var row = table.NewRow();

            table.Rows.Add(row);

            Data = row;
        }

        private object InternalReadField<T>(string fieldName)
        {
            CheckExistsColumn<T>(fieldName);

            return Data[fieldName];
        }
    }
}