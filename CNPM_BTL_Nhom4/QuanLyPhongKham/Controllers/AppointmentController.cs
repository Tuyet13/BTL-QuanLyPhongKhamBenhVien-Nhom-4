using Microsoft.AspNetCore.Mvc;
using System.Data;
using FamilyDoctorMVC.Data;
using FamilyDoctorMVC.Models;

namespace FamilyDoctorMVC.Controllers
{
    public class AppointmentController : Controller
    {
        // ================= DANH SÁCH LỊCH HẸN (READ ALL) =================
        // Truy cập qua URL: /Appointment/Index
        public IActionResult Index()
        {
            // 1. Gọi hàm thực thi truy vấn SQL để lấy toàn bộ dữ liệu từ bảng Appointment
            DataTable dt = DbHelper.ExecuteQuery("SELECT * FROM Appointment");

            // 2. Khởi tạo một danh sách rỗng để chứa các đối tượng Appointment
            List<Appointment> list = new List<Appointment>();

            // 3. Duyệt từng dòng (Row) trong DataTable lấy được từ Database
            foreach (DataRow row in dt.Rows)
            {
                // 4. Chuyển đổi dữ liệu từ dòng đó thành đối tượng Model Appointment và thêm vào list
                list.Add(new Appointment
                {
                    AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                    PatientId = Convert.ToInt32(row["PatientId"]),
                    DoctorUsr = row["DoctorUsr"].ToString(),
                    AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                    Status = row["Status"].ToString()
                });
            }

            // 5. Trả về View cùng với danh sách dữ liệu để hiển thị lên giao diện
            return View(list);
        }

        // ================= CHI TIẾT LỊCH HẸN (READ ONE) =================
        // Truy cập qua URL: /Appointment/Details/{id}
        public IActionResult Details(int id)
        {
            // Truy vấn lấy 1 bản ghi duy nhất dựa trên ID truyền vào
            string query = $"SELECT * FROM Appointment WHERE AppointmentId = {id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            // Kiểm tra nếu không tìm thấy dòng nào trong Database
            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            // Lấy dòng đầu tiên tìm được
            var row = dt.Rows[0];

            // Map dữ liệu từ DataRow sang Object Model
            Appointment a = new Appointment
            {
                AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                PatientId = Convert.ToInt32(row["PatientId"]),
                DoctorUsr = row["DoctorUsr"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                Status = row["Status"].ToString()
            };

            return View(a);
        }

        // ================= TẠO MỚI (CREATE) =================
        // GET: Hiển thị Form trống để người dùng nhập liệu
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tiếp nhận dữ liệu từ Form gửi lên để lưu vào DB
        [HttpPost]
        public IActionResult Create(Appointment a)
        {
            // Định dạng ngày tháng về chuẩn SQL (Năm-Tháng-Ngày Giờ:Phút:Giây)
            string date = a.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Câu lệnh SQL Insert dữ liệu (Mặc định trạng thái là 'Pending' - Đang chờ)
            string query = $@"
            INSERT INTO Appointment
            (PatientId, DoctorUsr, AppointmentDate, Status)
            VALUES
            ({a.PatientId}, '{a.DoctorUsr}', '{date}', 'Pending')";

            // Thực thi câu lệnh không trả về bảng (Insert/Update/Delete)
            DbHelper.ExecuteNonQuery(query);

            // Sau khi lưu xong, chuyển hướng người dùng về trang danh sách (Index)
            return RedirectToAction("Index");
        }

        // ================= CHỈNH SỬA (UPDATE) =================
        // GET: Lấy dữ liệu cũ và hiển thị lên Form để sửa
        public IActionResult Edit(int id)
        {
            string query = $"SELECT * FROM Appointment WHERE AppointmentId = {id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            var row = dt.Rows[0];

            return View(new Appointment
            {
                AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                PatientId = Convert.ToInt32(row["PatientId"]),
                DoctorUsr = row["DoctorUsr"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                Status = row["Status"].ToString()
            });
        }

        // POST: Cập nhật dữ liệu mới vào Database
        [HttpPost]
        public IActionResult Edit(Appointment a)
        {
            string date = a.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss");

            // Câu lệnh SQL Update dựa trên khóa chính AppointmentId
            string query = $@"
            UPDATE Appointment
            SET PatientId = {a.PatientId},
                DoctorUsr = '{a.DoctorUsr}',
                AppointmentDate = '{date}',
                Status = '{a.Status}'
            WHERE AppointmentId = {a.AppointmentId}";

            DbHelper.ExecuteNonQuery(query);

            return RedirectToAction("Index");
        }

        // ================= XÓA (DELETE) =================
        // GET: Hiển thị trang xác nhận xóa (Hỏi người dùng "Bạn có chắc chắn muốn xóa?")
        public IActionResult Delete(int id)
        {
            string query = $"SELECT * FROM Appointment WHERE AppointmentId = {id}";
            DataTable dt = DbHelper.ExecuteQuery(query);

            if (dt.Rows.Count == 0)
                return Content("KHÔNG CÓ DỮ LIỆU");

            var row = dt.Rows[0];

            return View(new Appointment
            {
                AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                PatientId = Convert.ToInt32(row["PatientId"]),
                DoctorUsr = row["DoctorUsr"].ToString(),
                AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                Status = row["Status"].ToString()
            });
        }

        // POST: Thực hiện hành động xóa thật sự sau khi người dùng xác nhận
        [HttpPost]
        [ActionName("Delete")] // Định danh đây là Action xử lý việc xóa
        public IActionResult DeleteConfirmed(int AppointmentId)
        {
            // Câu lệnh SQL xóa bản ghi
            string query = $"DELETE FROM Appointment WHERE AppointmentId = {AppointmentId}";
            DbHelper.ExecuteNonQuery(query);

            // Quay lại trang danh sách
            return RedirectToAction("Index");
        }
    }
}
