using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApp1
{
    class Program
    {
        static int InsertedId;

        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {

                db.Database.Log = Console.WriteLine;

                #region 延遲載入設定

                //db.Configuration.LazyLoadingEnabled = false;

                // 關閉代理屬性
                //db.Configuration.ProxyCreationEnabled = false;

                #endregion 延遲載入設定

                db.Department.Add(new Department() {
                    Budget = 100,
                    Name = "TEST",
                    StartDate = DateTime.Now.AddDays(+2)
                });

                db.SaveChanges();

                var department = db.Department.Include(c => c.Course).AsNoTracking();

                foreach (var dept in department) {
                    Console.WriteLine(dept.Name);
                    foreach (var _cou in dept.Course) {
                        Console.WriteLine(string.Format("\t{0}-{1}", _cou.CourseID, _cou.Title));

                    }

                }


                //QueryCourse(db);

                //InsertDepartment(db);

                //UpdateDepartment(db);

                //RemoveDepartment(db);

                Console.WriteLine("請輸入任意鍵繼續˙˙˙˙˙");
                Console.ReadKey();
            }
        }

        private static void InsertDepartment(ContosoUniversityEntities db)
        {
            var dept = new Department()
            {
                Name = "Will",
                Budget = 100,
                StartDate = DateTime.Now
            };

            db.Department.Add(dept);
            db.SaveChanges();

            InsertedId = dept.DepartmentID;
        }

        private static void UpdateDepartment(ContosoUniversityEntities db)
        {
            var dept = db.Department.Find(InsertedId);
            dept.Name = "John";
            db.SaveChanges();
        }

        private static void RemoveDepartment(ContosoUniversityEntities db)
        {
            db.Department.Remove(db.Department.Find(InsertedId));
            db.SaveChanges();
        }

        private static void QueryCourse(ContosoUniversityEntities db)
        {
            var data = from p in db.Course select p;

            foreach (var item in data)
            {
                Console.WriteLine(item.CourseID);
                Console.WriteLine(item.Title);
                Console.WriteLine();
            }
        }
    }
}
