using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleJson
{
    public class FProduct
    {

        public string EPC = string.Empty;
        public string Pname = string.Empty;
        public string productPici = string.Empty;
        public string temputer = string.Empty;
        public string mature = string.Empty;
        public string startTime = string.Empty;
        public string endTime = string.Empty;
        public string beiZhu = string.Empty;
        public string productState = string.Empty;
        public string state = string.Empty;







        public FProduct() { }
        public FProduct(string _EPC, string _Pname, string _productPici, string _temputer,
            string _mature, string _startTime, string _endTime, string _beiZhu, string _productState, string _state)
        {
            this.EPC = _EPC;
            this.Pname = _Pname;
            this.productPici = _productPici;
            this.temputer = _temputer;
            this.mature = _mature;
            this.startTime = _startTime;
            this.endTime = _endTime;
            this.beiZhu = _beiZhu;
            this.productState = _productState;
            this.state = _state;

        }

    }
}
