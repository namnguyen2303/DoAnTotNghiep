using Data.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Business
{
    public class GenericBusiness
    {
        public TranDungShopEntities context = new TranDungShopEntities();

        public TranDungShopEntities cnn = new TranDungShopEntities();

        public GenericBusiness(TranDungShopEntities context = null)
        {
            this.context = context == null ? new TranDungShopEntities() : context;
            cnn = this.context;
        }
    }
}
