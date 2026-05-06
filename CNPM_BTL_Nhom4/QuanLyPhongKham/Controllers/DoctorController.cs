using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;

namespace FamilyDoctorMVC.Controllers
{
    public class DoctorController : Controller
    {
        // ==========================================
        // 1. DANH SÁCH BÁC SĨ (LIST)
        // ==========================================
        public IActionResult Index()
        {
            // Truy vấn lấy toàn bộ dữ liệu từ bảng Doctor
            DataTable dt = DbHelper.ExecuteQuery("SELECT * FROM Doctor");
            
            // Trả về View và truyền trực tiếp đối tượng DataTable
            // Lưu ý: Ở View cần khai báo @model System.Data.DataTable
            return View(dt);
        }

        // ==========================================
        // 2. FORM TẠO MỚI (CREATE FORM)
        // ==========================================
        public IActionResult Create()
        {
            // Chỉ trả về giao diện trống để người dùng nhập thông tin bác sĩ mới
            return View();
        }

        // ==========================================
        // 3. LƯU DỮ LIỆU TẠO MỚI (SAVE CREATE)
        // ==========================================
        [HttpPost] // Chỉ nhận phương thức POST từ Form gửi lên
        public IActionResult Create(string DoctorUsr, string DoctorPwd, string DoctorName, string Phone, string Address)
        {
            // Sử dụng ký tự N trước chuỗi (N'{DoctorName}') để hỗ trợ lưu tiếng Việt có dấu (Unicode)
            string query = $@"
            INSERT INTO Doctor
            (DoctorUsr, DoctorPwd, DoctorName, Phone, Address)
            VALUES
            ('{DoctorUsr}', '{DoctorPwd}', N'{DoctorName}', '{Phone}', N'{Address}')";

            // Thực thi lệnh Insert vào cơ sở dữ liệu
            DbHelper.ExecuteNonQuery(query);

            // Sau khi tạo xong, chuyển hướng về trang chủ của Home thay vì Index của Doctor
            return RedirectToAction("Index", "Home");
        }

        // ==========================================
        // 4. XÓA BÁC SĨ (DELETE)
        // ==========================================
        public IActionResult Delete(string id)
        {
            // Thực hiện lệnh xóa trực tiếp dựa trên khóa chính là DoctorUsr (Tài khoản bác sĩ)
            string query = $"DELETE FROM Doctor WHERE DoctorUsr='{id}'";
            DbHelper.ExecuteNonQuery(query);

            // Xóa xong quay về trang chủ
            return RedirectToAction("Index", "Home");
        }

        // ==========================================
        // 5. CHI TIẾT BÁC SĨ (DETAILS)
        // ==========================================
        public IActionResult Details(string id)
        {
            // Lấy thông tin chi tiết của một bác sĩ theo mã định danh (id)
            DataTable dt = DbHelper.ExecuteQuery(
                $"SELECT * FROM Doctor WHERE DoctorUsr='{id}'"
            );

            // Truyền DataTable chứa 1 bản ghi sang View để hiển thị
            return View(dt);
        }

        // ==========================================
        // 6. FORM CHỈNH SỬA (EDIT FORM)
        // ==========================================
        public IActionResult Edit(string id)
        {
            // Lấy dữ liệu hiện tại của bác sĩ để điền vào các ô nhập liệu (Input) trong Form
            DataTable dt = DbHelper.ExecuteQuery(
                $"SELECT * FROM Doctor WHERE DoctorUsr='{id}'"
            );

            return View(dt);
        }

        // ==========================================
        // 7. CẬP NHẬT DỮ LIỆU (UPDATE)
        // ==========================================
        [HttpPost]
        public IActionResult Edit(string DoctorUsr, string DoctorPwd, string DoctorName, string Phone, string Address)
        {
            // Câu lệnh cập nhật thông tin bác sĩ dựa trên DoctorUsr
            // N'{...}' giúp giữ định dạng tiếng Việt cho Tên và Địa chỉ
            string query = $@"
            UPDATE Doctor
            SET DoctorPwd='{DoctorPwd}',
                DoctorName=N'{DoctorName}',
                Phone='{Phone}',
                Address=N'{Address}'
            WHERE DoctorUsr='{DoctorUsr}'";

            DbHelper.ExecuteNonQuery(query);

            // Cập nhật xong chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }
    }
}
