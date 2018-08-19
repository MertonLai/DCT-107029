

namespace MvcApplication1.Models {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Web;

    public partial class ContosoUniversityEntities : DbContext {
        public override int SaveChanges() {


            // 取得登入使用者資訊（透過 HttpContext）
            var _user = HttpContext.Current.Items["CurrentUser"];

            return base.SaveChanges();
        }
    }
}