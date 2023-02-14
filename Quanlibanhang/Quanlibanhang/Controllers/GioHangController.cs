using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlibanhang.Models;
using System.Net;
using System.Net.Mail;
namespace Quanlibanhang.Controllers
{
    public class GioHangController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }
        public RedirectToRouteResult AddToCart(string MaSP)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem>();
            }
            //Khai bao phuong thuc them san pham vao gio hang
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            if(giohang.FirstOrDefault(item=>item.MaSP == MaSP) == null)
            {
                SanPham sp = db.SanPhams.Find(MaSP);
                CartItem newItem = new CartItem();
                newItem.MaSP = MaSP;
                newItem.TenSP = sp.TenSP;
                newItem.SoLuong = 1;
                newItem.DonGia = Convert.ToDouble(sp.Dongia);
                giohang.Add(newItem);
            }
            else //San pham da co trong gio hang
            {
                CartItem cartitem = giohang.FirstOrDefault(m => m.MaSP == MaSP);
               // System.Diagnostics.Debug.WriteLine("So luong truoc do la: "+cartitem.SoLuong);
                cartitem.SoLuong++;
                
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(string MaSP)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem cartItem = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (cartItem != null)
            {
                giohang.Remove(cartItem);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Update(string MaSP,int txtSoLuong)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem cartItem = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if(cartItem != null)
            {
                cartItem.SoLuong = txtSoLuong;
                Session["giohang"] = giohang;
            }
           
            return RedirectToAction("Index");
        }
        public ActionResult Order(string Email,string Phone)
        {
            //Gui Email cho khach hang
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            string mss = "<html><body><table border='1><caption>Thong tin dat hang</caption><tr><th>STT</th><th>Ten Hang</th><th>So luong</th><th>Don gia</th><th>Thanh Tien</th></tr>";
            int i = 0;
            double tongtien = 0;
            foreach(CartItem item in giohang)
            {
                i++;
                mss += "<tr>";
                mss += "<td>" + i.ToString() + "</td>";
                mss += "<td>" + item.TenSP + "</td>";
                mss += "<td>" + item.SoLuong.ToString() + "</td>";
                mss += "<td>" + item.DonGia.ToString() + "</td>";
                mss += "<td>" + String.Format("{0:#,###}",item.SoLuong * item.DonGia) + "</td>";
                mss += "</tr>";
                tongtien += item.SoLuong * item.DonGia;
            }
            mss += "<tr><th> colspan='5'>Tong cong: " + String.Format("{0:#,### vnd}", tongtien) + "</th></tr></table>";
            MailMessage mail = new MailMessage("@gmail.com", Email, "Thong tin don hang", mss);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl= true;
            client.Credentials = new NetworkCredential("", "");
            mail.IsBodyHtml = true;
            client.Send(mail);
            return RedirectToAction("Index", "Home");
        }
    }
}