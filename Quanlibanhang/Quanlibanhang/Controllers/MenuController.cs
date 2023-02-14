using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlibanhang.Models;
namespace Quanlibanhang.Controllers
{
    public class MenuController : Controller
    {
        //private qlbanhangEntities db = new qlbanhangEntities();
        // GET: Menu
        public ActionResult Index()
        {
            using(qlbanhangEntities db = new qlbanhangEntities())
            {
                var loaisp = db.LoaiSPs.ToList();
                Hashtable tenloaisp = new Hashtable();
                foreach (var item in loaisp)
                {
                    tenloaisp.Add(item.MaLoaiSP, item.TenLoaiSP);
                }
                ViewBag.TenLoaiSp = tenloaisp;
                return PartialView("Index");
            }
            
        }
    }
}