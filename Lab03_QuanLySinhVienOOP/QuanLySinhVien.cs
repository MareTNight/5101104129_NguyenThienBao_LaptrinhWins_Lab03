using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSach = new List<SinhVien>();
        public bool Them(SinhVien sinhVien)
        {
            if (TimTheoMa(sinhVien.MaSinhVien) != null)
            {
                return false;
            }
            danhSach.Add(sinhVien);
            return true;
        }
        public bool Sua(string maSinhVien, double diemMoi)
        {
            SinhVien sinhVien = TimTheoMa(maSinhVien);
            if (sinhVien == null) return false;
            sinhVien.DiemTrungBinh = diemMoi;
            return true;
        }
        public bool Xoa(string maSinhVien)
        {
            SinhVien sinhVien = TimTheoMa(maSinhVien);
            if (sinhVien == null) return false;

            return danhSach.Remove(sinhVien);
        }
        public SinhVien TimTheoMa(string maSinhVien)
        {
            string ma = (maSinhVien ?? "").Trim();
            return danhSach.FirstOrDefault(sv =>string.Equals(sv.MaSinhVien, ma, StringComparison.OrdinalIgnoreCase));
        }
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            string ten = (tuKhoa ?? "").Trim();

            return danhSach.Where(sv =>sv.HoTen.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
        public List<SinhVien> SapXepTheoDiem()
        {
            danhSach = danhSach.OrderByDescending(sv => sv.DiemTrungBinh).ToList();
            return LayDanhSach();
        }
        public List<SinhVien> LocSinhVienDat()
        {
            return danhSach.Where(sv => sv.DiemTrungBinh >= 5).ToList();
        }
        public List<SinhVien> LayDanhSach()
        {
            // Trả về bản sao danh sách để bên ngoài không thêm/xóa trực tiếp. Các đối tượng SinhVIen vẫn là cùng các đối tượng
            return danhSach.ToList();
        }
    }
}
