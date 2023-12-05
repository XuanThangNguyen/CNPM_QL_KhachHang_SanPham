using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Customers_Products
{
    class SharedMethods
    {
        public static SQLConfig config = new SQLConfig();

        public static string TaoIdHoaDonDuyNhat()
        {
            string newId = TaoIdHoaDonNgauNhien();
            while (KiemTraIdTonTaiTrongCSDL(newId,"HoaDon"))
            {
                newId = TaoIdHoaDonNgauNhien();
            }
            return newId;
        }

        public static string TaoIdHoaDonNgauNhien()
        {
            DateTime now = DateTime.Now;
            string day = now.Day.ToString("00");
            string month = now.Month.ToString("00");
            string year = now.Year.ToString();

            // Tạo một số ngẫu nhiên (có thể bạn muốn thay đổi phần này để tạo số ngẫu nhiên theo quy tắc cụ thể)
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);

            // Tạo chuỗi ID
            StringBuilder idBuilder = new StringBuilder();
            idBuilder.Append("HD");
            idBuilder.Append(day);
            idBuilder.Append(month);
            idBuilder.Append(year.Substring(2)); 
            idBuilder.Append(randomNumber);

            return idBuilder.ToString();
        }

        private static bool KiemTraIdTonTaiTrongCSDL(string id,string tenBang)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM " + tenBang + " WHERE IdHoaDon = '" + id + "'";
                Object result = config.ExecuteScalar(query);
                return int.Parse(result.ToString()) > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
            return false;
        }

    }
    
}
