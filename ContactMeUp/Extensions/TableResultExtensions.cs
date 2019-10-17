using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMeUp.Extensions
{
    public static class TableResultExtensions
    {
        public static bool IsSuccessStatusCode(this TableResult tableResult)
        {
            return tableResult.HttpStatusCode >= 200 && tableResult.HttpStatusCode <= 299;
        }
    }
}
