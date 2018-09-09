using RestaurantBAL;
using RestaurantDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class RegistrationController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();
        string res;

        // GET: Registration
        //public ActionResult Index()
        //{
        //    Customer c = new Customer();
        //    //List<Customer> cus = new List<Customer>(restaurantBAL.CreateCustomerEntry(c));
        //    return View(c);
        //}

        public ActionResult Login()
        {
            if (Session["userName"] == null)
            {
                Customer customer = new Customer();
                return View(customer);

                
            }
            else if ("Admin" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if(Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer c)
        {
            try { 
            if (Session["userType"]==null)
            {
                if (c == null)
                {
                    ModelState.AddModelError("Email", "Enter your email number in proper format");
                    ModelState.AddModelError("Password", "Enter your valid number in proper format");
                    return View();
                }
                else
                {
                    if (c.Email == null || c.Password == null)
                    {
                        return View();
                    }
                    else
                    {
                        string Email = c.Email.ToString();
                        string Password = c.Password.ToString();

                        if ((IsExistEmail(Email.ToString()) == true))
                        {
                            res = Email;
                            var result = (from r in restaurantBAL.GetCustomers()
                                          where r.Email == res
                                          select r).First();
                            //if ((IsExistpass(Password.ToString()) == true))
                            if (result.Password == Password)
                            {
                                    if (result.Customer_Status.ToLower() == "inactive")
                                    {
                                        Response.Write("<div class=\"well\">The user account is blocked</div>");
                                        return View();
                                    }
                                    else
                                    {
                                        Session["userName"] = result.Name;
                                        Session["userType"] = "Customer";
                                        Session["userId"] = result.Customer_Id;
                                        Session["walletId"] = result.wallet_id;
                                        return RedirectToAction("Home", "CustomerMain");
                                    }
                            }
                            else
                            {
                                Response.Write("<div class=\"well\">Invalid password</div>");
                                return View();
                            }


                        }
                        else
                        {
                            if ((IsUserRestaurantOwner(Email.ToString()) == true))
                            {
                                var result = (from r in restaurantBAL.GetRestaurants()
                                              where r.Owner_Email == Email
                                              select r).First();
                                if (result.Password == Password)
                                {
                                        if (result.Res_Status.ToLower() == "inactive")
                                        {
                                            Response.Write("<div class=\"well\">The restaurant account is blocked or inactive</div>");
                                            return View();
                                        }
                                        else
                                        {
                                            Session["userName"] = result.Owner_Name;
                                            Session["userType"] = "Restaurant";
                                            Session["userId"] = result.Res_Id;
                                            Session["walletId"] = result.wallet_id;
                                            return RedirectToAction("Index", "RestaurantMain");
                                        }
                                }
                                else
                                {
                                    Response.Write("<div class=\"well\">Invalid password</div>");
                                    return View();
                                }
                            }
                            else
                            {
                                if (Email == "Admin@admin.com" && Password == "Password")
                                {
                                    Session["userName"] = "Admin Das Pandey";
                                    Session["userType"] = "Admin";
                                    Session["walletId"] = 100;
                                    return RedirectToAction("Index", "Admin",false);
                                }
                            }
                            Response.Write("<div class=\"well\">Email Id Password Does Not Match</div>");
                            //   return RedirectToAction("Create");
                            return View();
                        }
                    }
                }
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else if ("Admin" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult Create()
        {
            try { 
            if (Session["userName"]==null) {
                return View();
            }
            else if ("Admin" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer c)
        {
            try { 
            //if (!string.IsNullOrEmpty(c.Email))
            //{
            //    string emailreg = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
            //    Regex re = new Regex(emailreg);
            //    if (!re.IsMatch(c.Email))
            //    {
            //        ModelState.AddModelError("Email", "Enter your email number in proper format");
            //    }
            //}
            //if (!string.IsNullOrEmpty(c.MobileNo))
            //{
            //    string phpat = @"^\(?([7-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            //    Regex reg = new Regex(phpat);
            //    if (!reg.IsMatch(c.MobileNo))
            //    {
            //        ModelState.AddModelError("PhoneNumber", "Enter your  number in proper format");
            //    }
            //}


            if (Session["userName"] == null)
            {
                if (ModelState.IsValid)
                {
                    string id = c.Email;
                    if (IsExist(id) == true)
                    {
                            EmailModel model = new EmailModel();
                            model.From = "platedrestaurants@gmail.com";
                            model.To = c.Email;
                            model.Subject = "Thank you For Registering With Us";
                            model.Body = "Dear Privileged Customer,\n\n\t Thankyou for registering with us. This is your account activation mail you can login from your email and password now.";

                            new EmailBL().SendMail(model);

                            c.Customer_Status = "Active";
                            c.wallet_id = 150;
                        restaurantBAL.CreateCustomerEntry(c);
                        //Add session here
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">The mail id of user already exist</div>");
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else if ("Admin" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public bool IsExist(string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                    return false;
                var result1 = (from r in restaurantBAL.GetCustomers()
                               where r.Email.ToLower() == Email.ToString().ToLower()
                               select r).ToList();
                var result2 = (from r in restaurantBAL.GetRestaurants()
                               where r.Owner_Email.ToLower() == Email.ToString().ToLower()
                               select r).ToList();
                if (Email.ToLower() == "Admin@admin.com".ToLower()) { return false; };
                if (result1.Count != 0)
                    return false;
                if (result2.Count() != 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsExistpass(string paswd)
        {
            try
            {
                //if (string.IsNullOrEmpty(paswd))

                //    return false;

                var result = (from r in restaurantBAL.GetCustomers()
                              where r.Password == paswd.ToString()
                              select r).ToList();
                if (result.Count != 0)
                    return true;

                else
                    return false;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool IsExistEmail(string Email)
        {
            try
            {
                //if (string.IsNullOrEmpty(uname))

                //    return false;
                var result = (from r in restaurantBAL.GetCustomers()
                              where r.Email == Email.ToString()
                              select r).ToList();
                if (result.Count != 0)
                    return true;


                return false;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool IsUserRestaurantOwner(string userid)
        {
            try
            {
                var result = (from r in restaurantBAL.GetRestaurants()
                              where r.Owner_Email == userid.ToString()
                              select r).ToList();
                if (result.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public JsonResult IsUserExists(string name)
        {
            return IsExist(name) ? Json(true, JsonRequestBehavior.AllowGet) :
                Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RestCreate()
        {
            try { 
            if (Session["userName"] == null)
            {
                return View();
            }
            else if ("Admin" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        public ActionResult RestCreate(Restaurant r)
        {
            try { 
            if (Session["userName"] == null)
            {
                
                if (ModelState.IsValid)
                {
                string id = r.Owner_Email;
                    if (IsExistEmailrest(id) == true)
                    {
                            EmailModel model = new EmailModel();
                            model.From = "platedrestaurants@gmail.com";
                            model.To = r.Owner_Email;
                            model.Subject = "Thank you For Registering With Us";
                            model.Body = "Dear Privileged Restaurant,\n\n\t Thankyou for registering with us. This is to inform you that your account activation is pending approval from the admin.";

                            new EmailBL().SendMail(model);

                            r.wallet_id = 151;
                            r.Res_Status = "Inactive";
                        restaurantBAL.CreateRestaurantEntry(r);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">The email id for user already exist</div>");
                        return View();
                    }
                }
                else
                {
                return View();
                }

            
            }
            else if ("Admin" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "Admin");
            }
            else if ("Restaurant" == Session["userType"].ToString())
            {
                return RedirectToAction("Index", "RestaurantMain");
            }
            else if (Session["userType"].ToString() == "Customer")
            {
                return RedirectToAction("Home", "CustomerMain");
            }
            else
            {
                throw new Exception();
            }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public bool IsExistrest(string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                    return false;
                var result1 = (from r in restaurantBAL.GetCustomers()
                               where r.Email.ToLower() == Email.ToString().ToLower()
                               select r).ToList();
                var result2 = (from r in restaurantBAL.GetRestaurants()
                               where r.Owner_Email.ToLower() == Email.ToString().ToLower()
                               select r).ToList();
                if (Email.ToLower() == "Admin@admin.com".ToLower()) { return false; };
                if (result1.Count != 0)
                    return false;
                if (result2.Count() != 0)
                    return false;



                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool IsExistpassrest(string paswd)
        {
            try
            {
                //if (string.IsNullOrEmpty(paswd))

                //    return false;
                var result = (from r in restaurantBAL.GetRestaurants()
                              where r.Password == paswd.ToString()
                              select r).ToList();
                if (result.Count != 0)
                    return true;

                else
                    return false;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool IsExistEmailrest(string Email)
        {
            try
            {
                //if (string.IsNullOrEmpty(uname))

                //    return false;
                var result = (from r in restaurantBAL.GetRestaurants()
                              where r.Owner_Email == Email.ToString()
                              select r).ToList();
                if (result.Count != 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult ClearSession()
        {
            try {
                Session["userName"] = null;
                Session["userType"] = null;
                Session["userId"] = null;
                Session["walletId"] = null;
                Session.Abandon();
                return RedirectToAction("Login", "Registration");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }


        public ActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    List<Customer> customer = restaurantBAL.GetCustomers();
                    List<Restaurant> restaurant = restaurantBAL.GetRestaurants();


                    var custResult = (from p in customer
                                      where p.Email == Email
                                      select p.Password).ToList();

                    var restaurantResult = (from p in restaurant
                                            where p.Owner_Email == Email
                                            select p.Password).ToList();

                    custResult.AddRange(restaurantResult);

                    var emailAccountPassword = custResult.First();

                    if (emailAccountPassword != null)
                    {

                        EmailModel model = new EmailModel();
                        model.From = "platedrestaurants@gmail.com";
                        model.To = Email;
                        model.Subject = "Password Recovery";
                        model.Body = "This Email was sent automatically by Plated in response to recover your password. Your recent password is:" + emailAccountPassword;
                        new EmailBL().SendMail(model);

                        Response.Write("<div class=\"well\">Mail Has Been Sent To Your Registered Mail Id</div>");
                        RedirectToAction("Login", "Registration");
                    }
                    else
                    {
                        Response.Write("<div class=\"well\">Please Enter Correct Email Id</div>");
                        return View();
                    }
                }
                else
                {
                    Response.Write("<div class=\"well\">Please Enter Correct Email Id</div>");
                    return View();

                }


            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Error", ex);
            }


            return View();
        }




    }
}