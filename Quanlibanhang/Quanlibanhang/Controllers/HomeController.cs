using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Quanlibanhang.Models;
namespace Quanlibanhang.Controllers
{
    public class HomeController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        public ActionResult Index(int malsp=0,string txtSearch="")
        {
            System.Diagnostics.Debug.WriteLine("txtSearch"+txtSearch);
            if(txtSearch !="")
            {
                var sanphams = db.SanPhams.Include(s=>s.LoaiSP).Where(x=> x.TenSP.ToUpper().Contains(txtSearch.ToUpper()));
                return View(sanphams.ToList());
            }
            else
            {
                if (malsp == 0)
                {
                    var sanphams = db.SanPhams.Include(s => s.LoaiSP);
                    return View(sanphams.ToList());
                }
                else
                {
                    var sanphams = db.SanPhams.Include(s => s.LoaiSP).Where(x => x.MaLoaiSP == malsp);
                    return View(sanphams.ToList());
                }
            }
          
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}