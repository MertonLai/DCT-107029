
namespace ConsoleApp1.Models {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class ContosoUniversityEntities : DbContext {

        public override int SaveChanges() {

            // 取得所有EF快取資料（狀態不是 Unchanged 的）
            var entries = this.ChangeTracker.Entries();

            foreach (var entry in entries) {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added) {
                    entry.CurrentValues.SetValues(new {
                        ModifiedOn = DateTime.Now
                    });
                }
            }

            return base.SaveChanges();
        }

    }
}
