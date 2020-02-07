using System;
using System.Text;
using Grapp.AppBase.Std.Exceptions.Base;

namespace Grapp.ApplicationBase.Db
{
    public class SqlBathProcess
    {
        private readonly StringBuilder _buffer;
        private readonly int _maxLengthParameter;
        private readonly Action<string> _methodExecute;

        private readonly object _objSync = new object();
        private readonly char _parameterSeparator;

        public SqlBathProcess(int maxLengthParameter, char parameterSeparator, Action<string> methodExecute)
        {
            _maxLengthParameter = maxLengthParameter;
            _parameterSeparator = parameterSeparator;
            _methodExecute = methodExecute;

            _buffer = new StringBuilder();
        }

        public void AddParameterPart(string parameterPart)
        {
            if(String.IsNullOrEmpty(parameterPart))
            {
                throw new XException(message: "Não é possível adicionar um parâmetro parcial nulo ou vazio.");
            }

            lock(_objSync)
            {
                if(_buffer.Length + parameterPart.Length > _maxLengthParameter)
                {
                    _methodExecute(obj: _buffer.ToString());
                    _buffer.Length = 0;
                }

                _buffer.Append(value: parameterPart + _parameterSeparator);
            }
        }

        public void Flush()
        {
            lock(_objSync)
            {
                if(_buffer.Length > 0)
                {
                    _methodExecute(obj: _buffer.ToString());
                    _buffer.Length = 0;
                }
            }
        }
    }
}