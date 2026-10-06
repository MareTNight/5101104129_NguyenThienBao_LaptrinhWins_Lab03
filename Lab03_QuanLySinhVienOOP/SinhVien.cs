using System;

namespace Lab03_QuanLySinhVienOOP
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; private set; }
        public string MaLop { get; set; }
        private double diemTrungBinh;
        public double DiemTrungBinh
        {
            get { return diemTrungBinh; }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value)|| value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Điểm phải là số trong khoảng từ 0 đến 10.");
                }
                diemTrungBinh = value;
            }
        }
        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh,string maLop, double diemTrungBinh): base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien.Trim();
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }
        public string XepLoai()
        {
            if (DiemTrungBinh >= 8) return "Giỏi";
            if (DiemTrungBinh >= 6.5) return "Khá";
            if (DiemTrungBinh >= 5) return "Trung bình";
            return "Không đạt";
        }
        public override string LayThongTin()
        {
            return $"{MaSinhVien} | {base.LayThongTin()} | {MaLop} | {DiemTrungBinh:0.##} | {XepLoai()}";
        }
    }
}
