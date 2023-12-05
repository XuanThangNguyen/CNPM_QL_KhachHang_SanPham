using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QL_Customers_Products
{
    class DBConnect
    {
        SqlConnection connect;

        public SqlConnection Connect
        {
            get { return connect; }
            set { connect = value; }
        }
        string strConnect = ("Data Source = DESKTOP-UE7V70U\\SQLEXPRESS; Initial Catalog = QLKH_SP; User ID=sa;Password=123");
        public DBConnect()
        {
            connect = new SqlConnection(strConnect);
        }
        public void open()
        {
            if (connect.State == ConnectionState.Closed)
                connect.Open();
        }
        public void close()
        {
            if (connect.State == ConnectionState.Open)
                connect.Close();
        }
        public int getNonQuery(string sql)
        {
            open();
            SqlCommand cmd = new SqlCommand(sql, connect);
            int kq = cmd.ExecuteNonQuery();
            close();
            return kq;
        }
        public SqlDataReader getDataReader(string sql)
        {
            open();
            SqlCommand cmd = new SqlCommand(sql, connect);
            SqlDataReader rd = cmd.ExecuteReader();
            //close();
            return rd;
        }
        public object getScalar(string sql)
        {
            open();
            SqlCommand cmd = new SqlCommand(sql, connect);
            object kq = cmd.ExecuteScalar();
            close();
            return kq;
        }
        public DataTable getDataTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, connect);
            da.Fill(dt);
            return dt;
        }
        public void updateToDataBase(string sql)
        {
            open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connect;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            close();
        }
        public int getCount(string sql)
        {
            open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connect;
            cmd.CommandText = sql;
            int count = (int)cmd.ExecuteScalar();
            close();
            return count;
        }
    }
}
