using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.context;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        dopEntities2 db = new dopEntities2();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        public ActionResult MyCourses()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
        }

        public ActionResult ApplyCourses()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
        }

        [HttpPost]
        public ActionResult MyCourses(FormCollection collection)
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
        }

        [HttpPost]
        public ActionResult ApplyCourses(FormCollection collection)
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
        }

        [HttpPost]
        public ActionResult Register(user model)
        {
            user obj = new user();
            obj.name = model.name;
            obj.email = model.email;
            obj.password = model.password;
            obj.gender = model.gender;
            obj.qualification = model.qualification;
            obj.branch = model.branch;
            obj.rank=model.rank;
            obj.location= model.location;
            obj.medical=model.medical;  
            obj.age=model.age;
            obj.posting=model.posting;
            obj.yearsOfServices = model.yearsOfServices;

            try
            {
                var data = db.users.Where(s => s.email.Equals(model.email));
                if (data.Count() > 0)
                {
                    Session["Error"] = "Email id already exist";
                    return View("Register");
                }
                db.users.Add(obj);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return View("Login");

        }

        [HttpPost]
        public ActionResult UpdateUser(FormCollection collection)
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
        }

        [HttpPost]
        public ActionResult Update(user model)
        {
    
            var email = Session["Email"].ToString();
            user prevdata = new user();
            prevdata = db.users.First(s => s.email.Equals(email));
            
            prevdata.name = model.name;
            prevdata.email = model.email;
            prevdata.password = model.password;
            prevdata.gender = model.gender;
            prevdata.qualification = model.qualification;
            prevdata.branch = model.branch;
            prevdata.rank = model.rank;
            prevdata.location = model.location;
            prevdata.medical = model.medical;
            prevdata.age = model.age;
            prevdata.posting = model.posting;
            prevdata.yearsOfServices = model.yearsOfServices;

            try
            {
                db.Entry(prevdata).State=System.Data.EntityState.Modified;
                db.SaveChanges();
                Session["FullName"] = prevdata.name;
                Session["Email"] = prevdata.email;
                Session["Branch"] = prevdata.branch;
                Session["Rank"] = prevdata.rank;
                Session["Qualification"] = prevdata.qualification;
                Session["Age"] = prevdata.age;
                Session["Gender"] = prevdata.gender;
                Session["Medical"] = prevdata.medical;
                Session["Location"] = prevdata.location;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            

            return View("LearnerProfile",tables);

        }


        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");

        //    }
        //    return byte2String;
        //}

        [HttpPost]
        public ActionResult Login(user model)
        {
            if (ModelState.IsValid)
            {


                //var f_password = model.password;
                var data = db.users.Where(s => s.email.Equals(model.email) && s.password.Equals(model.password)).ToList();
                if (data.Count() > 0)
                {

                    //add session
                    Session["FullName"] = data.FirstOrDefault().name;
                    Session["Email"] = data.FirstOrDefault().email;
                    Session["Branch"] = data.FirstOrDefault().branch;
                    Session["Rank"] = data.FirstOrDefault().rank;
                    Session["Qualification"] = data.FirstOrDefault().qualification;
                    Session["Age"] = data.FirstOrDefault().age;
                    Session["Gender"] = data.FirstOrDefault().gender;
                    Session["Medical"] = data.FirstOrDefault().medical;
                    Session["Location"] = data.FirstOrDefault().location;
                    var tables = new AdminViewModel
                    {
                        Course = db.courses.ToList(),
                        CourseCondition = db.CourseConditions.ToList(),
                        UserCourses = db.usercourses.ToList(),
                        CourseImplication = db.CourseImplications.ToList(),
                        Condition = db.conditions.ToList(),
                        Users = db.users.ToList()
                    };
                    return View("LearnerProfile",tables);
                }
                else
                {
                    Session["Error"] = "Incorrect email/password";
                    return View("Login");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Demo(FormCollection collection)
        {
            

            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };


            if (ModelState.IsValid)
            {
                var course_id = collection["link"];
                var user_id = collection["user"];
                //Session["Email"] = user_id;
                try
                {
                    usercours uc = new usercours();
                    uc.course_id = Int16.Parse(course_id);
                    uc.user_id = Int16.Parse(user_id);
                    uc.status = "Pending_admin6";
                    uc.applyTime = DateTime.Now;
                    db.usercourses.Add(uc);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

                ViewBag.res = "Success";                
                return RedirectToAction("MyCourses", tables);
            }
            return RedirectToAction("MyCourses");

        }

        public ActionResult LearnerProfile()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
        }

        public ActionResult UpdateUser()
        {
            return View();
        }

        public ActionResult Demo()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            return View(tables);
            
        }

        public ActionResult ImplicationDetail()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Implication=db.implications.ToList()
            };
            return View(tables);

        }

        [HttpPost]
        public ActionResult GetDetails(FormCollection collection)
        {
            var course_id = collection["id"];

            ViewBag.course_id = course_id;
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Implication = db.implications.ToList()
            };
            return View("ImplicationDetail", tables);
        }

    }
}