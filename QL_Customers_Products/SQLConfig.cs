using System;
using System.Data;
using System.Data.SqlClient;

public class SQLConfig
{
    SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-UE7V70U\\SQLEXPRESS;Initial Catalog=QLKH_SP; User ID=sa;Password=123");
    //Truy vấn
    public bool ExecuteNonQuery(string query)
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Lỗi SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        return false;
    }
    // Lấy bảng dữ liệu
    public DataTable ExecuteTableQuery(string query)
    {
        DataTable dataTable = new DataTable();

        try
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(dataTable);
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Lỗi SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        return dataTable;
    }
    //Lấy 1 kiểu dữ liệu
    public object ExecuteScalar(string query)
    {
        object result = null;

        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            result = command.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Lỗi SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        return result;
    }
}
