using System;

namespace Lab03_QuanLySinhVienOOP
{
    public class Nguoi
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }
        // virtual: cho phép lớp con viết lại phương thức này.
        public virtual string LayThongTin()
        {
            return $"{HoTen} | {NgaySinh:dd/MM/yyyy}";
        }
    }
}
