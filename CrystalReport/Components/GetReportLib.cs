using Dr.Sale.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalReport.Components
{
   public class GetReportLib
    {

        private string _d { get; set; }
        public static event EventHandler<string> Report;

        public GetReportLib(string data)
        {

            _d = data;


            OnDataTransferred(_d);
        }



        protected virtual void OnDataTransferred(string data)
        {

            Report?.Invoke(this, StoreRoom.SendReport = data);
        }












    }
}
