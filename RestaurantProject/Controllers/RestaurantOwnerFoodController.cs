using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantBAL;
using RestaurantDAL;

namespace RestaurantProject.Controllers
{
    [NoDirectAccess]
    public class RestaurantOwnerFoodController : Controller
    {

        DatabaseBL restaurantBAL = new DatabaseBL();

        public ActionResult GetFoodItems(int resId)
        {
            try { 
                List<Food> foods = restaurantBAL.GetFoodItemsOfARestaurant(resId);
                return View(foods);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult Edit(int id)
        {
            try { 
                Food food = restaurantBAL.FindFoodItems(id);
                return View(food);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditFoodItem(Food food)
        {
            try { 
                restaurantBAL.EditFoodItems(food);
                return RedirectToAction("GetFoodItems");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        public ActionResult Delete(int id)
        {
            try { 
                Food obj = restaurantBAL.FindFoodItems(id);
                return View(obj);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteFeedback(int id)
        {
            try { 
                restaurantBAL.DeleteFoodItem(id);
                return RedirectToAction("GetFeedbacks");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

    }
}