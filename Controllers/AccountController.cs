using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString = "Data Source=NAMLE\\SQLEXPRESS;Initial Catalog=Quanlysinhvien;Integrated Security=True";

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                if (count > 0)
                {
                    // Đăng nhập thành công, chuyển hướng sang trang chủ
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Đăng nhập không thành công, hiển thị thông báo lỗi
                    ModelState.AddModelError("*", "Invalid username or password");
                   
                    return View();
                }
            }
        }
    }
}
