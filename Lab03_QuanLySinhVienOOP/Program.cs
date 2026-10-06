using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        private static readonly QuanLySinhVien quanLy = new QuanLySinhVien();
        private static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                HienMenu();
                int luaChon = NhapLuaChon();

                switch (luaChon)
                {
                    case 1: ThemSinhVien(); break;
                    case 2: InDanhSach(quanLy.LayDanhSach()); break;
                    case 3: TimSinhVienTheoMa(); break;
                    case 4: TimSinhVienTheoTen(); break;
                    case 5: SuaDiem(); break;
                    case 6: XoaSinhVien(); break;
                    case 7: InDanhSach(quanLy.SapXepTheoDiem()); break;
                    case 8: InDanhSach(quanLy.LocSinhVienDat()); break;
                    case 0:
                        Console.WriteLine("Đã thoát chương trình.");
                        return;
                }
                Console.WriteLine("\nNhấn Enter để trở về menu...");
                Console.ReadLine();
            }
        }
        private static void HienMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== QUẢN LÝ SINH VIÊN =====");
            Console.ResetColor();
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Xuất danh sách");
            Console.WriteLine("3. Tìm sinh viên theo mã");
            Console.WriteLine("4. Tìm sinh viên theo tên");
            Console.WriteLine("5. Sửa điểm trung bình");
            Console.WriteLine("6. Xóa sinh viên");
            Console.WriteLine("7. Sắp xếp theo điểm giảm dần");
            Console.WriteLine("8. Lọc sinh viên đạt");
            Console.WriteLine("0. Thoát");
        }
        private static int NhapLuaChon()
        {
            while (true)
            {
                Console.Write("Chọn chức năng: ");
                int luaChon;
                if (int.TryParse(Console.ReadLine(), out luaChon) && luaChon >= 0 && luaChon <= 8)
                {
                    return luaChon;
                }

                Console.WriteLine("Hãy nhập số nguyên từ 0 đến 8.");
            }
        }
        private static string NhapChuoi(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string giaTri = (Console.ReadLine() ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(giaTri)) return giaTri;
                Console.WriteLine("Thông tin này không được để trống.");
            }
        }
        private static DateTime NhapNgaySinh()
        {
            while (true)
            {
                string chuoi = NhapChuoi("Ngày sinh (dd/MM/yyyy): ");
                DateTime ngaySinh;
                if (DateTime.TryParseExact(chuoi, "dd/MM/yyyy",CultureInfo.InvariantCulture, DateTimeStyles.None,out ngaySinh) && ngaySinh <= DateTime.Today)
                {
                    return ngaySinh;
                }
                Console.WriteLine("Ngày sinh phải hợp lệ, đúng dd/MM/yyyy" + " và không nằm trong tương lai.");
            }
        }
        private static double NhapDiem()
        {
            while (true)
            {
                string chuoi = NhapChuoi("Điểm trung bình (0-10): ");
                // Chấp nhận cả . ,
                chuoi = chuoi.Replace(',', '.');
                double diem;
                if (double.TryParse(chuoi, NumberStyles.Float,CultureInfo.InvariantCulture, out diem) && !double.IsNaN(diem) && !double.IsInfinity(diem) && diem >= 0 && diem <= 10)
                {
                    return diem;
                }
                Console.WriteLine("Điểm không hợp lệ. Hãy nhập lại số từ 0 đến 10.");
            }
        }
        private static void ThemSinhVien()
        {
            string ma = NhapChuoi("Mã sinh viên: ");
            if (quanLy.TimTheoMa(ma) != null)
            {
                Console.WriteLine("Mã sinh viên đã tồn tại.");
                return;
            }
            string hoTen = NhapChuoi("Họ tên: ");
            DateTime ngaySinh = NhapNgaySinh();
            string maLop = NhapChuoi("Mã lớp: ");
            double diem = NhapDiem();
            SinhVien sinhVien = new SinhVien(ma, hoTen, ngaySinh, maLop, diem);
            bool thanhCong = quanLy.Them(sinhVien);
            Console.WriteLine(thanhCong ? "Thêm sinh viên thành công." : "Mã sinh viên đã tồn tại.");
        }
        private static void InDanhSach(IEnumerable<SinhVien> danhSach)
        {
            Console.WriteLine("\nSTT | Mã SV | Họ tên | Ngày sinh | Lớp | Điểm | Xếp loại");
            Console.WriteLine(new string('-', 75));
            int stt = 1;
            foreach (SinhVien sinhVien in danhSach)
            {
                // Gọi phiên bản override của LayThongTin trong SinhVien
                Console.WriteLine($"{stt} | {sinhVien.LayThongTin()}");
                stt++;
            }
            if (stt == 1) Console.WriteLine("Không có sinh viên để hiển thị.");
        }
        private static void TimSinhVienTheoMa()
        {
            string ma = NhapChuoi("Nhập mã cần tìm: ");
            SinhVien sinhVien = quanLy.TimTheoMa(ma);
            if (sinhVien == null)
                Console.WriteLine("Không tìm thấy sinh viên.");
            else
                Console.WriteLine(sinhVien.LayThongTin());
        }
        private static void TimSinhVienTheoTen()
        {
            string tuKhoa = NhapChuoi("Nhập từ khóa họ tên: ");
            InDanhSach(quanLy.TimTheoTen(tuKhoa));
        }
        private static void SuaDiem()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần sửa điểm: ");
            if (quanLy.TimTheoMa(ma) == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }
            double diemMoi = NhapDiem();
            quanLy.Sua(ma, diemMoi);
            Console.WriteLine("Đã cập nhật điểm trung bình.");
        }
        private static void XoaSinhVien()
        {
            string ma = NhapChuoi("Nhập mã sinh viên cần xóa: ");
            bool thanhCong = quanLy.Xoa(ma);
            Console.WriteLine(thanhCong ? "Đã xóa sinh viên." : "Không tìm thấy sinh viên.");
        }
    }
}
