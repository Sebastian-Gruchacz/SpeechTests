using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    class StaticClass : ICloneable<StaticClass>
    {
        private bool _isNew;
        private int _liczba;
        private string _name;

        public StaticClass()
        {
            _isNew = true;
        }

        public int Liczba
        {
            get { return _liczba; }
            set
            {
                _liczba = value;
                _isNew = false;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _isNew = false;
            }
        }

        public bool IsNew
        {
            get { return _isNew; }
        }

        public StaticClass Clone(StaticClass original)
        {
            return new StaticClass
            {
                _isNew = true,
                _liczba = original.Liczba,
                _name = original._name
            };
        }

        public StaticClass Clone()
        {
            return new StaticClass
            {
                _isNew = true,
                _liczba = this.Liczba,
                _name = this._name
            };
        }
    }

}
