using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using WebApplication2.context;
using System.Data.Entity;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Security.Principal;
using System.Data.Entity.Infrastructure;
using WebApplication2.ViewModel;
using System.Runtime.Remoting.Messaging;
using System.Linq.Expressions;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //object of database
        dopEntities1 db = new dopEntities1() ;
        

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

        
        public ActionResult UpdateAdmin()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Implication = db.implications.ToList(),
                Admins= db.admins.ToList()
            };
            return View(tables);
            
        }

        [HttpPost]
        public ActionResult Update(admin model)
        {
            //email of current admin
            var email = Session["Email"].ToString();
            admin prevdata = new admin();
            prevdata = db.admins.First(s => s.email.Equals(email));
            
            //details of current admin
            prevdata.name = model.name;
            prevdata.email = model.email;
            prevdata.password = model.password;
            prevdata.mobile = model.mobile;
            prevdata.branch = model.branch;

            Session["Name"] = prevdata.name;
            Session["Email"] = prevdata.email;
            Session["Branch"] = prevdata.branch;


            try
            {
                db.Entry(prevdata).State = System.Data.EntityState.Modified; //updating row in the database
                db.SaveChanges();
                Session["FullName"] = prevdata.name;          
                Session["Email"] = prevdata.email;
                Session["Branch"] = prevdata.branch;
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

            
            return View("UpdateAdmin");
        }

        

        [HttpPost]
        public ActionResult Register(admin model)
        {
            admin obj= new admin();
            obj.name = model.name;
            obj.email = model.email;

            Session["Error"] = "";

            obj.password = model.password;
            obj.mobile = model.mobile;
            
            obj.branch = model.branch;

            try
            {
                //Email already exist
                var data = db.admins.Where(s => s.email.Equals(model.email));
                if(data.Count() > 0)
                {
                    Session["Error"] = "Email id already exist";
                    return View("Register");
                }
                db.admins.Add(obj);
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

        public ActionResult Login(admin model)
        {
            if (ModelState.IsValid)
            {

                
                var f_password = model.password;
                var data = db.admins.Where(s => s.email.Equals(model.email) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                { 

                    //add session
                    Session["Name"] = data.FirstOrDefault().name;
                    Session["Email"] = data.FirstOrDefault().email;
                    Session["Branch"]=data.FirstOrDefault().branch;
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
                    return View("AdminProfile",tables);
                }
                else
                {
                    Session["Error"] = "Incorrect email/password";
                    return View("Login");
                }
            }
            return View();
        }

        public ActionResult AdminProfile()
        {
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
            return View(tables);
        }

        public ActionResult AddCourses()
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

        public ActionResult CourseList(FormCollection collection)
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Remarks = db.Remarks.ToList()
            };
            return View(tables);
        }
        //not used in the main website
        public ActionResult Demo()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Remarks = db.Remarks.ToList()
            };
            return View(tables);
        }

        [HttpPost]
        public ActionResult Details(FormCollection collection)
        {
            Session["selected_course"] = collection["link"];
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Remarks = db.Remarks.ToList()
            };
            return View("Candidates",tables);
        }

        public ActionResult Candidates()
        {
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(), 
                Remarks= db.Remarks.ToList() 
            };

            return View(tables);
        }

        //adding the course in the table in database
        [HttpPost]
        public ActionResult CourseDetails(FormCollection collection)
        {

            var email = Session["Email"];
            cours c = new cours();

            c.course_name = collection["course_name"];
            c.admin = collection["admin"];
            c.branch = collection["branch"];
            c.commencement_date = DateTime.Parse(collection["commencement_date"]);
            c.due_date = DateTime.Parse(collection["due_date"]);
            c.duration = Int16.Parse(collection["duration"]);
            c.location = collection["location"];
            DateTime currentDateTime = DateTime.Now;
            c.time = currentDateTime;
            c.status = "Pending with master";
            c.createdBy = Session["Name"].ToString();
            c.courseno = collection["course_no"];

            var courseID = -1;

            try
            {
                db.courses.Add(c);
                db.SaveChanges();
                courseID = c.course_id;
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

            CourseCondition d = new CourseCondition();
            d.course_id = courseID;

            CourseImplication e = new CourseImplication();
            e.course_id = courseID;

            //key-value pair of all inputs
            var data = collection.AllKeys.ToArray();
            var n = data.Length;

            var i = 8;  //first 11 are course details
            
            
            while(i<n)
            {
                d.course_id = courseID;
                string temp = data[i];
                //if last letter is C then it is default condition field
                if (temp[temp.Length - 1] == 'C')
                {
                    
                        if(temp.Equals("fromC") || temp.Equals("toC"))
                    {
                        i++;
                        continue;
                    }
                        var lst = db.conditions.Where(s => s.condition_name.Equals(temp.Substring(0, temp.Length - 1))).ToList();
                        
                        
                        d.condition_id = lst.FirstOrDefault().condition_id;
                        d.condition_value = collection[temp];
                        try
                        {
                            db.CourseConditions.Add(d);
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
                        i++;
                    
                }

                //I = default Implications
                else if (temp[temp.Length - 1] == 'I')
                {
                    var lst = db.implications.Where(s => s.implication_name.Equals(temp.Substring(0, temp.Length - 1))).ToList();
                    
                    e.implication_id = lst.FirstOrDefault().id;
                    e.implication_value = collection[temp];
                    try
                    {
                        db.CourseImplications.Add(e);
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
                    i++;
                }
                // new implications or conditions
                else 
                {
                    condition cond = new condition();
                    cond.condition_type = collection[temp];
                    
                    
                    if(collection[temp] == "text" || collection[temp] == "number")
                    {
                        cond.condition_name = data[i + 1].Substring(0, data[i + 1].Length - 1);
                        db.conditions.Add(cond);
                        db.SaveChanges();
                        var nm = data[i + 1].Substring(0, data[i + 1].Length - 1);
                        var lst = db.conditions.Where(s => s.condition_name.Equals(nm)).ToList();
                        d.condition_id = lst.FirstOrDefault().condition_id;
                        d.condition_value = collection[data[i + 1]];
                        
                        try
                        {
                            db.CourseConditions.Add(d);
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
                        i += 2;
                    }
                    
                    else
                    {
                        cond.condition_name = data[i + 2].Substring(0, data[i + 2].Length - 1);
                        db.conditions.Add(cond);
                        db.SaveChanges();
                        string options = collection[data[i + 1]];
                        string[] optionsArray = collection[data[i + 1]].Split(',');

                        var nm = data[i + 2].Substring(0, data[i + 2].Length - 1);
                        var lst = db.conditions.Where(s => s.condition_name.Equals(nm)).ToList();
                        d.condition_id = lst.FirstOrDefault().condition_id;
                        d.condition_value = collection[data[i + 2]];
                        foreach (string s in optionsArray)
                        {
                            
                            valueCondition vc = new valueCondition();
                            vc.condition_id = lst.FirstOrDefault().condition_id;
                            vc.value = s.ToString();
                            db.valueConditions.Add(vc);
                        }
                        try
                        {
                            db.CourseConditions.Add(d);
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
                        i += 3;
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

            return View("CourseList",tables);
        }

        [HttpPost]
        public ActionResult Approve(FormCollection collection)
        {
            var course_id = Int16.Parse(collection["link"]);
            var user_id = Int16.Parse(collection["user"]);
            var admin = collection["admin"];
            usercours prevdata = new usercours();
            prevdata = db.usercourses.FirstOrDefault(s => s.course_id==course_id && s.user_id==user_id);
            prevdata.applyTime = DateTime.Now;

            cor ch = new cor();
            ch=db.cors.FirstOrDefault(s => s.course_id == course_id && s.user_id == user_id && s.status.Equals("Pending"));
            ch.status = "Approved";
            try
            {
                db.Entry(ch).State = System.Data.EntityState.Modified;
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

            cor ch2 = new cor();
            ch2 = db.cors.FirstOrDefault(s => s.course_id == course_id && s.user_id == user_id && s.status.Equals("Pending"));

            //if (admin == "admin6")
            //{
            //    prevdata.status = "Pending_admin5";

            //}
            //else if (admin == "admin5")
            //{
            //    prevdata.status = "Pending_admin4";
            //}
            //else if (admin == "admin4")
            //{
            //    prevdata.status = "Pending_admin3";
            //}
            //else if (admin == "admin3")
            //{
            //    prevdata.status = "Pending_admin2";
            //}
            //else if (admin == "admin2")
            //{
            //    prevdata.status = "Pending_master";
            //}
            //else if (admin == "master")
            //{
            //    prevdata.status = "Approved";
            //}
            prevdata.status = "Pending_" + ch2.admin_name;

            ViewBag.id = course_id.ToString();

            try
            {
                db.Entry(prevdata).State = System.Data.EntityState.Modified;
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
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Remarks=db.Remarks.ToList()
            };
            return RedirectToAction("Candidates",tables);
        }

        [HttpPost]
        public ActionResult Reject(FormCollection collection)
        {
            var course_id = Int16.Parse(collection["link"]);
            var user_id = collection["user"];
            var admin = collection["admin"].ToString();
            usercours prevdata = new usercours();
            prevdata = db.usercourses.FirstOrDefault(s => s.course_id==course_id && s.user_id==int.Parse(user_id));
            //if (admin == "admin6")
            //{
            //    prevdata.status = "Rejected";
            //}
            //else if (admin == "admin5")
            //{
            //    prevdata.status = "Pending_admin6";
            //}
            //else if (admin == "admin4")
            //{
            //    prevdata.status = "Pending_admin5";
            //}
            //else if (admin == "admin3")
            //{
            //    prevdata.status = "Pending_admin4";
            //}
            //else if (admin == "admin2")
            //{
            //    prevdata.status = "Pending_admin3";
            //}
            //else if (admin == "master")
            //{
            //    prevdata.status = "Pending_admin2";
            //}
            cor ch = new cor();
            ch = db.cors.FirstOrDefault(s => s.course_id == course_id && s.user_id.Equals(user_id) && s.status.Equals("Pending"));
            var cur_id = ch.id-1;

            cor ch2 = new cor();
            ch2 = db.cors.FirstOrDefault(s => s.course_id == course_id && s.user_id.Equals(user_id)   && s.id==cur_id);
            ch2.status = "Pending";
            prevdata.status = "Pending_" + ch2.admin_name;
            try
            {
                db.Entry(ch2).State = System.Data.EntityState.Modified;
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

            ViewBag.id = course_id.ToString();

            try
            {
                db.Entry(prevdata).State = System.Data.EntityState.Modified;
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
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList(),
                Remarks=db.Remarks.ToList()
            };
            return RedirectToAction("Candidates", tables);
        }

        public ActionResult ConditionsImplications()
        {
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
            return View(tables);
        }

        [HttpPost]
        public ActionResult Existing(FormCollection collection)
        {
            var selected_course = collection["courseSelection"];

            var course_details = db.courses.Where(s => s.course_name.Equals(selected_course)).ToArray();

            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList(),
                Users = db.users.ToList()
            };
            ViewBag.message = course_details;
            return View("Addcourses",tables);
        }

        [HttpPost]
        public ActionResult GetDetails(FormCollection collection)
        {
            var course_id = collection["id"];
            Session["selected_course"] = course_id;
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
            return View("ConditionsImplications", tables);
        }

        [HttpPost]
        public ActionResult Preview(FormCollection collection)
        {
            var course_id = collection["link4"];

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
            return View("coursePreview", tables);
        }

        [HttpPost]
        public ActionResult Approve2(FormCollection collection) { 
            var course_id = Int16.Parse(collection["link2"]);
            
            cours prevdata=new cours();
            prevdata=db.courses.FirstOrDefault(s=>s.course_id==course_id);
            prevdata.status = "Approved";
            prevdata.time = DateTime.Now;
            try
            {
                db.Entry(prevdata).State = System.Data.EntityState.Modified;
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
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList()
            };
            return RedirectToAction("CourseList",tables);
        }

        [HttpPost]
        public ActionResult Reject2(FormCollection collection)
        {
            
            var course_id = Int16.Parse(collection["link3"]);
            cours prevdata = new cours();
            prevdata = db.courses.FirstOrDefault(s => s.course_id == course_id);
            prevdata.status = "Rejected";
            try
            {
                db.Entry(prevdata).State = System.Data.EntityState.Modified;
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
            var tables = new AdminViewModel
            {
                Course = db.courses.ToList(),
                CourseCondition = db.CourseConditions.ToList(),
                UserCourses = db.usercourses.ToList(),
                CourseImplication = db.CourseImplications.ToList(),
                Condition = db.conditions.ToList()
            };
            return RedirectToAction("CourseList", tables);
        }


        [HttpPost]
        public ActionResult Message(FormCollection collection)
        {
            var from_admin = collection["hidden_admin"];
            var to_admin = collection["to"];
            
            var user_id= collection["hidden_user"];
            var course_id = collection["hidden_course"];
            var comment = collection["remark"];
            Remark rm= new Remark();
            rm.to_admin = to_admin;
            rm.from_admin = from_admin;
            rm.course_id = Int16.Parse(course_id);
            rm.user_id = Int16.Parse(user_id);
            rm.remark1 = comment;
            db.Remarks.Add(rm);
            db.SaveChanges();
            return RedirectToAction("Candidates");
        }

       
    }
}