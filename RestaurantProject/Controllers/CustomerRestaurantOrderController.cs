using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestaurantBAL;
using RestaurantDAL;
using RestaurantProject.Models;

namespace RestaurantProject.Controllers
{
    
    public class CustomerRestaurantOrderController : Controller
    {
        DatabaseBL restaurantBAL = new DatabaseBL();

        // GET: Restaurant

        [HandleError]
        [NoDirectAccess]
        public ActionResult TakeOrderForBookingId(int BID, int resId)
        {
            try { 
                OrderListWithBIDAndFoodMenu orderModel = new OrderListWithBIDAndFoodMenu();
                orderModel.resId = resId;
                orderModel.bid = BID;
                orderModel.FoodStarters = restaurantBAL.GetStarterFoodItemsOfARestaurant(resId);
                orderModel.FoodMainCourse = restaurantBAL.GetMainCourseFoodItemsOfARestaurant(resId);
                orderModel.FoodDesserts = restaurantBAL.GetDessertFoodItemsOfARestaurant(resId);
                orderModel.Order = restaurantBAL.GetOrdersOfABooking(BID);
                return View(orderModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HandleError]
        [NoDirectAccess]
        public ActionResult AddToCart(int foodId, int BID, int resId)
        {
            try { 
                Order order = new Order() { Food_Id = foodId, BID = BID, Quantity = 1 };

                List<Order> orderList = restaurantBAL.GetOrdersOfABooking(BID);
                var findItem = (from r in orderList
                                where r.Food_Id == foodId
                                select r).ToList();

                int flag;

                if (findItem.Count > 0)
                {
                    Order orderForEditOrder = restaurantBAL.FindOrders(findItem.First().Order_Id);
                    if (orderForEditOrder.Quantity < 9)
                    {
                        orderForEditOrder.Quantity += 1;
                    }
                    flag = restaurantBAL.EditOrders(orderForEditOrder);
                }
                else
                {
                    flag = restaurantBAL.CreateOrdersEntry(order);
                }

                if (flag == 1)
                {
                    return RedirectToAction("TakeOrderForBookingId", new { BID = BID, resId = resId });
                }
                else
                {
                    throw new System.Exception();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HandleError]
        [NoDirectAccess]
        public ActionResult IncreaseQuantity(int orderId,int BID,int resId)
        {
            try { 
                Order order = restaurantBAL.FindOrders(orderId);
                if (order.Quantity < 9)
                {
                    order.Quantity += 1;
                }
                int flag = restaurantBAL.EditOrders(order);
                if (flag == 1)
                {
                    return RedirectToAction("TakeOrderForBookingId", new { BID = BID, resId = resId });
                }
                else
                {
                    throw new System.Exception();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HandleError]
        [NoDirectAccess]
        public ActionResult DecreaseQuantity(int orderId, int BID, int resId)
        {
            try { 
                Order order = restaurantBAL.FindOrders(orderId);
                if (order.Quantity > 1)
                {
                    order.Quantity -= 1;
                }
                int flag = restaurantBAL.EditOrders(order);
                if (flag == 1)
                {
                    return RedirectToAction("TakeOrderForBookingId", new { BID = BID, resId = resId });
                }
                else
                {
                    throw new System.Exception();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }
        [HandleError]
        [NoDirectAccess]
        public ActionResult DeleteFromCart(int orderId, int BID, int resId)
        {
            try { 
                int flag = restaurantBAL.DeleteOrder(orderId);
                if (flag == 1)
                    return RedirectToAction("TakeOrderForBookingId", new { BID = BID, resId = resId });
                else
                    throw new System.Exception();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HandleError]
        [NoDirectAccess]
        public ActionResult GenerateOrder(int BID, int resId)
        {
            try { 
                //Confirm the order by Yes or No
                //Show Menu
                if (restaurantBAL.IsOrderEmpty(BID))
                {
                    return RedirectToAction("PaymentWindow", new { BID = BID, resId = resId });
                }
                else
                {
                    return RedirectToAction("EmptyBAG");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HandleError]
        [NoDirectAccess]
        public ActionResult PaymentWindow(int BID,int resId)
        {
            try { 
                List<Order> orders = restaurantBAL.GetOrdersOfABooking(BID);
                ViewBag.OrderList = orders;
                ViewBag.resId = resId;
                ViewBag.BID = BID;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HandleError]
        [NoDirectAccess]
        public ActionResult EmptyBag()
        {
            try { 
                ViewBag.EmptyBag = "Please Add Item In The Cart";
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

        [HandleError]
        [NoDirectAccess]
        public ActionResult WalletHasInSufficientBalance()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", ex);
            }
        }

    }
}