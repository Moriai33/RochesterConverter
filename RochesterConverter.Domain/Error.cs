using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RochesterConverter.Domain
{
    public class Error
    {
        public string Errortext { get; }
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public Error(string errorText, int rowIndex, int columnIndex)
        {
            Errortext = errorText;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
    }
}
