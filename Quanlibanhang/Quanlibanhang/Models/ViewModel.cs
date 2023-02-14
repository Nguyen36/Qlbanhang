﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quanlibanhang.Models
{
    public class ViewModel
    {
        public KhachHang khachhang { get; set; }
        public CTHD cthd { get; set; }
        public HoaDon hoadon { get; set; }
        public LoaiSP loaisp { get; set; }
        public Nhanvien nhanvien { get; set; }
        public SanPham sanpham { get; set; }
        [DisplayFormat (DataFormatString = "{0:0,##0}")]
        public double Thanhtien { get; set; }

    }
}